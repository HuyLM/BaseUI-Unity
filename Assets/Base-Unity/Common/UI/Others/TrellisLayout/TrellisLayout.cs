

using System.Collections.Generic;
using UnityEngine;
namespace AtoLib.UI
{
    public class TrellisLayout : MonoBehaviour
    {
        [SerializeField] private int numberHigh;
        [SerializeField] private float spacingX;

        [SerializeField] private Vector2 sizeItem;

        [SerializeField] private TrellisHorizontal trellisHorizontalPrefab;

        public float HightWidth
        {
            get
            {
                return sizeItem.x * numberHigh + spacingX * (numberHigh - 1);
            }
        }

        public float HightHeigth
        {
            get
            {
                return sizeItem.y;
            }
        }

        public float LowWidth
        {
            get
            {
                return sizeItem.x * (numberHigh - 1) + spacingX * (numberHigh - 2);
            }
        }

        public float LowHeigth
        {
            get
            {
                return sizeItem.y;
            }
        }



        private List<Transform> items = new List<Transform>();
        private List<TrellisHorizontal> trellisHori = new List<TrellisHorizontal>();
        private TrellisHorizontal currentTrellisHorizontal;

        public void AddItem(Transform item)
        {
            items.Add(item);
            ChooseTrellisHorizontal();
            currentTrellisHorizontal.AddItem(item);
        }

        public void ChooseTrellisHorizontal()
        {
            int currentNumberItem = items.Count;
            int bias = (currentNumberItem) % (numberHigh * 2 - 1);
            if (bias == 1)
            { // add new hight trellis horizontal
                TrellisHorizontal newTrellis = Instantiate(trellisHorizontalPrefab, transform);
                newTrellis.Set(HightWidth, HightHeigth, spacingX, true);
                currentTrellisHorizontal = newTrellis;
            }
            else if (bias == numberHigh + 1)
            { // add new low trellis horizontal
                TrellisHorizontal newTrellis = Instantiate(trellisHorizontalPrefab, transform);
                newTrellis.Set(LowWidth, LowHeigth, spacingX, false);
                currentTrellisHorizontal = newTrellis;
            }
        }

    }
}