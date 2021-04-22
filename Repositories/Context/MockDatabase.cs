using System;
using System.Collections.Generic;
using PetsMicroservice.Models;

namespace PetsMicroservice.Repositories.Context
{
    public static class MockDatabase
    {
        public static IEnumerable<Pet> Pets => new List<Pet>
        {
            new Pet { Id = 1, Name = "Pet1", DateOfBirth = new DateTime(1998, 1, 1).Date, Address = "Address1" },
            new Pet { Id = 2, Name = "Pet2", DateOfBirth = new DateTime(2000, 10, 10).Date, Address = "Address4" },
            new Pet { Id = 3, Name = "Pet3", DateOfBirth = new DateTime(2001, 8, 13).Date, Address = "Address2" },
            new Pet { Id = 4, Name = "Pet4", DateOfBirth = new DateTime(1996, 4, 25).Date, Address = "Address3" },
            new Pet { Id = 5, Name = "Pet5", DateOfBirth = new DateTime(2015, 7, 18).Date, Address = "Address2" },
        };
    }
}
