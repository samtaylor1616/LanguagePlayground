﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqFaroShuffle
{
    class Program
    {
        static void Main(string[] args)
        {
            var startingDeck = (from s in Suits().LogQuery("Suit Generation")
                                from r in Ranks().LogQuery("Value Generation")
                                select new { Suit = s, Rank = r })
                                .LogQuery("Starting Deck")
                                .ToArray();

            foreach (var card in startingDeck)
                Console.WriteLine(card);

            Console.WriteLine();

            var times = 0;
            var shuffle = startingDeck;

            do
            {
                // Out shuffle
                /*
                shuffle = shuffle.Take(26)
                    .LogQuery("Top Half")
                    .InterleaveSequenceWith(shuffle.Skip(26).LogQuery("Bottom Half"))
                    .LogQuery("Shuffle")
                    .ToArray();
                */

                // In shuffle
                shuffle = shuffle.Skip(26)
                            .LogQuery("Bottom Half")
                            .InterleaveSequenceWith(shuffle.Take(26).LogQuery("Top Half"))
                            .LogQuery("Shuffle")
                            .ToArray();

                foreach (var card in shuffle)
                    Console.WriteLine(card);

                Console.WriteLine();
                times++;
            } while (!startingDeck.SequenceEquals(shuffle));

            Console.WriteLine($"It took {times} number of shuffles to get back to the original sequence\n");
        }

        static IEnumerable<string> Suits()
        {
            yield return "clubs";
            yield return "diamonds";
            yield return "hearts";
            yield return "spades";
        }

        static IEnumerable<string> Ranks()
        {
            yield return "two";
            yield return "three";
            yield return "four";
            yield return "five";
            yield return "six";
            yield return "seven";
            yield return "eight";
            yield return "nine";
            yield return "ten";
            yield return "jack";
            yield return "queen";
            yield return "king";
            yield return "ace";
        }
    }
}
