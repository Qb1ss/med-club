using UnityEngine;

public class Question : MonoBehaviour
{
    [SerializeField] private VarianButton[] _varianButtons = null;


    #region UNITY

    private void Start()
    {
        SettingRightVariant();

        VarianButton.OnSetAnswer.AddListener(SetAnswer);
    }

    #endregion

    #region PRIVATE METHODS

    ///установка правильного ответа
    private void SettingRightVariant()
    {
        int rightVarian = Random.Range(0, _varianButtons.Length); Debug.Log($"Right variant is {rightVarian}");

        foreach ( var button in _varianButtons)
        {
            button.IsRight = false;

            button.SettingColor();
        }

        _varianButtons[rightVarian].IsRight = true;
    }

    ///проверка правильности ответа
    private void SetAnswer(bool isRight)
    {
        Debug.Log(isRight);

        if (isRight == true)
        {
            SettingRightVariant();
        }
    }

    #endregion
}