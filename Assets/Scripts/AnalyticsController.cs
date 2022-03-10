using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class AnalyticsController : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        await UnityServices.InitializeAsync();
    }
    
    public void GameWinAnalyticsEvent()
    {
        Dictionary<string, object> param = new Dictionary<string, object>();

        Events.CustomData("GameWin", param);

        Events.Flush();
    }

    public void GameLoseAnalyticsEvent()
    {
        Dictionary<string, object> param = new Dictionary<string, object>();

        Events.CustomData("GameLose", param);

        Events.Flush();
    }

    public void BonusTileCreatedAnalyticsEvent()
    {
        Dictionary<string, object> param = new Dictionary<string, object>();

        Events.CustomData("BonusCreated", param);

        Events.Flush();
    }

}
