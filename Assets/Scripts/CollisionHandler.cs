using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] int loadLevelDelay = 3;

    private void OnCollisionEnter(Collision other)
    {

        switch(other.gameObject.tag)
        {
            case "Friendly": 
                Debug.Log("Hit Launchpad");
                break; 
            case "Finish":
                StartSuccesSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }  
    }

    void DisableMovement()
    {
        GetComponent<Movement>().enabled = false; 
    }
    private void StartSuccesSequence()
    {
        //Add sound effect on win
        //add animation for landing
        DisableMovement();
        Invoke("LoadNextLevel", loadLevelDelay);
    }

    void StartCrashSequence()
    {
        //Add sound effect on crash
        //add particle effect on crash
        DisableMovement();
        Invoke("ReloadCurrentLevel", loadLevelDelay);      
    }

    void LoadNextLevel()
    {  
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
            SceneManager.LoadScene(nextSceneIndex);      
    }
    void ReloadCurrentLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentSceneIndex);
    }
}


