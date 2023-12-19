using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BitterRoot : MonoBehaviour
{
    [Header("Bitter Root")]
    public GameObject PlayerBitterRoot;
    public GameObject PickupBitterRoot;
    public GameObject BitterRootyBadge;
    public GameObject showText1;


    [Header("Berry assign things")]
    public PlayerScript player;
    private float radius1 = 2.5f;

    public GameObject IntroShowText;


    private void Awake()
    {
        IntroShowText.SetActive(true);
        delayTimeNew2();
        //PlayerBitterRoot.SetActive(false);
        BitterRootyBadge.SetActive(false);
    }

    public void pickingBitterRoots()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < radius1)
        {
            
            
            PlayerBitterRoot.SetActive(true);
            PickupBitterRoot.SetActive(false);
            showText1.SetActive(true);
            delayTimeNew();
        }

    }

    public async void delayTimeNew()
    {
        await Task.Delay((int)(10f * 1000));
        showText1.SetActive(false);

    }

    public async void delayTimeNew2()
    {
        await Task.Delay((int)(10f * 1000));
        IntroShowText.SetActive(false);

    }
}
