using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

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

    #endregion

    #region COROUTINE

    private IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSeconds(_delayTime);

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