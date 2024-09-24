using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class AnimationDeck : MonoBehaviour
{
    [Header("PARAMETERS")]
    [Tooltip("Start delay")]
    [SerializeField] private float _delayTime = 0.5f;
    [SerializeField] private float _movingTime = 3f;

    [SerializeField] private Transform _spawnPosition = null;

    [SerializeField] private RectTransform _spawnParent = null;
    [SerializeField] private RectTransform _targetPosition = null;
    
    [SerializeField] private Image _cartBack = null;

    private void OnEnable()
    {
        AppLogic.OnStartQuiz.AddListener(StartingAnimation);
    }

    private void StartingAnimation()
    {
        StartCoroutine(AnimationCoroutine());
    }
    
    private IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSeconds(_delayTime);

        Image cartBack = Instantiate(_cartBack, _spawnParent.transform);
        cartBack.gameObject.GetComponent<RectTransform>().position = _spawnPosition.position;
        cartBack.rectTransform.DOMove(_targetPosition.position, _movingTime);
        cartBack.rectTransform.DOScale(2.22f, _movingTime);

        yield return new WaitForSeconds(_movingTime + (2 - _delayTime));

        Destroy(cartBack.gameObject);

        yield break;
    }
}