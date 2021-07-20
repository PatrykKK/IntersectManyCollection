using IntersectManyCollection.Comparers;
using IntersectManyCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using IntersectManyCollection.Extensions;

namespace IntersectManyCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>()
            {
                new Person(){ Name = "Patryk", Age = 32 },
                new Person(){ Name = "Bogdan", Age = 74 },
                new Person(){ Name = "Ryszard", Age = 21 },
                new Person(){ Name = "Zenek", Age = 54 },
                new Person(){ Name = "Kamil", Age = 45 },
                new Person(){ Name = "Krzysztof", Age = 12 }
            };
            List<Director> directiors1 = new List<Director>()
            {
                new Director(){ Name = "Patryk1", Age = 32 },
                new Director(){ Name = "Bogdan1", Age = 74 },
                new Director(){ Name = "Ryszard", Age = 45 },
                new Director(){ Name = "Zenek1", Age = 54 },
                new Director(){ Name = "Kamil2", Age = 45 },
                new Director(){ Name = "Krzysztof", Age = 12 }
            };
            List<Director> directiors2 = new List<Director>()
            {
                new Director(){ Name = "Patryk2", Age = 32 },
                new Director(){ Name = "Bogdan3", Age = 74 },
                new Director(){ Name = "Ryszard", Age = 21 },
                new Director(){ Name = "Zenek3", Age = 54 },
                new Director(){ Name = "Kamil1", Age = 45 },
                new Director(){ Name = "Krzysztof", Age = 12 }
            };
            List<List<Director>> directiors = new List<List<Director>>()
            {
                directiors1,
                directiors2
            };
            List<List<Person>> people1 = new List<List<Person>>() { people };
            var per = people.IntersectManyCollections(directiors, new ModelComparer<Person,Director>((x, y) => x.Name == y.Name && x.Age == y.Age)).ToList();
            

            per.ForEach(x => Console.WriteLine($"{x.Name} {x.Age}"));
        }
    }
}
