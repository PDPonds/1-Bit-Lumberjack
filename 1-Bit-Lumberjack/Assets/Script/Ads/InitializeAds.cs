using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string androidGameId;
    [SerializeField] string iosGameId;
    [SerializeField] bool isTesting;

    string gameId;

    private void Awake()
    {
#if UNITY_IOS
        gameId = iosGameId;
#elif UNITY_ANDROID
        gameId = androidGameId;
#elif UNITY_EDITOR
        gameId = androidGameId;
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, isTesting, this);
        }

    }

    public void OnInitializationComplete() { Debug.Log("Ads Initialized ....."); }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) { }


}
