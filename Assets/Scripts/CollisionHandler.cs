using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] int loadLevelDelay = 3;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip finishAudio;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem finishParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || collisionDisabled) {return;}
        
        switch(other.gameObject.tag)
        {
            case "Friendly": 
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
        //add animation for landing
        isTransitioning = true;
        finishParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(finishAudio);
        DisableMovement();
        Invoke("LoadNextLevel", loadLevelDelay);
    }

    void StartCrashSequence()
    {
        //add particle effect on crash
        isTransitioning = true;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
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



