using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyShootingRange : MonoBehaviour
{
    private GameObject playerObject;
    private Transform aimGunEndPointTransform;
    private Transform aimGunStartPointTransform;
    private Transform aimTransform;



    [SerializeField] private float lineOfSite;
    private float nextShootTime;
    private const float FIRE_RATE = .30f;
    private int clipAmmo = 15, maxClipAmmo = 15;

    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 gunStartPointPosition;
    }

    public event EventHandler<OnShootEventArgs> OnShoot;
    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }


    void FixedUpdate()
    {
        aimTransform = transform.Find("Aim");        
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        aimGunStartPointTransform = aimTransform.Find("GunStartPointPosition");

        float distanceFromPlayer = Vector2.Distance(playerObject.transform.position, transform.position);
        if (distanceFromPlayer < lineOfSite) {
            HandleShoot();                     
        }        
    }
    private void Reload()
    {
        clipAmmo = maxClipAmmo;
    }
    private void HandleShoot()
    {
        
        if (clipAmmo > 0)
        {            
            if (Time.time >= nextShootTime)
            {
                nextShootTime = Time.time + FIRE_RATE;
                OnShoot?.Invoke(this, new OnShootEventArgs
                {
                    gunStartPointPosition = aimGunStartPointTransform.position,
                    gunEndPointPosition = aimGunEndPointTransform.position,
                }); 
            }
            clipAmmo--;
        }
        else
        {
            Invoke(nameof(Reload), 2);
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
