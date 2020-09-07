namespace Customer.Domain.Model
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string City{ get; set; }
        public string ZipCode { get; set; }
        public string Complement { get; set; }
        public bool MainAddress { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
                
        public Address()
        {
            
        }
    }
}