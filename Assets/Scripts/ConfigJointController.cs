using UnityEngine;

// Joint controller class
public class ConfigJointController : MonoBehaviour
{
    public bool active = true;
    [Header("Transform target to match")]
    public Transform target;

    [Header("Start rotation of the joint")]
    public Quaternion startRotation;

    [Header("Forces and damping")]
    public float positionSpring = 1000.0f;
    public float positionDamper = 0.0f;
    public float maxForce = Mathf.Infinity;

    // The configurable joint
    private ConfigurableJoint configurableJoint;
    private SoftJointLimitSpring spring;
    private JointDrive jointDrive;
    private JointDrive slerpDrive;

    // Initialisation
    void Start()
    {
        // Get the joint
        configurableJoint = GetComponent<ConfigurableJoint>();

        // Set the start rotation
        startRotation = configurableJoint.transform.localRotation;

        // Set the linear spring limit
        spring = new SoftJointLimitSpring
        {
            damper = 0.0f,
            spring = 0.0f
        };
        configurableJoint.linearLimitSpring = spring;

        // Set the joint drives
        jointDrive = new JointDrive
        {
            positionSpring = 0.0f,
            positionDamper = 0.0f,
            maximumForce = 0.0f
        };
        configurableJoint.xDrive = jointDrive;
        configurableJoint.yDrive = jointDrive;
        configurableJoint.zDrive = jointDrive;

        // Set the angular joint drives
        configurableJoint.angularXDrive = jointDrive;
        configurableJoint.angularYZDrive = jointDrive;

        // Set the slerp drive
        slerpDrive = new JointDrive
        {
            positionSpring = positionSpring,
            positionDamper = positionDamper,
            maximumForce = maxForce
        };
        configurableJoint.slerpDrive = slerpDrive;

        // Set the joint values
        configurableJoint.rotationDriveMode = RotationDriveMode.Slerp;
        configurableJoint.projectionMode = JointProjectionMode.None;
        configurableJoint.enableCollision = true;
        configurableJoint.projectionDistance = 0.1f;
        configurableJoint.projectionAngle = 30.0f;
        configurableJoint.configuredInWorldSpace = false;

        // Set the joint motions
        configurableJoint.angularXMotion = ConfigurableJointMotion.Limited;
        configurableJoint.angularYMotion = ConfigurableJointMotion.Limited;
        configurableJoint.angularZMotion = ConfigurableJointMotion.Limited;
        configurableJoint.xMotion = ConfigurableJointMotion.Locked;
        configurableJoint.yMotion = ConfigurableJointMotion.Locked;
        configurableJoint.zMotion = ConfigurableJointMotion.Locked;
    }

    // Update the configurable joint controller
    public void UpdateConfigurableJointController()
    {
        // If the joint exsists - set the rotation
        if (configurableJoint != null)
            SetTargetRotation(target.localRotation, startRotation);
    }

    // Modify the slerp drive values
    public void ModifySlerpDrive(float positionSpring, float positionDamper, float maxForce)
    {
        slerpDrive.maximumForce = this.maxForce = maxForce;
        slerpDrive.positionSpring = this.positionSpring = positionSpring;
        slerpDrive.positionDamper = this.positionDamper = positionDamper;
        configurableJoint.slerpDrive = slerpDrive;
    }

    // Set the rotation of the joint
    void SetTargetRotation(Quaternion targetRotation, Quaternion startRotation)
    {
        // Calculate the rotation using joint axis and secondary axis
        Vector3 right = configurableJoint.axis;
        Vector3 forward = Vector3.Cross(configurableJoint.axis, configurableJoint.secondaryAxis).normalized;
        Vector3 up = Vector3.Cross(forward, right).normalized;
        Quaternion worldToJointSpace = Quaternion.LookRotation(forward, up);

        // Transform into world space
        Quaternion resultRotation = Quaternion.Inverse(worldToJointSpace);

        // Apply new rotation
        resultRotation *= Quaternion.Inverse(targetRotation) * startRotation;

        // Transform back into joint space
        resultRotation *= worldToJointSpace;

        // Set target rotation
        configurableJoint.targetRotation = resultRotation;
    }
}