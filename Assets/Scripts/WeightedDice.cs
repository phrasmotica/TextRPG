using System;
using System.Collections.Generic;
using System.Linq;

namespace TextRPG
{
    public class WeightedDice
    {
        private readonly List<Side> _sides;

        public WeightedDice(List<Side> sides)
        {
            if (!sides.Any())
            {
                throw new InvalidOperationException();
            }

            _sides = sides;
        }

        public int Roll(Func<int, int, int> random)
        {
            var weightSum = _sides.Select(s => s.Weight).Sum();

            var r = random(0, weightSum);

            var sideIndex = 0;
            var currentSum = 0;

            // if r is in the range between currentSum and currentSum + <next weight>,
            // we've rolled this side
            while (r > currentSum)
            {
                sideIndex++;
                currentSum += _sides[sideIndex].Weight;
            }

            return _sides[sideIndex].Value;
        }

        public float GetProbability(int value)
        {
            var side = _sides.FirstOrDefault(s => s.Value == value);
            if (side is null)
            {
                return 0;
            }

            return (float) side.Weight / _sides.Select(s => s.Weight).Sum();
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