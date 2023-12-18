using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 1;
    float distanceTravelled;

    private Animator _animator;

    public InputData _inputData;


    void Awake()
    {
        _animator = GetComponent<Animator>();
        _inputData = FindObjectOfType<InputData>();
    }

    void Update()
    {
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
        {
            _animator.SetBool("Walk", true);
        }

        if (_animator.GetBool("Walk"))
        {
            distanceTravelled += speed * Time.deltaTime;
            if (distanceTravelled >= pathCreator.path.length)
            {
                _animator.SetBool("Walk", false);
            }
            else {
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            }
        }

    }

}
