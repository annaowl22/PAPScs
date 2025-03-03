﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_2
{
    abstract class Driver
    {
        public abstract void info();
    }


    class BusDriver : Driver
    {
        private static readonly BusDriver instance = new BusDriver();

        public BusDriver() { } // Private constructor

        public override void info()
        {
            Console.WriteLine("Bus Driver on board");
        }

        public static BusDriver Instance
        {
            get { return instance; }
        }
    }


    class TaxiDriver : Driver
    {
        private static readonly TaxiDriver instance = new TaxiDriver();

        public TaxiDriver() { } // Private constructor

        public override void info()
        {
            Console.WriteLine("Taxi driver is here");
        }

        public static TaxiDriver Instance
        {
            get { return instance; }
        }
    }


    public class Passenger
    {
        public string Name { get; }

        public Passenger(string name)
        {
            Name = name;
        }

        public string GetPerson()
        {
            return Name;
        }
    }


    abstract class BoardAnyCar
    {
        public int LimitPassengers { get; } // Свойство с private set

        protected List<Passenger> passengers { get; } = new List<Passenger>();
        protected Driver driver { get; set; }

        public BoardAnyCar(int limit)
        {
            LimitPassengers = limit; // Присваиваем значение свойству LimitPassengers
        }

        public abstract Driver CreateDriver();
        public abstract void GetInfoAboutVehicle();

        public virtual void BoardPassenger(string passengerName)
        {
            passengers.Add(new Passenger(passengerName));
        }

        public virtual void SetDriver()
        {
            driver = CreateDriver();
        }

        public Driver GetDriver()
        {
            return driver;
        }

        public int GetLimitPassengers()
        {
            return LimitPassengers;
        }

        public void PrintPassengers()
        {
            Console.WriteLine("Пассажиры: ");
            foreach (var passenger in passengers)
            {
                Console.WriteLine(" - " + passenger.GetPerson());
            }
        }

        public void PrintDriver()
        {
            if (driver != null)
            {
                driver.info();
            }
            else
            {
                Console.WriteLine("Водитель не назначен.");
            }
        }

        public void PrintAllPersons()
        {
            GetInfoAboutVehicle();
            PrintPassengers();
            PrintDriver();
            Console.WriteLine("\n\n");
        }

        public virtual bool LetsGo()
        {
            GetInfoAboutVehicle();
            if (0 < passengers.Count && passengers.Count <= LimitPassengers && driver != null)
            {
                Console.WriteLine("Поездка осуществима.");
                return true;
            }
            else
            {
                Console.WriteLine("Поездка не осуществима:");
                if (passengers.Count > LimitPassengers)
                {
                    Console.WriteLine(" - Превышен лимит пассажиров: " + passengers.Count + " при " + LimitPassengers + " допустимых.");
                }
                if (passengers.Count == 0)
                {
                    Console.WriteLine(" - Отсутствуют пассажиры.");
                }
                if (driver == null)
                {
                    Console.WriteLine(" - Водитель остутсвует.");
                }
                return false;
            }
        }

    }


    class BoardTaxi : BoardAnyCar
    {
        public const int LimitPassengers = 4; // Константа лимита пассажиров

        public BoardTaxi() : base(LimitPassengers) { } // Передаем лимит в базовый класс



        public override Driver CreateDriver()
        {
            return new TaxiDriver(); // Возвращаем указатель на TaxiDriver
        }

        public override void GetInfoAboutVehicle()
        {
            Console.WriteLine("Такси:"); //Исправлено на "Такси", а не "Автобус"
        }
    }


    class BoardBus : BoardAnyCar
    {
        public const int LimitPassengers = 30; // Константа лимита пассажиров

        public BoardBus() : base(LimitPassengers) { } // Передаем лимит в базовый класс

        public override Driver CreateDriver()
        {
            return new BusDriver(); // Возвращаем указатель на TaxiDriver
        }

        public override void GetInfoAboutVehicle()
        {
            Console.WriteLine("Автобус:"); //Исправлено на "Такси", а не "Автобус"
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            BoardTaxi taxiBoard = new BoardTaxi(); // Создаем экземпляр BoardTaxi
            taxiBoard.SetDriver();
            taxiBoard.BoardPassenger("John Fall");
            taxiBoard.BoardPassenger("Jane Wallis");
            taxiBoard.BoardPassenger("Alice Green");
            taxiBoard.BoardPassenger("James Firewood");
            taxiBoard.PrintAllPersons();
            taxiBoard.LetsGo();

            BoardBus busBoard = new BoardBus();
            busBoard.BoardPassenger("Lancelot Donovan");
            busBoard.BoardPassenger("Natali Tompson");
            busBoard.BoardPassenger("Bill Gordon");
            busBoard.BoardPassenger("Jack Firewood");
            busBoard.BoardPassenger("Nakamura Isami");
            busBoard.BoardPassenger("Rayan Force");
            busBoard.BoardPassenger("Matt Stablee");
            busBoard.PrintAllPersons();
            busBoard.LetsGo();

            BoardTaxi taxiboard2 = new BoardTaxi();
            taxiboard2.SetDriver();
            taxiboard2.BoardPassenger("Feather1");
            taxiboard2.BoardPassenger("Feather2");
            taxiboard2.BoardPassenger("Feather3");
            taxiboard2.BoardPassenger("Feather4");
            taxiboard2.BoardPassenger("Feather5");
            taxiboard2.PrintAllPersons();
            taxiboard2.LetsGo();

            Console.WriteLine("------------------------------");
            Console.ReadKey(); // Чтобы консоль не закрывалась сразу

        }
    }
}
