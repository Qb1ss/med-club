using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(AudioController))]
public class AppLogic : MonoBehaviour
{
    #region EVENTS

    public static UnityEvent OnStartQuiz = new UnityEvent();

    #endregion

    [Header("PARAMETERS")]
    [Tooltip("Corrent answer value")]
    [SerializeField] private int _correntAnswerValue = 5;


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
    [Tooltip("Win screen")]
    [SerializeField] private GameObject _winScreen = null;
    [Tooltip("Lose screen")]
    [SerializeField] private GameObject _loseScreen = null;
    [Space(height: 5f)]

    [Tooltip("Start quiz button")]
    [SerializeField] private Button _startQuizButton = null;
    [Space(height: 5f)]

    private AudioController _audioController = null;


    #region UNITY
    private void Awake()
    {
        StartingApp();

        _audioController = GetComponent<AudioController>();
    }

    private void Start()
    {
        _startQuizButton.onClick.AddListener(() => StartingQuiz());
    }

    private void OnEnable()
    {
        QuestionManager.OnEndedQuiz.AddListener(EndedQuiz);
    }

    #endregion

    #region PUBLIC METHODS

    public void QuizRestart()
    {
        _questionCanvas.GetComponent<QuestionManager>().QuizRestart();
    }

    public void OnStartMenu()
    {
        _menuCanvas.gameObject.SetActive(true);
        _questionCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(false);

        _welcomeScreen.gameObject.SetActive(true);
    }

    #endregion

    #region PRIVATE METHODS

    ///запуск приложения: закрытие окон, открытие начального экрана
    private void StartingApp()
    {
        _menuCanvas.gameObject.SetActive(true);
        _questionCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(false);

        //_welcomeScreen.gameObject.SetActive(true);
    }

    ///начало викторины
    private void StartingQuiz()
    {
        _audioController.ButtonAudioSource.Play();

        _questionCanvas.gameObject.SetActive(true);
        _menuCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(false);

        OnStartQuiz?.Invoke();
    }

    ///конце викторины
    private void EndedQuiz(int rightAnswer)
    {
        _menuCanvas.gameObject.SetActive(false);
        _questionCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(true);

        if (rightAnswer < _correntAnswerValue)
        {
            _winScreen.gameObject.SetActive(false);
            _loseScreen.gameObject.SetActive(true);
        }
        else if (rightAnswer >= _correntAnswerValue)
        { 
            _winScreen.gameObject.SetActive(true);
            _loseScreen.gameObject.SetActive(false);
        }
    }

    #endregion
}