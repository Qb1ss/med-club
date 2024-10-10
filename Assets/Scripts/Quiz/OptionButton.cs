using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using System.Collections;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(Button), typeof(Image))]
[RequireComponent(typeof(AudioSource),typeof(RectTransform))]
public class OptionButton : MonoBehaviour
{
    #region CONSTS

    private const float ANIMATION_TIME = 1f;

    private const int STEP_COUNT = 2;

    #endregion

    #region EVENTS

    public static UnityEvent<bool> OnSetAnswer = new UnityEvent<bool>();

    #endregion

    [Header("PARAMETERS")]
    [Tooltip("Option")]
    [SerializeField] [TextArea(3, 6)] private string _option = null;
    [Space(height: 5f)]

    [Tooltip("Exti animation time")]
    [SerializeField] private float _exitAnimationTime = 1f;
    [Space(height: 5f)]

    [Tooltip("Default image of cart back")]
    [SerializeField] private Sprite _defaultSprite = null;
    [Tooltip("Image of cart back")]
    [SerializeField] private Sprite _cartBackSprite = null;
    [Tooltip("Image of incorrect option")]
    [SerializeField] private Sprite _incorrectOptionSprite = null;
    [Space(height: 5f)]

    [Tooltip("Correct audio clip")]
    [SerializeField] private AudioClip _correctClip = null;
    [Tooltip("Incorrect audio clip")]
    [SerializeField] private AudioClip _incorrectClip = null;
    [Tooltip("Correct audio clip")]
    [SerializeField] private AudioMixerGroup _correctMixer = null;
    [Tooltip("Incorrect audio clip")]
    [SerializeField] private AudioMixerGroup _incorrectMixer = null;
    [Space(height: 5f)]

    [Tooltip("Option display")]
    [SerializeField] private TextMeshProUGUI _optionDisplay = null;
    [Space(height: 5f)]

    [Tooltip("It is right option?")]
    [SerializeField] private bool _isRight = false;

    private Button _button = null;
    private Image _image = null;
    private AudioSource _audioSource = null;
    private RectTransform _rectTransform = null;


    #region UNITY

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _optionDisplay.text = _option;
        _optionDisplay.gameObject.SetActive(false);

        StartCoroutine(FlipAnimationCoroutine());

        _button.onClick.AddListener(() => SettingAnswer());

        _image.sprite = _cartBackSprite;
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

            gameObject.GetComponent<RectTransform>().DOScale(1f, 0.1f);

            _button.interactable = false;
        }
        if (_isRight == true)
        {
            _audioSource.clip = _correctClip;
            _audioSource.outputAudioMixerGroup = _correctMixer;
            _audioSource.Play();
        }

        OnSetAnswer?.Invoke(_isRight);
    }

    #endregion

    #region COROUTINE

    private IEnumerator FlipAnimationCoroutine()
    {
        _button.interactable = false;

        float stepAnimationTime = ANIMATION_TIME / STEP_COUNT;

        _rectTransform.DORotate(new Vector3(0f, -90f, 0f), stepAnimationTime);

        yield return new WaitForSeconds(stepAnimationTime);

        _image.sprite = _defaultSprite;
        _button.interactable = true;

        _rectTransform.DORotate(new Vector3(0f, 0f, 0f), stepAnimationTime);

        _optionDisplay.gameObject.SetActive(true);

        yield return new WaitForSeconds(stepAnimationTime);

        yield break;
    }

    private IEnumerator ExitAnimationCoroutine()
    {
        GameObject duplicate = Instantiate(gameObject, gameObject.transform.parent);
        duplicate.gameObject.GetComponent<RectTransform>().position = gameObject.transform.position;

        yield break;
    }

    #endregion
}
