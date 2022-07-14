using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // whenever we want to change sth in the scene, we need to add this unity engine
using UnityEngine.UI;

public class ControlButton : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Experiment is running");
        Scene scene = SceneManager.GetActiveScene();
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        if (sceneNumber == 0)   // sceneNumber=0: Instructions scene
        {
            Debug.Log(scene.name);      // Just to see the scene name in console
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   // load the Milgram experiment
        }
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
