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

    #region PRIVATE METHODS

    ///������ ����������: �������� ����, �������� ���������� ������
    private void StartingApp()
    {
        _menuCanvas.gameObject.SetActive(true);
        _questionCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(false);

        _welcomeScreen.gameObject.SetActive(true);
    }

    ///������ ���������
    private void StartingQuiz()
    {
        _audioController.ButtonAudioSource.Play();

        _questionCanvas.gameObject.SetActive(true);
        _menuCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(false);

        OnStartQuiz?.Invoke();
    }

    ///����� ���������
    private void EndedQuiz()
    {
        _menuCanvas.gameObject.SetActive(false);
        _questionCanvas.gameObject.SetActive(false);
        _endCanvas.gameObject.SetActive(true);
    }

    #endregion
}