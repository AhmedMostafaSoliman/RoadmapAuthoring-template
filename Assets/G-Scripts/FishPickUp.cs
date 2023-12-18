using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FishPickUp : MonoBehaviour
{
    [Header("Fish/Salmon")]
    public GameObject PlayerSpear;
    public GameObject PlayerFish;
    public GameObject Spear;
    public GameObject Fish;
    public GameObject SalmonBadge;
    public GameObject FishInBasket;
    public GameObject GroundSpear;
    public GameObject showText4;




    [Header("Fish Pick")]
    public PlayerScript player;
    private float radius1 = 2.5f;



    private void Awake()
    {
        PlayerSpear.SetActive(false);
        PlayerFish.SetActive(false);
        SalmonBadge.SetActive(false);
        FishInBasket.SetActive(false);
        GroundSpear.SetActive(false);
    }

    public void pickingSpearFish()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < radius1)
        {
            PlayerSpear.SetActive(true);
            PlayerFish.SetActive(true);
            Spear.SetActive(false);
            Fish.SetActive(false);
            showText4.SetActive(true);
            delayTimeNew1();
        }

    }

    public async void delayTimeNew1()
    {
        await Task.Delay((int)(10f * 1000));
        showText4.SetActive(false);

    }
}
