using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // Include this if using TextMeshPro

public class BerrySelector : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public static int score = 0;

    // Reference to your text element (Assign this from the Inspector)
    public TextMeshPro scoreText;

    public GameObject basket;

    public AudioSource ErrorSound;
    public AudioSource CorrectSound;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectEntered.AddListener(HandleGrab);
        grabInteractable.onSelectExited.AddListener(HandleRelease);
    }

    private void HandleGrab(XRBaseInteractor interactor)
    {
        if (gameObject.CompareTag("WormyBerry"))
        {
            ErrorSound.Play();
            gameObject.SetActive(false);
        }
    }

    private void HandleRelease(XRBaseInteractor interactor)
    {
        if (gameObject.CompareTag("GoodBerry"))
        {
            // check if game object coordinates is within the basket coordinates
            if (gameObject.transform.position.x >= basket.transform.position.x - 0.8f && gameObject.transform.position.x <= basket.transform.position.x + 0.8f)
            {
                if (gameObject.transform.position.z >= basket.transform.position.z - 0.8f && gameObject.transform.position.z <= basket.transform.position.z + 0.8f)
                {
                    CorrectSound.Play();
                    score += 1;
                    UpdateScoreDisplay();
                    //gameObject.SetActive(false);
                }
            }
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
