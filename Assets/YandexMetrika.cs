using UnityEngine;

public class YandexMetrika : MonoBehaviour
{
    public void GameStatistic()
    {
        Application.ExternalEval("ym(98589995, 'reachGoal', 'start_game')");
    }
}