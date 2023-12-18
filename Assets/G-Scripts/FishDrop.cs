using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FishDrop : MonoBehaviour
{
    [Header("Fish/Salmon")]
    public GameObject PlayerSpear;
    public GameObject PlayerFish;
    public GameObject Spear;
    public GameObject Fish;
    public GameObject SalmonBadge;
    public GameObject FishInBasket;
    public GameObject GroundSpear;
    public GameObject FishShowText;


    [Header("Fish Pick")]
    public PlayerScript player;
    private float radius = 3.5f;




    private void Awake()
    {
        PlayerSpear.SetActive(false);
        PlayerFish.SetActive(false);
        
    }

    public void DroppingFish()
    {


        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            PlayerSpear.SetActive(true);
            PlayerFish.SetActive(false);
            FishInBasket.SetActive(true);
            SalmonBadge.SetActive(true);
            FishShowText.SetActive(true);
            delayTimeNew();
        }

    }

    public async void delayTimeNew()
    {
        await Task.Delay((int)(5f * 1000));
        GroundSpear.SetActive(true);
        PlayerSpear.SetActive(false);
        FishShowText.SetActive(false);
    }
}
