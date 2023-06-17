using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField] private Transform Bar;
    [SerializeField] GameObject WeaponPF;

    public event EventHandler OnDead;
    private void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        healthSystem.OnDead += HealthSystem_OnDead;
    }
    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        Bar.localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }
    private void Start()
    {
        HealthSystem healthSystem = new HealthSystem(100);
        Setup(healthSystem);
    }
    public void Damage()
    {
        int damageAmount = UnityEngine.Random.Range(5, 20);
        healthSystem.Damage(damageAmount);
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        Transform parentTransform = transform.parent;
        // Dead! Destroy self
        if (OnDead != null) OnDead(this, EventArgs.Empty);
        Destroy(parentTransform.gameObject);
        UIHandler.score += 10;
        EnemySpawnHandler.DecreaseEnemyCount();
        GameObject GetAmmo = Instantiate(WeaponPF, Bar.position, Bar.rotation);
    }
}
