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
        public void info(){
            Console.WriteLine("Passenger " + name + " of " + category + " category is on board");
        }
    }

    abstract class Driver {
        abstract public void info();
        public string license{ get; set; }
    }

    class BusDriver : Driver{
    public override void info()
    {
      Console.WriteLine("Bus driver is on board");
    }

    }


    class TaxiDriver : Driver{

    public override void info()
    {
      Console.WriteLine("Taxi driver is on board");
    }

    }

    abstract class Car{
        public List<Passenger> passengers { get; } = new List<Passenger>();
      
        public Driver driver { get; set; }

        public abstract void BoardPassenger(Passenger passenger);

        public abstract void info();
    }

    class TaxiCar: Car{
        public bool childseat { get; set; }
        public static int PassengerLimit = 4;
        public override void BoardPassenger(Passenger passenger)
        {
            passengers.Add(passenger);
        }
        public override void info(){
            Console.WriteLine("The taxi is ready");
        }
    }

    class BusCar: Car{
        public static int PassengerLimit = 30;
        public override void BoardPassenger(Passenger passenger)
        {
            passengers.Add(passenger);
        }
        public override void info(){
            Console.WriteLine("The bus is ready");
        }
    }

    class Avtovaz{
        public BusCar CreateBus( Driver driver, List<Passenger> passengers){
            BusCar bus = new BusCar();
            bus.driver = driver;
            if(passengers!=null){
            foreach(Passenger passenger in passengers){
                bus.BoardPassenger(passenger);
            }
            }
            return bus;
        }

        public TaxiCar CreateTaxi(Driver driver, List<Passenger> passengers, bool childseat){
            TaxiCar taxi = new TaxiCar();
            taxi.driver = driver;
            if(passengers!=null){
            foreach(Passenger passenger in passengers){
                taxi.BoardPassenger(passenger);
            }
            }
            taxi.childseat = childseat;
            return taxi;
        }
        public BusCar CreateBus(Driver driver){
            BusCar taxi = new BusCar();
            taxi.driver = driver;
            return taxi;
        }

        public TaxiCar CreateTaxi(Driver driver, bool childseat){
            TaxiCar taxi = new TaxiCar();
            taxi.driver = driver;
            taxi.childseat = childseat;
            return taxi;
        }

    }

    class Mosgortrans{

        
        public BusDriver HireBusDriver(){
            BusDriver bus = new BusDriver();
            bus.license = "Bus";
            return bus;
        }

        public TaxiDriver HireTaxiDriver(){
            TaxiDriver bus = new TaxiDriver();
            bus.license = "Taxi";
            return bus;
        }
        public BusCar CreateAndFillBus(Avtovaz vaz, Driver driver, List<Passenger> passengers=null){
            if(passengers==null){
                BusCar bus = vaz.CreateBus(driver);
                return bus;
            }else{
                BusCar bus = vaz.CreateBus(driver,passengers);
                return bus;
            }

        }
        public TaxiCar CreateAndFillTaxi(Avtovaz vaz, Driver driver, List<Passenger> passengers=null, bool childseat=false){
            if(passengers==null){
                TaxiCar taxi = vaz.CreateTaxi(driver,childseat);
                return taxi;
            }else{
                TaxiCar taxi = vaz.CreateTaxi(driver,passengers,childseat);
                return taxi;
            }

        }

        public void StartBus(BusCar car){
            bool adults = false;
            foreach(Passenger passenger in car.passengers){
                if (String.Compare(passenger.category,"adult")==0 | String.Compare(passenger.category,"disabled")==0){
                    adults = true;
                }
            }

            if(String.Compare(car.driver.license,"Bus")!=0){
                Console.WriteLine("The driver has no license for this transport");
            }else if(car.driver == null){
                Console.WriteLine("No driver");
            }else if(car.passengers.Count == 0){
                Console.WriteLine("No passengers");
            }else if(car.passengers.Count > BusCar.PassengerLimit){
                Console.WriteLine("Too many passengers: ",car.passengers.Count);
            }else if(adults == false){
                Console.WriteLine("Children cannot go without adults");
            }else{
                car.info();
                car.driver.info();
                for(int i = 0; i < car.passengers.Count;i++){
                    car.passengers[i].info();
                }
                Console.WriteLine("Good drive");
            }
        }
        public void StartTaxi(TaxiCar car){
            bool adults = false;
            foreach(Passenger passenger in car.passengers){
                if (String.Compare(passenger.category,"adult")==0 | String.Compare(passenger.category,"disabled")==0){
                    adults = true;
                }
            }
            bool children = false;
            foreach(Passenger passenger in car.passengers){
                if (String.Compare(passenger.category,"child")==0){
                    children = true;
                }
            }
            
            if(String.Compare(car.driver.license,"Taxi")!=0){
                Console.WriteLine("The driver has no license for this transport");
            }else if(car.driver == null){
                Console.WriteLine("No driver");
            }else if(car.passengers.Count == 0){
                Console.WriteLine("No passengers");
            }else if(car.passengers.Count > TaxiCar.PassengerLimit){
                Console.WriteLine("Too many passengers: ",car.passengers.Count);
            }else if(adults == false){
                Console.WriteLine("Children cannot go without adults");
            }else if(children&car.childseat==false){
                Console.WriteLine("No childseat");
            }else{
                car.info();
                car.driver.info();
                for(int i = 0; i < car.passengers.Count;i++){
                    car.passengers[i].info();
                }
                Console.WriteLine("Good drive");
            }                
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Mosgortrans company = new Mosgortrans();
            Avtovaz vaz = new Avtovaz();
            List<Passenger> list_short = new List<Passenger>(){new Passenger("Alice","adult"),new Passenger("Jenny","child")};            
            List<Passenger> list_children = new List<Passenger>(){new Passenger("Jenny","child")};
            BusDriver busdriver = company.HireBusDriver();
            TaxiDriver taxidriver = company.HireTaxiDriver();
            BusCar Bus1 = company.CreateAndFillBus(vaz,busdriver,list_short);
            TaxiCar Taxi1 = company.CreateAndFillTaxi(vaz,busdriver,list_short,true);
            TaxiCar Taxi2 = company.CreateAndFillTaxi(vaz,taxidriver,list_children,true);
            TaxiCar Taxi3 = company.CreateAndFillTaxi(vaz,taxidriver,list_short,false);
            TaxiCar Taxi4 = company.CreateAndFillTaxi(vaz,taxidriver);
            company.StartBus(Bus1);
            company.StartTaxi(Taxi1);
            company.StartTaxi(Taxi2);          
            company.StartTaxi(Taxi3);          
            company.StartTaxi(Taxi4);





        }
    }
}
