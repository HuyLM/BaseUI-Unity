using Ftech.Lib.Common.UnityInspector.Editor;
using UnityEngine;


namespace Ftech.Lib.InventorySystem
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Data/Item/ItemData")]
    public class ItemData : ScriptableObject, IItem
    {
        [Header("[Item]")]
        [SerializeField, SpriteField] private Sprite icon;
        [SerializeField, Range(1, 9999)] private int id = 1;
        [SerializeField] private string displayName;
        [SerializeField, TextArea(3, 5)] private string description;
        [SerializeField] private ItemSlot price;

        public int Id
        {
            get => id;
            set => id = value;
        }
        public string Name
        {
            get => displayName;
            set => displayName = value;
        }
        public virtual Sprite Icon => icon;
        public string Description
        {
            get => description;
            set => description = value;
        }
        public ItemSlot Price => price;

        public virtual void Claim(int amount)
        {
            Debug.Log($"Add {amount} item {Id} in inventory");
        }
    }

}