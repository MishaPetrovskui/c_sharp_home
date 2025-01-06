using System;
using System.Text;
using System.Threading;
using Game;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Console.InputEncoding = UTF8Encoding.UTF8;
        CreditCard firstCard = new CreditCard("1234567812345678", "first", "123", "12", "2026", 1500.0);
        CreditCard secondCard = new CreditCard("8765432187654321", "second", "321", "01", "2027", 500.0);

        firstCard.Print();
        Console.WriteLine();
        secondCard.Print();

        int currentMonth = 1;
        int currentYear = 2025;

        if (firstCard.IsCardValid(currentMonth, currentYear) && secondCard.IsCardValid(currentMonth, currentYear))
        {
            Console.WriteLine("first and second card is valid.");
        }
        else
        {
            Console.WriteLine("cards is expired.");
        }

        double forwardAmount = 500.0;
        Console.WriteLine($"\nAttempting to forward {forwardAmount} uah...");
        bool forwardSuccess = firstCard.forwardMoney(secondCard, forwardAmount);

        if (forwardSuccess)
        {
            Console.WriteLine("\nForward successful!");
        }

        firstCard.Print();
        Console.WriteLine();
        secondCard.Print();

    }
}


//drugoe


namespace Game
{
    class CreditCard
    {
        public string CardNumber;
        public string SNM;
        public string CVV;
        public string Month;
        public string Year;
        public double Balance;

        public CreditCard(string cardNumber, string SNM, string cvv, string Month, string Year, double balance)
        {
            this.CardNumber = cardNumber;
            this.SNM = SNM;
            this.CVV = cvv;
            this.Month = Month;
            this.Year = Year;
            this.Balance = balance;
        }

        public CreditCard() : this("0000000000000000", "Unknown", "000", "01", "2026", 1000.0) { }

        public string GetCardNumber()
        {
            return this.CardNumber;
        }

        public string GetOwnerName()
        {
            return this.SNM;
        }

        public string GetCVV()
        {
            return this.CVV;
        }

        public string GetExpiryMonth()
        {
            return this.Month;
        }

        public string GetExpiryYear()
        {
            return this.Year;
        }

        public double GetBalance()
        {
            return this.Balance;
        }

        public void SetCardNumber(string cardNumber)
        {
            this.CardNumber = cardNumber;
        }

        public void SetOwnerName(string SNM)
        {
            this.SNM = SNM;
        }

        public void SetCVV(string cvv)
        {
            this.CVV = cvv;
        }

        public void SetExpiryMonth(string Month)
        {
            this.Month = Month;
        }

        public void SetExpiryYear(string Year)
        {
            this.Year = Year;
        }

        public void SetBalance(double balance)
        {
            this.Balance = balance;
        }


        public void Print()
        {
            Console.WriteLine($"-< {this.SNM} >-");
            Console.WriteLine($"Card Number: {this.CardNumber}");
            Console.WriteLine($"CVV: {this.CVV}");
            Console.WriteLine($"Expiration Date: {this.Month}/{this.Year}");
            Console.WriteLine($"Balance: {this.Balance} uah");
        }

        public bool IsCardValid(int currentMonth, int currentYear)
        {
            int expirationMonth = int.Parse(this.Month);
            int expirationYear = int.Parse(this.Year);

            if (expirationYear > currentYear)
            {
                return true;
            }
            else if (expirationYear == currentYear && expirationMonth >= currentMonth)
            {
                return true;
            }

            return false;
        }

        public void AddBalance(double value)
        {
            this.Balance += value;
            Console.WriteLine($"Balance of card {this.CardNumber} credited with {value} uah.");
        }
        public bool forwardMoney(CreditCard anotherCard, double value)
        {
            if (this.Balance >= value)
            {
                this.Balance -= value;
                anotherCard.AddBalance(value);
                Console.WriteLine($"Forward {value} uah from card {this.CardNumber} to card {anotherCard.CardNumber}.");
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient balance for the forward.");
                return false;
            }
        }
    }
}
