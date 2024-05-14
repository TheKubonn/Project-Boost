using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrust = 100f; // Creating a float for the speed of rocket to go up
    [SerializeField] private float rotationThrust = 1f; // Creating a float for the speed of rocket rotating
    [SerializeField] private AudioClip mainEngine;
    
    private Rigidbody _rigidbody; // Creating a reference to our rigidbody on the rocket
    private AudioSource _audioSource; // Creating a reference to our audiosource
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>(); // Initializing a RigidBody into our new variable
        _audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space)) // If we are pressing and holding space key
        {
            // We're adding a releative force which is Vector3.up (0,1,0) and multiply by our float and deltatime
            _rigidbody.AddRelativeForce(Vector3.up * (mainThrust * Time.deltaTime));
            if (!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            _audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A)) // If we are pressing and holding A key
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D)) // If we are pressing and holding D key
        {
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true; // Freezing rotation, so we can manually rotate
        // We're rotating our object using Vector3.forward (0,0,1) and multiply by our float and deltatime
        transform.Rotate(Vector3.forward * (rotationThisFrame * Time.deltaTime));
        _rigidbody.freezeRotation = false; // Unfreezing rotation so the physics system can take over
    }
}
