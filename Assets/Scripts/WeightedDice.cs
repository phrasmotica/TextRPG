using System;
using System.Collections.Generic;
using System.Linq;

namespace TextRPG
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

        public static WeightedDice Fair(params int[] values)
        {
            return new WeightedDice(values.Select(v => new Side(v, 1)).ToList());
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