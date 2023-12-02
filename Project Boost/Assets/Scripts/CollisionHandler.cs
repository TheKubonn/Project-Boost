using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelDelayTime = 2f;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip crashSound;

    [SerializeField] private ParticleSystem successParticle;
    [SerializeField] private ParticleSystem crashParticle;

    private AudioSource _audioSource;

    private bool _isTransitioning = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_isTransitioning)
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
    }

    private void StartCrashSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(crashSound);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), levelDelayTime);
    }

    private void StartSuccessSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(successSound);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
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
