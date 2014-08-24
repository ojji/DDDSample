using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDDSample.ApplicationServices.Interfaces;
using DDDSample.ApplicationServices.Messaging.Customer;
using DDDSample.ApplicationServices.ViewModels;
using DDDSample.Domain.Customer;
using DDDSample.Domain.ValueObjects;
using DDDSample.Infrastructure.Common.Domain;
using DDDSample.Infrastructure.Common.UnitOfWork;

namespace DDDSample.ApplicationServices.Implementations
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        private IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            if (customerRepository == null) { throw new ArgumentNullException("customerRepository"); }
            if (unitOfWork == null) { throw new ArgumentNullException("unitOfWork"); }
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public GetCustomerResponse GetCustomer(GetCustomerRequest getCustomerRequest)
        {
            GetCustomerResponse response = new GetCustomerResponse();
            Customer customer = null;
            try
            {
                customer = _customerRepository.FindBy(getCustomerRequest.Id);
                if (customer == null)
                {
                    response.Exception = GetStandardCustomerNotFoundException();
                }
                else
                {
                    response.Customer = customer.ConvertToViewModel();
                }
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }

        private Exception GetStandardCustomerNotFoundException()
        {
            return new ResourceNotFoundException("The requested customer was not found.");
        }

        public GetCustomersResponse GetAllCustomers()
        {
            GetCustomersResponse response = new GetCustomersResponse();
            IEnumerable<Customer> allCustomers = null;
            try
            {
                allCustomers = _customerRepository.FindAll();
                response.Customers = allCustomers.ConvertToViewModels();
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public InsertCustomerResponse InsertCustomer(InsertCustomerRequest insertCustomerRequest)
        {
            Customer newCustomer = AssignAvailablePropertiesToDomain(insertCustomerRequest.CustomerProperties);
            try
            {
                ThrowExceptionIfCustomerIsInvalid(newCustomer);
                _customerRepository.Insert(newCustomer);
                _unitOfWork.Commit();
                return new InsertCustomerResponse();
            }
            catch (Exception ex)
            {
                return new InsertCustomerResponse {Exception = ex};
            }
        }
        
        public UpdateCustomerResponse UpdateCustomer(UpdateCustomerRequest updateCustomerRequest)
        {
            try
            {
                Customer existingCustomer = _customerRepository.FindBy(updateCustomerRequest.Id);
                if (existingCustomer != null)
                {
                    Customer assignableProperties =
                        AssignAvailablePropertiesToDomain(updateCustomerRequest.CustomerProperties);
                    existingCustomer.CustomerAddress = assignableProperties.CustomerAddress;
                    existingCustomer.Name = assignableProperties.Name;
                    ThrowExceptionIfCustomerIsInvalid(existingCustomer);
                    _customerRepository.Update(existingCustomer);
                    _unitOfWork.Commit();
                }
                return new UpdateCustomerResponse { Exception = GetStandardCustomerNotFoundException() };
            }
            catch (Exception exception)
            {
                return new UpdateCustomerResponse { Exception = exception };
            }
        }

        public DeleteCustomerResponse DeleteCustomer(DeleteCustomerRequest deleteCustomerRequest)
        {
            try
            {
                Customer customerToDelete = _customerRepository.FindBy(deleteCustomerRequest.Id);
                if (customerToDelete != null)
                {
                    _customerRepository.Delete(customerToDelete);
                    _unitOfWork.Commit();
                    return new DeleteCustomerResponse();
                }
                return new DeleteCustomerResponse { Exception = GetStandardCustomerNotFoundException() };
            }
            catch (Exception ex)
            {
                return new DeleteCustomerResponse { Exception = ex };
            }
        }

        private Customer AssignAvailablePropertiesToDomain(CustomerPropertiesViewModel customerProperties)
        {
            return new Customer
            {
                Name = customerProperties.Name,
                CustomerAddress = new Address
                {
                    AddressLine1 = customerProperties.AddressLine1,
                    AddressLine2 = customerProperties.AddressLine2,
                    City = customerProperties.City,
                    PostalCode = customerProperties.PostalCode
                }
            };
        }

        private void ThrowExceptionIfCustomerIsInvalid(Customer newCustomer)
        {
            IEnumerable<BusinessRule> brokenRules = newCustomer.GetBrokenRules();
            if (brokenRules.Count() > 0)
            {
                var brokenRulesBuilder = new StringBuilder();
                brokenRulesBuilder.AppendLine("There were problems saving the customer object: ");
                foreach (var businessRule in brokenRules)
                {
                    brokenRulesBuilder.AppendLine(businessRule.RuleDescription);
                }

                throw new Exception(brokenRulesBuilder.ToString());
            }
        }
    }
}