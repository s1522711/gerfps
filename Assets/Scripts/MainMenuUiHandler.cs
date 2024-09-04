using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{   
    public AudioSource bobuxSound;
    
    public string GameScene;
    public GameObject bobuxImage;
    [Range(1, 10)]
    public int bobuxImageDuration = 5;
    public GameObject settingsMenuCanvas;

    void Start()
    {
        // this function is called when the script is first loaded
        // it is used to initialize the script
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSync", 0) == 1 ? 1 : 0;
    }

    // the UiHandler class is attached to the main menu canvas
    // this class script is used to handle the main menu buttons
    public void PlayGame()
    {
        // this function is called when the play button is clicked
        // it loads the game scene
        SceneManager.LoadSceneAsync(GameScene);
    }

    public void QuitGame()
    {
        // this function is called when the quit button is clicked
        // it quits the game
        Application.Quit();
    }

    public void BobuxButton()
    {
        // this function is called when the bobux button is clicked
        // it plays the bobux sound and shows the bobux image for 5 seconds
        bobuxImage.SetActive(true);
        bobuxImage.GetComponent<ImageAnimation>().Func_PlayUIAnim();
        StartCoroutine(HideBobuxImage());
        bobuxSound.Play();
    }

    IEnumerator HideBobuxImage()
    {
        // this function is used to hide the bobux image after 5 seconds
        yield return new WaitForSeconds(bobuxImageDuration);
        bobuxImage.SetActive(false);
        bobuxImage.GetComponent<ImageAnimation>().Func_StopUIAnim();
    }

    // Handle the settings button click
    public void OnSettingsButtonClick()
    {
        // disable the settings menu canvas
        gameObject.SetActive(false);
        settingsMenuCanvas.SetActive(true);
    }
}
