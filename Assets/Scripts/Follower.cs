using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTravelled;

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
