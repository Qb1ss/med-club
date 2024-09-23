using UnityEngine;

public class EndGameController : MonoBehaviour
{
    #region CONTST

    private const string LINK_TO_SITE = "https://pharmznanie.ru/";

    #endregion

    #region PUBLIC METHODS

    public void LinkToSite()
    {
        Application.OpenURL(LINK_TO_SITE);
    }

    #endregion
}