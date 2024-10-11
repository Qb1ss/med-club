using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform), typeof(GridLayoutGroup))]
public class CartGridAddaptiving : MonoBehaviour
{
    #region CONSTS

    private const float CART_SCALE_MULTYPLY_RATIO = 0.7f;

    private const int VERTICAL_COLUMN_VALUE = 2;
    private const int HORIZONTAL_COLUMN_VALUE = 4;

    #endregion

    private RectTransform _rectTransform = null;
    private GridLayoutGroup _gridLayoutGroup = null;
    private Camera _camera = null;


    #region UNITY

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();

        _camera = Camera.main;
    }

    private void Start()
    {
        GridUpdate(0);
    }

    private void OnEnable()
    {
        AppLogic.OnSideUpdate.AddListener(GridUpdate);
    }

    #endregion

    #region PRIVATE METHODS

    private void GridUpdate(float ratio)
    {
        float newYPosition = (_rectTransform.rect.size.y - _gridLayoutGroup.spacing.y) / VERTICAL_COLUMN_VALUE;
        float newXPosition = newYPosition * CART_SCALE_MULTYPLY_RATIO;

        _gridLayoutGroup.constraintCount = VERTICAL_COLUMN_VALUE;

        if (newXPosition * _gridLayoutGroup.constraintCount + _gridLayoutGroup.spacing.x > _rectTransform.rect.size.x)
        {
            newXPosition = (_rectTransform.rect.size.x - _gridLayoutGroup.spacing.x) / VERTICAL_COLUMN_VALUE;
            newYPosition = newXPosition / CART_SCALE_MULTYPLY_RATIO;
        }

        if (_rectTransform.rect.size.y * _gridLayoutGroup.constraintCount <= _rectTransform.rect.size.x)
        {
            _gridLayoutGroup.constraintCount = HORIZONTAL_COLUMN_VALUE;

            newXPosition = (_rectTransform.rect.size.x - (_gridLayoutGroup.spacing.x * (HORIZONTAL_COLUMN_VALUE - 1))) / HORIZONTAL_COLUMN_VALUE;
            newYPosition = newXPosition / CART_SCALE_MULTYPLY_RATIO;

            if (newYPosition >= _rectTransform.rect.size.y)
            {
                newYPosition = _rectTransform.rect.size.y;
                newXPosition = newYPosition * CART_SCALE_MULTYPLY_RATIO;
            }
        }

        _gridLayoutGroup.cellSize = new Vector2(newXPosition, newYPosition);
    }

    #endregion
}
