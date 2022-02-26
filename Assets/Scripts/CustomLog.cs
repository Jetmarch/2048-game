using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLog : MonoBehaviour
{
    
    string log;
    Queue logQueue = new Queue();
    void Start()
    {
        Debug.Log("Log");
        Debug.Log("Log2");
    }

    private void OnEnable()
    {
        Application.logMessageReceived += Application_logMessageReceived;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= Application_logMessageReceived;
    }

    private void Application_logMessageReceived(string logString, string stackTrace, LogType type)
    {
        log = logString;

        string nStr = "\n [" + type + "] : " + log;
        logQueue.Enqueue(nStr);
        if (type == LogType.Exception)
        {
            nStr = "\n" + stackTrace;
            logQueue.Enqueue(nStr);
        }
        log = string.Empty;
        foreach(string logItem in logQueue)
        {
            log += logItem;
        }
    }


    private void OnGUI()
    {
        GUILayout.Label(log);
    }
}
