using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PickUpBerries : MonoBehaviour
{
    [Header("Berries")]
    public GameObject PlayerBerry;
    public GameObject PickupBerry1;
    public GameObject BerryBadge;
    public GameObject SalmonBadge;
    public GameObject BearBadge;
    public GameObject BitterRootyBadge;
    public GameObject GroundSpear;
    public GameObject showText2;


    [Header("Berry assign things")]
    public PlayerScript player;
    private float radius1 = 2.5f;
    


    private void Awake()
    {
        PlayerBerry.SetActive(false);
        BerryBadge.SetActive(false);
        BearBadge.SetActive(false);
        BitterRootyBadge.SetActive(false);
        GroundSpear.SetActive(false);
    }

    public void pickingBerries()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < radius1)
        {
            PlayerBerry.SetActive(true);
            PickupBerry1.SetActive(false);
            showText2.SetActive(true);
            delayTimeNew();
        }
       
    }

    public async void delayTimeNew()
    {
        await Task.Delay((int)(10f * 1000));
        showText2.SetActive(false);

    }

}
