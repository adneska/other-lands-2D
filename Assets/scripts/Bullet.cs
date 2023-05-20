using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Health health;
    
    public void Setup(Vector3 shootDir)
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        float moveSpeed = 10f;
        rigidbody2D.AddForce(shootDir * moveSpeed, ForceMode2D.Impulse);

        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Physics Hit Detection
        Target target = collider.GetComponent<Target>();
        if (target != null)
        {
            // Hit a Target  
            if(collider.gameObject.tag == "Enemy")
            {
                collider.gameObject.GetComponent<Health>().Damage();
                Destroy(gameObject);
            } else if (collider.gameObject.tag == "Player")
            {
                collider.gameObject.GetComponent<PlayerHealth>().Damage();
            }
            Destroy(gameObject);
        }
    }
    private static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
    
}
