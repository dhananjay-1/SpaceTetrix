using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // SerializeField makes the data member editable through inspector and non-editable through other scripts
    // The value specified here for SerializeField is just the default value which will be shown on inspector
    [SerializeField] float mainThrust = 100f;  // thrust for flying up
    [SerializeField] float rcsThrust = 100f;   // thrust for rotation, rcs : rotation control system

    Rigidbody rigidbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Ok");
                break;
            case "Finish":
                print("Finish");
                break;
            default:
                print("dead");
                break;
        }
    }

    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float flyingSpeed = mainThrust;
                
            rigidbody.AddRelativeForce(Vector3.up * flyingSpeed);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true; // Disabling the physics of rotation while taking manual control just to provide more stability

        float rotationSpeed = rcsThrust * Time.deltaTime;  // Time.deltaTime gives the time for which previous frame remained and is a good estimator of current frame time span
                                                           // If frame time span is less, the rotationSpeed is less and is more for longer frame time span, this ensures frame rate independence

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        rigidbody.freezeRotation = false;  // Enabling the physics of rotation again 
    }
}
