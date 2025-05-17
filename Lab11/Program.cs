using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection;

interface IObserver
{
    void Update(Object ob);
}

interface IObservable
{
    void RegisterObserver(IObserver o);
    void RemoveObserver(IObserver o);
    void NotifyObservers();
}

class Assortiment
{
    public Dictionary<string, int> products;
    public Assortiment()
    {
        products = new Dictionary<string, int>();
        Random random = new Random();
        products.Add("bread", 0);
        products.Add("cabbage", 0);
        products.Add("carrot", 0);
        products.Add("champoo", 0);
        products.Add("eggs", 0);
        products.Add("potato", 0);
        products.Add("sausages", 0);
        products.Add("soap", 0);
    }
    public void UpdateAssortiment()
    {
        Random random = new Random();
        products["bread"] = random.Next(20, 40);
        products["cabbage"] = random.Next(20, 30);
        products["carrot"] = random.Next(30, 50);
        products["champoo"] = random.Next(100, 130);
        products["eggs"] = random.Next(90, 120);
        products["potato"] = random.Next(60, 100);
        products["sausages"] = random.Next(160, 250);
        products["soap"] = random.Next(70, 90);
    }
}

class YandexMarket : IObservable
{
    Assortiment assortiment;
    List<IObserver> customers;
    public YandexMarket()
    {
        assortiment = new Assortiment();
        customers = new List<IObserver>();
    }

    public void RegisterObserver(IObserver observer)
    {
        customers.Add(observer);
    }
    public void RemoveObserver(IObserver observer)
    {
        customers.Remove(observer);
    }
    public void NotifyObservers()
    {
        foreach (IObserver customer in customers)
        {
            customer.Update(assortiment);
        }
    }
    public void UpdateAssortiment()
    {
        assortiment.UpdateAssortiment();
        NotifyObservers();
    }
}

class SaleHunter : IObserver
{
    public string Name { get; set; }
    Assortiment previous_assortiment;
    public SaleHunter(string name)
    {
        previous_assortiment = new Assortiment();
        Name = name;
    }
    public void Update(object assortiment)
    {
        Assortiment new_assortiment = (Assortiment)assortiment;
        string response = Name + " says: Today are sales: \n";
        foreach (var product in new_assortiment.products)
        {
            if (product.Value < previous_assortiment.products[product.Key])
            {
                int sale = previous_assortiment.products[product.Key] - product.Value;
                response += "The " + product.Key + " is " + sale.ToString() + " cheaper than yesterday\n";
            }
        }
        Console.WriteLine(response);
        foreach (var product in new_assortiment.products)
        {
            previous_assortiment.products[product.Key] = product.Value;
        }
    }
}

class Student : IObserver
{
    public string Name { get; set; }
    int Budget { get; set; }
    public Student(string name, int budget)
    {
        Name = name;
        Budget = budget;
    }
    public void Update(object assortiment)
    {
        Assortiment assort = (Assortiment)assortiment;
        if (assort.products["bread"] + assort.products["sausages"] + assort.products["eggs"] > Budget)
        {
            Console.WriteLine(Name + " says: Today I'm not eating(((\n");
        }
        else
        {
            Console.WriteLine(Name + " says: Today I'm eating)))\n");
        }
    }
}

class Cook : IObserver
{
    public string Name { get; set; }

    public Cook(string name)
    {
        Name = name;
    }
    public void Update(object assortiment)
    {
        Assortiment assort = (Assortiment)assortiment;
        int cost = 0;
        cost += assort.products["potato"];
        cost += assort.products["cabbage"];
        cost += assort.products["carrot"];
        cost += assort.products["eggs"];
        Console.WriteLine(Name + " says: today my products cost " + cost.ToString());
    }
}

class Program()
{
    static void Main(string[] args)
    {
        YandexMarket market = new YandexMarket();
        Student maks = new Student("Max", 350);
        Cook bree = new Cook("Bree");
        SaleHunter katya = new SaleHunter("Katya");
        market.RegisterObserver(maks);
        market.RegisterObserver(bree);
        market.RegisterObserver(katya);
        market.UpdateAssortiment();
        market.UpdateAssortiment();
    }
}
