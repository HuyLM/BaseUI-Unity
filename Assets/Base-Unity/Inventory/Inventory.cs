using Ftech.Lib.Common;
using Ftech.Lib.Common.UnityInspector.Editor;
using System.Collections.Generic;
using UnityEngine;


namespace Ftech.Lib.InventorySystem
{
    [CreateAssetMenu(fileName = "NewInventory", menuName = "Data/Item/Inventory")]
    public class Inventory : SingletonScriptableObject<Inventory>
    {

        [System.Serializable]
        private class SaveDataModel
        {
            public ItemSlot[] i;
            public int[] ii;

            public SaveDataModel(int capacity, int capacityInfinity)
            {
                i = new ItemSlot[capacity];
                ii = new int[capacityInfinity];
            }
        }

        private readonly Dictionary<int, ItemSlot> itemDictionary = new Dictionary<int, ItemSlot>();
        [ItemField] private List<int> infiniteItemIds = new List<int>();


        public IEnumerable<ItemSlot> GetAllItem()
        {
            return itemDictionary.Values;
        }

        public void AddInitialize(params ItemSlot[] items)
        {
            foreach (ItemSlot item in items)
            {
                if (ItemDatabase.Constains(item.Id))
                {
                    int amount = item.Amount;
                    if (itemDictionary.ContainsKey(item.Id))
                    {
                        itemDictionary[item.Id].Stack(amount);
                    }
                    else
                    {
                        itemDictionary.Add(item.Id, new ItemSlot(item.Id, amount));
                    }
                }
            }
        }

        public virtual void Add(params ItemSlot[] items)
        {
            foreach (ItemSlot item in items)
            {
                Add(item.Id, item.Amount);
            }
        }

        public virtual void Add(int id, int amount)
        {
            if (ItemDatabase.Constains(id))
            {
                if (itemDictionary.ContainsKey(id))
                {
                    itemDictionary[id].Stack(amount);
                }
                else
                {
                    itemDictionary.Add(id, new ItemSlot(id, amount));
                }
                EventDispatcher.Instance.Dispatch(new EventKey.OnInventoryChange() { ID = id });
            }
        }

        public virtual void Remove(params ItemSlot[] items)
        {
            foreach (ItemSlot item in items)
            {
                Remove(item.Id, item.Amount);
            }
        }

        public virtual void Remove(int id, int amount)
        {
            if (itemDictionary.ContainsKey(id))
            {
                itemDictionary[id].Destack(amount);
                EventDispatcher.Instance.Dispatch(new EventKey.OnInventoryChange() { ID = id });
            }
        }

        public virtual ItemSlot GetItem(int id)
        {
            if (IsInfinite(id))
            {
                return new ItemSlot(id, int.MaxValue);
            }

            if (itemDictionary.TryGetValue(id, out ItemSlot item))
            {
                return item;
            }

            return new ItemSlot(id, 0);
        }

        public string SaveToJson()
        {
            if (itemDictionary == null)
            {
                return null;
            }

            SaveDataModel saveData = new SaveDataModel(itemDictionary.Count, infiniteItemIds.Count);

            int index = 0;
            foreach (int id in itemDictionary.Keys)
            {
                saveData.i[index] = itemDictionary[id];
                index++;
            }
            for (int i = 0; i < infiniteItemIds.Count; ++i)
            {
                saveData.ii[i] = infiniteItemIds[i];
            }
            return JsonUtility.ToJson(saveData);
        }

        public void LoadFromJson(string json)
        {
            SaveDataModel saveData = null;
            if (!string.IsNullOrEmpty(json))
            {
                saveData = JsonUtility.FromJson<SaveDataModel>(json);
            }

            if (saveData == null)
            {
                return;
            }

            foreach (ItemSlot item in saveData.i)
            {
                AddInitialize(item);
            }

            for (int i = 0; i < saveData.ii.Length; ++i)
            {
                infiniteItemIds[i] = saveData.ii[i];
            }
        }

        private bool IsInfinite(int id)
        {
            if (infiniteItemIds != null)
            {
                return infiniteItemIds.Contains(id);
            }
            return false;
        }

        public void AddInfiniteItem(int id)
        {
            if (infiniteItemIds != null)
            {
                if (!infiniteItemIds.Contains(id))
                {
                    infiniteItemIds.Add(id);
                }
            }
        }

        public void RemoveInfiniteItem(int id)
        {
            if (infiniteItemIds != null)
            {
                if (infiniteItemIds.Contains(id))
                {
                    infiniteItemIds.Remove(id);
                }
            }
        }


    }
}