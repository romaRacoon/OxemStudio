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
        private Stable _stable = new Stable(new AnimalFactory());

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

            for (int i = 0; i < 5; i++)
            {
                _stable.AddNewChicken();
            }

            _stable.AddNewCow();
            _stable.Information();

            CollectProduct();

            Console.WriteLine(_stable.GetAllAmountProducts());
        }
    }

    class Stable
    {
        private AnimalFactory _animalFactory;

        private List<Product> _products = new List<Product>();
        private List<Animal> _animals = new List<Animal>();

        private int _template = 1;

        public Stable(AnimalFactory animalFactory)
        {
            _animalFactory = animalFactory;
        }

        public void CollectProduct()
        {
            Random random = new Random();
            var animal = _animals[random.Next(0, _animals.Count)];
            int product = animal.GetProduct();

            Console.WriteLine($"Собрано {product} продукта у животного под регистрационным номером {animal.RegistrationNumber}");

            _products.Add(new Product(animal, product));
        }

        public void Initialize()
        {
            int registrationNumber;

            int animalsAmount = 30;

            for (int i = 0; i < animalsAmount; i++)
            {
                registrationNumber = _template;

                if (i < 9)
                    _animals.Add(_animalFactory.CreateCow(registrationNumber));
                else
                    _animals.Add(_animalFactory.CreateChicken(registrationNumber));

                _template++;
            }
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

        public void AddNewChicken()
        {
            _animals.Add(_animalFactory.CreateChicken(_template));

            _template++;
        }

        public void AddNewCow()
        {
            _animals.Add(_animalFactory.CreateCow(_template));

            _template++;
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
        public Chicken(int registrationNumber) : base(registrationNumber) { }

        public override int GetProduct()
        {
            Random random = new Random();
            int productAmount = 0;

            productAmount = random.Next(0, 2);

            return productAmount;
        }
    }

    class Cow : Animal
    {
        public Cow(int registrationNumber) : base(registrationNumber) { }

        public override int GetProduct()
        {
            Random random = new Random();
            int productAmount = 0;

            productAmount = random.Next(8, 12);

            return productAmount;
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

    class AnimalFactory : IAnimalFactory
    {
        public Animal CreateChicken(int registrationNumber)
        {
            return new Chicken(registrationNumber);
        }

        public Animal CreateCow(int registrationNumber)
        {
            return new Cow(registrationNumber);
        }
    }

    interface IAnimalFactory
    {
        public Animal CreateCow(int registrationNumber);

        public Animal CreateChicken(int registrationNumber);
    }
}
