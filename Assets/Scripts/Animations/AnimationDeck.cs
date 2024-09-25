using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using NUnit.Framework;

[RequireComponent(typeof(AudioSource))]
public class AnimationDeck : MonoBehaviour
{
    #region CONSTS

    private const float WAITING_TIME = 2f;
    private const float MAX_SCALE = 2f;

    #endregion

    [Header("PARAMETERS")]
    [Tooltip("Start delay")]
    [SerializeField] private float _delayTime = 0.5f;
    [Tooltip("Moving time")]
    [SerializeField] private float _movingTime = 1f;

    [Header("Cart backs")]
    [Tooltip("Cart backs for 1-2 question")]
    [SerializeField] private Image _cartBack1 = null;
    [Tooltip("Cart backs for 3-4 question")]
    [SerializeField] private Image _cartBack2 = null;
    [Tooltip("Cart backs for 5-6 question")]
    [SerializeField] private Image _cartBack3 = null;
    [Tooltip("Cart backs for 7-8 question")]
    [SerializeField] private Image _cartBack4 = null;

    private int _currentQustion = 0;
    private int _maxQuestion = 8;

    [Header("COMPONENTS")]
    [Tooltip("Spawn position")]
    [SerializeField] private Transform _spawnPosition = null;
    [Tooltip("Target position")]
    [SerializeField] private RectTransform _targetPosition = null;
    [Space(height: 5f)]
    [Tooltip("Spawn parent")]
    [SerializeField] private RectTransform _spawnParent = null;
    
    [Tooltip("Image of cart back")]
    [SerializeField] private Image _cartBack = null;

    private AudioSource _audioSource = null;

    #region UNITY

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        AppLogic.OnStartQuiz.AddListener(StartingAnimation);
        OptionButton.OnSetAnswer.AddListener(StartAnimation);
    }

    #endregion

    #region PRIVATE METHODS

    private void StartingAnimation()
    {
        StartCoroutine(AnimationCoroutine());
    }
    private void StartAnimation(bool answer)
    {
        if (answer == false) return;

        StartCoroutine(AnimationCoroutine());
    }

    private void CartBackAnimation()
    {
        if (_currentQustion > 8) return;

        if (_currentQustion == 2) Destroy(_cartBack4.gameObject);
        if (_currentQustion == 4) Destroy(_cartBack3.gameObject);
        if (_currentQustion == 6) Destroy(_cartBack2.gameObject);
        if (_currentQustion == 8) Destroy(_cartBack1.gameObject);
    }

    #endregion

    #region COROUTINE

    private IEnumerator AnimationCoroutine()
    {
        _currentQustion++;

        yield return new WaitForSeconds(_delayTime);

        CartBackAnimation();

        Image cartBack = Instantiate(_cartBack, _spawnParent.transform);
        cartBack.gameObject.GetComponent<RectTransform>().position = _spawnPosition.position;

        _audioSource.priority = Random.Range(100, 200);
        _audioSource.Play();

        cartBack.rectTransform.DOMove(_targetPosition.position, _movingTime);
        cartBack.rectTransform.DOScale(MAX_SCALE, _movingTime);

        yield return new WaitForSeconds(_movingTime + (WAITING_TIME - _delayTime));

        Destroy(cartBack.gameObject);

        yield break;
    }

    #endregion
}