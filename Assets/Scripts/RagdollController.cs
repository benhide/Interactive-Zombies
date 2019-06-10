using System.Collections.Generic;
using UnityEngine;

// Ragdoll controller class
public class RagdollController : MonoBehaviour
{
    [Header("Master and Slave transforms list")]
    public List<Transform> ragdollTransforms = new List<Transform>();

    // Configurable joints list
    private List<ConfigurableJoint> configurableJoints = new List<ConfigurableJoint>();

    // Applied force list
    private List<ApplyForce> ragdollAppliedForces = new List<ApplyForce>();

    // Slave ridigbodies
    private List<Rigidbody> ragdollRigidbodies = new List<Rigidbody>();

    // Ragdoll hips transforms
    private Transform ragdollHips = null;

    // Center of mass
    private Vector3 centerOfMass = Vector3.zero;

    // The animator
    public Animator animator;

    // Use this for initialization
    public void Init(GameObject animator)
    {
        // Get all the configurable joints
        foreach (Transform transform in transform.GetComponentsInChildren<Transform>())
        {
            // Get the rigidbodies
            if (transform.GetComponent<Rigidbody>())
                ragdollRigidbodies.Add(transform.GetComponent<Rigidbody>());

            // Get all the configurable joints
            ConfigurableJoint configurableJoint = transform.GetComponent<ConfigurableJoint>();
            if (configurableJoint != null)
            {
                // Get the configurable joints
                configurableJoints.Add(configurableJoint);

                // Get all the slave transforms
                ragdollTransforms.Add(transform);
            }

            // Get the hips
            if (transform.name.Contains("Hips"))
                ragdollHips = transform;
        }

        // Set all the animation transform targets
        foreach (ConfigurableJoint configurableJoint in configurableJoints)
        {
            // Add the configurable joint controller to the joints
            configurableJoint.gameObject.AddComponent<ConfigJointController>();

            // Get the transform in the master
            foreach (Transform transform in animator.transform.GetComponentsInChildren<Transform>())
            {
                // If the names match - animtor transform and configurable joints
                if (transform.name == configurableJoint.gameObject.name)
                {
                    // Set the target - disable joint
                    configurableJoint.GetComponent<ConfigJointController>().target = transform;
                    configurableJoint.gameObject.GetComponent<ConfigJointController>().enabled = true;

                    // Add the applied forces scripts - add to list
                    if (configurableJoint.gameObject.GetComponent<ApplyForce>())
                        ragdollAppliedForces.Add(configurableJoint.gameObject.GetComponent<ApplyForce>());
                }
            }
        }

        // Set the rigidbody drags
        foreach (Rigidbody body in ragdollRigidbodies)
        {
            body.angularDrag = 0.0f;
            body.drag = 0.0f;
            body.interpolation = RigidbodyInterpolation.Interpolate;
            body.detectCollisions = true;

            // Not the hips
            if (!body.name.Contains("Hips"))
                body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        // Get the animator
        this.animator = animator.GetComponent<Animator>();
    }

    // Update the ragdoll controller
    public void RagdollControllerUpdate(Transform hips)
    {
        // All the joints
        foreach (ConfigurableJoint joint in configurableJoints)
        {
            // If the joint exsists
            if (joint)
            {
                // Update the controllers
                joint.GetComponent<ConfigJointController>().UpdateConfigurableJointController();
            }
        }

        // Set the hips position
        SetHipsPositionRotation(hips);
    }

    // Set the hips position
    void SetHipsPositionRotation(Transform hips)
    {
        // Temp position
        Vector3 position = CenterOfMass();

        // Set the hip positions
        ragdollHips.position = Vector3.Lerp(hips.position, position, 0.25f);
        ragdollHips.rotation = ragdollHips.rotation;
    }

    // Enable the joint limits
    public void EnableFreeJointMotion(bool freeMotion)
    {
        // Set all the configurable joints joint motion
        foreach (ConfigurableJoint joint in configurableJoints)
            EnableFreeJointMotion(joint, freeMotion);
    }

    // Enable the joint limits
    public void EnableFreeJointMotion(ConfigurableJoint joint, bool freeMotion)
    {
        // If the joint exsists
        if (joint)
        {
            // Free motion
            if (freeMotion)
            {
                joint.angularXMotion = ConfigurableJointMotion.Free;
                joint.angularYMotion = ConfigurableJointMotion.Free;
                joint.angularZMotion = ConfigurableJointMotion.Free;
            }

            // Limited motion
            else
            {
                joint.angularXMotion = ConfigurableJointMotion.Limited;
                joint.angularYMotion = ConfigurableJointMotion.Limited;
                joint.angularZMotion = ConfigurableJointMotion.Limited;
            }
        }
    }

    // Modify the configurable joints slerp drives
    public void ModifyJoints(ConfigurableJoint joint, float positionSpring, float positionDamper, float maxForce, bool freeMotion)
    {
        ConfigJointController configJointController = joint.GetComponent<ConfigJointController>();
        configJointController.ModifySlerpDrive(positionSpring, positionDamper, maxForce);
        EnableFreeJointMotion(joint, freeMotion);
    }

    // Get the configurable joints
    public List<ConfigurableJoint> ConfigurableJoints()
    {
        return configurableJoints;
    }

    // Get the rigidbodies
    public List<Rigidbody> RigidBodies()
    {
        return ragdollRigidbodies;
    }

    // Get the applied forces 
    public List<ApplyForce> AppliedForce()
    {
        return ragdollAppliedForces;
    }

    // Get the ragdoll hips
    public Transform RagdollHips()
    {
        return ragdollHips;
    }

    // Get the center of mass
    Vector3 CenterOfMass()
    {
        // Reset the center of mass
        centerOfMass = Vector3.zero;
        float mass = 0.0f;

        // Loop through each rigidbody
        foreach (Rigidbody body in ragdollRigidbodies)
        {
            // Calculate the center of mass
            centerOfMass += body.worldCenterOfMass * body.mass;
            mass += body.mass;
        }

        // Calculate the center of mass
        return centerOfMass /= mass;
    }

    //// Draw gizmos 
    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(centerOfMass, 0.1f);
    //}
}