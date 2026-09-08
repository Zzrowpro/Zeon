
using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TrackingProjectile : Projectile
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform target;
    private Sprite notTargeting;
    private Sprite targeting;
    private bool isTargeting;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        Raycast();
    }

    void Update()
    {
        if (isTargeting)
        {
            spriteRenderer.sprite = targeting;
        }
        else
        {
            spriteRenderer.sprite = notTargeting;
        }
    }

    void FixedUpdate()
    {
        if(target != null)
        {
            Vector2 direction = target.position - transform.position;
            rb.linearVelocity = direction * bulletSpeed;
            isTargeting = true;
        }
        if(target == null)
        {
            isTargeting = false;
        }
    }

    private void Raycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        if (hit)
        {
            target = hit.transform;
        }
    }
}
