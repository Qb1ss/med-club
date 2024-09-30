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

    private const float MIN_QUIZ_RATIO = 100f;

    #endregion

    #region EVENTS

    public static UnityEvent OnExitAnimation = new UnityEvent();
    public static UnityEvent<int> OnEndedQuiz = new UnityEvent<int>();

    #endregion

    [Header("PARAMETERS")]
    [Tooltip("Question object")]
    [SerializeField] private Question[] _questions = null;

    private float _ratio = 0;

    private int _rightAnswer = 0;
    private int _currentQuestion = 0;
    private int _attempt = 0;

    [Header("COMPONENTS")]
    [Tooltip("Min grid with buttons")]
    [SerializeField] private TextMeshProUGUI _minQuestionDisplay = null;
    [Tooltip("Max grid with buttons")]
    [SerializeField] private TextMeshProUGUI _maxQuestionDisplay = null;
    [Tooltip("Min question display")]
    [Space(height: 5f)]
    [SerializeField] private GridLayoutGroup _minGrid = null;
    [Tooltip("Max question display")]
    [SerializeField] private GridLayoutGroup _maxGrid = null;
    [Space(height: 10f)]

    [Tooltip("Audio controller")]
    [SerializeField] private AudioController _audioController = null;
    
    private TextMeshProUGUI _questionText = null;
    private GridLayoutGroup _optionButtons = null;

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
        AppLogic.OnSideUpdate.AddListener(RatioSaved);
    }

    #endregion

    #region PUBLIC METHODS

    public void QuizRestart()
    {
        _currentQuestion = 0;
    }

    #endregion

    #region PRIVATE METHODS

    ///���������� ����������
    private void RatioSaved(float ratio)
    {
        _ratio = ratio;

        SideUpdated(); 
        QuestionUpdating();
    }

    ///���������� ����������
    private void SideUpdated()
    {
        TextMeshProUGUI newDisplay = null;
        GridLayoutGroup newGridLayoutGroup = null;

        if (_ratio <= MIN_QUIZ_RATIO)
        {
            _minQuestionDisplay.gameObject.SetActive(true);
            _maxQuestionDisplay.gameObject.SetActive(false);
            
            newDisplay = _minQuestionDisplay;

            _minGrid.gameObject.SetActive(true);
            _maxGrid.gameObject.SetActive(false);

            newGridLayoutGroup = _minGrid;
        }

        if (_ratio > MIN_QUIZ_RATIO)
        {
            _minQuestionDisplay.gameObject.SetActive(false);
            _maxQuestionDisplay.gameObject.SetActive(true);

            newDisplay = _maxQuestionDisplay;

            _minGrid.gameObject.SetActive(false);
            _maxGrid.gameObject.SetActive(true);

            newGridLayoutGroup = _maxGrid;
        }

        _questionText = newDisplay;
        _optionButtons = newGridLayoutGroup;
    }

    ///������ ���������� �������
    private void QuestionUpdating()
    {
        CleaningTheGrid();

        _questionText.text = _questions[_currentQuestion].QuestionText;

        StartCoroutine(QuestionUpdatingCoroutine());

    }

    ///������� �������� ��������
    private void CleaningTheGrid()
    {
        _positionsValue.Clear();
        _optionButton.Clear();

        foreach (Transform child in _optionButtons.transform)
        {
            Destroy(child.gameObject);
        }
    }


    ///�������� ������������ ������
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

    ///�������� ���������� ��������
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

    ///���������� �������
    private IEnumerator QuestionUpdatingCoroutine()
    {
        yield return new WaitForSeconds(ANIMATION_TIME);

        _attempt = 0;

        StartCoroutine(SetPositionCoroutine());
        StopCoroutine(SetPositionCoroutine());

        yield break;
    }

    ///��������� ��������� ������
    private IEnumerator SetPositionCoroutine()
    {
        if (_optionButtons.gameObject.activeInHierarchy == false)
        {
            yield break;
        }

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
