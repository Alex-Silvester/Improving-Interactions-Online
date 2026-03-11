using UnityEngine;
using UnityEngine.InputSystem;

public class SpringCharacterController : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject otherPlayer = null;
    [SerializeField] float rotation;

    Vector3 direction = Vector3.zero;

    [SerializeField] bool classicControl = false;
    bool moving => direction.magnitude != 0 ;

    // Update is called once per frame

    void FixedUpdate()
    {
        if(classicControl)
        {
            float y_vel = rb.linearVelocity.y;
            rb.linearVelocity = direction * speed * Time.deltaTime + new Vector3(0, y_vel, 0);

            transform.LookAt(otherPlayer.transform);
            transform.Rotate(0, rotation, 0);
        }
        else if(moving)
        {
            transform.LookAt(direction + transform.position);

            float y_vel = rb.linearVelocity.y;
            rb.linearVelocity = transform.forward * speed * Time.deltaTime + new Vector3(0, y_vel, 0);
        }
    }

    public void OnMove(InputValue input)
    {
        Vector2 temp = input.Get<Vector2>();
        direction = new Vector3(temp.x, 0, temp.y);
    }
}
