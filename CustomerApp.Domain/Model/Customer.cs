using System;
using System.Collections.Generic;

namespace CustomerApp.Domain.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Address> Addresses { get; set; }

        public Customer()
        {

        }
    }
}