using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuManagement : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip clickSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            audioSource.PlayOneShot(clickSound);
        }
    }
    public void ReturToMainMenu(){
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        audioSource.PlayOneShot(clickSound);
    }
}
