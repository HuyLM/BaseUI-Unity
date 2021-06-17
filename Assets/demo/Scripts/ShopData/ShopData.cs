using AtoLib;
using GameSystem.Common.UnityInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "Resource/ShopData")]
public class ShopData : SingletonScriptableObject<ShopData>
{
    [SerializeField] private GameItem[] items;
    [SerializeField, SpriteField] private Sprite icon;
    public int CurrentUsingItemID { get; set; }

    public List<GameItem> GetShopItems()
    {
        List<GameItem> result = new List<GameItem>();
        for (int i = 0; i < items.Length; ++i)
        {
            if (items[i].IsExpiryDate)
            {
                result.Add(items[i]);
            }
        }
        return result;
    }

    public List<GameItem> GetInventoryItems()
    {
        List<GameItem> result = new List<GameItem>();
        for (int i = 0; i < items.Length; ++i)
        {
            if (!items[i].IsExpiryDate)
            {
                result.Add(items[i]);
            }
        }
        return result;
    }

    #region Save/Load
    public void LoadFromJson(string json)
    {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json))
        {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null)
        {
            for (int i = 0; i < items.Length; ++i)
            {
                items[i].LoadFromJson(null);
            }
            CurrentUsingItemID = -1;
            return;
        }
        int index = 0;
        for (; index < saveData.items.Length; ++index)
        {
            items[index].LoadFromJson(saveData.items[index]);
        }
        for (; index < items.Length; ++index)
        {
            items[index].LoadFromJson(null);
        }
    }

    public string SaveToJson()
    {
        SaveData saveData = new SaveData(items.Length);
        saveData.usingItemID = CurrentUsingItemID;
        for (int i = 0; i < items.Length; ++i)
        {
            saveData.items[i] = items[i].SaveToJson();
        }
        return JsonUtility.ToJson(saveData);
    }

    [System.Serializable]
    public class SaveData
    {
        public string[] items;
        public int usingItemID;

        public SaveData(int capacity)
        {
            items = new string[capacity];
        }
    }
    #endregion
}
