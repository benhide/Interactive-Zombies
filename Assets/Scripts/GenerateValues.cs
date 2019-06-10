using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
// Generate vaues class
[System.Serializable]
public class GenerateValues : MonoBehaviour
{
    // Which legs are active
    private string[] ACTIVE_LEGS = new string[] { "LEFT", "RIGHT", "BOTH" };

    // Which arms are active
    private string[] ACTIVE_ARMS = new string[] { "LEFT", "RIGHT", "BOTH", "NONE" };

    [Header("Generate the values")]
    public bool generateValues = true;
    [SerializeField]
    public int seed;
    [SerializeField]
    Random.State randomState;

    [Header("Use forces on the spine")]
    [SerializeField]
    bool activeSpine;
    [SerializeField]
    bool activeHead;
    [SerializeField]
    bool useSpineForce = false;
    [SerializeField]
    float forceStrength = 100.0f;

    [Header("Which legs / arms are active")]
    [SerializeField]
    string activeLegs;
    [SerializeField]
    string activeArms;

    [Header("Spring position theresholds")]
    [SerializeField]
    [Range(0.0f, 500.0f)]
    float minSpringPosition;
    [Range(0.0f, 500.0f)]
    [SerializeField]
    float maxSpringPosition;
    [SerializeField]
    float springPosition = 500.0f;

    [Header("Position damper theresholds")]
    [SerializeField]
    [Range(1.0f, 100.0f)]
    float minPositionDamper;
    [Range(1.0f, 100.0f)]
    [SerializeField]
    float maxPositionDamper;
    [SerializeField]
    float positionDamper = 10.0f;

    [Header("Animator hand IK position theresholds")]
    [SerializeField]
    [Range(0.1f, 0.5f)]
    float minXDistIK;
    [SerializeField]
    [Range(0.1f, 0.5f)]
    float maxXDistIK;
    [SerializeField]
    [Range(1.0f, 1.5f)]
    float minYDistIK;
    [SerializeField]
    [Range(1.0f, 2.0f)]
    float maxYDistIK;
    [SerializeField]
    [Range(0.1f, 1.0f)]
    float minZDistIK;
    [SerializeField]
    [Range(0.1f, 0.5f)]
    float maxZDistIK;

    // The controllers
    private Controller controller;
    public TestingData testingData;

    // This is Main Camera in the Scene
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        // Get the controller
        controller = GetComponent<Controller>();
        mainCamera = Camera.main;
        Random.InitState(seed);
        randomState = Random.state;
    }

    // Update is called once per frame
    void Update()
    {
        // Generate
        if (generateValues)
        {
            // Seed
            //Random.InitState(seed);

            // Generate the values
            GenerateNewValues();

            // Reset flag
            generateValues = false;

            // Get the random state
            randomState = Random.state;
        }
    }

    // Generate the values
    void GenerateNewValues()
    {
        // Joint values
        GenerateJointValues();

        // IK values
        GenerateIkValues();

        // Set the active legs
        SetActiveLegs();

        // Set the active arms
        SetActiveArms();

        // Set the spine / head active
        SetActiveSpine();
        SetActiveHead();

        // Apply forces
        GenerateAdditionalForces();
    }

    // Generate the joint spring values
    void GenerateJointValues()
    {
        // Ragdoll springs
        GenerateLeftLegJointValues();
        GenerateRightLegJointValues();
        GenerateLeftArmJointValues();
        GenerateRightArmJointValues();
    }

    // Generate the joint spring values
    void GenerateLeftLegJointValues(bool disabled = false)
    {
        // If not disabled
        if (!disabled)
        {
            controller.leftUpperLegSpringPosition = controller.leftLowerLegSpringPosition = controller.leftFootSpringPosition = springPosition;
            controller.leftUpperLegPositionDamper = controller.leftLowerLegPositionDamper = controller.leftFootPositionDamper = positionDamper;
        }

        // Disabled
        else
        {
            controller.leftUpperLegSpringPosition = Random.Range(minSpringPosition, maxSpringPosition);
            controller.leftUpperLegPositionDamper = Random.Range(minPositionDamper, maxPositionDamper);
            controller.leftLowerLegSpringPosition = Random.Range(minSpringPosition, maxSpringPosition);
            controller.leftLowerLegPositionDamper = Random.Range(minPositionDamper, maxPositionDamper);
            controller.leftFootSpringPosition = Random.Range(minSpringPosition, maxSpringPosition);
            controller.leftFootPositionDamper = Random.Range(minPositionDamper, maxPositionDamper);
        }
    }

    // Generate the joint spring values
    void GenerateRightLegJointValues(bool disabled = false)
    {
        // If not disabled
        if (!disabled)
        {
            controller.rightUpperLegSpringPosition = controller.rightLowerLegSpringPosition = controller.rightFootSpringPosition = springPosition;
            controller.rightUpperLegPositionDamper = controller.rightLowerLegPositionDamper = controller.rightFootPositionDamper = positionDamper;
        }

        // Disabled
        else
        {
            controller.rightUpperLegSpringPosition = Random.Range(minSpringPosition, maxSpringPosition);
            controller.rightUpperLegPositionDamper = Random.Range(minPositionDamper, maxPositionDamper);
            controller.rightLowerLegSpringPosition = Random.Range(minSpringPosition, maxSpringPosition);
            controller.rightLowerLegPositionDamper = Random.Range(minPositionDamper, maxPositionDamper);
            controller.rightFootSpringPosition = Random.Range(minSpringPosition, maxSpringPosition);
            controller.rightFootPositionDamper = Random.Range(minPositionDamper, maxPositionDamper);
        }
    }

    // Generate the joint spring values
    void GenerateLeftArmJointValues(bool disabled = false)
    {
        // If not disabled
        if (!disabled)
        {
            controller.leftUpperArmSpringPosition = controller.leftLowerArmSpringPosition = controller.leftHandSpringPosition = springPosition;
            controller.leftUpperArmPositionDamper = controller.leftLowerArmPositionDamper = controller.leftHandPositionDamper = positionDamper;
        }

        // Disabled
        else
        {
            controller.leftUpperArmSpringPosition = Random.Range(minSpringPosition, maxSpringPosition * 0.5f);
            controller.leftUpperArmPositionDamper = Random.Range(minPositionDamper, maxPositionDamper);
            controller.leftLowerArmSpringPosition = Random.Range(minSpringPosition, maxSpringPosition * 0.5f);
            controller.leftLowerArmPositionDamper = Random.Range(minPositionDamper, maxPositionDamper);
        }

        // Hand Values
        controller.leftHandSpringPosition = Random.Range(minSpringPosition, maxSpringPosition * 0.5f);
        controller.leftHandPositionDamper = Random.Range(minPositionDamper, maxPositionDamper);
    }

    // Generate the joint spring values
    void GenerateRightArmJointValues(bool disabled = false)
    {
        // If not disabled
        if (!disabled)
        {
            controller.rightUpperArmSpringPosition = controller.rightLowerArmSpringPosition = controller.rightHandSpringPosition = springPosition;
            controller.rightUpperArmPositionDamper = controller.rightLowerArmPositionDamper = controller.rightHandPositionDamper = positionDamper;
        }

        // Disabled
        else
        {
            controller.rightUpperArmSpringPosition = Random.Range(minSpringPosition, maxSpringPosition * 0.5f);
            controller.rightUpperArmPositionDamper = Random.Range(minPositionDamper, maxPositionDamper);
            controller.rightLowerArmSpringPosition = Random.Range(minSpringPosition, maxSpringPosition * 0.5f);
            controller.rightLowerArmPositionDamper = Random.Range(minPositionDamper, maxPositionDamper);
        }

        // Hand Values
        controller.rightHandSpringPosition = Random.Range(minSpringPosition, maxSpringPosition * 0.5f);
        controller.rightHandPositionDamper = Random.Range(minPositionDamper, maxPositionDamper);
    }

    // Generate the joint spring values
    void SetActiveHead()
    {
        // Set spine active?
        activeHead = Random.Range(0, 2) == 0 ? false : true;

        // If not disabled
        if (activeHead)
        {
            controller.headSpringPosition = springPosition;
            controller.headPositionDamper = positionDamper;
        }

        // Disabled
        else
        {
            controller.headSpringPosition = Random.Range(minSpringPosition, maxSpringPosition);
            controller.headPositionDamper = Random.Range(minPositionDamper, maxPositionDamper);
        }
    }

    // Set the active legs
    void SetActiveLegs()
    {
        // Switch on random range - set foot physics material
        activeLegs = ACTIVE_LEGS[Random.Range(0, 3)];
        switch (activeLegs)
        {
            // Left foot active
            case "LEFT":
                controller.ragdollLeftFoot.GetComponent<BoxCollider>().material = controller.lowFriction;
                controller.ragdollRightFoot.GetComponent<BoxCollider>().material = controller.highFriction;
                GenerateRightLegJointValues(true);
                break;

            // Right foot active
            case "RIGHT":
                controller.ragdollLeftFoot.GetComponent<BoxCollider>().material = controller.highFriction;
                controller.ragdollRightFoot.GetComponent<BoxCollider>().material = controller.lowFriction;
                GenerateLeftLegJointValues(true);
                break;

            // Both feet active
            case "BOTH":
                controller.ragdollLeftFoot.GetComponent<BoxCollider>().material = controller.lowFriction;
                controller.ragdollRightFoot.GetComponent<BoxCollider>().material = controller.lowFriction;
                break;
        }
    }

    // Set the active arms
    void SetActiveArms()
    {
        // Switch on random range 
        activeArms = ACTIVE_ARMS[Random.Range(0, 4)];
        switch (activeArms)
        {
            // Left arm active
            case "LEFT":
                GenerateRightArmJointValues(true);
                controller.SetIKWeighgts(1.0f, 0.0f, 1.0f);
                break;

            // Right arm active
            case "RIGHT":
                GenerateLeftArmJointValues(true);
                controller.SetIKWeighgts(0.0f, 1.0f, 1.0f);
                break;

            // Both arms active
            case "BOTH":
                controller.SetIKWeighgts(1.0f, 1.0f, 1.0f);
                break;

            // Both arms active
            case "NONE":
                GenerateLeftArmJointValues(true);
                GenerateRightArmJointValues(true);
                controller.SetIKWeighgts(0.0f, 0.0f, 1.0f);
                break;
        }
    }

    // Set the active spine
    void SetActiveSpine()
    {
        // Set spine active?
        activeSpine = Random.Range(0, 2) == 0 ? false : true;
        useSpineForce = !activeSpine;

        // If spine active
        if (activeSpine)
        {
            controller.spineSpringPosition = springPosition;
            controller.spinePositionDamper = positionDamper;
        }

        // Disabled
        else
        {
            controller.spineSpringPosition = Random.Range(minSpringPosition, maxSpringPosition * 0.5f);
            controller.spinePositionDamper = Random.Range(minPositionDamper, maxPositionDamper);
        }
    }

    // Generate IK values
    void GenerateIkValues()
    {
        // Set the Ik positions
        controller.leftHand.localPosition = new Vector3(Random.Range(-minXDistIK, -maxXDistIK), Random.Range(minYDistIK, maxYDistIK), Random.Range(minZDistIK, maxZDistIK));
        controller.rightHand.localPosition = new Vector3(Random.Range(minXDistIK, maxXDistIK), Random.Range(minYDistIK, maxYDistIK), Random.Range(minZDistIK, maxZDistIK));
        controller.look.localPosition = mainCamera.transform.position;
    }

    // Generate forces
    void GenerateAdditionalForces()
    {
        // Get the applied forces scripts
        List<ApplyForce> appliedForces = controller.AppliedForces();

        // Switch on active feet
        switch (activeLegs)
        {
            // Active foot left - apply downward force to right foot (right foot disabled)
            case "LEFT":
                appliedForces[1].applyForces = true;
                appliedForces[1].yDirection = -1.0f;
                appliedForces[1].forceStrength = 10.0f;
                break;

            // Active foot right - apply downward force to left foot (left foot disabled)
            case "RIGHT":
                appliedForces[0].applyForces = true;
                appliedForces[0].yDirection = -1.0f;
                appliedForces[0].forceStrength = 10.0f;
                break;
        }

        // Spine
        if (useSpineForce)
        {
            appliedForces[2].applyForces = true;
            appliedForces[2].xDirection = Random.Range(-1.0f, 1.0f);
            appliedForces[2].zDirection = Random.Range(-1.0f, 0.0f);
            appliedForces[2].forceStrength = forceStrength;
        }

        // Else no spine force
        else appliedForces[2].applyForces = false;
    }
}
