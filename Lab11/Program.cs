using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab1_2
{

    class Passenger{
        public string name;
        public string category;

        public Passenger(string set_name, string set_category){
            name = set_name;
            category = set_category;
        }
        public void information(){
            Console.WriteLine("Passenger ",name, " of ", category, " category is on board");
        }
    }

    abstract class Driver {
        abstract public void information();
        public string license;
    }

    class BusDriver : Driver{
        
    public static string license = "Bus";

    public override void information()
    {
      Console.WriteLine("Bus driver is on board");
    }

    }


    class TaxiDriver : Driver{

    public static string license = "Taxi";

    public override void information()
    {
      Console.WriteLine("Taxi driver is on board");
    }

    }

    abstract class Car{
        public int PassengerLimit;

        public List<Passenger> passengers { get; } = new List<Passenger>();
      
        public Driver driver { get; set; }

        public abstract void BoardPassenger(Passenger passenger);

        public abstract void information();
    }

    class TaxiCar: Car{
        public static int PassengerLimit = 4;
        public override void BoardPassenger(Passenger passenger)
        {
            passengers.Add(passenger);
        }
        public override void information(){
            Console.WriteLine("The taxi is ready");
        }
    }

    class BusCar: Car{
        public static int PassengerLimit = 30;
        public override void BoardPassenger(Passenger passenger)
        {
            passengers.Add(passenger);
        }
        public override void information(){
            Console.WriteLine("The bus is ready");
        }
    }

    class Avtovaz{
        public BusCar CreateBus(){
            return new BusCar();
        }

        public TaxiCar CreateTaxi(){
            return new TaxiCar();
        }

    }

    class Mosgortrans{

        
        public BusDriver HireBusDriver(){
            return new BusDriver();
        }

        public TaxiDriver HireTaxiDriver(){
            return new TaxiDriver();
        }
        public BusCar CreateBus(Avtovaz vaz){
            return vaz.CreateBus();
        }
        public TaxiCar CreateTaxi(Avtovaz vaz){
            return vaz.CreateTaxi();
        }

        public void SetDriver(Car car, Driver driver){
            car.driver = driver;
        }

        public void BoardPassenger(Car car, Passenger passenger){
            car.BoardPassenger(passenger);
        }
        public void Start(Car car){
            bool adults = false;
            foreach(Passenger passenger in car.passengers){
                if (String.Compare(passenger.category,"adult")==0 | String.Compare(passenger.category,"disabled")==0){
                    adults = true;
                }
            }
            bool children = false;
            foreach(Passenger passenger in car.passengers){
                if (String.Compare(passenger.category,"child")==0){
                    adults = true;
                }
            }
            if(0 < car.passengers.Count & adults & car.passengers.Count <= car.PassengerLimit & car.driver != null){
                if(String.Compare(car.driver.license,"Bus")==0 & car.PassengerLimit == 30 | String.Compare(car.driver.license,"Taxi")==0 & car.PassengerLimit == 4){
                    car.information();
                    car.driver.information();
                    foreach(Passenger passenger in car.passengers){
                        passenger.information();
                    }
                    Console.WriteLine("Good drive");
                }
            }else if(car.driver == null){
                Console.WriteLine("No driver");
            }else if(car.passengers.Count == 0){
                Console.WriteLine("No passengers");
            }else if(adults == false){
                Console.WriteLine("Children cannot go without adults");
            }else if(car.driver.license == "Bus" & car.PassengerLimit == 4 | car.driver.license == "Taxi" & car.PassengerLimit == 30){
                Console.WriteLine("The driver has no license for this transport");
            }

        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Mosgortrans company = new Mosgortrans();
            Avtovaz vaz = new Avtovaz();
            BusCar Bus1 = company.CreateBus(vaz);
            TaxiCar Taxi1 = company.CreateTaxi(vaz);
            TaxiCar Taxi2 = company.CreateTaxi(vaz);
            TaxiCar Taxi3 = company.CreateTaxi(vaz);
            TaxiDriver taxidriver = company.HireTaxiDriver();
            BusDriver busdriver = company.HireBusDriver();
            company.SetDriver(Bus1, busdriver);
            company.BoardPassenger(Bus1,new Passenger("Alice","adult"));
            company.Start(Bus1);


        }
    }
}
