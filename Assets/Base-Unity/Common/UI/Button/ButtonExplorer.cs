using UnityEngine;
using UnityEngine.UI;
namespace AtoLib.UI
{
    public class ButtonExplorer : ButtonBase
    {
        [Header("==== Content (No need to assign) ====")]
        [SerializeField] private Text titleText;
        [SerializeField] private Image mainBg;
        [SerializeField] private Image mainIcon;
        [SerializeField] private GameObject notifyIcon;

        [Header("==== Sound Effect ====")]
        [SerializeField] private bool soundClickEnable = true;
        [SerializeField] private AudioClip clickSoundEffect;

        [Header("==== Custom Disable State ====")]
        [SerializeField] private DisableType disableType = DisableType.NONE;
        [SerializeField] private GameObject disableMask;
        [SerializeField] private Color disableColor = new Color(0.9f, 0.9f, 0.9f, 0.9f);
        [SerializeField] private Material disableMat;
        [SerializeField] private Material enableMat;

        public enum DisableType { NONE, COLOR, MASK, MATERIAL }

        private void Awake()
        {
            if (titleText == null)
                titleText = GetComponentInChildren<Text>();
            if (mainBg == null)
                mainBg = GetComponent<Image>();
            if (mainBg == null)
                mainBg = GetComponentInChildren<Image>();
        }

        protected override void InvokeOnClick()
        {
            base.InvokeOnClick();
            // TODO: Apply sound effect
            if (!soundClickEnable)
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
        public override void SetState(bool enable)
        {
            base.SetState(enable);

            switch (disableType)
            {
                case DisableType.NONE:
                break;
                case DisableType.COLOR:
                SetColor(enable ? Color.white : disableColor);
                break;
                case DisableType.MASK:
                if (disableMask)
                    disableMask.SetActive(!enable);
                break;
                case DisableType.MATERIAL:
                if (mainBg != null)
                    mainBg.material = enable ? enableMat : disableMat;
                if (mainIcon != null)
                    mainIcon.material = enable ? enableMat : disableMat;
                break;

                default:
                break;
            }
        }

        public void SetTitle(string text)
        {
            if (titleText)
                titleText.text = text;
        }

        public void SetBackgroundSprite(Sprite sp)
        {
            if (mainBg)
                mainBg.sprite = sp;
        }

        public void SetIconSprite(Sprite sp)
        {
            if (mainIcon)
                mainIcon.sprite = sp;
        }

        public void SetColor(Color color)
        {
            if (mainBg)
                mainBg.color = color;
            if (mainIcon)
                mainIcon.color = color;
        }
        public void SetMaterial(bool enable)
        {
            if (mainBg)
                mainBg.material = enable ? enableMat : disableMat;
            if (mainIcon)
                mainIcon.material = enable ? enableMat : disableMat;
        }
        public void SetNoti(bool show, string notiTitle = "")
        {
            if (!notifyIcon)
            {
                Debug.LogFormat(string.Format("[ButtonExplorer] Set up <color = red>notifyIcon</color> first, plz!"));
                return;
            }
            notifyIcon.SetActive(show);

            if (string.IsNullOrEmpty(notiTitle))
                return;
            var notiTitleText = notifyIcon.GetComponent<Text>();
            if (!notiTitleText)
                notiTitleText = notifyIcon.GetComponentInChildren<Text>();
            if (!notiTitleText)
                return;
            notiTitleText.text = notiTitle;
        }
        #endregion
    }
}