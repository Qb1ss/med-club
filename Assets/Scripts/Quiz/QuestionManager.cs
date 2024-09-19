using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

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

        foreach (OptionButton option in _questions[_currentQuestion].OptionButton)
        {
            SettingRandomPosition(option);
        }
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

    ///случайная позиция вопроса
    private void SettingRandomPosition(OptionButton option)
    {
        int position = Random.Range(0, _questions[_currentQuestion].OptionButton.Length); Debug.Log($"Position: {position}");

        Instantiate(option, _optionButtons.transform);
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
}
