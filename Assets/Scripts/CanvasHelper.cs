using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

namespace TechSettings.CanvasHelper
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public class CanvasHelper : MonoBehaviour
    {
        #region CONST

        private const string NAME_SAFE_AREA = "SafeArea";

        #endregion

        #region EVENT

        public static UnityEvent OnResolutionOrOrientationChanged = new UnityEvent();

        #endregion

        private static ScreenOrientation _lastOrientation = ScreenOrientation.LandscapeLeft;

        private static bool _screenChangeVarsInitialized = false;

        private static List<CanvasHelper> _helpers = new List<CanvasHelper>();

        private static Vector2 _lastResolution = Vector2.zero;
        private static Rect _lastSafeArea = Rect.zero;

        private Canvas _canvas;
        private RectTransform _rectTransform;
        private RectTransform _safeAreaTransform;


        #region MONO

        private void Awake()
        {
            if (!_helpers.Contains(this))
            {
                _helpers.Add(this);
            }

            _canvas = GetComponent<Canvas>();
            _rectTransform = GetComponent<RectTransform>();

            _safeAreaTransform = transform.Find(NAME_SAFE_AREA) as RectTransform;

            if (!_screenChangeVarsInitialized)
            {
                _lastOrientation = Screen.orientation;
                _lastResolution.x = Screen.width;
                _lastResolution.y = Screen.height;
                _lastSafeArea = Screen.safeArea;

                _screenChangeVarsInitialized = true;
            }

            ApplySafeArea();
        }


        private void OnDestroy()
        {
            if (_helpers != null && _helpers.Contains(this))
            {
                _helpers.Remove(this);
            }
        }

        #endregion

        private void Update()
        {
            if (_helpers[0] != this)
            {
                return;
            }

            if (Application.isMobilePlatform && Screen.orientation != _lastOrientation)
            {
                OrientationChanged();
            }

            if (Screen.safeArea != _lastSafeArea)
            {
                SafeAreaChanged();
            }

            if (Screen.width != _lastResolution.x || Screen.height != _lastResolution.y)
            {
                ResolutionChanged();
            }
        }

        #region Private Methods

        private void ApplySafeArea()
        {
            if (_safeAreaTransform == null)
            {
                return;
            }

            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= _canvas.pixelRect.width;
            anchorMin.y /= _canvas.pixelRect.height;
            anchorMax.x /= _canvas.pixelRect.width;
            anchorMax.y /= _canvas.pixelRect.height;

            _safeAreaTransform.anchorMin = anchorMin;
            _safeAreaTransform.anchorMax = anchorMax;
        }



        private static void OrientationChanged()
        {
            _lastOrientation = Screen.orientation;
            _lastResolution.x = Screen.width;
            _lastResolution.y = Screen.height;

            OnResolutionOrOrientationChanged.Invoke();
        }


        private static void ResolutionChanged()
        {
            _lastResolution.x = Screen.width;
            _lastResolution.y = Screen.height;

            OnResolutionOrOrientationChanged.Invoke();
        }


        private static void SafeAreaChanged()
        {
            _lastSafeArea = Screen.safeArea;

            for (int i = 0; i < _helpers.Count; i++)
            {
                _helpers[i].ApplySafeArea();
            }
        }

        #endregion
    }
}