using UnityEngine;

public class EndGameController : MonoBehaviour
{
    #region CONTST

    private const string LINK_TO_SITE = "https://disk.yandex.ru/d/23GpBN_vWBjeQA";

    #endregion

    [Header("COMPONENTS")]
    [Tooltip("Min win menu")]
    [SerializeField] private GameObject _minWinMenu = null;
    [Tooltip("Min lose menu")]
    [SerializeField] private GameObject _minLoseMenu = null;
    [Space(height: 5f)]

    [Tooltip("Middle win menu")]
    [SerializeField] private GameObject _middleWinMenu = null;
    [Tooltip("Middle lose menu")]
    [SerializeField] private GameObject _middleLoseMenu = null;
    [Space(height: 5f)]
    
    [Tooltip("Max win menu")]
    [SerializeField] private GameObject _maxWinMenu = null;
    [Tooltip("Max lose menu")]
    [SerializeField] private GameObject _maxLoseMenu = null;
    [Space(height: 10f)]

    [Tooltip("Audio controller")]
    [SerializeField] private AudioController _audioController = null;

    #region UNITY

    private void Awake()
    {
        WindowsDisabling();
    }

    private void OnEnable()
    {
        AppLogic.OnWinGame.AddListener(WinWindowOpening);
        AppLogic.OnLoseGame.AddListener(LoseWindowOpening);
    }

    #endregion

    #region PUBLIC METHODS

    public void LinkToSite()
    {
        if (_audioController != null) _audioController.ButtonAudioSource.Play();

        Application.OpenURL(LINK_TO_SITE);
    }

    #endregion

    #region PRIVATE METHODS

    private void WinWindowOpening(int rightAnswers)
    {
        _minWinMenu.gameObject.SetActive(true);
        _middleWinMenu.gameObject.SetActive(true);
        _maxWinMenu.gameObject.SetActive(true);
    }

    private void LoseWindowOpening(int rightAnswers)
    {
        _minLoseMenu.gameObject.SetActive(true);
        _middleLoseMenu.gameObject.SetActive(true);
        _maxLoseMenu.gameObject.SetActive(true);
    }

    private void WindowsDisabling()
    {
        _minWinMenu.SetActive(false);
        _minLoseMenu.SetActive(false);
        _middleWinMenu.SetActive(false);
        _middleLoseMenu.SetActive(false);
        _maxWinMenu.SetActive(false);
        _maxLoseMenu.SetActive(false);
    }

    #endregion
}