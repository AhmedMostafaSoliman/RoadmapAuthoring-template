using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BasketStrawberries : MonoBehaviour
{
    [Header("Berries")]
    public GameObject PlayerBerry;
    public GameObject DropBerry;
    public GameObject BerryBadge;
    public GameObject SalmonBadge;
    public GameObject BerryGreatWorkShowText;

    [Header("Berry assign things")]
    public PlayerScript player;
    private float radius = 3.5f;

     


    private void Awake()
    {
        PlayerBerry.SetActive(false);
        DropBerry.SetActive(false);
        SalmonBadge.SetActive(false);
    }

    public void DroppingBerries()
    {
        
            
        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            PlayerBerry.SetActive(false);
            DropBerry.SetActive(true);
            BerryBadge.SetActive(true);
            BerryGreatWorkShowText.SetActive(true);
            delayTimeNew();
        }


    }

    public async void delayTimeNew()
    {
        await Task.Delay((int)(5f * 1000));
        BerryGreatWorkShowText.SetActive(false);

    }
}