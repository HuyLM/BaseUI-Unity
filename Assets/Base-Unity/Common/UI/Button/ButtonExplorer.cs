using UnityEngine;
using UnityEngine.UI;
namespace Ftech.Lib.UI
{
    public class ButtonExplorer : ButtonBase
    {
        [SerializeField] private Image mainBg;

        [Header("==== Sound Effect ====")]
        [SerializeField] private bool clickSoundEnable = false;
        [SerializeField] private AudioClip clickSoundEffect;

        [Header("==== Custom Disable State ====")]
        [SerializeField] private DisableType disableType = DisableType.NONE;
        [SerializeField] private GameObject disableMask;
        [SerializeField] private Color enableColor = Color.white;
        [SerializeField] private Color disableColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        [SerializeField] private Material enableMat;
        [SerializeField] private Material disableMat;
        [SerializeField] private Sprite enableSprite;
        [SerializeField] private Sprite disableSprite;

#if UNITY_EDITOR
        public DisableType MyDisableType { get => disableType; }
#endif

        public enum DisableType { NONE, COLOR, MASK, MATERIAL, SPRITE }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (mainBg == null)
                mainBg = GetComponent<Image>();
            if (mainBg == null)
                mainBg = GetComponentInChildren<Image>();
        }

        protected override void InvokeOnClick()
        {
            base.InvokeOnClick();
            // TODO: Apply sound effect
            if (!clickSoundEnable)
                return;
            if (clickSoundEffect)
            {
                //SoundManager.Instance.PlaySoundEffect(clickSoundEffect); [Comment]
            }
            else
            {
                //SoundManager.Instance.PlayClickEffect();
            }
        }

        #region Explore Persionality
        protected override void SetState(bool enable)
        {
            base.SetState(enable);

            switch (disableType)
            {
                case DisableType.NONE:
                break;
                case DisableType.COLOR:
                SetColor(enable ? enableColor : disableColor);
                break;
                case DisableType.MASK:
                if (disableMask)
                    disableMask.SetActive(!enable);
                break;
                case DisableType.MATERIAL:
                if (mainBg != null)
                    mainBg.material = enable ? enableMat : disableMat;
                break;

                case DisableType.SPRITE:
                {
                    if (mainBg != null)
                    {
                        mainBg.sprite = enable ? enableSprite : disableSprite;
                    }
                    break;
                }
                default:
                break;
            }
        }

        public void SetColor(Color color)
        {
            if (mainBg)
                mainBg.color = color;
        }

        #endregion
    }
}