using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Abilities : MonoBehaviour
{
   [SerializeField] private GameObject scarabPrefab;

   [Header("Scarab Settings")]
   [SerializeField] private int scarabCount = 2;
   [SerializeField] private float scarabCooldown = 5f;
   private float scarabCooldownTimer = 0f;
   [SerializeField] private float scarabSpawnSpeed = 0.5f;
   [SerializeField] private Transform scarabSpawnPoint;
   [SerializeField] private bool canSpawnScarab = true;
    void Update()
    {
        if(scarabCooldownTimer > 0f)
        {
            scarabCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Z) && CanSpawnScarab())
        {
            StartCoroutine(SpawnScarabCoroutine());
        }
    }



    private void spawnScarab()
    {
       if (scarabPrefab == null)
       {
           Debug.LogWarning("Abilities: No scarab prefab assigned!");
           return;
       }


       Instantiate(scarabPrefab, scarabSpawnPoint.position, scarabSpawnPoint.rotation);
       Debug.Log("Scarab spawned!");
    }

    private bool CanSpawnScarab()
    {
        return scarabCooldownTimer <= 0f && canSpawnScarab;
    }

       private IEnumerator SpawnScarabCoroutine()
       {
        scarabCooldownTimer = scarabCooldown;
           for (int i = 0; i < scarabCount; i++)
           {
               spawnScarab();
               yield return new WaitForSeconds(scarabSpawnSpeed);
           }
              
       }
   }

