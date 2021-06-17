using UnityEngine;
using GameSystem.Common.UnityInspector;
using UnityEngine.Serialization;


namespace AtoLib.InventorySystem
{
    [System.Serializable]
    public class ItemSlot : IItemInstance
    {
        public static ItemSlot Empty = new ItemSlot(ItemDatabase.NoneId, 0);

        [FormerlySerializedAs("item")]
        [SerializeField, ItemField] protected int i;
        [FormerlySerializedAs("amount")]
        [SerializeField] protected int a;

        private IItem item;

        public IItem Item
        {
            get
            {
                if (item == null)
                {
                    ItemDatabase.TryGetItem(Id, out item);
                }
                return item;
            }
        }

        public int Amount
        {
            set
            {
                a = value;
            }

            get
            {
                return a;
            }
        }

        public int Id => i;

        public string Name
        {
            get
            {
                return Item?.Name;
            }
        }

        public string Description
        {
            get
            {
                return Item?.Description;
            }
        }

        public Sprite Icon
        {
            get
            {
                return Item?.Icon;
            }
        }

        public bool IsEmpty => Id == ItemDatabase.NoneId || a <= 0;

        public ItemSlot(int itemId, int amount)
        {
            this.i = itemId;
            this.a = amount;
        }

        public override string ToString()
        {
            return $"{Name} - {Amount}";
        }

        public string ToShortString()
        {
            return $"{Id}{Amount}";
        }

        public void Stack(int amount)
        {
            this.a += amount;
        }

        public void Destack(int amount)
        {
            this.a -= amount;
            if (this.a < 0)
            {
                this.a = 0;
            }
        }

        public void Claim()
        {
            IItem item = Item;
            if (item != null)
            {
                item.Claim(Amount);
            }
        }

        public void Claim(float multi)
        {
            IItem item = Item;
            if (item != null)
            {
                item.Claim(Mathf.RoundToInt(Amount * multi));
            }
        }

    }
}