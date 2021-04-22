using System;
using System.Collections.Generic;
using PetsMicroservice.Models;

namespace PetsMicroservice.Repositories.Context
{
    public static class MockDatabase
    {
        public static IEnumerable<Pet> Pets => new List<Pet>
        {
            new Pet { Id = 1, Name = "Pet1", DateOfBirth = DateTime.Now.AddYears(10), Address = "Address1" },
            new Pet { Id = 2, Name = "Pet2", DateOfBirth = DateTime.Now, Address = "Address4" },
            new Pet { Id = 3, Name = "Pet3", DateOfBirth = DateTime.Now.AddYears(-10), Address = "Address2" },
            new Pet { Id = 4, Name = "Pet4", DateOfBirth = DateTime.Now.AddMonths(3), Address = "Address3" },
            new Pet { Id = 5, Name = "Pet5", DateOfBirth = DateTime.Now.AddYears(6), Address = "Address2" },
        };
    }
}
