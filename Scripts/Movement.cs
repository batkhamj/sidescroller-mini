using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 120f;
    [SerializeField] float rotationThrust = 1f; 
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
            StopRotation();
    }

    void StartThrust()
    {
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    void StopThrust()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }


    void RotateLeft()
    {
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
        ApplyRotation(rotationThrust);
    }

        void RotateRight()
    {
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
        ApplyRotation(-rotationThrust);
    }

    void StopRotation()
    {
        rightBooster.Stop();
        leftBooster.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freeze rotation so player manually rotates
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze rotation so physics takes over
    }
}
