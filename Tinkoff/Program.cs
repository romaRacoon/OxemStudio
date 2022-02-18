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

            _stable.AddNewAnimals();
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

            int animalsAmount = 30;

            for (int i = 0; i < animalsAmount; i++)
            {
                registrationNumber = _template;

                if (i < 9)
                    _animals.Add(new Animal(registrationNumber, 8, 13, "Корова"));
                else
                    _animals.Add(new Animal(registrationNumber, 0, 2, "Курица"));

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

        public void AddNewAnimals()
        {
            int animalsAmount = 6;

            for (int i = 0; i < animalsAmount; i++)
            {
                if (i < 1)
                    _animals.Add(new Animal(_template, 8, 13, "Корова"));
                else
                    _animals.Add(new Animal(_template, 0, 2, "Курица"));

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

    class Animal
    {
        private int _minValueProductCollection = -1;
        private int _maxValueProductCollection = -1;
        private int _registrationNumber;
        private string _type;

        public string Type => _type;
        public int RegistrationNumber => _registrationNumber;

        public Animal(int registrationNumber, string type)
        {
            _registrationNumber = registrationNumber;
            _type = type;
        }

        public Animal(int registrationNumber,int minValueProductCollection,int maxValueProductCollection, string type)
        {
            _registrationNumber = registrationNumber;
            _minValueProductCollection = minValueProductCollection;
            _maxValueProductCollection = maxValueProductCollection;
            _type = type;
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
            Console.WriteLine($"{_type} - регистрационный номер {_registrationNumber}");
        }
    }
}
