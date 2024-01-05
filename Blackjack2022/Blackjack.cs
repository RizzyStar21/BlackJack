using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack2022
{
    class Blackjack
    {
        public List<Card> shoe = new List<Card>();
        public List<Card> playerDeck = new List<Card>();
        public List<Card> dealerDeck = new List<Card>();

        public void GiveToPlayer()
        {
            DealTo(playerDeck);
        }
        public void GiveToDealer()
        {
            DealTo(dealerDeck);
        }
        private void DealTo(List<Card> deck)
        {
            if (shoe.Count < 1)
            {
                throw new Exception("Trying to deal from empty shoe");
            }

            Card c = shoe[0];
            deck.Add(c);
            shoe.RemoveAt(0);
        }
        private void ClearDeal(List<Card> deck)
        {
            deck.Clear();
            playerDeck.Clear();
            dealerDeck.Clear();
        }

        public void Clear()
        {
            ClearDeal(shoe);
            InitShoe();
        }
        public void InitShoe()
        {
            string[] suits = new string[] { "Hearts", "Diamonds", "Clubs", "Spades" };

            // Create 52 cards and place them into the shoe/dispenser
            for (int k = 0; k < 4; k++)
            {
                for (int i = 1; i < 14; i++)
                {
                    Card c = new Card();
                    c.Suit = suits[k];
                    c.Rank = i;
                    shoe.Add(c);
                }
            }

            Shuffle();
        }

        public void Shuffle()
        {
            Random rand = new Random();

            for(int i = 0; i < shoe.Count; i++)
            {
                int index = rand.Next(51);
                Card card = shoe[index];
                shoe[index] = shoe[i];
                shoe[i] = card;
            }
        }

        public void Start()
        {
            GiveToPlayer();
            GiveToDealer();
            GiveToPlayer();
            GiveToDealer();
        }

        public int Hit()
        {
            GiveToPlayer();

            if(GetPlayerSum() > 21)
            {
                return -1;
            }

            return 0;
        }

        public int Stand() {

            GiveToDealer();
            if (GetDealerSum() < GetPlayerSum() || GetDealerSum() > 21) { 
                return -1;
            }
            return 0;
        }

        public int GetPlayerSum()
        {
            int sum = 0;
            foreach(Card c in playerDeck)
            {
                sum += c.GetBlackjackValue();  // Naive
            }

            return sum;
        }
        public int GetDealerSum()
        {
            int sum = 0;
            foreach (Card c in dealerDeck)
            {
                sum += c.GetBlackjackValue();  // Naive
            }

            return sum;
        }
    }
}
