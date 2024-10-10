using UnityEngine;
using UnityEngine.Events;

public class GameMenu : MonoBehaviour
{
    #region EVENTS

    public static UnityEvent OnStartGame = new UnityEvent();

    #endregion

    [Header("COMPONENTS")]
    [Tooltip("Yandex metrika")]
    [SerializeField] private YandexMetrika _metrika = null;


    #region PUBLIC METHODS

    public void GameStarting()
    {
        OnStartGame.Invoke();

        _metrika.OnStartGameStatistic();
    }

    #endregion
}