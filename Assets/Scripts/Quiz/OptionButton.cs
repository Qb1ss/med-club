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
    [Space(height: 5f)]

    [Tooltip("Image of incorrect option")]
    [SerializeField] private Sprite _incorrectOptionSprite = null;
    [Space(height: 5f)]

    [Tooltip("Color of correct option")]
    [SerializeField] private Color _correctOptionColor = Color.green;
    [Tooltip("Color of incorrect option")]
    [SerializeField] private Color _incorrectOptionColor = Color.red;

    [Header("COMPONENTS")]
    [Tooltip("Option display")]
    [SerializeField] private TextMeshProUGUI _optionDisplay = null;

    [SerializeField] private bool _isRight = false;

    private Button _button = null;
    private Image _image = null;

    #region PUBLIC FIELDS

    public bool IsRight { get => _isRight; set => _isRight = value; }

    #endregion


    #region UNITY

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
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
        if (_isRight == false)
        {
            _image.sprite = _incorrectOptionSprite;
            _optionDisplay.color = _incorrectOptionColor;

            _optionDisplay.gameObject.SetActive(false);
        }
        if (_isRight == true)
        {
            _optionDisplay.color = _correctOptionColor;
            ///
        }

        OnSetAnswer?.Invoke(_isRight);
    }

    #endregion
}
