using UnityEngine;
using UnityEngine.SceneManagement;

public class ErrorController : MonoBehaviour
{
    #region CONSTS

    private const string LINT_TO_SITE = "https://pharmznanie.ru/";

    #endregion


    #region PUBLIC METHODS

    public void LinkToRestartSite()
    {
        // Application.OpenURL(LINT_TO_SITE);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion
}
