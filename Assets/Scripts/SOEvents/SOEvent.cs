using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOEvent", menuName = "Events")]
public class SOEvent : ScriptableObject
{
    private List<SOListener> listeners = new List<SOListener>();

    public void Raise()
    {
        for(int i = listeners.Count - 1; i >= 0; i-- )
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(SOListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(SOListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
