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
        private readonly Dictionary<KeyCode, int> _itemMap = new()
        {
            [KeyCode.Alpha1] = 1,
            [KeyCode.Alpha2] = 2,
            [KeyCode.Alpha3] = 3,
        };

        public List<Sprite> ItemSprites;

        public event Action<IItem[]> OnCollect;

        private void Update()
        {
            CollectNewItems();
        }

        public Sprite GetSprite(IItem item)
        {
            var spriteIndex = item.Id - 1;
            return ItemSprites[spriteIndex];
        }

        private void CollectNewItems()
        {
            foreach (var (keyCode, id) in _itemMap)
            {
                if (Input.GetKeyUp(keyCode))
                {
                    var item = CreateItem(id);

                    var newItems = new List<IItem>
                    {
                        item,
                    };

                    // hold shift to add an entire stack
                    var additionalCount = Input.GetKey(KeyCode.LeftShift) ? item.MaxStackSize - 1 : 0;

                    for (var i = 0; i < additionalCount; i++)
                    {
                        newItems.Add(CreateItem(id));
                    }

                    OnCollect?.Invoke(newItems.ToArray());
                }
            }
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
