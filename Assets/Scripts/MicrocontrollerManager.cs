using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class TemperatureSensors
{
    [SerializeField] public bool isHeaterOn;
    [SerializeField] public bool isHeated;
}
public class MicrocontrollerManager : MonoBehaviour
{
    public static Action<string, string> ToggleHeater;
    [SerializeField] public List<TemperatureSensors> temperatureSensors;



    // Start is called before the first frame update
    void Start()
    {
        ToggleHeater += ToggleHeaters;
    }

    public void ToggleHeaters(string index, string isHeater)
    {
        if (temperatureSensors != null)
        {
            temperatureSensors[int.Parse(index)].isHeaterOn = Convert.ToBoolean(isHeater);
            Debug.Log($"{index}: {temperatureSensors[int.Parse(index)].isHeaterOn}");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
