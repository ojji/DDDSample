using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DDDSample.Repository.Memory.Database
{
    public class InMemoryDatabaseObjectContext
    {
        public List<DatabaseCustomer> DatabaseCustomers { get; set; }

        public static InMemoryDatabaseObjectContext Instance
        {
            get { return Nested.Instance; }
        }

        private class Nested
        {
            static Nested()
            {
                
            }

            internal static InMemoryDatabaseObjectContext Instance = new InMemoryDatabaseObjectContext();
        }

        public InMemoryDatabaseObjectContext()
        {
            InitializeDatabaseCustomers();
        }

        public void AddEntity<T>(T databaseEntity)
        {
            if (databaseEntity is DatabaseCustomer)
            {
                DatabaseCustomer databaseCustomer = databaseEntity as DatabaseCustomer;
                databaseCustomer.Id = DatabaseCustomers.Count + 1;
                DatabaseCustomers.Add(databaseCustomer);
            }
        }

        public void UpdateEntity<T>(T databaseEntity)
        {
            if (databaseEntity is DatabaseCustomer)
            {
                var dbCustomer = databaseEntity as DatabaseCustomer;
                var dbCustomerToUpdate = (from c in DatabaseCustomers where c.Id == dbCustomer.Id select c).FirstOrDefault();
                if (dbCustomerToUpdate == null) { throw new ArgumentException(string.Format("DatabaseCustomer with id {0} cannot be found.", dbCustomer.Id)); }
                dbCustomerToUpdate.Name = dbCustomer.Name;
                dbCustomerToUpdate.Address = dbCustomer.Address;
                dbCustomerToUpdate.City = dbCustomer.City;
                dbCustomerToUpdate.Country = dbCustomer.Country;
                dbCustomerToUpdate.Telephone = dbCustomer.Telephone;
            }
        }

        public void DeleteEntity<T>(T databaseEntity)
        {
            if (databaseEntity is DatabaseCustomer)
            {
                var dbCustomer = databaseEntity as DatabaseCustomer;
                var dbCustomerToDelete = (from c in DatabaseCustomers where c.Id == dbCustomer.Id select c).FirstOrDefault();
                if (dbCustomerToDelete == null) { throw new ArgumentException(string.Format("DatabaseCustomer with id {0} cannot be found.", dbCustomer.Id)); }
                DatabaseCustomers.Remove(dbCustomerToDelete);
            }
        }

        private void InitializeDatabaseCustomers()
        {
            DatabaseCustomers = new List<DatabaseCustomer>();
            DatabaseCustomers.Add(new DatabaseCustomer { Id = 1, Name = "Bob Delon", Address = "Main str 143.", City = "New York", Country = "United States", Telephone = "555-1234-4223" });
            DatabaseCustomers.Add(new DatabaseCustomer { Id = 2, Name = "Kovács József", Address = "Katona utca 12.", City = "Budapest", Country = "Hungary", Telephone = "70-544-1234" });
            DatabaseCustomers.Add(new DatabaseCustomer { Id = 3, Name = "Heidi Klumm", Address = "Goethe strasse 3.", City = "Berlin", Country = "Germany", Telephone = "1-34-2321-3313" });
        }
    }
}