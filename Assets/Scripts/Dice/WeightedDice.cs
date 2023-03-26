using System;
using System.Collections.Generic;
using System.Linq;

namespace TextRPG.Dice
{
    public class WeightedDice
    {
        public List<Side> Sides { get; }

        public WeightedDice(List<Side> sides)
        {
            if (!sides.Any())
            {
                throw new InvalidOperationException();
            }

            Sides = sides;
        }

        public int Roll(Func<int, int, int> random)
        {
            var weightSum = Sides.Select(s => s.Weight).Sum();

            var r = random(0, weightSum);

            var sideIndex = 0;
            var currentSum = 0;

            // if r is in the range between currentSum and currentSum + <next weight>,
            // we've rolled this side
            while (r > currentSum)
            {
                sideIndex++;
                currentSum += Sides[sideIndex].Weight;
            }

            return Sides[sideIndex].Value;
        }

        public float GetProbability(int value)
        {
            var side = Sides.FirstOrDefault(s => s.Value == value);
            if (side is null)
            {
                return 0;
            }

            return (float) side.Weight / Sides.Select(s => s.Weight).Sum();
        }

        /// <summary>
        /// Creates a D6 where each side is equally likely to appear.
        /// </summary>
        public static WeightedDice FairD6()
        {
            var sides = Enumerable.Range(1, 6).Select(v => new Side(v, 1)).ToList();
            return new WeightedDice(sides);
        }

        /// <summary>
        /// Creates a D6 where the high values are twice as likely to appear as the low values.
        /// </summary>
        public static WeightedDice FavourableD6()
        {
            var sides = Enumerable.Range(1, 6).Select(v => new Side(v, v > 3 ? 2 : 1)).ToList();
            return new WeightedDice(sides);
        }

        // TODO: add builder methods
    }

    public class Side
    {
        public int Value { get; }

        public int Weight { get; }

        public Side(int value, int weight)
        {
            Value = value;
            Weight= weight;
        }
    }
}