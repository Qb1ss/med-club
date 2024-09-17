using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
    [Space(height: 10f)]

    [Tooltip("Welcome screen")]
    [SerializeField] private GameObject _welcomeScreen = null;
    [Space(height: 5f)]

    [Tooltip("Start quiz button")]
    [SerializeField] private Button _startQuizButton = null;


    #region UNITY
    private void Awake()
    {
        StartingApp();
    }

    #endregion

    #region PRIVATE METHODS

    ///запуск приложения: закрытие окон, открытие начального экрана
    private void StartingApp()
    {
        _menuCanvas.gameObject.SetActive(true);
        _questionCanvas.gameObject.SetActive(false);

        _welcomeScreen.gameObject.SetActive(true);
        _startQuizButton.gameObject.SetActive(true);
        _startQuizButton.onClick.AddListener(() => StartingQuiz());
    }

    ///начало викторины
    private void StartingQuiz()
    {
        _questionCanvas.gameObject.SetActive(true);
        _menuCanvas.gameObject.SetActive(false);

        OnStartQuiz?.Invoke();
    }

    #endregion
}