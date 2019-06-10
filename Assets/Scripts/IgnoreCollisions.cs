using UnityEngine;

// Ignore collisions class
public class IgnoreCollisions : MonoBehaviour
{
    // Objects to ignore collisions
    public GameObject[] objectsToIgnore;

    // Start is called before the first frame update
    void Start()
    {
        // Go through all objects to ignore
        for (int i = 0; i < objectsToIgnore.Length; i++)
        {
            // If the objects have colliders - ignore the collsions
            if (objectsToIgnore[i].GetComponent<Collider>() && gameObject.GetComponent<Collider>())
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), objectsToIgnore[i].GetComponent<Collider>());
        }
    }
}
