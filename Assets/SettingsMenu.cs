using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;
    [SerializeField] TMP_Dropdown resolutionDropDown;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;
    private List<Resolution> filteredResolutions;

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen= isFullscreen;
    }

    public void SetResolution(int currentResolutionIndex)
    {
        Resolution resolution = resolutions[currentResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    private void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropDown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (!filteredResolutions.Any(x => x.width == resolutions[i].width && x.height == resolutions[i].height))
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();

        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string option = filteredResolutions[i].width + " x " + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (filteredResolutions[i].width == Screen.width &&
                filteredResolutions[i].height == Screen.height &&
                filteredResolutions[i].refreshRate == currentRefreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }
}
