using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrixLineComplete : MonoBehaviour
{
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] AudioClip successSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(successSound);
        successParticles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
