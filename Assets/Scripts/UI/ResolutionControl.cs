using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionControl : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    
    private List<Resolution> filteredResolutions;
    private int currentResolutionIndex = 0;

    void Start()
    {
        UpdateResolutionOptions();
    }

    void Update()
    {
        // Verifique se a resolução da tela foi alterada em tempo real
        if (Screen.resolutions.Length != filteredResolutions.Count)
        {
            UpdateResolutionOptions();
        }
    }

    void UpdateResolutionOptions()
    {
        filteredResolutions = new List<Resolution>();
        Resolution[] resolutions = Screen.resolutions;

        // Obtenha a resolução atual da tela
        Resolution currentResolution = Screen.currentResolution;

        foreach (Resolution res in resolutions)
        {
            if (res.width == currentResolution.width && res.height == currentResolution.height)
            {
                // Adicione a resolução atual, independentemente da taxa de atualização
                filteredResolutions.Add(res);
            }
            else if (res.width != currentResolution.width || res.height != currentResolution.height)
            {
                // Adicione outras resoluções com diferentes larguras e alturas
                filteredResolutions.Add(res);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height;
            options.Add(resolutionOption);

            if (filteredResolutions[i].width == currentResolution.width && filteredResolutions[i].height == currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Atualiza as opções da lista suspensa
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex >= 0 && resolutionIndex < filteredResolutions.Count)
        {
            Resolution resolution = filteredResolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, true);
        }
        else
        {
            Debug.LogWarning("Tentativa de definir uma resolução inválida.");
        }
    }
}