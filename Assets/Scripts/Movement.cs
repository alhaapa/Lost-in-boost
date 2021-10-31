using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] ParticleSystem mainParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            StartThrusting();

        }

        else
        {
            StopThrusting();
        }

    }

     void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            LeftRotation();

        }

        else if (Input.GetKey(KeyCode.D))
        {
            RightRotation();

        }

        else
        {
            StopRotation();
        }


    }

  

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);


        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSound);
        }

        if (!mainParticles.isPlaying)
        {
            mainParticles.Play();
        }
    }

    void StopThrusting()
    {
        mainParticles.Stop();
        audioSource.Stop();
    }

    
    void LeftRotation()
    {
        ApplyRotation(rotationThrust);
        if (!rightParticles.isPlaying)
        {
            rightParticles.Play();
        }
    }

    void RightRotation()
    {
        ApplyRotation(-rotationThrust);
        if (!leftParticles.isPlaying)
        {
            leftParticles.Play();
        }
    }

    void StopRotation()
    {
        leftParticles.Stop();
        rightParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freeze rotation for manual rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreeze rotation
    }
     
}

 