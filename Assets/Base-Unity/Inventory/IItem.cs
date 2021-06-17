using AtoLib.Common;
using UnityEngine;


namespace AtoLib.InventorySystem
{
    public interface IItem : IEventParams
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }
        Sprite Icon { get; }
        void Claim(int amount);
    }

    public interface IItemInstance : IEventParams
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }
        Sprite Icon { get; }
        IItem Item { get; }
        int Amount { get; }
        bool IsEmpty { get; }
    }
}