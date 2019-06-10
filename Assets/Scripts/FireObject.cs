using UnityEngine;

// Fire an object class
public class FireObject : MonoBehaviour
{
    // The bullet
    public GameObject bullet;

    // Distance from camera
    public float distance = 10.0f;

    // Fire timer
    private float timer = 0.1f;
    private float timerReset = 0.1f;

    // Update is called once per frame
    void Update()
    {
        // Reduce the timer
        timer -= Time.deltaTime;

        // Fire bullets
        if (Input.GetMouseButton(0) && timer < 0.0f)
        {
            // Reset the timer
            timer = timerReset;

            // Set ethe position
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

            // Camera to world space
            position = Camera.main.ScreenToWorldPoint(position);

            // Instantiate object
            GameObject go = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;

            // Set llok rotation
            go.transform.LookAt(position);

            // Add force
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * 100.0f, ForceMode.Impulse);

            // Destroy after 2 seconds
            Destroy(go, 2.0f);
        }
    }
}