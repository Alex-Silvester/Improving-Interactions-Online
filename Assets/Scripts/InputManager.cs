using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    Vector2 velocity = Vector2.zero;
    Vector2 overallAcceleration = Vector2.zero;

    [Header("Variable Values")]
    [SerializeField] Vector2 Acceleration;
    [SerializeField] float accelerationMagnitude;
    [SerializeField] Vector2 Velocity;
    [SerializeField] float velocityMagnitude;
    [SerializeField] bool playerMoving;


    [Header("Torso Settings")]
    [SerializeField] float torsoPullForce = 5f;
    [SerializeField] float torsoMass = 1f;
    Vector2 torsoAcceleration = Vector2.zero;
    float torsoAccelerationMagnitude = 0;
    [SerializeField] bool torsoMovesPlayer = false;

    [Header("Leg Settings")]
    [SerializeField] float legPullForce = 5f;
    [SerializeField] float legMass = 1f;
    Vector2 legAcceleration = Vector2.zero;
    float legAccelerationMagnitude = 0;

    [Header("Other Settings")]
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float breakMultiplier = 5f;
    [SerializeField] float threshold = 0.1f;

    bool moving;

    [Header("GameObjects")]
    [SerializeField] Rigidbody playerRigidbody;
    [SerializeField] GameObject torso;
    [SerializeField] GameObject legs;
    [SerializeField] GameObject playerCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        torsoAccelerationMagnitude = torsoPullForce / torsoMass;
        legAccelerationMagnitude = legPullForce / legMass;

        playerCamera.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        updateVariableView();

        torsoAccelerationMagnitude = torsoPullForce / torsoMass;
        legAccelerationMagnitude = legPullForce / legMass;

        overallAcceleration = (torsoMovesPlayer ? torsoAcceleration : Vector3.zero) + legAcceleration;

        if (velocity.magnitude >= maxSpeed)
        {
            velocity = velocity.normalized*(maxSpeed - threshold);
        }

        if (velocity.magnitude < maxSpeed && moving)
        { 
            velocity += overallAcceleration * Time.deltaTime;
        }
        else if (velocity.magnitude < threshold)
        {
            velocity = Vector2.zero;
        }
        else if (!moving)
        {
            Vector2 direction = new Vector2(Mathf.Sign(playerRigidbody.linearVelocity.x), Mathf.Sign(playerRigidbody.linearVelocity.z));
            velocity -= direction * Time.deltaTime * breakMultiplier;
        }


        if (overallAcceleration.magnitude < threshold)
        {
            overallAcceleration = Vector2.zero;
        }



        playerRigidbody.linearVelocity = Vec2toVec3(velocity);
    }

    public void OnTorsoMove(InputValue input)
    {
        Vector2 direction = input.Get<Vector2>();

        torsoAcceleration = direction.normalized * torsoAccelerationMagnitude;

        torso.transform.LookAt((torsoAcceleration.magnitude == 0 ? Vector3.forward : Vec2toVec3(torsoAcceleration).normalized) + torso.transform.position, Vector3.up);

        if (!torsoMovesPlayer) return;

        if((torsoAcceleration + legAcceleration).magnitude > 0f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    public void OnLegsMove(InputValue input)
    {
        Vector2 direction = input.Get<Vector2>();

        legAcceleration = direction.normalized * legAccelerationMagnitude;

        legs.transform.LookAt((legAcceleration.magnitude == 0 ? Vector3.forward : Vec2toVec3(legAcceleration).normalized) + legs.transform.position, Vector3.up);

        if (((torsoMovesPlayer ? torsoAcceleration : Vector3.zero) + legAcceleration).magnitude > 0f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    public void OnTorsoInteract()
    {
        torso.GetComponent<torsoController>().torsoInteract();
    }

    private Vector3 Vec2toVec3(Vector2 velocity)
    {
        return new Vector3(velocity.x, 0, velocity.y);
    }

    private void updateVariableView()
    {
        Acceleration = overallAcceleration;
        Velocity = velocity;

        accelerationMagnitude = Acceleration.magnitude;
        velocityMagnitude = Velocity.magnitude;

        playerMoving = moving;
    }

    public void dropBox(bool closeText = false)
    {
        if(torso.GetComponent<torsoController>().holdingBox)
            torso.GetComponent<torsoController>().dropBox(closeText);
    }
}
