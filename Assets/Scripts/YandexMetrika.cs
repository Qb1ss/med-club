using UnityEngine;

public class YandexMetrika : MonoBehaviour
{
    public void OnStartGameStatistic()
    {
        Application.ExternalEval("ym(98757139,'reachGoal','start_game')");
    }

    public void OnSuccessQuizStatistic()
    {
        Application.ExternalEval("ym(98757139,'reachGoal','success_game')");
    }

    public void OnFailQuizStatistic()
    {
        Application.ExternalEval("ym(98757139,'reachGoal','fail_game')");
    }
}