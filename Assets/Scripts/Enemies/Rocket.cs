using UnityEngine;
//This is a super class that should not be used on any objects. Its in the back managing classes
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Rocket : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField]private float acceleration = 15f;
    [SerializeField]private float maxSpeed = 8f;
    [SerializeField]private float rotationSpeed = 10f;
    [SerializeField]private float linearDrag = 1f;
    [SerializeField]private float sidewaysDamping = 3f;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        HandleThrust();
        ClampSpeed();
        KillSidewaysVelocity();
    }

    void HandleThrust()
    {
        rb.AddForce(transform.up * acceleration);
    }

    void ClampSpeed()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void KillSidewaysVelocity()
    {
        Vector2 forward = transform.up;
        Vector2 right = transform.right;

        float forwardSpeed = Vector2.Dot(rb.linearVelocity, forward); // Calculate the forward speed by taking the dot product of the linear velocity and the forward direction.
        float sidewaysSpeed = Vector2.Dot(rb.linearVelocity, right);

        rb.linearVelocity = forward * forwardSpeed + right * sidewaysSpeed * (1f - sidewaysDamping * Time.fixedDeltaTime);
    }

    private void HandleRotation()
    {
        //Not in use currently no valid pathing system.
    }
    //Look for a decent pathing system for 2D when u get 
    // The rotation handling will work with the pathing system


}