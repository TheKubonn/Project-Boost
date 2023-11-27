using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Movement : MonoBehaviour
{
    // Tworzymy Rigidbody o nazwie _rb
    private Rigidbody _rb;
    private AudioSource _audioSource;
    [SerializeField] private float mainThrust = 1000f;
    [SerializeField] private float rotationThrust = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        // Wraz z zaczęciem gry inicjalizujemy go
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Vector3 to vector składający się z 3 liczb, oraz posiada oba: kierunek i magnitude
            _rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!_audioSource.isPlaying)
            { 
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.D)))
        {
            ApplyRotation(rotationThrust);
        }

        if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.A)))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        _rb.freezeRotation = true;  // Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rb.freezeRotation = false; // Unfreezing rotation so the physics system can take over
    }
}
