using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Transform pfBullet;
    private void Awake()
    {
        GetComponent<EnemyShootingRange>().OnShoot += EnemyShooting_OnShoot;
    }
    private void EnemyShooting_OnShoot(object sender, EnemyShootingRange.OnShootEventArgs e)
    {
        Transform bulletTransform = Instantiate(pfBullet, e.gunEndPointPosition, Quaternion.identity);
        Vector3 shootDir = (e.gunEndPointPosition - e.gunStartPointPosition).normalized;
        bulletTransform.GetComponent<Bullet>().Setup(shootDir);
    }
}
