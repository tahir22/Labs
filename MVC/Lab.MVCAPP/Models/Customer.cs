using System;
using System.Collections.Generic;

namespace Lab.MVCAPP.Models
{
    static public class MockDataProvider
    {
        static public IEnumerable<Customer> GetCustomers()
        {
            var list = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "Tabeer",
                    Profile = "/img/tabeer.jpg"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Zarnab",
                    Profile = "/img/zarnab.jpg"
                },
            };

            return list;
        }
    }
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Profile { get; set; }
    }
}
