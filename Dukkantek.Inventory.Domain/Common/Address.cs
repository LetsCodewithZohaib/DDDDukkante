using System;
using System.Collections.Generic;
using Dukkantek.Inventory.Domain.Common;

namespace Dukkantek.Inventory.Domain.Common
{
    public class Address : ValueObject
    {
        public String HouseNo { get; private set; }
        public String Street { get; private set; }
        public String City { get; private set; }
        public String State { get; private set; }
        public String Country { get; private set; }
        public String CountryCode { get; private set; }
        public String ZipCode { get; private set; }
        
        public Address() { }

        public Address(string houseno, string street, string city, string state, string country, string countrycode, string zipcode)
        {
            HouseNo = houseno;
            Street = street;
            City = city;
            State = state;
            Country = country;
            CountryCode = countrycode;
            ZipCode = zipcode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return HouseNo;
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return CountryCode;
            yield return ZipCode;
        }
    }
}
