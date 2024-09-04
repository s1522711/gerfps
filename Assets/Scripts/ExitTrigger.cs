using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ExitTrigger.OnTriggerEnter()");
        Debug.Log("ExitTrigger.OnTriggerEnter() - other.tag: " + other.tag);
        if (other.tag == "Player")
        {
            Debug.Log("ExitTrigger.OnTriggerEnter() - Player entered exit trigger");
            // play sound and wait for it to finish
            audioSource.Play();
            audioSource.mute = false;
            //Application.Quit();
            StartCoroutine(WaitForSound());
        }
    }

    IEnumerator WaitForSound()
    {
        // wait for sound to finish
        yield return new WaitForSeconds(audioSource.clip.length);

        // quit game
        //Application.Quit();
        ExitToMenu.ExitGame();
    }
}
