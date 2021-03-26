using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float upThrust = 1000f;
    [SerializeField] float rotationThrust = 150f;
    [SerializeField] AudioClip mainEngineAudio;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;

    Rigidbody rbRocket;
    AudioSource audioSource;

    void Start()
    {
        rbRocket = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();  
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rbRocket.AddRelativeForce(Vector3.up * upThrust * Time.deltaTime);
            if(!mainThrustParticles.isPlaying && !audioSource.isPlaying)
            {
                mainThrustParticles.Play();
                audioSource.PlayOneShot(mainEngineAudio);
            }
        } 
        else
        {
            audioSource.Stop();
            mainThrustParticles.Stop();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if (!rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            if (!leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Play();
            }
        }
        else 
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rbRocket.freezeRotation = true; //freeze rotation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rbRocket.freezeRotation = false;
    }
}
