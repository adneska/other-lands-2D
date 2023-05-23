using UnityEngine;
using System;
using UnityEngine.Rendering.Universal;

public class PlayerAimWeapon : MonoBehaviour
{
    private UIHandler handler;

    [SerializeField] private Transform pfBullet;
    [SerializeField] private Light2D weaponLight;
    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Transform aimGunStartPointTransform;
    PlayerInputActions Actions;

    private float nextShootTime;
    private const float FIRE_RATE = .15f;
    public int currentClip, maxClipSize, currentAmmo, maxAmmoSize;

    public event EventHandler<OnShootEventArgs> OnShoot;
    public event EventHandler<AmmoChangedEventArgs> OnAmmoChanged;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 gunStartPointPosition;
    }
    public class AmmoChangedEventArgs : EventArgs
    {
        public int updateCurrentClip, updateMaxClipSize, updateCurrentAmmo, updateMaxAmmoSize;
    }

    private void Awake()
    {
        currentClip = 15;
        maxClipSize = 15;
        currentAmmo = 90;
        maxAmmoSize = 90;
        UpdateAmmoTextUI();
        aimTransform = transform.Find("Aim");
        Actions = new PlayerInputActions();
        Actions.Player.Shoot.performed += ctx => HandleShoot();        
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        aimGunStartPointTransform = aimTransform.Find("GunStartPointPosition");
    }

    // Update is called once per frame
    private void Update()
    {
        if (!UIHandler.GameIsPaused) HandleAiming();
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
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
        weaponLight.transform.eulerAngles = new Vector3(0, 0, angle - 90); //меняем направление света (-90 чтобы светило вправо)
    }
    private void HandleShoot()
    {
        if (!UIHandler.GameIsPaused)
        {
            if (currentClip > 0)
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
                currentClip--;
            }
            UpdateAmmoTextUI();
        }        
    }
    public void Reload()
    {
        int reloadAmmount = maxClipSize - currentClip;
        reloadAmmount = (currentAmmo - reloadAmmount) >= 0 ? reloadAmmount : currentAmmo;
        currentClip += reloadAmmount;
        currentAmmo -= reloadAmmount;

        UpdateAmmoTextUI();
    }
    public void AddAmmo(int ammoAmount)
    {
        currentAmmo+= ammoAmount;
        if(currentAmmo > maxAmmoSize)
        {
            currentAmmo = maxAmmoSize;
        }
        UpdateAmmoTextUI();
    }

    private void UpdateAmmoTextUI()
    {
        OnAmmoChanged?.Invoke(this, new AmmoChangedEventArgs
        {
            updateCurrentClip = currentClip,
            updateMaxClipSize = maxClipSize,
            updateCurrentAmmo = currentAmmo,
            updateMaxAmmoSize = maxAmmoSize
        });
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    private void OnEnable()
    {
        Actions.Player.Enable();
    }
    private void OnDisable()
    {
        Actions.Player.Disable();
    }
}
