using UnityEngine;

public class BerrySelector : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    GameObject selectedObject = hit.collider.gameObject;

                    if (selectedObject.tag == "GoodBerry")
                    {
                        Debug.Log("Good berry selected!");
                        // Implement what happens when a good berry is selected
                    }
                    else if (selectedObject.tag == "WormyBerry")
                    {
                        Debug.Log("Wormy berry selected! Avoid this.");
                        // Implement what happens when a wormy berry is selected
                    }
                }
            }
        }
    }
}
