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

    private void Start()
    {
        rbRocket = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ProcessThrust();
        ProcessRotation();  
    }

    private void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            LeftRotation();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RightRotation();
        }
        else
        {
            StopRotate();
        }
    }

    private void StartThrusting()
    {
        rbRocket.AddRelativeForce(Vector3.up * upThrust * Time.deltaTime);
        if (!mainThrustParticles.isPlaying && !audioSource.isPlaying)
        {
            mainThrustParticles.Play();
            audioSource.PlayOneShot(mainEngineAudio);
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticles.Stop();
    }

    private void StopRotate()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    private void LeftRotation()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    private void RightRotation()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rbRocket.freezeRotation = true; //freeze rotation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rbRocket.freezeRotation = false;
    }
}
