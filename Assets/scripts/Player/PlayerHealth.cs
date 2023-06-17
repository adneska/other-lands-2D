using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField] private Transform Bar;
    [SerializeField] private UIHandler uihandler;

    public event EventHandler OnDead;
    public static int score;

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
        if (OnDead != null) OnDead(this, EventArgs.Empty);
        uihandler.GameOver();
    }
}
