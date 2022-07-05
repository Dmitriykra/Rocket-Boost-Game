using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    float delayTime = 2f;
    [SerializeField] AudioClip succcess;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem succcessParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransition = false;
    bool collisionDisable = false;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheatLoadLevel();
    }
   

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    
    void CheatLoadLevel(){
        if(Input.GetKeyDown(KeyCode.L)){
            Debug.Log("L pressed");
            Invoke("LoadNewLevel", delayTime);
        } else if (Input.GetKeyDown(KeyCode.C)){
            collisionDisable = !collisionDisable;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(isTransition || collisionDisable){
            return;
        }
        switch (other.gameObject.tag) {
                case "Finish":
                StartSuccessSequences(); 
                break;

                case "Frendly":
                Debug.Log("Frendly");
                break;

                default:
                StartCrashSequences();
                break;
            
        }
    }

    private void StartSuccessSequences()
    {
        succcessParticles.Play(true);
        isTransition = true;
        audioSource.Stop();
        audioSource.PlayOneShot(succcess);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNewLevel", delayTime);
        
    }

    void StartCrashSequences(){
        crashParticles.Play(true);
        isTransition = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadMethod", delayTime);
        
    }


    void LoadNewLevel(){
        int currentSceneindex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneInd = currentSceneindex + 1;
        if(nextSceneInd == SceneManager.sceneCountInBuildSettings){
            nextSceneInd = 0;
        }
        SceneManager.LoadScene(nextSceneInd);  
    
    }
    void ReloadMethod() {
        int currentSceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneindex);         
    }
    
}
