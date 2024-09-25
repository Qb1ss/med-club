using UnityEngine;
using System.Collections;
using DG.Tweening;

public enum TypeScreen
{
    Welcome = 0,
    End = 1
}

[RequireComponent(typeof (AudioSource))]
public class AnimationScreen : MonoBehaviour
{
    [Header("PARAMETERS")]
    [Tooltip("Type screen")]
    [SerializeField] private TypeScreen _typeScreen = TypeScreen.Welcome;
    [Space(height: 10f)]

    [Tooltip("Start delay")]
    [SerializeField] private float _startDelayTime = 0.5f;
    [Tooltip("Animation delay")]
    [SerializeField] private float _animationTime = 0.5f;
    [Tooltip("Delay between step")]
    [SerializeField] private float _delayBetweenStepTime = 0.1f;

    [Header("COMPONENTS")]
    [Tooltip("Character rect transform")]
    [SerializeField] private RectTransform _character = null;
    [Tooltip("Text rect transform")]
    [SerializeField] private RectTransform _textArea = null;
    [Tooltip("call to action rect transform")]
    [SerializeField] private RectTransform _callToAction = null;
    [Tooltip("Button rect transform")]
    [SerializeField] private RectTransform _button = null;
    [Space(height: 5f)]

    [Tooltip("Character target")]
    [SerializeField] private RectTransform _characterStartPosition = null;
    [Tooltip("Text area target")]
    [SerializeField] private RectTransform _textAreaStartPosition = null;
    [Tooltip("Call to action target")]
    [SerializeField] private RectTransform _callToActionStartPosition = null;
    [Tooltip("Button target")]
    [SerializeField] private RectTransform _buttonStartPosition = null;

    private Vector3 _characterTarget = Vector3.zero;
    private Vector3 _textAreaTarget = Vector3.zero;
    private Vector3 _callToActionTarget = Vector3.zero;
    private Vector3 _buttonTarget = Vector3.zero;

    private AudioSource _audioSource = null;

    #region UNITY

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartParametersUpdating();

        StartCoroutine(StartAnimationCoroutine());
    }

    #endregion

    #region PRIVATE METHODS

    private void StartParametersUpdating()
    {
        if (_typeScreen == TypeScreen.Welcome || _typeScreen == TypeScreen.End)
        {
            if (_characterTarget != null)
            {
                _characterTarget = _character.position;
                _character.position = _characterStartPosition.position;
            }

            if (_textAreaTarget != null)
            {
                _textAreaTarget = _textArea.position;
                _textArea.position = _textAreaStartPosition.position;
            }

            if (_callToActionTarget != null)
            {
                _callToActionTarget = _callToAction.position;
                _callToAction.position = _callToActionStartPosition.position;
            }

            if (_buttonTarget != null)
            {
                _buttonTarget = _button.position;
                _button.position = _buttonStartPosition.position;
            }
        }
    }

    #endregion

    #region COROUTINE

    private IEnumerator StartAnimationCoroutine()
    {
        if (_typeScreen == TypeScreen.Welcome || _typeScreen == TypeScreen.End)
        {
            yield return new WaitForSeconds(_startDelayTime);

            if (_character != null) _character.DOMove(_characterTarget, _animationTime);

            _audioSource.Play();

            yield return new WaitForSeconds(_animationTime + _delayBetweenStepTime);

            _audioSource.Play();

            if (_textArea != null) _textArea.DOMove(_textAreaTarget, _animationTime);

            yield return new WaitForSeconds(_animationTime + _delayBetweenStepTime);

            _audioSource.Play();
            
            if (_callToAction != null) _callToAction.DOMove(_callToActionTarget, _animationTime * 2);
            if (_button != null) _button.DOMove(_buttonTarget, _animationTime * 2);

            yield return new WaitForSeconds(_animationTime + _delayBetweenStepTime);
        }

        yield break;
    }

    #endregion
}