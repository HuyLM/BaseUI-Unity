using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ftech.Lib.UI
{
    public class TabControl : MonoBehaviour
    {
        [SerializeField] private TabButton[] tabButtons;

        private void Start()
        {
            for (int i = 0; i < tabButtons.Length; ++i)
            {
                tabButtons[i].SetOnTabClicked(OnTabButtonClicked);
            }
        }

        protected void OnTabButtonClicked(int index)
        {
            SelectTab(index);
        }

        protected virtual void SelectTab(int index)
        {
            for (int i = 0; i < tabButtons.Length; ++i)
            {
                if (index == tabButtons[i].TabIndex)
                {
                    tabButtons[i].SetActiveTab(false);
                }
                else
                {
                    tabButtons[i].SetActiveTab(true);
                }
            }
        }

        public void ForceSelectTab(int index)
        {
            SelectTab(index);
        }


    }
}
