using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // Include this if using TextMeshPro

public class BerrySelector : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public static int score = 0;

    // Reference to your text element (Assign this from the Inspector)
    public TextMeshPro scoreText;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectEntered.AddListener(HandleGrab);
    }

    private void HandleGrab(XRBaseInteractor interactor)
    {
        if (gameObject.CompareTag("GoodBerry"))
        {
            score++;
            UpdateScoreDisplay(); // Update the score display
        }

        gameObject.SetActive(false);
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
