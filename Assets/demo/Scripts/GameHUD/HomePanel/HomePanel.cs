using Ftech.Lib.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : DOTweenFrame
{
    [SerializeField] private Image imgAvatar;
    [SerializeField] private ButtonBase btnSetting;
    [SerializeField] private ButtonBase btnTutorial;
    [SerializeField] private ButtonBase btnPlay;
    [SerializeField] private ButtonBase btnShop;


    private void Start()
    {
        btnSetting.onClick.AddListener(OnSettingButtonClicked);
        btnTutorial.onClick.AddListener(OnTutorialButtonClicked);
        btnPlay.onClick.AddListener(OnPlayButtonClicked);
        btnShop.onClick.AddListener(OnShopButtonClicked);
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false)
    {
        base.OnShow(onCompleted, instant);
    }

    protected override void OnPause(Action onCompleted = null, bool instant = false)
    {
        base.OnPause(onCompleted, instant);

        // Add Listener SFS
    }

    protected override void OnResume(Action onCompleted = null, bool instant = false)
    {
        base.OnResume(onCompleted, instant);

        // Remove Listener SFS
    }


    private void OnSettingButtonClicked()
    {
        Debug.Log("Open Setting");
    }

    private void OnTutorialButtonClicked()
    {
        Debug.Log("Open Tutorial");
    }

    private void OnPlayButtonClicked()
    {
        Debug.Log("Open Play");
    }

    private void OnShopButtonClicked()
    {
        GameHUD.Instance.Show<ShopPanel>(pauseCurrent: true);
    }
}
