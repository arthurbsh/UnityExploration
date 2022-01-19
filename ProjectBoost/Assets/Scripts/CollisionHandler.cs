using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float restartDelay = 1.5f;

    [SerializeField] float nextLevelDelay = 1.5f;

    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;


    bool isTransitioning = false;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other)
    {

        if (isTransitioning) {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "LaunchPad":
                Debug.Log("Hit launch pad");
                break;

            case "LandingPad":
                StartSuccessSequence();
                break;

            case "Obstacle":
                StartCrashSequence();
                
                break;
            default:
                break;
        }
    }

    private void StartSuccessSequence() {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.clip = null;
        audioSource.PlayOneShot(success);
        Invoke("LoadNextLevel", nextLevelDelay);
    }

    private void StartCrashSequence() {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.clip = null;
        audioSource.PlayOneShot(crash);
        Invoke("RestartLevel", restartDelay);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextLevel()
    {

        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
            
    }
}
