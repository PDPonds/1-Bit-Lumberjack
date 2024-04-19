using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsManager : Singleton<AdsManager>
{
    public InitializeAds initializeAds;
    public RewardedAds rewardAds;

    [Header("===== Reward Ads =====")]
    [SerializeField] float rewardsTime;
    float curRewardTime;
    [SerializeField] GameObject adsReward;
    [SerializeField] GameObject choiceToWatchAds;
    [SerializeField] GameObject choiceToWatchAdsBorder;
    [SerializeField] Button watchAdsBut;
    [SerializeField] Button cancleToWatchAdsBut;
    [Space(5f)]
    [SerializeField] GameObject rewardsAdsInfo;
    [SerializeField] GameObject rewardsAdsInfoBorder;
    [SerializeField] Button takeRewardBut;

    private void Awake()
    {
        rewardAds.LoadRewardAds();

        curRewardTime = rewardsTime;

        Button adsRewardButton = adsReward.GetComponent<Button>();
        adsRewardButton.onClick.AddListener(() => EnableChoiceToWatchAds());

        watchAdsBut.onClick.AddListener(() => ShowAdsReward());
        cancleToWatchAdsBut.onClick.AddListener(() => DisableChoiceToWatchAds());
        takeRewardBut.onClick.AddListener(() => DisableRewardInfo());
    }

    private void Update()
    {
        AutoEnableAdsReward();
    }
    #region On Touch Bird

    public void OnTapBirdAndGetReward()
    {
        float i = Random.Range(0, 1f);
        if (i > 0.5f) return;

        EnableChoiceToWatchAds();
    }

    #endregion

    #region Rewards Ads
    void EnableAdsReward()
    {
        Scale(adsReward, Vector3.one, 0.5f);
    }

    void DisableAdsReward()
    {
        curRewardTime = rewardsTime;
        Scale(adsReward, Vector3.zero, 0.5f);
    }

    void AutoEnableAdsReward()
    {
        if (adsReward.transform.localScale == Vector3.zero)
        {
            curRewardTime -= Time.deltaTime;
            if (curRewardTime <= 0)
            {

                EnableAdsReward();
            }
        }
    }

    #endregion

    #region Choice To Watch Ads
    public void EnableChoiceToWatchAds()
    {
        DisableAdsReward();

        choiceToWatchAds.gameObject.SetActive(true);
        Scale(choiceToWatchAdsBorder, Vector3.one, 0.5f);
    }

    void DisableChoiceToWatchAds()
    {
        Scale(choiceToWatchAdsBorder, Vector3.zero, 0.5f, () => choiceToWatchAds.SetActive(false));
    }
    #endregion

    #region Reward Info
    public void EnableRewardsInfo()
    {
        rewardsAdsInfo.gameObject.SetActive(true);
        Scale(rewardsAdsInfoBorder, Vector3.one, 0.5f);
    }

    void DisableRewardInfo()
    {
        Scale(rewardsAdsInfoBorder, Vector3.zero, 0.5f, () => rewardsAdsInfo.SetActive(false));
        CoinGenerator.Instance.SpawnAndSetupCoin(GameManager.curPhase * 10);
    }
    #endregion

    public void ShowAdsReward()
    {
        DisableChoiceToWatchAds();
        rewardAds.ShowRewardAds();
    }

    #region Scale UI
    public delegate void AfterScaleUIEvent();

    void Scale(GameObject go, Vector3 scale, float time)
    {
        LeanTween.scale(go, scale, time).setEaseInOutCubic();
    }

    void Scale(GameObject go, Vector3 scale, float time, AfterScaleUIEvent afterScaleEvent)
    {
        LeanTween.scale(go, scale, time).setEaseInOutCubic()
            .setOnComplete(() => afterScaleEvent?.Invoke());
    }

    #endregion

}
