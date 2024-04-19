using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
{
    [SerializeField] string androidAdsUnitId;
    [SerializeField] string iosAdsUnitId;

    string adsUnitId;

    private void Awake()
    {
#if UNITY_IOS
        adsUnitId = iosGameId;
#elif UNITY_ANDROID
        adsUnitId = androidAdsUnitId;
#endif
    }

    public void LoadRewardAds()
    {
        Advertisement.Load(adsUnitId, this);
    }

    public void ShowRewardAds()
    {
        Advertisement.Show(adsUnitId, this);
        LoadRewardAds();
    }

    #region LoadCallbacks
    public void OnUnityAdsAdLoaded(string placementId)
    {

    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }
    #endregion

    #region ShowCallbacks
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == adsUnitId && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            AdsManager.Instance.EnableRewardsInfo();
        }

    }
    #endregion

}
