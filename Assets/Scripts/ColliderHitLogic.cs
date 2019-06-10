using System.Collections.Generic;
using UnityEngine;

public class ColliderHitLogic : MonoBehaviour
{
    public Animator animator;
    public Rigidbody hips;
    bool dying = false;
    bool dead = false;
    Controller controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponentInParent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dying && !dead)
        {
            animator.enabled = false;
            hips.isKinematic = false;
            dead = true;

            // Get the applied forces scripts
            List<ApplyForce> appliedForces = controller.AppliedForces();
            foreach (ApplyForce applyForce in appliedForces)
            {
                applyForce.applyForces = false;
            }

            controller.FreeJointMotion(true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 10.0f)
        {
            dying = true;
        }
    }
}
