using UnityEngine;
using UnityEngine.UI;

public enum AddaptiveSide
{
    Horizontal = 0, 
    Vertical = 1
}

[RequireComponent(typeof(RectTransform), typeof(GridLayoutGroup))]
public class CartGridAddaptiving : MonoBehaviour
{
    #region CONSTS

    private const float CART_SCALE_MULTYPLY = 0.7f;

    #endregion

    [Header("PARAMETERS")]
    [Tooltip("Addaprive side")]
    [SerializeField] private AddaptiveSide _addaptiveSide = AddaptiveSide.Horizontal;

    private float _ratio = 0f;

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
        GridUpdate(_ratio);
    }

    private void OnEnable()
    {
        AppLogic.OnSideUpdate.AddListener(GridUpdate);
    }

    #endregion

    #region PRIVATE METHODS

    private void GridUpdate(float ratio)
    {
        if (_addaptiveSide == AddaptiveSide.Vertical)
        {
            _ratio = ratio;

            float newYPosition = _rectTransform.rect.size.y / 2 - _gridLayoutGroup.spacing.y;
            float newXPosition = newYPosition * CART_SCALE_MULTYPLY;

            _gridLayoutGroup.cellSize = new Vector2(newXPosition, newYPosition);
        }
    }

    #endregion
}
