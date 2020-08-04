using System;
using System.Linq;
using HerokuDeploySample.Models;

namespace HerokuDeploySample.Data
{
    public class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student{FirstMidName="Carson",LastName="Alexander"},
                new Student{FirstMidName="Meredith",LastName="Alonso"},
                new Student{FirstMidName="Arturo",LastName="Anand"},
                new Student{FirstMidName="Gytis",LastName="Barzdukas"},
                new Student{FirstMidName="Yan",LastName="Li"},
                new Student{FirstMidName="Peggy",LastName="Justice"},
                new Student{FirstMidName="Laura",LastName="Norman"},
                new Student{FirstMidName="Nino",LastName="Olivetto"}
            };

            context.Students.AddRange(students);
            context.SaveChanges();
        }
    }
}