using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform pfBullet;
    private void Awake()
    {
        GetComponent<PlayerAimWeapon>().OnShoot += PlayerShooting_OnShoot;
    }

    private void PlayerShooting_OnShoot(object sender, PlayerAimWeapon.OnShootEventArgs e)
    {
        Transform bulletTransform = Instantiate(pfBullet, e.gunEndPointPosition, Quaternion.identity);
        
        Vector3 shootDir = (e.gunEndPointPosition - e.gunStartPointPosition).normalized;
        
        bulletTransform.GetComponent<Bullet>().Setup(shootDir);        
    }
}
