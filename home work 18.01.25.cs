using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAME;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        List<Pizza> menu = new List<Pizza>
        {
            new Pizza("Vegeteriana", GAME.Size.Small, 149.0),
            new Pizza("Vegeteriana", GAME.Size.Medium, 199.0),
            new Pizza("Vegeteriana", GAME.Size.Large, 249.0),
            new Pizza("Stagioni", GAME.Size.Small, 119.0),
            new Pizza("Stagioni", GAME.Size.Medium, 188.0),
            new Pizza("Stagioni", GAME.Size.Large, 416.0),
            new Pizza("Frutti", GAME.Size.Small, 300.0),
            new Pizza("Frutti", GAME.Size.Medium, 349.0),
            new Pizza("Frutti", GAME.Size.Large, 399.0),
            new Pizza("Romana", GAME.Size.Small, 139.0),
            new Pizza("Romana", GAME.Size.Medium, 220.0),
            new Pizza("Romana", GAME.Size.Large, 299.0)
        };

        Console.WriteLine("Введіть інформацію про клієнта:");
        Console.Write("Ім'я: ");
        string customerName = Console.ReadLine();
        Console.Write("Телефон: ");
        string phoneNumber = Console.ReadLine();
        Console.Write("Адреса: ");
        string address = Console.ReadLine();

        Customer customer = new Customer(customerName, phoneNumber, address);
        Order order = new Order(customer);

        while (true)
        {
            Console.WriteLine("Меню піц:");
            for (int i = 0; i < menu.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menu[i].GetDescription()}");
            }
            Console.Write("Оберіть номер піци або 0 для завершення: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 0) break;

            if (choice > 0 && choice <= menu.Count)
            {
                order.AddPizza(menu[choice - 1]);
                Console.WriteLine("Піцу додано до замовлення!");
            }
            else
            {
                Console.WriteLine("Невірний вибір.");
            }
        }

        Console.WriteLine("\nІнформація про замовлення:");
        order.PrintOrderDetails();
    }
}




//drugoe



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
/*using GAME.spells;*/
using System.Diagnostics;
using System.Drawing;

namespace GAME
{
    enum Size
    {
        Small, Medium, Large
    }

    class Pizza
    {
        public string? name;
        public Size pizzaSize;
        public double? pizzaPrice;

        public string? Name { get { return name; } set { this.name = value; } }

        public Size Size
        {
            get { return pizzaSize; }
        }

        public double? PizzaPrice
        {
            get { return pizzaPrice; }
        }

        public Pizza() : this("None", Size.Small, 89) { }

        public Pizza(string? name, Size pizzaSize, double? pizzaPrice)
        {
            this.name = name;
            this.pizzaSize = pizzaSize;
            this.pizzaPrice = pizzaPrice;
        }

        public string GetDescription()
        {
            return $"{this.name} - {this.pizzaSize} - {this.pizzaPrice} uah";
        }
    }
    class Customer
    {
        public string? name;
        public string phoneNumber;
        public string? addres;

        public string? Name { get { return name; } set { this.name = value; } }

        public string PhoneNumber
        {
            get { return phoneNumber; }
        }

        public string? Addres
        {
            get { return addres; }
        }

        public Customer() : this("None", " ", "none") { }

        public Customer(string? name, string phoneNumber, string? addres)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.addres = addres;
        }

        public string GetCustomerInfo()
        {
            return $"{this.name} - +380 {this.phoneNumber} - {this.addres}";
        }
    }

    class Order
    {
        public Customer Customer { get; set; }
        public List<Pizza> Pizzas { get; set; }
        public double? totalPrice { get; set; }

        public Order(Customer customer)
        {
            Customer = customer;
            Pizzas = new List<Pizza>();
        }

        public void AddPizza(Pizza pizza)
        {
            this.Pizzas.Add(pizza);
            CalculateTotalPrice();
        }

        public void CalculateTotalPrice()
        {
            totalPrice = 0;
            foreach (var pizza in Pizzas)
            {
                totalPrice += pizza.PizzaPrice;
            }
        }

        public void PrintOrderDetails()
        {
            Console.WriteLine(Customer.GetCustomerInfo());
            Console.WriteLine("Замовлені піци:");
            foreach (var pizza in Pizzas)
            {
                Console.WriteLine($"- {pizza.GetDescription()}");
            }
            Console.WriteLine($"Загальна сума: {totalPrice} грн");
        }
    }
}
