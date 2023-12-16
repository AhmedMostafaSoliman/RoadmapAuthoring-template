using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BerryInteraction : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectEntered.AddListener(HandleGrab);
    }

    private void HandleGrab(XRBaseInteractor interactor)
    {
        // Check the tag to provide specific feedback
        if (gameObject.CompareTag("GoodBerry"))
        {
            Debug.Log("Good berry (ttikwtkwt) picked: " + gameObject.name);
        }
        else if (gameObject.CompareTag("WormyBerry"))
        {
            Debug.Log("Wormy berry (skksalaʔq) picked: " + gameObject.name);
        }

        // Disable the berry object (to simulate picking it)
        gameObject.SetActive(false);
    }
}


