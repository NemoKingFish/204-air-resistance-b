using UnityEngine;

public class Airplane : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float enginePower = 50f;
    [SerializeField] float liftBooster = 0.5f;
    [SerializeField] float drag = 0.003f;
    [SerializeField] float angularDrag = 0.03f;

    [SerializeField] float yawPower = 50f;
    [SerializeField] float pitchPower = 50f;
    [SerializeField] float rollPower = 30f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //1. Engine Trust (Engine Power)
        //Pressing Spacebar applies force in the forward disrection of the airplane(tranform.forward).
        //Simulates engine thrust, making the airplane accelerate forward.
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.forward * enginePower);
        }

        //2. Lift Force - Simulates how airplane agin altitude
        Vector3 lift = Vector3.Project(rb.linearVelocity, transform.forward);
        rb.AddForce(transform.up * lift.magnitude * liftBooster);

        //3.Drag (Air Resistance) - Prevents infinite acceleration
        rb.linearDamping = rb.linearVelocity.magnitude * drag;
        rb.angularDamping = rb.linearVelocity.magnitude * angularDrag;

        //4.Rotation Controls - Pitch, Yaw, and Roll
        float yaw = Input.GetAxis("Horizontal") * yawPower;   // Left/right (A/D)
        float pitch = Input.GetAxis("Vertical") * pitchPower; // Noes Up/Down (W/S)
        float roll = Input.GetAxis("Roll") * rollPower;       // Roll (Q/E)

        rb.AddTorque(transform.up * yaw);        // Yaw (Trun Left/right)
        rb.AddTorque(transform.right * pitch);   // Pitch (Nose up/down)
        rb.AddTorque(transform.forward * roll);  // Roll (Tilting)
    }
}
