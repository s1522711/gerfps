using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuHandler : MonoBehaviour
{
    public Slider displaySlider;
    public TextMeshProUGUI DisplaySelectorText;
    public Toggle VSyncToggle;
    public GameObject MainMenuCanvas;
    public GameObject RestartPopupCanvas;
    private bool readyToChangeDisplay = true;
    private int oldDisplayIndex;

    // Start is called before the first frame update
    void Start()
    {
        // set the display selector slider value to the current selected display index
        displaySlider.value = PlayerPrefs.GetInt("UnitySelectMonitor", 0);
        // set the max and min values of the display selector slider
        displaySlider.maxValue = Display.displays.Length - 1;
        displaySlider.minValue = 0;

        // set the VSync toggle value to the current VSync value
        VSyncToggle.isOn = PlayerPrefs.GetInt("VSync", 0) == 1;

        // update the ui elements
        DisplaySelectorText.SetText("Display: " + displaySlider.value.ToString());
        VSyncToggle.GetComponentInChildren<Text>().text = VSyncToggle.isOn ? "Onononon" : "OffOffOffOff";

        // make sure the restart popup is hidden
        RestartPopupCanvas.SetActive(false);
    }

    public void OnDisplaySliderValueChanged()
    {
        // Get the selected display index from the slider value
        int selectedDisplayIndex = Mathf.RoundToInt(displaySlider.value);
    
        // Update the display selector text
        DisplaySelectorText.SetText("Display: " + selectedDisplayIndex.ToString());

        oldDisplayIndex = PlayerPrefs.GetInt("UnitySelectMonitor", 0);

        // Switch the game to the selected display
        SwitchDisplay(selectedDisplayIndex);
    }

    public void SwitchDisplay(int displayIndex)
    {
        // Ensure the selected display index is within the valid range
        if (displayIndex >= 0 && displayIndex < Display.displays.Length)
        {
            // Save the selected display index in player prefs
            PlayerPrefs.SetInt("UnitySelectMonitor", displayIndex);
            PlayerPrefs.Save();
            if (readyToChangeDisplay)
            {
                RestartPopupCanvas.SetActive(true);
            }
            readyToChangeDisplay = true;
        }
        else
        {
            Debug.LogWarning("Invalid display index: " + displayIndex);
        }
    }

    // Handle the VSync toggle value change
    public void OnVSyncToggleValueChanged()
    {
        // set the VSync value in the player prefs
        PlayerPrefs.SetInt("VSync", VSyncToggle.isOn ? 1 : 0);
        // set the VSync value in the game
        QualitySettings.vSyncCount = VSyncToggle.isOn ? 1 : 0;
        // set the vsync value in player prefs
        PlayerPrefs.SetInt("VSync", VSyncToggle.isOn ? 1 : 0);
        // update the Vsync checkbox label
        VSyncToggle.GetComponentInChildren<Text>().text = VSyncToggle.isOn ? "Onononon" : "OffOffOffOff";
    }

    // Handle the back button click
    public void OnBackButtonClick()
    {
        // disable the settings menu canvas
        gameObject.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    public void OnRestartConfirmButtonClick()
    {
        // Restart the game
        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe")); //new program Application.Quit(); //kill current process
        Application.Quit();
    }

    public void OnRestartCancelButtonClick()
    {
        // Close the restart popup
        RestartPopupCanvas.SetActive(false);
        readyToChangeDisplay = false;
        // Reset the display selector slider value to the current selected display index
        displaySlider.value = oldDisplayIndex;
        // Update the display selector text
        DisplaySelectorText.SetText("Display: " + displaySlider.value.ToString());
    }
}
