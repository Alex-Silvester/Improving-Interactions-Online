using UnityEngine;
using UnityEngine.InputSystem;

public class SpringCharacterController : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject otherPlayer;
    [SerializeField] float rotation;

    Vector3 direction = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = direction * speed * Time.deltaTime;
        transform.LookAt(otherPlayer.transform);
        transform.Rotate(0, rotation, 0);
    }

    public void OnMove(InputValue input)
    {
        Vector2 temp = input.Get<Vector2>();
        direction = new Vector3(temp.x, 0, temp.y);
    }
}
