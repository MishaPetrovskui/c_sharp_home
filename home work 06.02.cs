using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeckofCards;
using System.Net.Http;
using System.Text.Json;

namespace Deck
{
    class Program
    {

        string[] cardss = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "JACK", "QUEEN", "KING", "ACE" };
        

        static void bubble_sort(int[] array)
        {
            bool is_changed = true;
            int a;
            while (is_changed)
            {
                is_changed = false;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        a = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = a;
                        is_changed = true;
                    }
                }
            }
        }

        public static bool Pair(cardes listOfCards)
        {
            for (int i = 0; i < listOfCards.cards.Count() - 1; i++)
                for (int j = i + 1; j < listOfCards.cards.Count(); j++)
                    if (listOfCards.cards[i].value == listOfCards.cards[j].value)
                        return true;
            return false;
        }

        public static bool TwoPair(cardes listOfCards)
        {
            int pairs = 0;
            for (int i = 0; i < listOfCards.cards.Count() - 1; i++)
                for (int j = i + 1; j < listOfCards.cards.Count(); j++)
                    if (listOfCards.cards[i].value == listOfCards.cards[j].value)
                        pairs++;
                        if (pairs == 2) 
                            return true;
            return false;
        }

        public static bool Set(cardes listOfCards)
        {
            for (int i = 0; i < listOfCards.cards.Count() - 2; i++)
                for (int j = i + 1; j < listOfCards.cards.Count(); j++)
                    if (listOfCards.cards[i].value == listOfCards.cards[j].value)
                        for (int h = j + 1; h < listOfCards.cards.Count(); h++)
                            if (listOfCards.cards[j].value == listOfCards.cards[h].value)
                                return true;
            
            return false;
        }
        public static bool Square(cardes listOfCards){
            for (int i = 0; i < listOfCards.cards.Count() - 3; i++)
                for (int j = i + 1; j < listOfCards.cards.Count(); j++)
                    if (listOfCards.cards[i].value == listOfCards.cards[j].value)
                        for (int h = j + 1; h < listOfCards.cards.Count(); h++)
                            if (listOfCards.cards[j].value == listOfCards.cards[h].value)
                                for (int n = h + 1; n < listOfCards.cards.Count(); n++ )
                                    if (listOfCards.cards[h].value == listOfCards.cards[n].value)
                                        return true;
            
            return false;
        }

        public static bool Straight(cardes listOfCards)
        {
            var mc = new Program();
            int[] cd = new int[listOfCards.cards.Count()]; 
            for (int i = 0; i < listOfCards.cards.Count(); i++)
                cd[i] = 0;
            for (int i = 0; i < listOfCards.cards.Count(); i++)
            {
                for (int j = 0; j < mc.cardss.Length; j++)
                {
                    if (mc.cardss[j] == listOfCards.cards[i].value)
                        cd[i] = j;
                }
            }
            bubble_sort(cd);
            for (int i = 0; i < listOfCards.cards.Count() - 1; i++)
            {
                if (cd[i + 1] - cd[i] != 1)
                    return false;
            }
            return true;
        }

        public static bool Flash(cardes listOfCards){
            for (int i = 0; i < listOfCards.cards.Count() - 1; i++)
            {
                if (listOfCards.cards[i].suit != listOfCards.cards[i + 1].suit)
                    return false;
            }
            return true;
        }

        public static bool StritFlash(cardes listOfCards){
            return Straight(listOfCards) && Flash(listOfCards);
        }

        public static bool FlashRoyal(cardes listOfCards)
        {
            if (!Flash(listOfCards))
                return false;
            int found = 0;

            foreach (var card in listOfCards.cards)
            {
                if (card.value == "10" || card.value == "JACK" || card.value == "QUEEN" || card.value == "KING" || card.value == "ACE")
                    found++;
            }
            return found == 5;
        }

        public static bool FullHouse(cardes listOfCards)
        {
            var mc = new Program();
            int[] a = new int[mc.cardss.Length];
            foreach (var card in listOfCards.cards)
            {
                for (int i = 0; i < mc.cardss.Length; i++)
                {
                    if (card.value == mc.cardss[i])
                    {
                        a[i]++;
                        break;
                    }
                }
            }
            bool first = false;
            bool second = false;

            for (int i = 0; i < a.Length;i++)
            {
                if (a[i] == 3)
                    first = true;
                if (a[i] == 2)
                    second = true;
            }
            return first && second;
        }

        public static async Task Main(string[] args)
        {
            deck res = new deck(1);
            Console.WriteLine(res.deck_id);
            Console.WriteLine(res.remaining);
            Console.WriteLine(res.cards);
            int r = 5;
            cardes a = new cardes();
            a = await deck.card(r, res.deck_id);
            res = new deck(res.deck_id);

            if (a == null)
                Console.WriteLine("error");

            else foreach (var card in a.cards)
                {
                    card.print();
                    Console.WriteLine();
                }
            Console.WriteLine($"Pair \t\t{Pair(a)}");
            Console.WriteLine($"Two Pair \t{TwoPair(a)}");
            Console.WriteLine($"Set \t\t{Set(a)}");
            Console.WriteLine($"Square \t\t{Square(a)}");
            Console.WriteLine($"Straight \t{Straight(a)}");
            Console.WriteLine($"Flash \t\t{Flash(a)}");
            Console.WriteLine($"FullHouse \t{FullHouse(a)}");
            Console.WriteLine($"StritFlash \t{StritFlash(a)}");
            Console.WriteLine($"FlashRoyal \t{FlashRoyal(a)}");
            Console.WriteLine();
            Console.WriteLine(res.remaining);
        }
    }

}




//drugoe



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Deck
{
    enum suits
    {
        Hearts,
        Clubs,
        Spades,
        Diamonds
    }

    class deckResponse
    {
        public string deck_id { get; set; }
        public int remaining { get; set; }
        public bool shuffled { get; set; }
    }

    class deck
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public bool shuffled { get; set; }
        public List<card> cards { get; set; }
        public int remaining { get; set; }

        public deck(string deck_id)
        {
            string url = $"https://deckofcardsapi.com/api/deck/{deck_id}/";
            deckResponse deck = deckRequest(url);
            this.deck_id = deck_id;
            this.remaining = deck.remaining;
            this.shuffled = deck.shuffled;
        }

        public deck(int deckCount = 1)
        {
            var client = new HttpClient();
            string url = "https://deckofcardsapi.com/api/deck/new/shuffle/";
            string parameters = $"?deck_count={deckCount}";
            var deck = deckRequest(url + parameters);
            this.deck_id = deck.deck_id;
            this.remaining = deck.remaining;
            this.shuffled = deck.shuffled;
        }
        private deckResponse deckRequest(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var JsonResponse = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<deckResponse>(JsonResponse);
            }
            return null;
        }

        public async Task<deck> ReShuffle()
        {
            var client = new HttpClient();
            string url = $"https://deckofcardsapi.com/api/deck/{this.deck_id}/shuffle/?remaining=true";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var JsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<deck>(JsonResponse);
            }
            return null;
        }
        public static async Task<cardes> card(int count, string deck_id)
        {
            var client = new HttpClient();
            string url = $"https://deckofcardsapi.com/api/deck/{deck_id}/draw/?count={count}";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var JsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<cardes>(JsonResponse);
            }
            return null;
        }
        public async Task<deck> CardBack()
        {
            var client = new HttpClient();
            string url = $"https://deckofcardsapi.com/api/deck/{this.deck_id}/shuffle/";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var JsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<deck>(JsonResponse);

            }
            return null;
        }


    }
    class cardes
    {
        public List<card> cards { get; set; }
    }
    class card
    {
        public string code { get; set; }
        public string value { get; set; }
        public string suit { get; set; }

        public override string ToString()
        {
            string a = "", b = "";
            if (this.suit == "HEARTS")
                a = "♥";
            else if (this.suit == "CLUBS")
                a = "♣";
            else if (this.suit == "DIAMONDS")
                a = "♦";
            else if (this.suit == "SPADES")
                a = "♠";
            if (value == "0" || value == "10")
                b = "1";
            return $"{b}{code}\b{a}";
        }
        public void print()
        {
            string a = "", b = "";
            if (this.suit == "HEARTS")
                a = "♥";
            else if (this.suit == "CLUBS")
                a = "♣";
            else if (this.suit == "DIAMONDS")
                a = "♦";
            else if (this.suit == "SPADES")
                a = "♠";
            if (value == "0" || value == "10")
                b = "1";
            if (this.suit == "HEARTS" || this.suit == "CLUBS")
            { Console.ForegroundColor = ConsoleColor.Red; Console.BackgroundColor = ConsoleColor.White; }
            else { Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White; }
            Console.Write($"{b}{code}\b{a}");
            Console.ResetColor();
        }

    }
}
