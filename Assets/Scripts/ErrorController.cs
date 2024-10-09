using UnityEngine;
using UnityEngine.SceneManagement;

public class ErrorController : MonoBehaviour
{
    #region CONSTS

    private const string LINT_TO_SITE = "https://pro-srk.ru/?utm_source=busco&utm_medium=game&utm_campaign=all";

    #endregion


    #region PUBLIC METHODS

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LinkToSite()
    {
        Application.OpenURL(LINT_TO_SITE);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion
}
