using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameKit.CardSystem
{

    public class Deck<T>
    {
        public IList<T> Cards => cards.AsReadOnly();
        public IList<T> DrawPile => drawPile.AsReadOnly();
        public IList<T> DiscardPile => discardPile.AsReadOnly();
        public IList<T> Hand => hand.AsReadOnly();

        public Action<T> OnDrawCard { get; set; } = delegate { };
        public Action<T> OnDiscardCard { get; set; } = delegate { };

        List<T> cards = new();
        List<T> drawPile = new();
        List<T> discardPile = new();
        List<T> hand = new();

        /// <summary>
        /// Adds cards to the deck.
        /// </summary>
        /// <param name="cards"></param>
        public void AddCards(IEnumerable<T> cards)
        {
            foreach (var card in cards)
            {
                AddCard(card);
            }
        }

        /// <summary>
        /// Adds a card to the deck.
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(T card)
        {
            cards.Add(card);
        }

        /// <summary>
        /// Removes a card from the deck.
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCard(T card)
        {
            cards.Remove(card);
            drawPile.Remove(card);
            discardPile.Remove(card);
            hand.Remove(card);
        }

        /// <summary>
        /// Shuffles the deck, and resets the hand and piles.
        /// </summary>
        public void Shuffle()
        {
            hand.Clear();
            discardPile.Clear();
            cards.Shuffle();
            drawPile = new List<T>(cards);
        }

        /// <summary>
        /// Draws cards, and reshuffles the draw pile if there are not enough cards.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public List<T> DrawCards(int num)
        {
            var drawn = new List<T>();
            int toDraw = num;

            while (toDraw > 0)
            {
                if (drawPile.Count == 0)
                {
                    if (discardPile.Count == 0)
                        break;

                    drawPile.AddRange(discardPile);
                    discardPile.Clear();
                    drawPile.Shuffle();
                }

                var card = drawPile[0];
                drawPile.RemoveAt(0);
                hand.Add(card);
                drawn.Add(card);

                OnDrawCard(card);
                toDraw--;
            }

            return drawn;
        }

        /// <summary>
        /// Discards all cards in the hand.
        /// </summary>
        /// <returns></returns>
        public List<T> DiscardHand()
        {
            var discarded = new List<T>();

            for (int i = hand.Count - 1; i >= 0; i--)
            {
                var card = hand[i];
                DiscardCard(card);
                discarded.Add(card);
            }

            return discarded;
        }

        /// <summary>
        /// Discards a card in the hand.
        /// </summary>
        /// <param name="card"></param>
        public void DiscardCard(T card)
        {
            if (!hand.Contains(card)) return;
            discardPile.Add(card);
            hand.Remove(card);
            OnDiscardCard(card);
        }
    }
}