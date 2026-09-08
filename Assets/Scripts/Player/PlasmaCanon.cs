using UnityEngine;
using UnityEngine.InputSystem;

public class PlasmaCanon : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject  projectilePrefabs;

    [Header("Ammo Settings")]
    [SerializeField] private int ammo = 200;

    [Header("Fire Rate Settings")]
    [SerializeField] private float shootingRate = 0.5f;
    private float nextFireTime = 0f;

    [Header("Instantiation Points")]
    [SerializeField]private Transform firePoint1;
    [SerializeField]private Transform firePoint2;

    public int Ammo => ammo; // Read-only public access if needed by UI

    void Update()
    { 
        // Use GetButton for automatic fire; swap to GetButtonDown for single-shot
        if (Input.GetButton("Fire2") && CanFire()&& SettingsManager.instance.shotType == true)
        {
            Fire();
        }
        else if(Keyboard.current.spaceKey.wasPressedThisFrame && CanFire() && SettingsManager.instance.shotType == false)
        {
            Fire(); 
        }
        
       
    }

    private bool CanFire()
    {
        return ammo > 0 && Time.time >= nextFireTime;
    }

    private void Fire()
    {
        if (projectilePrefabs.[0] == null)
        {
            Debug.LogWarning("PlasmaCanon: No projectile prefab assigned!");
            return;
        }

        ammo--;
        nextFireTime = Time.time + shootingRate;

        //Instantiation
        Instantiate(projectilePrefabs.[0],firePoint1.position, firePoint1.rotation);
        Instantiate(projectilePrefabs.[0],firePoint2.position,firePoint2.rotation);

        Debug.Log($"Fired! Ammo remaining: {ammo}");

        if (ammo <= 0)
        {
            Debug.Log("Out of ammo!");
            // OnOutOfAmmo?.Invoke(); // Uncomment if using an event
        }
    }

    

    // Optional: call this to reload
    public void Reload(int amount)
    {
        ammo += amount;
    }
}