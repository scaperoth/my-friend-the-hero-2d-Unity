using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{                       // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = false;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;


    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector3 targetVelocity)
    {
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // If the input is moving the player right and the player is facing left...
        if (targetVelocity.x > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (targetVelocity.x < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    public Vector3 MoveTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Move(direction);
        return direction;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        Transform childTransform = transform.GetChild(0).transform;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = childTransform.localScale;
        theScale.x *= -1;
        childTransform.localScale = theScale;
    }
}