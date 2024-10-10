using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum AppState
{
    Menu = 0, 
    Quiz = 1, 
    End = 2
}

public class Addaptive : MonoBehaviour
{
    #region CONSTS

    private const float MIN_MENU_RATIO = 100f;
    private const float MIDDLE_MENU_RATIO = 120;
    private const float MAX_MENU_RATIO = 145f;

    private const float MIN_QUIZ_RATIO = 100f;

    private const float MIN_END_MENU_RATIO = 90f;
    private const float MAX_END_MENU_RATIO = 150f;

    private const float MIN_BACKGROUND_RATIO = 75f; ///v1 = 55 | v2 = 75 | v3.1 = 57 | v3.2 = 75
    private const float SMALL_BACKGROUND_RATIO = 100f; 
    private const float MIDDLE_BACKGROUND_RATIO = 150f;
    private const float MAX_BACKGROUND_RATIO = 177f; ///v1-2 = 180 | v3.1 = 177 | v3.2 = nn

    private const float MIN_ERROR_RATIO = 75f;
    private const float SMALL_ERROR_RATIO = 100f;
    private const float MIDDLE_ERROR_RATIO = 150f;
    private const float MAX_ERROR_RATIO = 150f;

    #endregion

    #region EVENTS

    public static UnityEvent<float> OnSideUpdate = new UnityEvent<float>();

    #endregion

    [Header("COMPONENTS")]
    [Header("Menu")]
    [Tooltip("Thiin menu")]
    [SerializeField] private GameObject _thinMenu = null;
    [Tooltip("Min menu")]
    [SerializeField] private GameObject _minMenu = null;
    [Tooltip("Middle menu")]
    [SerializeField] private GameObject _middleMenu = null;
    [Tooltip("Max menu")]
    [SerializeField] private GameObject _maxMenu = null;

    [Header("Quiz")]
    [Tooltip("Min quiz")]
    [SerializeField] private GameObject _minQuiz = null;
    [Tooltip("Max quiz")]
    [SerializeField] private GameObject _maxQuiz = null;

    [Header("End game menu")]
    [Tooltip("Min end game menu")]
    [SerializeField] private GameObject _minEndGameMenu = null;
    [Tooltip("Middle end game menu")]
    [SerializeField] private GameObject _middleEndGameMenu = null;
    [Tooltip("Max end game menu")]
    [SerializeField] private GameObject _maxEndGameMenu = null;

    [Header("Background")]
    [Tooltip("Min background")]
    [SerializeField] private GameObject _minBackground = null;
    [Tooltip("Small background")]
    [SerializeField] private GameObject _smallBackground = null;
    [Tooltip("Middle background")]
    [SerializeField] private GameObject _middleBackground = null;
    [Tooltip("Max background")]
    [SerializeField] private GameObject _maxBackground = null;

    [Header("Error")]
    [Tooltip("Min background")]
    [SerializeField] private GameObject _minError = null;
    [Tooltip("Small background")]
    [SerializeField] private GameObject _smallError = null;
    [Tooltip("Middle background")]
    [SerializeField] private GameObject _middleError = null;
    [Tooltip("Max background")]
    [SerializeField] private GameObject _maxError = null;
    [Space(height: 5f)]

    [Header("Background Image")]
    [Tooltip("Menu sprite")]
    [SerializeField] private Sprite _menuSprite = null;
    [Tooltip("Menu sprite rotate")]
    [SerializeField] private Sprite _menuRotateSprite = null;
    [Space(height: 3f)]

    [Tooltip("Quiz sprite")]
    [SerializeField] private Sprite _quizSprite = null;
    [Tooltip("Quiz sprite rotate")]
    [SerializeField] private Sprite _quizRotateSprite = null;
    [Space(height: 3f)]

    [Tooltip("End sprite")]
    [SerializeField] private Sprite _endSprite = null;
    [Tooltip("End sprite rotate")]
    [SerializeField] private Sprite _endRotateSprite = null;

    [Header("Canvases")]
    [Tooltip("Canvas background")]
    [SerializeField] private Canvas _backgroundCanvas = null;
    [Tooltip("Canvas error")]
    [SerializeField] private Canvas _errorCanvas = null;

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

    #region PUBLIC METHODS

    public void ImageBackgroundChange(AppState appState)
    {
        if (appState == AppState.Menu)
        {
            _minBackground.GetComponent<Image>().sprite = _menuRotateSprite;
            _smallBackground.GetComponent<Image>().sprite = _menuRotateSprite;
            _middleBackground.GetComponent<Image>().sprite = _menuSprite;
            _maxBackground.GetComponent<Image>().sprite = _menuSprite;
        }
        else if (appState == AppState.Quiz)
        {
            _minBackground.GetComponent<Image>().sprite = _quizRotateSprite;
            _smallBackground.GetComponent<Image>().sprite = _quizRotateSprite;
            _middleBackground.GetComponent<Image>().sprite = _quizSprite;
            _maxBackground.GetComponent<Image>().sprite = _quizSprite;
        }
        else if (appState == AppState.End)
        {
            _minBackground.GetComponent<Image>().sprite = _endRotateSprite;
            _smallBackground.GetComponent<Image>().sprite = _endRotateSprite;
            _middleBackground.GetComponent<Image>().sprite = _endSprite;
            _maxBackground.GetComponent<Image>().sprite = _endSprite;
        }
    }

    #endregion

    #region PRIVATE METHODS

    private void AddaptiveApp()
    {
        float ratio = (float)_camera.pixelWidth / (float)_camera.pixelHeight * 100;

        if (_lastRatio == ratio)
        {
            return;
        }
        else
        {
            MenuAddaptiving(ratio);
            QuizAddaptiving(ratio);
            EndGameMenuAddaptiving(ratio);
            BackgroundAddaptiving(ratio);
            ErrorAddaptiving(ratio);

            OnSideUpdate?.Invoke(ratio);

            Debug.Log($"Ratio = {ratio}");

            _lastRatio = ratio;
        }
    }

    private void MenuAddaptiving(float ratio)
    {
        if (ratio <= MIN_MENU_RATIO)
        {
            _thinMenu.gameObject.SetActive(true);
            _minMenu.gameObject.SetActive(false);
            _middleMenu.gameObject.SetActive(false);
            _maxMenu.gameObject.SetActive(false);
        }

        if (ratio > MIN_MENU_RATIO && ratio <= MIDDLE_MENU_RATIO)
        {
            _thinMenu.gameObject.SetActive(false);
            _minMenu.gameObject.SetActive(true);
            _middleMenu.gameObject.SetActive(false);
            _maxMenu.gameObject.SetActive(false);
        }

        if (ratio > MIDDLE_MENU_RATIO && ratio <= MAX_MENU_RATIO)
        {
            _thinMenu.gameObject.SetActive(false);
            _minMenu.gameObject.SetActive(false);
            _middleMenu.gameObject.SetActive(true);
            _maxMenu.gameObject.SetActive(false);
        }

        if (ratio > MAX_MENU_RATIO)
        {
            _thinMenu.gameObject.SetActive(false);
            _minMenu.gameObject.SetActive(false);
            _middleMenu.gameObject.SetActive(false);
            _maxMenu.gameObject.SetActive(true);
        }
    }

    private void QuizAddaptiving(float ratio)
    {
        if (ratio <= MIN_QUIZ_RATIO)
        {
            _minQuiz.gameObject.SetActive(true);
            _maxQuiz.gameObject.SetActive(false);
        }

        if (ratio > MIN_QUIZ_RATIO)
        {
            _minQuiz.gameObject.SetActive(false);
            _maxQuiz.gameObject.SetActive(true);
        }
    }

    private void EndGameMenuAddaptiving(float ratio)
    {
        if (ratio <= MIN_END_MENU_RATIO)
        {
            _minEndGameMenu.gameObject.SetActive(true);
            _middleEndGameMenu.gameObject.SetActive(false);
            _maxEndGameMenu.gameObject.SetActive(false);
        }

        if (ratio > MIN_END_MENU_RATIO && ratio <= MAX_END_MENU_RATIO)
        {
            _minEndGameMenu.gameObject.SetActive(false);
            _middleEndGameMenu.gameObject.SetActive(true);
            _maxEndGameMenu.gameObject.SetActive(false);
        }

        if (ratio > MAX_END_MENU_RATIO)
        {
            _minEndGameMenu.gameObject.SetActive(false);
            _middleEndGameMenu.gameObject.SetActive(false);
            _maxEndGameMenu.gameObject.SetActive(true);
        }
    }

    private void BackgroundAddaptiving(float ratio)
    {
        if (ratio <= MIN_BACKGROUND_RATIO)
        {
            _backgroundCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;

            _minBackground.gameObject.SetActive(true);
            _smallBackground.gameObject.SetActive(false);
            _middleBackground.gameObject.SetActive(false);
            _maxBackground.gameObject.SetActive(false);
        }

        if (ratio > MIN_BACKGROUND_RATIO && ratio <= SMALL_BACKGROUND_RATIO)
        {
            _backgroundCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;

            _minBackground.gameObject.SetActive(false);
            _smallBackground.gameObject.SetActive(true);
            _middleBackground.gameObject.SetActive(false);
            _maxBackground.gameObject.SetActive(false);
        }

        if (ratio > SMALL_BACKGROUND_RATIO && ratio <= MAX_BACKGROUND_RATIO)
        {
            _backgroundCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;

            _minBackground.gameObject.SetActive(false);
            _smallBackground.gameObject.SetActive(false);
            _middleBackground.gameObject.SetActive(true);
            _maxBackground.gameObject.SetActive(false);
        }

        if (ratio > MAX_BACKGROUND_RATIO)
        {
            _backgroundCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;

            _minBackground.gameObject.SetActive(false);
            _smallBackground.gameObject.SetActive(false);
            _middleBackground.gameObject.SetActive(false);
            _maxBackground.gameObject.SetActive(true);
        }
    }

    private void ErrorAddaptiving(float ratio)
    {
        if (ratio <= MIN_ERROR_RATIO)
        {
            _errorCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;

            _minError.gameObject.SetActive(true);
            _smallError.gameObject.SetActive(false);
            _middleError.gameObject.SetActive(false);
            _maxError.gameObject.SetActive(false);
        }

        if (ratio > MIN_ERROR_RATIO && ratio <= SMALL_ERROR_RATIO)
        {
            _errorCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;

            _minError.gameObject.SetActive(false);
            _smallError.gameObject.SetActive(true);
            _middleError.gameObject.SetActive(false);
            _maxError.gameObject.SetActive(false);
        }

        if (ratio > SMALL_ERROR_RATIO && ratio <= MAX_ERROR_RATIO)
        {
            _errorCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;

            _minError.gameObject.SetActive(false);
            _smallError.gameObject.SetActive(false);
            _middleError.gameObject.SetActive(true);
            _maxError.gameObject.SetActive(false);
        }

        if (ratio > MAX_ERROR_RATIO)
        {
            _errorCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;

            _minError.gameObject.SetActive(false);
            _smallError.gameObject.SetActive(false);
            _middleError.gameObject.SetActive(false);
            _maxError.gameObject.SetActive(true);
        }
    }

    #endregion
}