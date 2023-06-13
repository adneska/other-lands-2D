using System;
using System.Collections;
using UnityEngine;

public class EnemyShootingRange : MonoBehaviour
{
    private GameObject playerObject;
    private Transform aimGunEndPointTransform;
    private Transform aimGunStartPointTransform;
    private Transform aimTransform;

    [SerializeField] AudioSource ShootingSound;

    [SerializeField] private float lineOfSite;
    private float nextShootTime;
    private const float FIRE_RATE = .50f;
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
    private IEnumerator ReloadWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Reload();
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

                ShootingSound.Play();
            }
            clipAmmo--;
        }
        else
        {
            float reloadDelay = 2f;
            StartCoroutine(ReloadWithDelay(reloadDelay));
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
