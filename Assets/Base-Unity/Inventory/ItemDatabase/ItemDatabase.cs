using System.Collections.Generic;
using UnityEngine;

namespace AtoLib.InventorySystem
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Data/Item/ItemDatabase1")]
    public class ItemDatabase : ScriptableObject
    {
        private static ItemDatabase instance;

        public static bool HasInstance => instance != null;

        public static ItemDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    string path = typeof(ItemDatabase).Name;
                    ItemDatabase data = Resources.Load<ItemDatabase>(path);
                    if (data == null)
                    {
                        Debug.LogErrorFormat("[DATABASE] The asset {0} no found!", path);
                    }
                    else
                    {
                        Initialize(data);
                    }
                }
                return instance;
            }
            private set { instance = value; }
        }

        public static void Initialize(ItemDatabase data)
        {
            if (data == null)
            {
                Debug.LogErrorFormat($"[DATABASE] The {data.GetType().Name} is null!");
            }
            else
            {
                Instance = data;
                Instance.OnInitialize();
            }
        }

        public const int NoneId = 0;

        [SerializeField] private ItemCollector[] collectors;

        private Dictionary<int, ItemData> itemDictionary;
#if UNITY_EDITOR
        private Dictionary<int, ItemTypeName> itemTypeDictionary;
#endif

        [ContextMenu("Reload")]
        protected void OnInitialize()
        {
            int totalItem = 0;
            foreach (var collector in collectors)
            {
                totalItem += collector.Items.Length;
            }
            itemDictionary = new Dictionary<int, ItemData>(totalItem);
#if UNITY_EDITOR
            itemTypeDictionary = new Dictionary<int, ItemTypeName>(totalItem);
#endif

            foreach (ItemCollector collector in collectors)
            {
                foreach (ItemData item in collector.Items)
                {
                    if (itemDictionary.ContainsKey(item.Id))
                    {
                        continue;
                    }

                    itemDictionary.Add(item.Id, item);
#if UNITY_EDITOR
                    itemTypeDictionary.Add(item.Id, new ItemTypeName() { Item = item, NameType = collector.NameCollector });
#endif
                }
            }
        }

        public static int GetCount()
        {
#if UNITY_EDITOR
            return Instance.itemTypeDictionary.Count;
#endif
            return 0;
        }

#if UNITY_EDITOR
        public static IEnumerable<ItemTypeName> GetAllItem()
        {
            foreach (ItemTypeName item in Instance.itemTypeDictionary.Values)
            {
                yield return item;
            }
        }
#endif

        public static bool TryGetItem(int id, out IItem item)
        {
            if (Instance.itemDictionary.TryGetValue(id, out ItemData i))
            {
                item = i;
                return true;
            }

            item = null;
            return false;
        }

        public static bool Constains(int id)
        {
            return Instance.itemDictionary.ContainsKey(id);
        }

#if UNITY_EDITOR
        public class ItemTypeName
        {
            private ItemData item;
            private string nameType;

            public ItemData Item { get => item; set => item = value; }
            public string NameType { get => nameType; set => nameType = value; }
        }
#endif
    }
}