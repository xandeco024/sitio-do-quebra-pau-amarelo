using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private GameObject settingsCanvas;

    void Start()
    {
        settingsCanvas = GetComponent<GameObject>();
    }

    void Update()
    {
        
    }

    void BackB()
    {
        settingsCanvas.SetActive(false);
    }
}
