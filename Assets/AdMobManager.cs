using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class AdMobManager : MonoBehaviour
{
    //TODO: Buradaki appId'ye kendi appIdinizi yapıştırın.
    private string _appId = "ca-app-pub-5406409821252930~3890799233";

    private BannerView _bannerAd;

    private InterstitialAd _fullscreenAd;

    private RewardBasedVideoAd _rewardBasedVideoAd;

    // Bu test Banner Id si 
    private string _bannerAdId = "ca-app-pub-3940256099942544/6300978111";
    private string _fullScreenAdId = "ca-app-pub-3940256099942544/1033173712";
    private string _rewardedAdID = "ca-app-pub-3940256099942544/5224354917";


    public Button bannerButton;
    public Button fullScreenButton;
    public Button rewardedButton;
    public Text callbackText;

    public Button changeTextButton;

    private void Start()
    {
        
        MobileAds.Initialize(_appId);

        bannerButton.onClick.AddListener(requestBannerAd);
        fullScreenButton.onClick.AddListener(requestFullScreenAd);
        rewardedButton.onClick.AddListener(requestRewardedAd);

        changeTextButton.onClick.AddListener(hideBanner);

    }
    
    void hideBanner()
    {
        _bannerAd.Hide();
        callbackText.text = "Banner Hide";
    }

    public void requestBannerAd()
    {
        _bannerAd = new BannerView(_bannerAdId, AdSize.Banner, AdPosition.Bottom);

        AdRequest adRequest = new AdRequest.Builder().Build();

        // burada banner reklamımızın AdMobdan yüklüyoruz ve göstermek için hazır hale getiriyoruz gibi düşünebilirsiniz.
        _bannerAd.LoadAd(adRequest);

        
        _bannerAd.OnAdLoaded += (sender, args) =>
        {
            callbackText.text = "Bannner Loaded";
            _bannerAd.Show();
        };
    }


    public void requestFullScreenAd()
    {
        _fullscreenAd = new InterstitialAd(_fullScreenAdId);

        AdRequest adRequest = new AdRequest.Builder().Build();

        _fullscreenAd.LoadAd(adRequest);

        _fullscreenAd.OnAdLoaded += (sender, args) => { _fullscreenAd.Show(); };

        _fullscreenAd.OnAdClosed += (sender, args) => { callbackText.text = "FullScreenAd closed"; };
    }


    // REWARDEDAD - START
    public void requestRewardedAd()
    {
        _rewardBasedVideoAd = RewardBasedVideoAd.Instance;

        AdRequest adRequest = new AdRequest.Builder().Build();

        _rewardBasedVideoAd.LoadAd(adRequest, _rewardedAdID);

        _rewardBasedVideoAd.OnAdLoaded += (sender, args) => { _rewardBasedVideoAd.Show(); };

        _rewardBasedVideoAd.OnAdRewarded += (sender, reward) =>
        {
            _bannerAd.Hide();
            callbackText.text = "reward earned" + reward.Amount;
        };
    }
}