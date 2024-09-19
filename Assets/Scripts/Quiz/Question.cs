using UnityEngine;

public class Question : MonoBehaviour
{
    [Header("PARAMETERS")]
    [Tooltip("Question text")]
    [SerializeField] [TextArea(2, 5)] private string _questionText = "";

    [Header("COMPONENTS")]
    [Tooltip("Options")]
    [SerializeField] private OptionButton[] _optionButtons = null;

    #region PUBLIC FIELDS

    public string QuestionText { get => _questionText; set => _questionText = value; }

    public OptionButton[] OptionButton { get => _optionButtons; set => _optionButtons = value; }

    #endregion
}