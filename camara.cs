using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef = null;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    
    [SerializeField] [Range(0f, 90f)] float maxRotation = 50f;
    [SerializeField] [Range(0f, 6f)] float rotSpeed = 0.5f;

    public bool canSeePlayer;
    
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        radius = 50;
        angle = 140;
        playerRef = GameObject.FindGameObjectWithTag("Player"); if(playerRef == null) Debug.LogError("No se encontro objeto con la Tag 'Player'");
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        camRotation();
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
            if(canSeePlayer){Debug.Log("viendo al lobo");}
            else {Debug.Log("no se ve el lobo");}
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
    private void camRotation()
    {
       if(!canSeePlayer)
        {
            transform.rotation = Quaternion.Euler(0f,maxRotation * Mathf.Sin(Time.time * rotSpeed),0f);
        }
        else if(canSeePlayer)
        {           
            //transform.LookAt(playerRef.transform);
             var lookPos = playerRef.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);

            audioSource.Play();
        }
    }
}