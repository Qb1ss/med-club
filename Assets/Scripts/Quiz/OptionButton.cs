using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
using UnityEngine.Audio;

[RequireComponent(typeof(Button), typeof(Image))]
[RequireComponent(typeof(AudioSource))]
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

    [Tooltip("Correct audio clip")]
    [SerializeField] private AudioClip _correctClip = null;
    [Tooltip("Incorrect audio clip")]
    [SerializeField] private AudioClip _incorrectClip = null;
    [Tooltip("Correct audio clip")]
    [SerializeField] private AudioMixerGroup _correctMixer = null;
    [Tooltip("Incorrect audio clip")]
    [SerializeField] private AudioMixerGroup _incorrectMixer = null;
    [Space(height: 5f)]

    [Header("COMPONENTS")]
    [Tooltip("Option display")]
    [SerializeField] private TextMeshProUGUI _optionDisplay = null;

    [SerializeField] private bool _isRight = false;

    private Button _button = null;
    private Image _image = null;
    private AudioSource _audioSource = null;

    #region PUBLIC FIELDS

    public bool IsRight { get => _isRight; set => _isRight = value; }

    #endregion


    #region UNITY

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
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
            _audioSource.clip = _incorrectClip;
            _audioSource.outputAudioMixerGroup = _incorrectMixer;
            _audioSource.Play();

            _image.sprite = _incorrectOptionSprite;
            _optionDisplay.color = _incorrectOptionColor;

            gameObject.GetComponent<RectTransform>().DOScale(1f, 0.1f);

            _button.interactable = false;
        }
        if (_isRight == true)
        {
            _audioSource.clip = _correctClip;
            _audioSource.outputAudioMixerGroup = _correctMixer;
            _audioSource.Play();

            _optionDisplay.color = _correctOptionColor;
        }

        OnSetAnswer?.Invoke(_isRight);
    }

    #endregion
}
