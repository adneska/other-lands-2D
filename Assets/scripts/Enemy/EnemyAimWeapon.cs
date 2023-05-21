using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimWeapon : MonoBehaviour
{
    [SerializeField] private Transform pfBullet;    
    private Transform aimTransform;
    private GameObject playerObject;


    private void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        aimTransform = transform.Find("Aim");        
    }
    private void Update()
    {
        HandleAiming();
    }
    private void HandleAiming()
    {
        Vector3 PlayerPosition = playerObject.transform.position;

        Vector3 aimDirection = (PlayerPosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 AimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            AimLocalScale.y = -1f;
        }
        else
        {
            AimLocalScale.y = +1f;
        }
        aimTransform.localScale = AimLocalScale;
    }
    
}
