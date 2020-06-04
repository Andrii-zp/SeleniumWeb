﻿using System;
using System.Collections;

namespace csharp_example
{
    internal class DataProviders
    {
        public static IEnumerable ValidCustomers
        {
            get
            {
                yield return new Customer()
                {
                    Firstname = "Adam3",
                    Lastname = "Smith",
                    Phone = "+0123456789",
                    Address = "Hidden Place",
                    Postcode = "12345",
                    City = "New City",
                    Country = "US",
                    Zone = "KS",
                    Email = "adam" + (long)(DateTime.Now - DateTime.MinValue).TotalMilliseconds + "@smith.me",
                    Password = "qwerty"
                };
                yield return new Customer()
                {
                    Firstname = "Adam4",
                    Lastname = "Smith2",
                    Phone = "+0123456789",
                    Address = "Hidden Place",
                    Postcode = "12345",
                    City = "New City22",
                    Country = "US",
                    Zone = "KS",
                    Email = "adam22" + (long)(DateTime.Now - DateTime.MinValue).TotalMilliseconds + "@smith.me",
                    Password = "qwerty"
                };
                /* ... */
            }
        }
    }
}