using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private int levelLoadDelay = 2;
    [SerializeField] private AudioClip successAudio;
    [SerializeField] private AudioClip crashAudio;
    [SerializeField] private ParticleSystem successParticles;
    [SerializeField] private ParticleSystem crashParticles;
    
    private AudioSource _audioSource;
    private DebugCheats _debugCheats;

    private bool _isTransitioning = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _debugCheats = GetComponent<DebugCheats>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isTransitioning || _debugCheats.collisionDisabled) { return; }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly object");
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    private void StartCrashSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(crashAudio);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    private void StartFinishSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(successAudio);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }
    
    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
