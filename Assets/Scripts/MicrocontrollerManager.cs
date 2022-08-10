using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[Serializable]
public class TemperatureSensors
{
    public static Color heated = Color.red;
    public static Color notHeated = Color.green;
    [SerializeField] public bool isHeaterOn;
    [SerializeField] public bool isHeated;
    [SerializeField] public GameObject button;
}
public class MicrocontrollerManager : MonoBehaviour
{
    public static Action<string, string> ToggleHeater;
    public static Action<string, string> UpdateStatus;
    [SerializeField] public List<TemperatureSensors> temperatureSensors;



    // Start is called before the first frame update
    void Start()
    {
        ToggleHeater += ToggleHeaters;
        UpdateStatus += UpdateHeatStatus;
    }

    public void ToggleHeaters(string index, string isHeater)
    {
        if (temperatureSensors != null)
        {
            temperatureSensors[int.Parse(index)].isHeaterOn = Convert.ToBoolean(isHeater);
            Debug.Log($"{index}: {temperatureSensors[int.Parse(index)].isHeaterOn}");
        }
    }

    private void UpdateHeatStatus(string index, string isHeated)
    {
        Image buttonImage = temperatureSensors[int.Parse(index)].button.GetComponent<Image>();
        buttonImage.color = Convert.ToBoolean(isHeated) ? TemperatureSensors.heated : TemperatureSensors.notHeated;
    }
    

    private void OnDisable()
    {
        ToggleHeater -= ToggleHeaters;
        UpdateStatus -= UpdateHeatStatus;
    }
}
