using UnityEngine;

// Ik perlin noise class
public class IKPerlinNoise : MonoBehaviour
{
    // The animator
    public Animator anim;

    // Distance covered per second along Y axis of Perlin plane
    public float yScale = 0.25f;

    // Start position
    public Vector3 startPosition;

    // The amount of noise
    public float yAmount;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Amount of perlin noise
        yAmount = Mathf.PerlinNoise(0.0f, GetCurrentAnimatorTime(anim)) * 0.5f;

        // Temp vector
        Vector3 position = Vector3.zero;

        // Apply noise to temp vector
        position.y = yAmount;

        // Set new psoition
        transform.localPosition = startPosition + position;
    }

    // Get the current anaimator state time
    float GetCurrentAnimatorTime(Animator targetAnim, int layer = 0)
    {
        // Animator stte
        AnimatorStateInfo animState = targetAnim.GetCurrentAnimatorStateInfo(layer);

        // Calculate the current time of the clip
        float currentTime = animState.normalizedTime % 1.0f;

        // Return the current time
        return currentTime;
    }
}
