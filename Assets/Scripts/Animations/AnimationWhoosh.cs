using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Audio;

public enum TypeScreen
{
    Welcome = 0,
    End = 1
}

public enum WhooshSide
{
    Left = 0,
    Right = 1,
    Top = 2,
    Bottom = 3
}

[RequireComponent(typeof (AudioSource), typeof(RectTransform))]
public class AnimationScreen : MonoBehaviour
{
    [Header("PARAMETERS")]
    [Tooltip("Type screen")]
    [SerializeField] private TypeScreen _typeScreen = TypeScreen.Welcome;
    [Tooltip("Whoosh side")]
    [SerializeField] private WhooshSide _whooshSide = WhooshSide.Left;
    [Space(height: 10f)]

    [Tooltip("Whoosh clip")]
    [SerializeField] private AudioClip _whooshClip = null;
    [Tooltip("Whoosh mixer group")]
    [SerializeField] private AudioMixerGroup _mixerGroup = null;
    [Space(height: 10f)]

    [Tooltip("Start delay")]
    [SerializeField] private float _startDelayTime = 0.5f;
    [Tooltip("Animation delay")]
    [SerializeField] private float _animationTime = 0.5f;

    [SerializeField] private Vector3 _startPosition = Vector3.zero;
    [SerializeField] private Vector3 _targetPosition = Vector3.zero;

    private AudioSource _audioSource = null;
    private RectTransform _rectTransform = null;

    #region UNITY

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _rectTransform = GetComponent<RectTransform>();
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
        _audioSource.outputAudioMixerGroup = _mixerGroup;
        _audioSource.clip = _whooshClip;

        _targetPosition = _rectTransform.localPosition;

        if (_whooshSide == WhooshSide.Left)
        {
            _startPosition = new Vector3(_rectTransform.localPosition.x + 2000, _rectTransform.localPosition.y, _rectTransform.localPosition.z);
        }

        if (_whooshSide == WhooshSide.Right)
        {
            _startPosition = new Vector3(_rectTransform.localPosition.x - 2000, _rectTransform.localPosition.y, _rectTransform.localPosition.z);
        }

        if (_whooshSide == WhooshSide.Top)
        {
            _startPosition = new Vector3(_rectTransform.localPosition.x, _rectTransform.localPosition.y + 2000, _rectTransform.localPosition.z);
        }

        if (_whooshSide == WhooshSide.Bottom)
        {
            _startPosition = new Vector3(_rectTransform.localPosition.x, -_rectTransform.localPosition.y - 2000, _rectTransform.localPosition.z);
        }

        _rectTransform.localPosition = _startPosition;
    }

    #endregion

    #region COROUTINE

    private IEnumerator StartAnimationCoroutine()
    {
        yield return new WaitForSeconds(_startDelayTime);

        _rectTransform.DOLocalMove(_targetPosition, _animationTime);

        _audioSource.Play();

        yield return new WaitForSeconds(_animationTime);

        yield break;
    }

    #endregion
}