using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] public float mainthrust;
    [SerializeField] public float rotationthrust;
    [SerializeField] AudioClip mainEngine;

    AudioSource audioSource;
    public Rigidbody rb;

    [SerializeField] ParticleSystem mainThruster;
    [SerializeField] ParticleSystem sideThrusterLeft;
    [SerializeField] ParticleSystem sideThrusterRight;

     [SerializeField] GameObject thrustbutton;
     [SerializeField] GameObject leftbutton;
     [SerializeField] GameObject rightbutton;


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
        if (thrustbutton.tag == "Selected") // Check if interactable is selected
        {
            rb.AddRelativeForce(Vector3.up * mainthrust * Time.deltaTime);
            mainThruster.Play();
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (leftbutton.tag == "Selected")
        {
            ApplyRotation(rotationthrust);
            sideThrusterRight.Play();
        }
        else if (rightbutton.tag == "Selected")
        {
            sideThrusterLeft.Play();
            ApplyRotation(-rotationthrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //freezing rotation so physics system can take over
    }

}
