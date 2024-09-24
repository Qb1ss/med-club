using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using DG.Tweening;

[RequireComponent(typeof(RectTransform), typeof(Button))]
public class AnimationButton : MonoBehaviour
{
    #region CONSTS

    private const float ANIMATION_TIME = 0.2f;

    private const float MAX_SCALE = 1.05f;
    private const float STANDART_SCALE = 1f;

    #endregion

    private RectTransform _rectTransform = null;
    private Button _button = null;


    #region UNITY

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _button = GetComponent<Button>();
    }

    #endregion

    #region PUBLIC METHODS

    public void OnMouseEnter()
    {
        if (_button.interactable == false) return;

        StartCoroutine(EnterAnimationCoroutine());
    }

    public void OnMouseExit()
    {
        if (_button.interactable == false) return;

        StartCoroutine(ExitAnimationCoroutine());
    }

    #endregion

    #region COROUTINES

    private IEnumerator EnterAnimationCoroutine()
    {
        _rectTransform.DOScale(MAX_SCALE, ANIMATION_TIME);

        yield return new WaitForSeconds(ANIMATION_TIME);

        yield break;
    }

    private IEnumerator ExitAnimationCoroutine()
    {
        _rectTransform.DOScale(STANDART_SCALE, ANIMATION_TIME);

        yield return new WaitForSeconds(ANIMATION_TIME);

        yield break;
    }

    #endregion
}