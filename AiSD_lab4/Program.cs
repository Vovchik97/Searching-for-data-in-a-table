using System;
using System.Collections.Generic;
using System.Linq;

namespace AiSD_lab4
{
    class Subscriber
    {
        public string Id { get; }
        public string Name { get; }
        public List<Phone> Phones { get; }

        public Subscriber(string id, string name)
        {
            Id = id;
            Name = name;
            Phones = new List<Phone>();
        }
    }

    class Phone
    {
        public string Number { get; }
        public bool IsWorking { get; }

        public Phone(string number)
        {
            Number = number;
            IsWorking = true;
        }
    }

    class Payment
    {
        public string SubscriberId { get; }
        public DateTime Date { get; }
        public decimal Amount { get; }

        public Payment(string subscriberId, DateTime date, decimal amount)
        {
            SubscriberId = subscriberId;
            Date = date;
            Amount = amount;
        }
    }

    class DataStorage
    {
        private readonly List<Subscriber> _subscribers;
        private readonly List<Payment> _payments;

        public DataStorage()
        {
            _subscribers = new List<Subscriber>
            {
                new Subscriber("001", "Иванов Иван Иванович"),
                new Subscriber("002", "Петров Петр Петрович"),
                new Subscriber("003", "Сидоров Сидор Сидорович")
            };

            _payments = new List<Payment>
            {
                new Payment("001", new DateTime(2020, 1, 1), 100),
                new Payment("001", new DateTime(2020, 2, 1), 200),
                new Payment("002", new DateTime(2020, 1, 1), 150),
                new Payment("003", new DateTime(2020, 1, 1), 120)
            };
        }

        public Subscriber SequentialSearchSubscriberById(string id)
        {
            return _subscribers.FirstOrDefault(subscriber => subscriber.Id == id);
        }

        public Subscriber BinarySearchSubscriberById(string id)
        {
            int left = 0;
            int right = _subscribers.Count - 1;

            while (left <= right)
            {
                int middle = (left + right) / 2;
                if (_subscribers[middle].Id == id)
                {
                    return _subscribers[middle];
                }
                else if (string.Compare(_subscribers[middle].Id, id) < 0)
                {
                    left = middle + 1;
                }
                else
                {
                    right = middle - 1;
                }
            }
            return null;
        }

        public decimal GetTotalPaymentBySubscriberId(string id)
        {
            return _payments.Where(payment => payment.SubscriberId == id).Sum(payment => payment.Amount);
        }
    }

    class Program
    {
        const string SearchById = "1";
        const string Exit = "2";
        static void Main(string[] args)
        {
            DataStorage dataStorage = new DataStorage();

            while (true)
            {
                Console.WriteLine("Выберите один из вариантов: ");
                Console.WriteLine("1 - Поиск по ID");
                Console.WriteLine("2 - Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case SearchById:
                        Console.WriteLine("Введите ID абонента для поиска:");
                        string id = Console.ReadLine();

                        Subscriber foundSubscriber = dataStorage.SequentialSearchSubscriberById(id);

                        DisplaySubscriberDetails(foundSubscriber, dataStorage, id);

                        foundSubscriber = dataStorage.BinarySearchSubscriberById(id);

                        DisplaySubscriberDetails(foundSubscriber, dataStorage, id);

                        break;

                    case Exit:
                        return;

                    default:
                        Console.WriteLine("Выберите действие из списка");
                        break;
                }
            }
        }

        static void DisplaySubscriberDetails(Subscriber subscriber, DataStorage dataStorage, string id)
        {
            if (subscriber != null)
            {
                decimal totalPayment = dataStorage.GetTotalPaymentBySubscriberId(id);
                Console.WriteLine($"Найденный абонент: {subscriber.Name}");
                Console.WriteLine($"Общая сумма платежей: {totalPayment}");
            }
            else
            {
                Console.WriteLine("Абонент с указанным ID не найден");
            }
        }
    }
}


