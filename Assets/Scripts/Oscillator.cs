using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 _startingPos;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] /*[Range(0,1)]*/ private float movementFactor;
    [SerializeField] private float period = 2f;
    void Start()
    {
        _startingPos = transform.position;
    }
    
    void Update()
    {
        float cycles = Time.time / period; // continually growing over time
        
        const float tau = Mathf.PI * 2f; // constant value of 6.283 etc..
        float rawSinWave = Mathf.Sin(cycles * tau); // Going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so it's cleaner
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = _startingPos + offset;
    }
}
