using UnityEngine;
using UnityEngine.UI;

public class Addaptive : MonoBehaviour
{
    #region CONSTS

    private const float MIN_RATIO = 55f;
    private const float SMALL_RATIO = 100f;
    private const float MIDDLE_RATIO = 150f;
    private const float MAX_RATIO = 180f;

    #endregion

    [Header("COMPONENTS")]
    [Header("Background")]
    [Tooltip("Min background")]
    [SerializeField] private GameObject _minBackground = null;
    [Tooltip("Small background")]
    [SerializeField] private GameObject _smallBackground = null;
    [Tooltip("Middle background")]
    [SerializeField] private GameObject _middleBackground = null;
    [Tooltip("Max background")]
    [SerializeField] private GameObject _maxBackground = null;

    [Space(height: 5f)]
    [SerializeField] private Canvas _backgroundCanvas = null;

    private float _lastRatio = 0f;

    private Camera _camera = null;


    #region UNITY

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        AddaptiveApp();
    }

    #endregion

    #region PRIVATE METHODS

    private void AddaptiveApp()
    {
        float ratio = (float)_camera.pixelWidth / (float)_camera.pixelHeight * 100;

        if (_lastRatio == ratio) return;

        if (ratio <= MIN_RATIO)
        {
            _backgroundCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;

            _minBackground.gameObject.SetActive(true);
            _smallBackground.gameObject.SetActive(false);
            _middleBackground.gameObject.SetActive(false);
            _maxBackground.gameObject.SetActive(false);
        }

        if (ratio > MIN_RATIO && ratio <= SMALL_RATIO)
        {
            _backgroundCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;

            _minBackground.gameObject.SetActive(false);
            _smallBackground.gameObject.SetActive(true);
            _middleBackground.gameObject.SetActive(false);
            _maxBackground.gameObject.SetActive(false);
        }

        if (ratio > SMALL_RATIO && ratio <= MAX_RATIO)
        {
            _backgroundCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;

            _minBackground.gameObject.SetActive(false);
            _smallBackground.gameObject.SetActive(false);
            _middleBackground.gameObject.SetActive(true);
            _maxBackground.gameObject.SetActive(false);
        }

        if (ratio > MAX_RATIO)
        {
            _backgroundCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;

            _minBackground.gameObject.SetActive(false);
            _smallBackground.gameObject.SetActive(false);
            _middleBackground.gameObject.SetActive(false);
            _maxBackground.gameObject.SetActive(true);
        }

        Debug.Log($"Ratio = {ratio}");

        _lastRatio = ratio;
    }

    #endregion
}