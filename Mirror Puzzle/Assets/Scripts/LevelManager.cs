using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject canvas;

    Scene currentScene;

    string sceneName;

    bool soundReady = true;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene ();
 
        sceneName = currentScene.name;
    }

    void ShowCanvas(){
        canvas.SetActive(true);
    }

    void HideCanvas(){
        canvas.SetActive(false);
    }

    public void LevelUpProcess(){
        if(soundReady){
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayWinSound();
            soundReady = false;
        }
        StartCoroutine(LevelUp(2f));
        
    }

    IEnumerator LevelUp(float delay){
        ShowCanvas();

        yield return new WaitForSeconds(delay);

        HideCanvas();

        GoToNextScene();

        soundReady = true;

        yield return null;
    }

    void GoToNextScene(){
        if(sceneName == "Level1"){
            SceneManager.LoadScene("Level2", LoadSceneMode.Single);
        }
        else if(sceneName == "Level2"){
            SceneManager.LoadScene("Level3", LoadSceneMode.Single);
        }
        else if(sceneName == "Level3"){
            SceneManager.LoadScene("Win", LoadSceneMode.Single);
        }
    }
}
