using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(AudioController))]
public class AppLogic : MonoBehaviour
{
    #region EVENTS

    public static UnityEvent OnStartQuiz = new UnityEvent();

    #endregion

    [Header("COMPONENTS")]
    [Tooltip("Menu canvas")]
    [SerializeField] private Canvas _menuCanvas = null;
    [Tooltip("Question canvas")]
    [SerializeField] private Canvas _questionCanvas= null;
    [Tooltip("End canvas")]
    [SerializeField] private Canvas _endCanvas = null;
    [Space(height: 10f)]

    [Tooltip("Welcome screen")]
    [SerializeField] private GameObject _welcomeScreen = null;
    [Space(height: 5f)]

    [Tooltip("Start quiz button")]
    [SerializeField] private Button _startQuizButton = null;
    [Space(height: 5f)]

    [Tooltip("Audio controller")]
    [SerializeField] private AudioController _audioController = null;


    #region UNITY
    private void Awake()
    {
        StartingApp();
    }

    private void OnEnable()
    {
        QuestionManager.OnEndedQuiz.AddListener(EndedQuiz);
    }

    #endregion

    #region PRIVATE METHODS

    ///запуск приложения: закрытие окон, открытие начального экрана
    private void StartingApp()
    {
        _menuCanvas.gameObject.SetActive(true);
        _questionCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(false);

        _welcomeScreen.gameObject.SetActive(true);
        _startQuizButton.gameObject.SetActive(true);
        _startQuizButton.onClick.AddListener(() => StartingQuiz());
    }

    ///начало викторины
    private void StartingQuiz()
    {
        _questionCanvas.gameObject.SetActive(true);
        _menuCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(false);

        _audioController.CardAudioSource.Play();

        OnStartQuiz?.Invoke();
    }

    ///конце викторины
    private void EndedQuiz()
    {
        _menuCanvas.gameObject.SetActive(false);
        _questionCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(true);
    }

    #endregion
}