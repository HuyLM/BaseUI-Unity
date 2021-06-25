using Ftech.Lib.InventorySystem;
using UnityEngine;

namespace Ftech.ZodiacBattle
{
    [CreateAssetMenu(fileName = "GameItemDatabase", menuName = "Data/Item/GameItemDatabase")]
    public class GameItemDatabase : ItemDatabase
    {
        static GameItemDatabase _instance = null;
        public static GameItemDatabase Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = Resources.FindObjectsOfTypeAll<GameItemDatabase>()[0];
                    _instance.OnInitialize();
                }
                return _instance;
            }
        }
    }
}
