using UnityEngine;

public class EndGameController : MonoBehaviour
{
    #region CONTST

    private const string LINK_TO_SITE = "https://pharmznanie.ru/";

    #endregion

    [Header("COMPONENTS")]
    [Tooltip("Audio controller")]
    [SerializeField] private AudioController _audioController = null;


    #region PUBLIC METHODS

    public void LinkToSite()
    {
        if (_audioController != null) _audioController.ButtonAudioSource.Play();

        Application.OpenURL(LINK_TO_SITE);
    }

    #endregion
}