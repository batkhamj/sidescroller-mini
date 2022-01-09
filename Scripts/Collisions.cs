using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f; 
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool disableCollision = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        DebugKeys();
    }

    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            disableCollision = !disableCollision;
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || disableCollision)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                NextLevelSequence();
                break;
            default:
                CrashSequence();    
                break;
        }    
    }

    void CrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop(); 
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);   
    }
    void NextLevelSequence()
    {
        isTransitioning = true; 
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", delay);
    }
    void ReloadLevel()
    {   
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }
    void NextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;
        if(nextIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextIndex = 0; 
        }
        SceneManager.LoadScene(nextIndex);
    }
}
