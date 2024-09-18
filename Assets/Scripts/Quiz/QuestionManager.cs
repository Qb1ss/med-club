using UnityEngine;
using UnityEngine.Events;

public class QuestionManager : MonoBehaviour
{
    #region EVENTS

    public static UnityEvent OnEndedQuiz = new UnityEvent();

    #endregion

    [Header("PARAMETERS")]
    [Tooltip("Question object")]
    [SerializeField] private Question[] _questions = null;

    [SerializeField] private int _currentQuestion = 0;

    [Header("COMPONENTS")]
    [Tooltip("Buttons with option")]
    [SerializeField] private VarianButton[] _varianButtons = null;


    #region UNITY

    private void Start()
    {
        VarianButton.OnSetAnswer.AddListener(SetAnswer);

        _currentQuestion = 0;
    }

    private void OnEnable()
    {
        if(gameObject.activeInHierarchy == true) QuestionUpdating();
    }

    #endregion

    #region PRIVATE METHODS

    ///обновление вопроса
    private void QuestionUpdating() //
    {
        Debug.Log($"Question: {_questions[_currentQuestion].MainText}");

        SettingRightVariant();
    }  

    ///установка правильного ответа
    private void SettingRightVariant()
    {
        int rightVarian = Random.Range(0, _varianButtons.Length); Debug.Log($"Right variant is {rightVarian}"); //

        foreach (var button in _varianButtons)
        {
            button.IsRight = false;

            button.SettingColor();
        }

        _varianButtons[rightVarian].IsRight = true;
    }

    ///проверка правильности ответа
    private void SetAnswer(bool isRight)
    {
        Debug.Log(isRight);

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
}
