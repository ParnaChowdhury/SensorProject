using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public struct TemperatureSensors
{
    [SerializeField] private int index;
    [SerializeField] private bool isHeated;
}
public class MicrocontrollerManager : MonoBehaviour
{
    public static Action<string, string> ToggleHeater;
    [SerializeField] private List<TemperatureSensors> temperatureSensors;
    
    // Start is called before the first frame update
    void Start()
    {
        ToggleHeater += ToggleHeaters;
    }

    private void ToggleHeaters(string arg1, string s)
    {
        Debug.Log($"{arg1}:{s}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
