using Ftech.Lib;
using Ftech.Lib.InventorySystem;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameItem", menuName = "Data/Item/GameItemData")]
public class GameItem : ItemData
{
    [SerializeField] private float useTime; // hour

    public DateTime ExpiryDate { get; set; }

    public float UseTime { get => useTime; }

    public bool IsExpiryDate
    {
        get
        {
            return DateTime.Now.CompareTo(ExpiryDate) >= 0;
        }
    }

    public virtual void Use()
    {
        ShopData.Instance.CurrentUsingItem = this;
    }

    public virtual void Buy()
    {
        ResetExpiryDate();
    }

    public void ResetExpiryDate()
    {
        ExpiryDate = DateTime.Now.AddHours(useTime);
    }

    public void LoadFromJson(string json)
    {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json))
        {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null)
        {
            ExpiryDate = DateTime.Now.AddHours(-1);
            return;
        }
        ExpiryDate = saveData.ExpiryDate;
    }

    public string SaveToJson()
    {
        SaveData saveData = new SaveData();
        saveData.ExpiryDate = ExpiryDate;
        return JsonUtility.ToJson(saveData);
    }

    [System.Serializable]
    private class SaveData
    {
        [SerializeField] private JsonDateTime expiryDate;
        public DateTime ExpiryDate { get => expiryDate; set => expiryDate = value; }
    }
}
