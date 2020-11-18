using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]   // This just disallows attachment of components other than this script to the obstacle  
public class Oscillate : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;  

    [Range(0, 1)] [SerializeField] float movementFactor;

    [SerializeField] float period = 2f; //setting the time for one cycle of oscillation 

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)  // avoiding period to be zero, we didn't directly write period==0f because == operator is unpredictable for floating point numbers 
        {
            return;
        }

        float cycles = Time.time/period;
        float angleInRad = 2 * Mathf.PI * cycles;

        movementFactor = Mathf.Sin(angleInRad);  // range -1 to 1
        movementFactor = movementFactor / 2f + 0.5f;  // to make its range from 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
