using System;
using System.Collections.Generic;

namespace Tinkoff
{
    class Program
    {
        static void Main(string[] args)
        {
            int registationNumber = 1;

            Farm farm = new Farm();

            for (int i = 0; i < 10; i++)
            {
                farm.AddAnimal(new Cow(registationNumber));
                registationNumber++;
            }
            for (int i = 0; i < 20; i++)
            {
                farm.AddAnimal(new Chicken(registationNumber));
                registationNumber++;
            }

            farm.Information();

            farm.CollectProduct();

            farm.ShowAllAmountProducts();

            for (int i = 0; i < 5; i++)
            {
                farm.AddAnimal(new Chicken(registationNumber));
                registationNumber++;
            }

            farm.AddAnimal(new Cow(registationNumber));
            farm.Information();

            farm.CollectProduct();

            farm.ShowAllAmountProducts();
        }
    }

    class Farm
    {
        private List<int> _products = new List<int>();
        private List<Animal> _animals = new List<Animal>();

        public void CollectProduct()
        {
            Random random = new Random();

            for (int i = 0; i < _animals.Count; i++)
            {
                var animal = _animals[i];
                int product = animal.GetProduct();

                Console.WriteLine($"Собрано {product} продукта у животного под регистрационным номером {animal.RegistrationNumber}");

                _products.Add(product);
            }
        }

        public void Information()
        {
            for (int i = 0; i < _animals.Count; i++)
            {
                _animals[i].Information();
            }
        }

        public void ShowAllAmountProducts()
        {
            int productsAmount = 0;

            for (int i = 0; i < _products.Count; i++)
            {
                productsAmount += _products[i];
            }

            Console.WriteLine($"Всего собрано продукции - {productsAmount}");
        }


        public void AddAnimal(Animal animal)
        {
            _animals.Add(animal);
        }
    }

    class Chicken : Animal
    {
        public Chicken(int registrationNumber) : base(registrationNumber) { }

        public override int GetProduct()
        {
            Random random = new Random();

            return random.Next(0, 2);
        }
    }

    class Cow : Animal
    {
        public Cow(int registrationNumber) : base(registrationNumber) { }

        public override int GetProduct()
        {
            Random random = new Random();

            return random.Next(8, 13);
        }
    }

    abstract class Animal
    {
        private int _registrationNumber;

        public int RegistrationNumber => _registrationNumber;

        public Animal(int registrationNumber)
        {
            _registrationNumber = registrationNumber;
        }

        public abstract int GetProduct();

        public void Information()
        {
            Console.WriteLine($"{GetType().Name} - регистрационный номер {_registrationNumber}");
        }
    }
}
