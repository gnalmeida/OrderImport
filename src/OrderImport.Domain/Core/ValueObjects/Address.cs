namespace OrderImport.Domain.Core.ValueObjects
{
    public class Address
    {
        public Address(string street, string number, string complement, string neighborhood, string city, string state, string country, string postalCode)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }
    }
}
