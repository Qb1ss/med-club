using UnityEngine;
using UnityEngine.Events;

public class GameMenu : MonoBehaviour
{
    #region EVENTS

    public static UnityEvent OnStartGame = new UnityEvent();

    #endregion

    #region PUBLIC METHODS

    public void GameStarting()
    {
        OnStartGame.Invoke();
    }

    #endregion
}