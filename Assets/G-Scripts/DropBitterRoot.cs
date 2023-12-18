using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DropBitterRoot : MonoBehaviour
{
    [Header("Berries")]
    public GameObject PlayerBitterRoot;
    public GameObject DropingBitterRoot;
    public GameObject BitterRootyBadge;
    public GameObject BitterRootShowText;

    [Header("Berry assign things")]
    public PlayerScript player;
    private float radius = 3.5f;




    private void Awake()
    {
        PlayerBitterRoot.SetActive(false);
        DropingBitterRoot.SetActive(false);
    }

    public void DroppingBitterRoots()
    {


        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            PlayerBitterRoot.SetActive(false);
            DropingBitterRoot.SetActive(true);
            BitterRootyBadge.SetActive(true);
            BitterRootShowText.SetActive(true);
            delayTimeNew();
        }


    }

    public async void delayTimeNew()
    {
        await Task.Delay((int)(5f * 1000));
        BitterRootShowText.SetActive(false);

    }
}
