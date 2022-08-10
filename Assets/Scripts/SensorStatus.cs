using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensorStatus : MonoBehaviour
{
    public static Action<bool> ReceiveHeatStatus;
    [SerializeField] private Color Heated;
    [SerializeField] private Color Cooled;

    private Image panelImage;
    
    // Start is called before the first frame update
    void Start()
    {
        panelImage = GetComponent<Image>();
        ReceiveHeatStatus += UpdatePanelColor;
    }

    private void UpdatePanelColor(bool isHeated)
    {
        panelImage.color = isHeated ? Heated : Cooled;
    }
}
