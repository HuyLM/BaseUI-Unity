using Ftech.Lib.Common;
using UnityEngine;


namespace Ftech.Lib.InventorySystem
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