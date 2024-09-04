using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToMenu : MonoBehaviour
{
    public KeyCode exitKey = KeyCode.Escape;
    public bool enableExitKey = true;

    void Start()
    {
        #if UNITY_EDITOR
            exitKey = KeyCode.Keypad0;
        #endif
        Debug.Log("ExitToMenu.Start() - exitKey: " + exitKey);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"pressed code: {Input.inputString}");
        if (Input.GetKeyDown(exitKey) && enableExitKey)
        {
            ExitGame();
        }
    }

    public static void ExitGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenu");
    }
}
