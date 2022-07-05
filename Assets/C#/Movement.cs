using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem rocketJet;
    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;

    AudioSource audioData;
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
      
    }
    void ProcessThrust()
    {
      if(Input.GetKey(KeyCode.Space))
        {
            StartingThrust();
        }
        else
        {
            StopThrust();
        }
    }

    private void StopThrust()
    {
        audioData.Stop();
        rocketJet.Stop(true);
    }

    private void StartingThrust()
    {
        rocketJet.Play(true);
        if (!audioData.isPlaying)
        {
            audioData.PlayOneShot(mainEngine);
        }

        rb.AddRelativeForce(Vector3.up *
        mainThrust *
        Time.deltaTime);
    }

    void ProcessRotation()
    {
        RotateLeft();
        RotateRight();
    }

    private void RotateRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rightThrust.Play(true);
            transformToRotate(-rotationThrust);
        }
        else
        {
            rightThrust.Stop(true);
        }
    }

    private void RotateLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            leftThrust.Play(true);
            transformToRotate(rotationThrust);
        }
        else
        {
            leftThrust.Stop(true);
        }
    }

    private void transformToRotate(float rotationThisFrame)
    {
      //allow to manually rotate the rocket
      rb.freezeRotation = true;

      transform.Rotate(Vector3.forward * 
        rotationThisFrame * Time.deltaTime);

      rb.freezeRotation = false;  

    }
}
