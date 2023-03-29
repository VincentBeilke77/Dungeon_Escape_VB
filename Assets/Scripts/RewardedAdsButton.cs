using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField]
    Button _showAdButton;
    [SerializeField]
    string _androidAdUnitId = "Rewarded_Android";
    [SerializeField]
    string _iOSAdUnitId = "Rewarded_iOS";
    string _placementId = null; // This will remain null for unsupported platforms

    private void Awake()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _placementId = _androidAdUnitId;
#endif

        //_showAdButton.interactable = false;
    }

    private void Start()
    {
        LoadAd();
    }

    public void LoadAd()
    {
        Debug.Log($"Loading Ad: {_placementId}");
        Advertisement.Load(_placementId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Ad Loaded: {_placementId}");

        if (placementId.Equals(_placementId))
        {
            // configure the button to call the ShowAdd() method when clicked:
            _showAdButton.onClick.AddListener(ShowAd);
            // enable the button for users to click:
            _showAdButton.interactable = true;
        }
    }

    public void ShowAd()
    {
        Debug.Log("Show Ad clicked!");
        // disable the button
        _showAdButton.interactable = false;
        // then show the ad
        Advertisement.Show(_placementId, this);

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(_placementId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)) 
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.AddDiamonds(100);
            UIManager.Instance.UpdateDiamondCount(100);
            // Load another ad:
            Advertisement.Load( _placementId, this);
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {placementId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    { }

    public void OnUnityAdsShowClick(string placementId)
    { }

    void OnDestroy()
    {
        _showAdButton.onClick.RemoveAllListeners();
    }
}
