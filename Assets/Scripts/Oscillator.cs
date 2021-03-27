using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    
    float movementFactor;

    void Start()
    {
        startPosition = transform.position;       
    }

    void Update()
    {
        if(period <= Mathf.Epsilon) {return;}
        float cycles = Time.time / period;  // Continually growing over time
        const float tau = Mathf.PI * 2f;    
        float rawSinWave = Mathf.Sin(cycles * tau); // from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; //recalculate to 0 to 1 so it's cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;
    }
}
