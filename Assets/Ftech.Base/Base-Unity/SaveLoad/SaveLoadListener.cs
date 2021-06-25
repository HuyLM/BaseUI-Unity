using System;
using UnityEngine;
namespace Ftech.Lib
{
    public class SaveLoadListener : MonoBehaviour {
        private bool isLoaded;
        private void Awake() {
            DontDestroyOnLoad(this);
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            SaveLoad.Load();
            isLoaded = true;
            CheckDayOpenGame();
        }

        private void OnApplicationPause(bool pause) {
            if (!isLoaded) {
                return;
            }
            if (pause) {
                SaveLoad.Save();
                DateTime today = DateTime.Today;
            }
        }

        private void OnApplicationQuit() {
            if (!isLoaded) {
                return;
            }
            SaveLoad.Save();
            DateTime today = DateTime.Today;
        }

        private void CheckDayOpenGame() {

        }
    }
}
