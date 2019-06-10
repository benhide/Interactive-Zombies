using System.Collections.Generic;
using UnityEngine;

// Animator controller
public class AnimatorController : MonoBehaviour
{
    [Header("Animator transforms list")]
    public List<Transform> animatorTransforms;
    private List<HumanBodyBones> animatorBodyBones;

    // Use IK - Use IK Hints
    private bool ikActive = false;

    // Animator IK Targets
    private Transform leftHand = null;
    private Transform rightHand = null;
    private Transform look = null;

    // IK Weighting
    private float ikLeftHandWeight = 0.0f;
    private float ikRightHandWeight = 0.0f;
    private float ikLookWeight = 0.0f;

    // Animator component
    private Animator animator;

    // The animator hips
    private Transform animatorHips;

    // Init is called before the first frame update
    public void Init()
    {
        // Reference to animator
        animator = GetComponent<Animator>();

        // Animator body bones
        animatorBodyBones = new List<HumanBodyBones>
        {
            HumanBodyBones.LeftUpperLeg,
            HumanBodyBones.LeftLowerLeg,
            HumanBodyBones.LeftFoot,
            HumanBodyBones.RightUpperLeg,
            HumanBodyBones.RightLowerLeg,
            HumanBodyBones.RightFoot,
            HumanBodyBones.Chest,
            HumanBodyBones.LeftUpperArm,
            HumanBodyBones.LeftLowerArm,
            HumanBodyBones.LeftHand,
            HumanBodyBones.Head,
            HumanBodyBones.RightUpperArm,
            HumanBodyBones.RightLowerArm,
            HumanBodyBones.RightHand
        };

        // The animator body bones transforms
        for (int i = 0; i < animatorBodyBones.Count; i++)
            animatorTransforms.Add(animator.GetBoneTransform(animatorBodyBones[i]));

        // Get the animator hips
        animatorHips = animator.GetBoneTransform(HumanBodyBones.Hips);
    }

    // Update the animtor controller
    public void UpdateAnimatorController()
    {
        // Feet pivot weight 0 = left / 1 = right
        float pivotWeight = animator.pivotWeight;
        //Debug.Log(animator.pivotWeight);
    }

    // Get the animators hips transform
    public Transform AnimtorHips()
    {
        return animatorHips;
    }

    // Set animator playback speed
    public void SetAnimatorSpeed(float speed)
    {
        animator.speed = speed;
    }

    // Callback for calculating IK
    void OnAnimatorIK()
    {
        // If there is an animator
        if (animator)
        {
            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {
                // Set the look target position, if one has been assigned
                if (look != null)
                {
                    animator.SetLookAtWeight(ikLookWeight);
                    animator.SetLookAtPosition(look.position);
                }

                // Set the left hand target position and rotation, if one has been assigned
                if (leftHand != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikLeftHandWeight);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikLeftHandWeight);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHand != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, ikRightHandWeight);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, ikRightHandWeight);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
                }
            }

            // If the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }


    // Use IK
    public void UseIK(bool useIK)
    {
        ikActive = useIK;
    }

    // Set IK values
    public void SetIKWeights(float leftHandWeight, float rightHandWeight, float lookWeight)
    {
        ikLeftHandWeight = leftHandWeight;
        ikRightHandWeight = rightHandWeight;
        ikLookWeight = lookWeight;
    }

    // Set IK transforms
    public void SetIKTransforms(Transform leftHand, Transform rightHand, Transform look)
    {
        this.leftHand = leftHand;
        this.rightHand = rightHand;
        this.look = look;
    }
}