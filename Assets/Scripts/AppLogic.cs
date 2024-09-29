using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioController))]
public class AppLogic : MonoBehaviour
{
    #region EVENTS

    public static UnityEvent OnStartQuiz = new UnityEvent();
    public static UnityEvent<float> OnSideUpdate = new UnityEvent<float>();
    public static UnityEvent<int> OnWinGame = new UnityEvent<int>();
    public static UnityEvent<int> OnLoseGame = new UnityEvent<int>();

    #endregion

    [Header("PARAMETERS")]
    [Tooltip("Corrent answer value")]
    [SerializeField] private int _correntAnswerValue = 5;

    [Header("COMPONENTS")]
    [Tooltip("Menu canvas")]
    [SerializeField] private GameMenu _menuCanvas = null;
    [Tooltip("Question canvas")]
    [SerializeField] private QuestionManager _questionCanvas= null;
    [Tooltip("End canvas")]
    [SerializeField] private EndGameController _endCanvas = null;
    [Tooltip("Error canvas")]
    [SerializeField] private ErrorController _errorCanvas = null;

    private float _ratio = 0f;

    private AudioController _audioController = null;


    #region UNITY
    private void Awake()
    {
        StartingApp();

        _audioController = GetComponent<AudioController>();
    }

    private void Start()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    private void OnEnable()
    {
        Addaptive.OnSideUpdate.AddListener(RatioSaved);
        GameMenu.OnStartGame.AddListener(StartingQuiz);
        QuestionManager.OnEndedQuiz.AddListener(EndedQuiz);
    }

    #endregion

    #region PUBLIC METHODS

    public void QuizRestart()
    {
        _questionCanvas.GetComponent<QuestionManager>().QuizRestart();

        OnStartMenu();
    }

    public void OnStartMenu()
    {
        _menuCanvas.gameObject.SetActive(true);
        _questionCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(false);
        _errorCanvas.gameObject.SetActive(false);
    }

    #endregion

    #region PRIVATE METHODS

    ///запуск приложения: закрытие окон, открытие начального экрана
    private void StartingApp()
    {
        _menuCanvas.gameObject.SetActive(true);
        _questionCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(false);
        _errorCanvas.gameObject.SetActive(false);
    }

    ///начало викторины
    private void StartingQuiz()
    {
        _audioController.ButtonAudioSource.Play();

        _menuCanvas.gameObject.SetActive(false);
        _questionCanvas.gameObject.SetActive(true);
        _endCanvas.gameObject.SetActive(false);
        _errorCanvas.gameObject.SetActive(false);

        OnStartQuiz?.Invoke();
        OnSideUpdate?.Invoke(_ratio);
    }

    ///конце викторины
    private void EndedQuiz(int rightAnswer)
    {
        _menuCanvas.gameObject.SetActive(false);
        _questionCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(true);
        _errorCanvas.gameObject.SetActive(false);

        if (rightAnswer < _correntAnswerValue) OnLoseGame?.Invoke(rightAnswer);
        else if (rightAnswer >= _correntAnswerValue) OnWinGame?.Invoke(rightAnswer);
    }

    ///ошибка разрешения
    private void RatioError()
    {
        _errorCanvas.gameObject.SetActive(true);

        _menuCanvas.gameObject.SetActive(false);
        _questionCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(false);

        Debug.Log("Error: please restart game");
    }

    ///сохранение и передача разрешения
    private void RatioSaved(float ratio)
    {
        _ratio = ratio;
        
        //if (_questionCanvas.gameObject.activeInHierarchy == true) RatioError();

        OnSideUpdate?.Invoke(_ratio);
    }

    #endregion
}