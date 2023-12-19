using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BearScript : MonoBehaviour
{
    [Header("Berry assign things")]
    public PlayerScript player;
    private float radius1 = 2.5f;

    public GameObject BearShowText;
    public GameObject BearBadge;
    public GameObject showText3;

    [SerializeField] private AudioSource bearAudio;

    private void Awake()
    {
        //BearBadge.SetActive(false);
    }

    public void BearMethod()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < radius1)
        {
            
            BearShowText.SetActive(true);
            delayTimeNew();
            showText3.SetActive(true);
            delayTimeNew1();
            bearAudio.Play();
        }

    }

    public async void delayTimeNew()
    {
        await Task.Delay((int)(5f * 1000));
        BearShowText.SetActive(false);
        BearBadge.SetActive(true);
        
    }

    public async void delayTimeNew1()
    {
        await Task.Delay((int)(10f * 1000));
        showText3.SetActive(false);

    }
}
