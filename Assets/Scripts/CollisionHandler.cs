using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float leveLoadDelay = 2f;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;
    
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
  
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start() 
    {
       audioSource = GetComponent <AudioSource>();
    }
   
    void Update() 
    {
       
       RespondToDebugKeys();
       
   

          

    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
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
        
      

        if(isTransitioning || collisionDisabled)
        {
            return;
        }
    

        else
        {

            switch (other.gameObject.tag)
            {
                case "Friendly":
                    break;

                case "Finish":
                    StartSuccessSequence();
                    break;

                default:
                    //Load current level
                    StartCrashSequence();
                    break;
            }
        }
    }

    void StartSuccessSequence()
    {
       
            isTransitioning = true;
            GetComponent<Movement>().enabled = false;
            audioSource.Stop();
            audioSource.PlayOneShot(successSound);
            successParticles.Play();
           
            Invoke("LoadNextLevel", leveLoadDelay);
        
    }

    void StartCrashSequence()
    {
        
            isTransitioning = true;
            GetComponent<Movement>().enabled = false;
            audioSource.Stop();
            audioSource.PlayOneShot(crashSound,0.4F);
            crashParticles.Play();
            Invoke("ReloadLevel", leveLoadDelay);

    }

        
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);

    }
     void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
