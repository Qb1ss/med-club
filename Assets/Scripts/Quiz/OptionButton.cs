using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(Button))]
public class OptionButton : MonoBehaviour
{
    #region EVENTS

    public static UnityEvent<bool> OnSetAnswer = new UnityEvent<bool>();

    #endregion

    [Header("PARAMETERS")]
    [Tooltip("Option")]
    [SerializeField] private string _optionText = "";

    [Header("COMPONENTS")]
    [Tooltip("Option display")]
    [SerializeField] private TextMeshProUGUI _optionDisplay = null;

    [SerializeField] private bool _isRight = false;

    private Button _button = null;

    #region PUBLIC FIELDS

    public bool IsRight { get => _isRight; set => _isRight = value; }

    #endregion


    #region UNITY

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _optionDisplay.text = $"{_optionText}";

        _button.onClick.AddListener(() => SettingAnswer());
    }

    #endregion

    #region PUBLIC METHODS

    public void SettingColor()
    {
        _button.GetComponent<Image>().color = Color.white;
    }

    #endregion

    #region PRIVATE METHODS

    private void SettingAnswer()
    {
        if (_isRight == false) _button.GetComponent<Image>().color = Color.red;
        if (_isRight == true) _button.GetComponent<Image>().color = Color.green;

        OnSetAnswer?.Invoke(_isRight);
    }

    #endregion
}
