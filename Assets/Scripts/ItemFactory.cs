using System;
using System.Collections.Generic;
using TextRPG.Items;
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

        public event Action<IItem[]> OnCollect;

        public void Collect(int id)
        {
            var newItems = new List<IItem>
            {
                CreateItem(id),
            };

            OnCollect?.Invoke(newItems.ToArray());
        }

        public Sprite GetSprite(IItem item)
        {
            var spriteIndex = item.Id - 1;
            return ItemSprites[spriteIndex];
        }

        public static IItem CreateItem(int id) => id switch
        {
            1 => new Potion(),
            2 => new Coin(),
            3 => new Sword(),
            4 => new Key(),
            _ => throw new NotImplementedException(),
        };
    }
}
