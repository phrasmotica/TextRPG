using VideoGameExperiments.Items;

namespace TextRPG.Items
{
    public class Key : IItem
    {
        public int Id => 4;

        public string Name => "key";

        public int MaxStackSize => 1;
    }
}
