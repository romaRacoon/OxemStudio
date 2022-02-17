using System;
using System.Collections.Generic;

namespace Tinkoff
{
    class Program
    {
        static void Main(string[] args)
        {
            Farm farm = new Farm();

            farm.Work();
        }
    }

    class Farm
    {
        private string[] _weekDay = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
        private Stable _stable = new Stable();

        private void CollectProduct()
        {
            for (int i = 0; i < _weekDay.Length; i++)
            {
                Console.WriteLine(_weekDay[i]);
                _stable.CollectProduct();
            }
        }

        public void Work()
        {
            _stable.Initialize();
            _stable.Information();

            CollectProduct();

            Console.WriteLine(_stable.GetAllAmountProducts());

            _stable.AddNewAnimals(1, 5);
            _stable.Information();

            CollectProduct();

            Console.WriteLine(_stable.GetAllAmountProducts());
        }
    }

    class Stable
    {
        private List<Product> _products = new List<Product>();
        private List<Animal> _animals = new List<Animal>();

        private int _template = 1;

        public void Initialize()
        {
            int registrationNumber;

            int cowsAmount = 10;
            int chickenAmount = 20;

            for (int i = 0; i < cowsAmount; i++)
            {
                registrationNumber = _template;
                _animals.Add(new Cow(registrationNumber, 8, 13));

                _template++;
            }

            for (int i = 0; i < chickenAmount; i++)
            {
                registrationNumber = _template;
                _animals.Add(new Chicken(registrationNumber, 0, 2));

                _template++;
            }
        }

        public void CollectProduct()
        {
            Random random = new Random();
            var animal = _animals[random.Next(0, _animals.Count)];
            int product = animal.CollectProduct();

            Console.WriteLine($"Собрано {product} продукта у животного под регистрационным номером {animal.RegistrationNumber}");

            _products.Add(new Product(animal, product));
        }

        public void Information()
        {
            for (int i = 0; i < _animals.Count; i++)
            {
                _animals[i].Information();
            }
        }

        public int GetAllAmountProducts()
        {
            int allAmount = 0;

            for (int i = 0; i < _products.Count; i++)
            {
                allAmount += _products[i].Amount;
            }

            return allAmount;
        }

        public void AddNewAnimals(int cowsAmount, int chickenAmount)
        {
            for (int i = 0; i < chickenAmount; i++)
            {
                _animals.Add(new Chicken(_template, 0, 2));

                _template++;
            }

            for (int i = 0; i < cowsAmount; i++)
            {
                _animals.Add(new Cow(_template, 8, 13));

                _template++;
            }
        }
    }

    class Product
    {
        private Animal _animal;
        private int _amount;

        public int Amount => _amount;

        public Product(Animal animal,int amount)
        {
            _animal = animal;
            _amount = amount;
        }
        
        public void Information()
        {
            Console.WriteLine($"Количество продукта {_amount} у животного в регистрационным номером {_animal.RegistrationNumber}");
        }
    }

    class Chicken : Animal
    {
        public Chicken(int registrationNumber, int minValueProductCollection, int maxValueProductCollection) : base(registrationNumber, minValueProductCollection, maxValueProductCollection) { }
    }

    class Cow : Animal
    {
        public Cow(int registrationNumber, int minValueProductCollection, int maxValueProductCollection) : base(registrationNumber, minValueProductCollection, maxValueProductCollection) { }
    }

    class Animal
    {
        private int _minValueProductCollection = -1;
        private int _maxValueProductCollection = -1;
        private int _registrationNumber;

        public int RegistrationNumber => _registrationNumber;

        public Animal(int registrationNumber)
        {
            _registrationNumber = registrationNumber;
        }

        public Animal(int registrationNumber,int minValueProductCollection,int maxValueProductCollection)
        {
            _registrationNumber = registrationNumber;
            _minValueProductCollection = minValueProductCollection;
            _maxValueProductCollection = maxValueProductCollection;
        }

        public int CollectProduct()
        {
            Random random = new Random();
            int productAmount = 0;

            if (_maxValueProductCollection != -1)
            {
                productAmount = random.Next(_minValueProductCollection, _maxValueProductCollection);

                return productAmount;
            }
            else
            {
                productAmount = random.Next(1, 30);

                return productAmount;
            }
        }

        public void Information()
        {
            Console.WriteLine($"{GetType().Name} - регистрационный номер {_registrationNumber}");
        }
    }
}
