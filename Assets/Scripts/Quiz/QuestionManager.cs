using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.UIElements;

public class QuestionManager : MonoBehaviour
{
    #region EVENTS

    public static UnityEvent OnEndedQuiz = new UnityEvent();

    #endregion

    [Header("PARAMETERS")]
    [Tooltip("Question object")]
    [SerializeField] private Question[] _questions = null;

    private int _currentQuestion = 0;

    [Header("COMPONENTS")]
    [Tooltip("Question display")]
    [SerializeField] private TextMeshProUGUI _questionText = null;

    [Space(height: 5f)]
    [Tooltip("Buttons with option")]
    [SerializeField] private GridLayoutGroup _optionButtons = null;

    private List<OptionButton> _optionButton = new List<OptionButton>();
    private List<int> _positionsValue = new List<int>();


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

    #region PRIVATE METHODS

    ///обновление вопроса
    private void QuestionUpdating()
    {
        CleaningTheGrid();

        _questionText.text = _questions[_currentQuestion].QuestionText;

        StartCoroutine(SetPositionCoroutine());
    }

    ///очистка прошлого варианта
    private void CleaningTheGrid()
    {
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
            QuestionValueCheching();
        }
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
            OnEndedQuiz?.Invoke();
        }
    }

    #endregion

    #region COROUTINE

    ///случайная вариантов ответа
    private IEnumerator SetPositionCoroutine()
    {
        for (int i = 0; i < _questions[_currentQuestion].OptionButton.Length; i++)
        {
            int j = UnityEngine.Random.Range(0, _questions[_currentQuestion].OptionButton.Length); Debug.Log($"J = {j}");

            while (_positionsValue.Contains(j) == true)
            {
                j = UnityEngine.Random.Range(0, _questions[_currentQuestion].OptionButton.Length); Debug.Log($"J = {j}");
            }
         
            _positionsValue.Add(j);

            _optionButton.Add(_questions[_currentQuestion].OptionButton[j]);

            Instantiate(_optionButton[i], _optionButtons.transform);
        }

        Debug.Log("End for");

        yield break;
    }

    #endregion
}
