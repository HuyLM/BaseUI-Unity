using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using Ftech.Lib.InventorySystem;

namespace Ftech.Lib.InventorySystem
{
    [CustomEditor(typeof(Inventory))]
    public class InventoryInspector : Editor
    {
        private Inventory inventory;
        private Vector2 scroll;
        private int id;
        private int amount;

        private void Awake()
        {
            inventory = target as Inventory;
        }

        public override void OnInspectorGUI()
        {
            if (!inventory)
            {
                return;
            }

            scroll = EditorGUILayout.BeginScrollView(scroll, true, true);
            foreach (ItemSlot item in inventory.GetAllItem())
            {
                EditorGUILayout.BeginVertical("Box");
                GUIContent content = new GUIContent(item.Icon?.texture, $"{item.Name}\n{item.Description}");
                GUILayout.Box(content, GUILayout.Width(64), GUILayout.Height(64));
                GUILayout.Label($"x{item.Amount}", EditorStyles.centeredGreyMiniLabel, GUILayout.Width(64));
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal();

            GUIContent[] contents = new GUIContent[ItemDatabase.GetCount() + 1];
            contents[0] = new GUIContent("None", "None");
            int[] optionsValue = new int[ItemDatabase.GetCount() + 1];
            optionsValue[0] = ItemDatabase.NoneId;

            int index = 1;
            foreach (var itemType in ItemDatabase.GetAllItem())
            {
                string type = itemType.NameType;
                string name = $"{itemType.Item.Name} (ID: {itemType.Item.Id})";
                contents[index] = new GUIContent(type + "/" + name);
                optionsValue[index] = itemType.Item.Id;
                index++;
            }

            id = EditorGUILayout.IntPopup(id, contents, optionsValue);
            amount = EditorGUILayout.IntField(amount);

            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Add"))
            {
                inventory.Add(this.id, amount);
            }
            if (GUILayout.Button("Remove"))
            {
                inventory.Remove(this.id, amount);
            }
        }
    }
}