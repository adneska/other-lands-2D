using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;
    [SerializeField] TMP_Dropdown resolutionDropDown;

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
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();


        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }
}
