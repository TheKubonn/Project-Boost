using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelDelayTime = 2f;
    
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        // to do - add SFX upon crash
        // to do - add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().enabled = false;
        Invoke(nameof(ReloadLevel), levelDelayTime);
    }

    private void StartSuccessSequence()
    {
        // to do - add SFX upon crash
        // to do - add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().enabled = false;
        Invoke(nameof(NextLevel), levelDelayTime);
    }
    
    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 1;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
