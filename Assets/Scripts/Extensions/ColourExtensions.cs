using UnityEngine;

namespace TextRPG.Extensions
{
    public static class ColourExtensions
    {
        public static Color WithAlpha(this Color colour, float alpha)
        {
            return new Color(colour.r, colour.g, colour.b, alpha);
        }
    }
}
