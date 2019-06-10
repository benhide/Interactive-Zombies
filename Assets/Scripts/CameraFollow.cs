using UnityEngine;

// Camera class
public class CameraFollow : MonoBehaviour
{
    [Header("Move and rotation speed")]
    public float moveSpeed;
    public float rotateSpeed;

    [Header("Target Gameobject")]
    public GameObject target;

    // Mouse values
    private float pitch;
    private float yaw;
    private Vector3 mouseMovement;

    // Camera offset
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    // Update
    void Update()
    {
        // Camera rotation
        if (Input.GetMouseButton(1))
        {
            // Set pitch and yaw
            pitch += rotateSpeed * -Input.GetAxis("Mouse Y");
            yaw += rotateSpeed * Input.GetAxis("Mouse X");

            // Clamp pitch
            pitch = Mathf.Clamp(pitch, -90.0f, 90.0f);

            // Handle y axis
            while (yaw < 0.0f) yaw += 360.0f;
            while (yaw >= 360.0f) yaw -= 360.0f;

            // Set transfrom angles
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        transform.position = target.transform.position + offset;

        // Camera fly
        if (Input.GetKey(KeyCode.W)) offset += (transform.forward * moveSpeed);
        if (Input.GetKey(KeyCode.A)) offset += (-transform.right * moveSpeed);
        if (Input.GetKey(KeyCode.S)) offset += (-transform.forward * moveSpeed);
        if (Input.GetKey(KeyCode.D)) offset += (transform.right * moveSpeed);
        if (Input.GetKey(KeyCode.Q)) offset += (-transform.up * moveSpeed);
        if (Input.GetKey(KeyCode.E)) offset += (transform.up * moveSpeed);
    }
}