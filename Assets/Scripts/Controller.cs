using System.Collections.Generic;
using UnityEngine;

// Controller
[System.Serializable]
public class Controller : MonoBehaviour
{
    // Use IK"
    private bool ikActive = true;

    // Enable free joint motion
    private bool freeJointMotion = false;

    // IK Weighting
    [Range(0.0f, 1.0f)]
    public float ikLeftHandWeight = 0.0f;
    [Range(0.0f, 1.0f)]
    public float ikRightHandWeight = 0.0f;
    [Range(0.0f, 1.0f)]
    public float ikLookWeight = 0.0f;

    [Header("Hide the meshes")]
    public bool hideMeshAnimator = false;
    public bool hideMeshRagdoll = false;

    [Header("Animator playback speed")]
    [Range(0.0f, 1.0f)]
    public float animatorSpeed;

    [Header("Feet Properties")]
    public PhysicMaterial lowFriction;
    public PhysicMaterial highFriction;
    public GameObject ragdollLeftFoot;
    public GameObject ragdollRightFoot;

    [Header("Animator IK Targets")]
    public Transform leftHand = null;
    public Transform rightHand = null;
    public Transform look = null;


    [Header("Left Upper Leg - Ragdoll controls")]
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    public float leftUpperLegSpringPosition = 10000.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    public float leftUpperLegPositionDamper = 10.0f;
    private float leftUpperLegMaxForce = Mathf.Infinity;
    [SerializeField]
    public bool leftUpperLegFreeJointMotion = false;

    [Header("Left Lower Leg - Ragdoll joint controls")]
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    public float leftLowerLegSpringPosition = 10000.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    public float leftLowerLegPositionDamper = 10.0f;
    private float leftLowerLegMaxForce = Mathf.Infinity;
    [SerializeField]
    public bool leftLowerLegFreeJointMotion = false;

    [Header("Left Foot - Ragdoll joint controls")]
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    public float leftFootSpringPosition = 10000.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    public float leftFootPositionDamper = 10.0f;
    private float leftFootMaxForce = Mathf.Infinity;
    [SerializeField]
    public bool leftFootFreeJointMotion = false;

    [Header("Right Upper Leg - Ragdoll joint controls")]
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    public float rightUpperLegSpringPosition = 10000.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    public float rightUpperLegPositionDamper = 10.0f;
    private float rightUpperLegMaxForce = Mathf.Infinity;
    [SerializeField]
    public bool rightUpperLegFreeJointMotion = false;

    [Header("Right Lower Leg - Ragdoll joint controls")]
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    public float rightLowerLegSpringPosition = 10000.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    public float rightLowerLegPositionDamper = 10.0f;
    private float rightLowerLegMaxForce = Mathf.Infinity;
    [SerializeField]
    public bool rightLowerLegFreeJointMotion = false;

    [Header("Right Foot - Ragdoll joint controls")]
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    public float rightFootSpringPosition = 10000.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    public float rightFootPositionDamper = 10.0f;
    private float rightFootMaxForce = Mathf.Infinity;
    [SerializeField]
    public bool rightFootFreeJointMotion = false;

    [Header("Spine - Ragdoll joint controls")]
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    public float spineSpringPosition = 10000.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    public float spinePositionDamper = 10.0f;
    private float spineMaxForce = Mathf.Infinity;
    [SerializeField]
    public bool spineFreeJointMotion = false;

    [Header("Left Upper Arm - Ragdoll joint controls")]
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    public float leftUpperArmSpringPosition = 10000.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    public float leftUpperArmPositionDamper = 10.0f;
    private float leftUpperArmMaxForce = Mathf.Infinity;
    [SerializeField]
    public bool leftUpperArmFreeJointMotion = false;

    [Header("Left Lower Arm - Ragdoll joint controls")]
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    public float leftLowerArmSpringPosition = 10000.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    public float leftLowerArmPositionDamper = 10.0f;
    private float leftLowerArmMaxForce = Mathf.Infinity;
    [SerializeField]
    public bool leftLowerArmFreeJointMotion = false;

    [Header("Left Hand - Ragdoll joint controls")]
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    public float leftHandSpringPosition = 10000.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    public float leftHandPositionDamper = 10.0f;
    private float leftHandMaxForce = Mathf.Infinity;
    [SerializeField]
    public bool leftHandFreeJointMotion = false;

    [Header("Head - Ragdoll joint controls")]
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    public float headSpringPosition = 10000.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    public float headPositionDamper = 10.0f;
    private float headMaxForce = Mathf.Infinity;
    [SerializeField]
    public bool headFreeJointMotion = false;

    [Header("Right Upper Arm - Ragdoll joint controls")]
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    public float rightUpperArmSpringPosition = 10000.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    public float rightUpperArmPositionDamper = 10.0f;
    private float rightUpperArmMaxForce = Mathf.Infinity;
    [SerializeField]
    public bool rightUpperArmFreeJointMotion = false;

    [Header("Right Lower Arm - Ragdoll joint controls")]
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    public float rightLowerArmSpringPosition = 10000.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    public float rightLowerArmPositionDamper = 10.0f;
    private float rightLowerArmMaxForce = Mathf.Infinity;
    [SerializeField]
    public bool rightLowerArmFreeJointMotion = false;

    [Header("Right Hand - Ragdoll joint controls")]
    [Range(0.0f, 10000.0f)]
    [SerializeField]
    public float rightHandSpringPosition = 10000.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    public float rightHandPositionDamper = 10.0f;
    private float rightHandMaxForce = Mathf.Infinity;
    [SerializeField]
    public bool rightHandFreeJointMotion = false;

    [Header("Blending from animation to ragdoll")]
    public bool blendAll = true;
    [Range(0.0f, 1.0f)]
    public float leftUpperLegBlend;
    [Range(0.0f, 1.0f)]
    public float leftLowerLegBlend;
    [Range(0.0f, 1.0f)]
    public float leftFootBlend;
    [Range(0.0f, 1.0f)]
    public float rightUpperLegBlend;
    [Range(0.0f, 1.0f)]
    public float rightLowerLegBlend;
    [Range(0.0f, 1.0f)]
    public float rightFootBlend;
    [Range(0.0f, 1.0f)]
    public float spineBlend;
    [Range(0.0f, 1.0f)]
    public float leftUpperArmBlend;
    [Range(0.0f, 1.0f)]
    public float leftLowerArmBlend;
    [Range(0.0f, 1.0f)]
    public float leftHandBlend;
    [Range(0.0f, 1.0f)]
    public float headBlend;
    [Range(0.0f, 1.0f)]
    public float rightUpperArmBlend;
    [Range(0.0f, 1.0f)]
    public float rightLowerArmBlend;
    [Range(0.0f, 1.0f)]
    public float rightHandBlend;
    [Range(0.0f, 1.0f)]
    public float allBlend;

    // Animator, ragdoll and model gameobjects
    private GameObject animator = null;
    private GameObject ragdoll = null;

    // Ragdoll, animator, model controllers
    private RagdollController ragdollController;
    private AnimatorController animatorController;
    private ModelController modelController;

    // Start is called before the first frame update
    void Awake()
    {
        // References to gameobjects
        animator = transform.GetChild(0).gameObject;
        ragdoll = transform.GetChild(2).gameObject;

        // Hide/show the meshes
        HideMeshes(hideMeshAnimator, animator);
        HideMeshes(hideMeshRagdoll, ragdoll);

        // Get the controllers and initialise
        // Animator
        animatorController = GetComponentInChildren<AnimatorController>();
        animatorController.Init();

        // Ragdoll
        ragdollController = GetComponentInChildren<RagdollController>();
        ragdollController.Init(animatorController.gameObject);

        // Model
        modelController = GetComponentInChildren<ModelController>();
        modelController.Init(animatorController.animatorTransforms);

        // Set the animator IK
        SetAnimatorIK();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Hide/show the meshes
        HideMeshes(hideMeshAnimator, animator);
        HideMeshes(hideMeshRagdoll, ragdoll);

        // Set the model controller values
        SetModelControls();

        // Set the animator IK weights
        SetAnimatorIKWeights();

        // Go through joints
        foreach (ConfigurableJoint joint in ragdollController.ConfigurableJoints())
        {
            // If the joint exsists
            if (joint)
            {
                // Left leg
                if (joint.name.Contains("LeftUpLeg")) ragdollController.ModifyJoints(joint, leftUpperLegSpringPosition, leftUpperLegPositionDamper, leftUpperLegMaxForce, leftUpperLegFreeJointMotion);
                if (joint.name.Contains("LeftLeg")) ragdollController.ModifyJoints(joint, leftLowerLegSpringPosition, leftLowerLegPositionDamper, leftLowerLegMaxForce, leftLowerLegFreeJointMotion);
                if (joint.name.Contains("LeftFoot")) ragdollController.ModifyJoints(joint, leftFootSpringPosition, leftFootPositionDamper, leftFootMaxForce, leftFootFreeJointMotion);

                // Right leg
                if (joint.name.Contains("RightUpLeg")) ragdollController.ModifyJoints(joint, rightUpperLegSpringPosition, rightUpperLegPositionDamper, rightUpperLegMaxForce, rightUpperLegFreeJointMotion);
                if (joint.name.Contains("RightLeg")) ragdollController.ModifyJoints(joint, rightLowerLegSpringPosition, rightLowerLegPositionDamper, rightLowerLegMaxForce, rightLowerLegFreeJointMotion);
                if (joint.name.Contains("RightFoot")) ragdollController.ModifyJoints(joint, rightFootSpringPosition, rightFootPositionDamper, rightFootMaxForce, rightFootFreeJointMotion);

                // Spine
                if (joint.name.Contains("Spine1")) ragdollController.ModifyJoints(joint, spineSpringPosition, spinePositionDamper, spineMaxForce, spineFreeJointMotion);

                // Left arm
                if (joint.name.Contains("LeftArm")) ragdollController.ModifyJoints(joint, leftUpperArmSpringPosition, leftUpperArmPositionDamper, leftUpperArmMaxForce, leftUpperArmFreeJointMotion);
                if (joint.name.Contains("LeftForeArm")) ragdollController.ModifyJoints(joint, leftLowerArmSpringPosition, leftLowerArmPositionDamper, leftLowerArmMaxForce, leftLowerArmFreeJointMotion);
                if (joint.name.Contains("LeftHand")) ragdollController.ModifyJoints(joint, leftHandSpringPosition, leftHandPositionDamper, leftHandMaxForce, leftHandFreeJointMotion);

                // Head
                if (joint.name.Contains("Head")) ragdollController.ModifyJoints(joint, headSpringPosition, headPositionDamper, headMaxForce, headFreeJointMotion);

                // Right arm
                if (joint.name.Contains("RightArm")) ragdollController.ModifyJoints(joint, rightUpperArmSpringPosition, rightUpperArmPositionDamper, rightUpperArmMaxForce, rightUpperArmFreeJointMotion);
                if (joint.name.Contains("RightForeArm")) ragdollController.ModifyJoints(joint, rightLowerArmSpringPosition, rightLowerArmPositionDamper, rightLowerArmMaxForce, rightLowerArmFreeJointMotion);
                if (joint.name.Contains("RightHand")) ragdollController.ModifyJoints(joint, rightHandSpringPosition, rightHandPositionDamper, rightHandMaxForce, rightHandFreeJointMotion);
            }
        }

        // Allow free motion of joints
        ragdollController.EnableFreeJointMotion(freeJointMotion);

        // Update the ragdoll controller
        ragdollController.RagdollControllerUpdate(animatorController.AnimtorHips());

        // Updatethe animator controller
        animatorController.UpdateAnimatorController();

        // Update the model controller
        modelController.UpdateModelController(ragdollController.ragdollTransforms, animatorController.animatorTransforms, ragdollController.RagdollHips());

        // Set animator speed
        SetAnimatorSpeed();
    }

    // Hide meshes
    void HideMeshes(bool hideMesh, GameObject gameObject)
    {
        // Disable skinned mesh on the gameobject
        SkinnedMeshRenderer skinnedMeshAnimator;
        if (skinnedMeshAnimator = gameObject.GetComponentInChildren<SkinnedMeshRenderer>())
        {
            // Enable / disable the skinner mesh renderers
            skinnedMeshAnimator.enabled = !hideMesh;
            SkinnedMeshRenderer[] skinnedMeshes = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer mesh in skinnedMeshes)
                mesh.enabled = !hideMesh;
        }
    }

    // Set the model controller values
    void SetModelControls()
    {
        // Blending between animation to ragdoll
        modelController.leftUpperLegBlend = leftUpperLegBlend;
        modelController.leftLowerLegBlend = leftLowerLegBlend;
        modelController.leftFootBlend = leftFootBlend;
        modelController.rightUpperLegBlend = rightUpperLegBlend;
        modelController.rightLowerLegBlend = rightLowerLegBlend;
        modelController.rightFootBlend = rightFootBlend;
        modelController.spineBlend = spineBlend;
        modelController.leftUpperArmBlend = leftUpperArmBlend;
        modelController.leftLowerArmBlend = leftLowerArmBlend;
        modelController.leftHandBlend = leftHandBlend;
        modelController.headBlend = headBlend;
        modelController.rightUpperArmBlend = rightUpperArmBlend;
        modelController.rightLowerArmBlend = rightLowerArmBlend;
        modelController.rightHandBlend = rightHandBlend;

        // If blending all blends
        if (blendAll)
        {
            // Blending between animation to ragdoll
            modelController.leftUpperLegBlend = leftUpperLegBlend = allBlend;
            modelController.leftLowerLegBlend = leftLowerLegBlend = allBlend;
            modelController.leftFootBlend = leftFootBlend = allBlend;
            modelController.rightUpperLegBlend = rightUpperLegBlend = allBlend;
            modelController.rightLowerLegBlend = rightLowerLegBlend = allBlend;
            modelController.rightFootBlend = rightFootBlend = allBlend;
            modelController.spineBlend = spineBlend = allBlend;
            modelController.leftUpperArmBlend = leftUpperArmBlend = allBlend;
            modelController.leftLowerArmBlend = leftLowerArmBlend = allBlend;
            modelController.leftHandBlend = leftHandBlend = allBlend;
            modelController.headBlend = headBlend = allBlend;
            modelController.rightUpperArmBlend = rightUpperArmBlend = allBlend;
            modelController.rightLowerArmBlend = rightLowerArmBlend = allBlend;
            modelController.rightHandBlend = rightHandBlend = allBlend;
        }
    }

    // Set the animator IKs
    void SetAnimatorIK()
    {
        // Ik active and hints flags
        animatorController.UseIK(ikActive);

        // Animator ik positions
        animatorController.SetIKTransforms(leftHand, rightHand, look);

        // Ik weighting
        animatorController.SetIKWeights(ikLeftHandWeight, ikRightHandWeight, ikLookWeight);
    }

    // Set the animator IK weights
    void SetAnimatorIKWeights()
    {
        animatorController.SetIKWeights(ikLeftHandWeight, ikRightHandWeight, ikLookWeight);
    }

    // Set the animator playback speed
    void SetAnimatorSpeed()
    {
        animatorController.SetAnimatorSpeed(animatorSpeed);
    }

    // Get the applied forces
    public List<ApplyForce> AppliedForces()
    {
        return ragdollController.AppliedForce();
    }

    // Set free joint motion
    public void FreeJointMotion(bool freeMotion)
    {
        freeJointMotion = freeMotion;
    }

    // Set the IK weight
    public void SetIKWeighgts(float leftIKweight, float rightIKweight, float lookIkWeight)
    {
        ikLeftHandWeight = leftIKweight;
        ikRightHandWeight = rightIKweight;
        ikLookWeight = lookIkWeight;
    }
}