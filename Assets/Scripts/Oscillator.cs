using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 _startingPos;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] [Range(0,1)] private float movementFactor;
    void Start()
    {
        _startingPos = transform.position;
    }
    
    void Update()
    {
        Vector3 offset = movementVector * movementFactor;
        transform.position = _startingPos + offset;
    }
}
