using UnityEngine;

namespace AtoLib.InventorySystem
{
    [CreateAssetMenu(fileName = "ItemCollector", menuName = "Data/Item/ItemDatabase/ItemCollector")]
    public class ItemCollector : ScriptableObject
    {
        [SerializeField] private string nameCollector = "other";
        [SerializeField] private ItemData[] items;

        public ItemData[] Items { get => items; }
        public string NameCollector { get => nameCollector; }
    }
}