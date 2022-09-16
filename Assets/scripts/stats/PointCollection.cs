using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDE;

public class PointCollection : MonoBehaviour
{

    public event System.Action CollectedPoints;
    public int CurrentPoints { get; private set; }
    public GameEventPure listener;

    private void Start()
    {
        listener.OnRaised += (object data) =>
        {
            int points = (int)data;
            CurrentPoints += Mathf.Abs(points);

            CollectedPoints?.Invoke();
        };
    }

    private void OnDestroy()
    {
        CollectedPoints.RemoveAllListeners();
    }

}
