using UnityEngine;

// Apply force class
public class ApplyForce : MonoBehaviour
{
    [Header("Strength of the applied force")]
    [Range(0.0f, 10.0f)]
    public float maxDistance = 0.0f;

    [Header("Object to repel")]
    public GameObject objectToRepel;

    [Header("Draw force line")]
    public bool draw = false;

    [Header("Apply forces")]
    public bool applyForces = false;

    [Header("Object to repel active")]
    public bool repelObject = true;

    [Header("Force strength")]
    [Range(0.0f, 100.0f)]
    public float forceStrength = 10.0f;

    [Header("Force direction")]
    [Range(-1.0f, 1.0f)]
    public float xDirection = 0.0f;
    [Range(-1.0f, 1.0f)]
    public float yDirection = 0.0f;
    [Range(-1.0f, 1.0f)]
    public float zDirection = 0.0f;

    // The rigidbody
    protected new Rigidbody rigidbody;

    // Direction of the force
    private Vector3 forceDirection;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If the object exsists
        if (objectToRepel && repelObject)
        {
            // The force multiplier
            Vector3 heading = transform.position - objectToRepel.transform.position;
            float forceMultiplier = maxDistance - Vector3.Distance(transform.position, objectToRepel.transform.position);

            // If the force is greater than 0 -  add relative force
            if (forceMultiplier > 0) rigidbody.AddRelativeForce(heading.normalized * forceMultiplier, ForceMode.Acceleration);
            if (draw) Debug.DrawLine(gameObject.transform.position, heading.normalized * forceMultiplier, Color.red);
        }

        // If applying forces to rigidbody
        if (applyForces)
        {
            // Set the force direction
            forceDirection = new Vector3(xDirection, yDirection, zDirection);

            // Add force
            rigidbody.AddRelativeForce(forceDirection * forceStrength, ForceMode.Force);
            if (draw) Debug.DrawLine(rigidbody.position, forceDirection.normalized * forceStrength, Color.blue);
        }
    }
}