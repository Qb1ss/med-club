using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioController))]
public class AppLogic : MonoBehaviour
{
    #region EVENTS

    public static UnityEvent OnStartQuiz = new UnityEvent();
    public static UnityEvent OnUpdateSide = new UnityEvent();

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

    private AudioController _audioController = null;


    #region UNITY
    private void Awake()
    {
        StartingApp();

        _audioController = GetComponent<AudioController>();
    }

    private void OnEnable()
    {
        GameMenu.OnStartGame.AddListener(StartingQuiz);
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
    }

    #endregion

    #region PRIVATE METHODS

    ///запуск приложения: закрытие окон, открытие начального экрана
    private void StartingApp()
    {
        _menuCanvas.gameObject.SetActive(true);
        _questionCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(false);
    }

    ///начало викторины
    private void StartingQuiz()
    {
        _audioController.ButtonAudioSource.Play();

        _menuCanvas.gameObject.SetActive(false);
        _questionCanvas.gameObject.SetActive(true);
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

        }
        else if (rightAnswer >= _correntAnswerValue)
        { 

        }
    }

    #endregion
}