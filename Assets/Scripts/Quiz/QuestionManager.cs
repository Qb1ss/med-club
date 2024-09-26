using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class QuestionManager : MonoBehaviour
{
    #region CONSTS

    private const float ANIMATION_TIME = 3f;

    #endregion

    #region EVENTS

    public static UnityEvent OnExitAnimation = new UnityEvent();
    public static UnityEvent<int> OnEndedQuiz = new UnityEvent<int>();

    #endregion

    [Header("PARAMETERS")]
    [Tooltip("Question object")]
    [SerializeField] private Question[] _questions = null;

    private int _rightAnswer = 0;
    private int _currentQuestion = 0;
    private int _attempt = 0;

    [Header("COMPONENTS")]
    [Tooltip("Question display")]
    [SerializeField] private TextMeshProUGUI _questionText = null;
    [Space(height: 5f)]

    [Tooltip("Buttons with option")]
    [SerializeField] private GridLayoutGroup _optionButtons = null;
    [Space(height: 10f)]
    
    [Tooltip("Audio controller")]
    [SerializeField] private AudioController _audioController = null;

    private List<OptionButton> _optionButton = new List<OptionButton>();
    private List<int> _positionsValue = new List<int>();

    #region PUBLIC FIELDS

    public int RightAnswer { get => _rightAnswer; set => _rightAnswer = value; }

    #endregion


    #region UNITY

    private void Start()
    {
        _currentQuestion = 0;

        OptionButton.OnSetAnswer.AddListener(SetAnswer);
    }

    private void OnEnable()
    {
        if(gameObject.activeInHierarchy == true) QuestionUpdating();
    }

    #endregion

    #region PUBLIC METHODS

    public void QuizRestart()
    {
        _currentQuestion = 0;
    }

    #endregion

    #region PRIVATE METHODS

    ///запуск обновление вопроса
    private void QuestionUpdating()
    {
        CleaningTheGrid();

        _questionText.text = _questions[_currentQuestion].QuestionText;

        StartCoroutine(QuestionUpdatingCoroutine());

    }

    ///очистка прошлого варианта
    private void CleaningTheGrid()
    {
        _positionsValue.Clear();
        _optionButton.Clear();

        foreach (Transform child in _optionButtons.transform)
        {
            Destroy(child.gameObject);
        }
    }


    ///проверка правильности ответа
    private void SetAnswer(bool isRight)
    {
        if (isRight == true)
        {
            if (_attempt == 0) _rightAnswer++;

            OnExitAnimation?.Invoke();

            _audioController.CorrectAudioSource.Play();

            QuestionValueCheching();
        }

        _attempt++;
    }

    ///проверка количества вопросов
    private void QuestionValueCheching()
    {
        if (_currentQuestion < _questions.Length - 1)
        {
            _currentQuestion++;

            QuestionUpdating();
        }
        else 
        {
            OnEndedQuiz?.Invoke(_rightAnswer);

            _currentQuestion = 0;
            _rightAnswer = 0;
        }
    }

    #endregion

    #region COROUTINE

    ///обновление вопроса
    private IEnumerator QuestionUpdatingCoroutine()
    {
        yield return new WaitForSeconds(ANIMATION_TIME);

        _attempt = 0;

        StartCoroutine(SetPositionCoroutine());
        StopCoroutine(SetPositionCoroutine());

        yield break;
    }

    ///случайная вариантов ответа
    private IEnumerator SetPositionCoroutine()
    {
        for (int i = 0; i < _questions[_currentQuestion].OptionButton.Length; i++)
        {
            int j = Random.Range(0, _questions[_currentQuestion].OptionButton.Length);

            while (_positionsValue.Contains(j) == true)
            {
                j = Random.Range(0, _questions[_currentQuestion].OptionButton.Length);
            }
         
            _positionsValue.Add(j);

            _optionButton.Add(_questions[_currentQuestion].OptionButton[j]);

            Instantiate(_optionButton[i], _optionButtons.transform);
        }

        yield break;
    }

    #endregion
}
