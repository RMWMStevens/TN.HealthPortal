﻿namespace TN.HealthPortal.Lib.Entities
{
    public class Address
    {
        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public Country Country { get; set; }
    }
}
