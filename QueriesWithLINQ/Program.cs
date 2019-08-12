using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueriesWithLINQ
{


    class Program
    {

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            QueryStringArray();
            //get the array returned and print it
            Console.ForegroundColor = ConsoleColor.Green;
            int[] intArray = QueryIntArray();

            Console.ForegroundColor = ConsoleColor.White;
            foreach (int i in intArray)
            Console.WriteLine(i);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            QueryArrayList();
            Console.ForegroundColor = ConsoleColor.White;
            QueryCollection();
            Console.ForegroundColor = ConsoleColor.Green;
            QueryAnimalData();
            Console.ReadLine();

        }

        static void QueryStringArray()
        {
            string[] dogs = {"K 9", "Brian Griffin",
            "Scooby Doo", "Old Yeller", "Rin Tin Tin",
            "Benji", "Charlie B. Barkin", "Lassie",
            "Snoopy"};


            // Get strings with spaces and put in 
            // alphabetical order
            var dogSpaces = from dog in dogs
                            where dog.Contains(" ")
                            orderby dog descending
                            select dog;

            foreach (var i in dogSpaces)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();
        }

        //query that orders nums that are greater than 20
        static int[] QueryIntArray()
        {
            int[] nums = { 5, 10, 15, 20, 25, 30, 35 };
            // get numbers bigger then 20
            var gt20 = from num in nums
                       where num > 20
                       orderby num
                       select num;

            foreach (var i in gt20)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            // the type is an enumerable
            Console.WriteLine($"Get Type: {gt20.GetType()}");

            // can convert it into a list or array
            var listGT20 = gt20.ToList<int>();
            var arrayGT20 = gt20.ToArray();

            // if you change the data the query auto updates
            nums[0] = 40;

            foreach (var i in gt20)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();
            return arrayGT20;
        }

        static void QueryArrayList()
        {
            ArrayList famAnimals = new ArrayList()
            {
                new Animal
                {
                    Name = "Heidi", Height = .8, Weight = 18
                },
                new Animal
                {
                    Name = "Shrek", Height = 4, Weight = 130
                },
                new Animal
                {
                    Name = "Congo", Height = 3.8, Weight = 90
                }
            };

            //convert the ArrayList into an IEnumerable
            var famAnimalEnum = famAnimals.OfType<Animal>();

            //order the array list by name where weight is <= 90
            var smAnimals = from animal in famAnimalEnum
                            where animal.Weight <= 90
                            orderby animal.Name
                            select animal;

            foreach (var animal in smAnimals)
            {
                Console.WriteLine("{0} weighs {1}lbs", animal.Name, animal.Weight);
            }
            Console.WriteLine();
        }

        static void QueryCollection()
        {
            var animalList = new List<Animal>()
            {
                new Animal { Name = "German Shepherd", Height = 25, Weight = 77},
                new Animal { Name = "Chihuahua", Height = 7, Weight = 4.4},
                new Animal { Name = "Saint Bernard", Height = 30, Weight = 200 },
            };

            //order the dog where the weight is above 70 and the height is above 25
            var bigDogs = from dog in animalList
                          where (dog.Weight > 70) && (dog.Height > 25)
                          orderby dog.Name
                          select dog;

            foreach (var dog in bigDogs)
            {
                Console.WriteLine("A {0} weighs {1}lbs", dog.Name, dog.Weight);
            }
            Console.WriteLine();
        }


        static void QueryAnimalData()
        {
            Animal[] animals = new[]
            {
                new Animal{Name = "German Shepherd", Height = 25, Weight = 77, AnimalID = 1 },
                new Animal{ Name = "Chihuahua", Height = 7, Weight = 4.4, AnimalID = 2},
                new Animal{Name = "Saint Bernard", Height = 30, Weight = 200, AnimalID = 3},
                new Animal{Name = "Pug", Height = 12, Weight = 16, AnimalID = 1},
                new Animal{Name = "Beagle", Height = 15, Weight = 23, AnimalID = 2}
            };

            Owner[] owners = new[]
            {
                new Owner{ Name = "Doug Parks", OwnerID = 1},
                new Owner{ Name = "Sally Smoth", OwnerID = 2},
                new Owner{ Name = "Paul Brooks", OwnerID = 3}
            };

            
            // get name and height from the animal
            var nameHeight = from a in animals
                             select new { a.Name, a.Height };

            // convert to an object array
            Array arrNameHeight = nameHeight.ToArray();

            foreach (var i in arrNameHeight)
            {
                Console.WriteLine(i.ToString());
            }
            Console.WriteLine();

            //create an inner join
            // join info in owners and animals using equal values for ids store values for animal and owner
            var innerJoin = from animal in animals
                            join owner in owners on animal.AnimalID
                            equals owner.OwnerID
                            select new { OwnerName = owner.Name, AnimalName = animal.Name };

            foreach (var i in innerJoin)
            {
                Console.WriteLine("{0} owns {1}", i.OwnerName, i.AnimalName);
            }
            Console.WriteLine();

            //create a group inner join 
            // gets all animals and put them in a newly created owner group if their
            // IDs match the owners ID
            var groupJoin =
                from owner in owners
                orderby owner.OwnerID
                join animal in animals on owner.OwnerID
                equals animal.AnimalID into ownerGroup
                select new
                {
                    Owner = owner.Name,
                    Animals = from owner2 in ownerGroup
                              orderby owner2.Name
                              select owner2
                };

            int totalAnimals = 0;

            foreach (var ownerGroup in groupJoin)
            {
                Console.WriteLine(ownerGroup.Owner);
                foreach (var animal in ownerGroup.Animals)
                {
                    totalAnimals++;
                    Console.WriteLine("* {0}", animal.Name);
                }
            }

        }


    }

}
