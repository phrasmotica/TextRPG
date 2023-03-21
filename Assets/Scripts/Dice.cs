using Dice;

namespace Assets.Scripts
{
    public class Dice
    {
        public static RollResult RollD6() => Roller.Roll("1d6");
    }
}
