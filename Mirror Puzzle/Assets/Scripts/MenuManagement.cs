using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
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
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
            audioSource.PlayOneShot(clickSound);
        }
    }
    public void StartGame(){
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        audioSource.PlayOneShot(clickSound);
    }
}
