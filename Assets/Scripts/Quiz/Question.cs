using UnityEngine;

public class Question : MonoBehaviour
{
    [Header("PARAMETERS")]
    [Tooltip("Main text")]
    [SerializeField] [TextArea(2, 5)] private string _mainText = "";
    [SerializeField] [TextArea(2, 5)] private string[] _optionsText = null;

    #region PUBLIC FIELDS

    public string MainText { get { return _mainText; } set { _mainText = value; } }
    public string[] OptionsText { get { return _optionsText; } set { _optionsText = value; } }

    #endregion
}