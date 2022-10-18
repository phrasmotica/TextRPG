using System;
using System.Collections.Generic;
using UnityEngine;
using VideoGameExperiments.Items;

namespace TextRPG
{
    /// <summary>
    /// Script for creating new items.
    /// </summary>
    public class ItemFactory : MonoBehaviour
    {
        public List<Sprite> ItemSprites;

        public event Action<IItem[]> OnCreate;

        public void Create(int id)
        {
            var item = CreateItem(id);

            var newItems = new List<IItem>
            {
                item,
            };

            OnCreate?.Invoke(newItems.ToArray());
        }

        public Sprite GetSprite(IItem item)
        {
            var spriteIndex = item.Id - 1;
            return ItemSprites[spriteIndex];
        }

        private static IItem CreateItem(int id) => id switch
        {
            1 => new Potion(),
            2 => new Coin(),
            3 => new Sword(),
            _ => throw new NotImplementedException(),
        };
    }
}
