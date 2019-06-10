using System.Collections.Generic;
using UnityEngine;

// Model controller
public class ModelController : MonoBehaviour
{
    [Header("Model transforms list")]
    public List<Transform> modelTransforms = new List<Transform>();

    [Header("Blending from animation to ragdoll")]
    public float leftUpperLegBlend;
    public float leftLowerLegBlend;
    public float leftFootBlend;
    public float rightUpperLegBlend;
    public float rightLowerLegBlend;
    public float rightFootBlend;
    public float spineBlend;
    public float leftUpperArmBlend;
    public float leftLowerArmBlend;
    public float leftHandBlend;
    public float headBlend;
    public float rightUpperArmBlend;
    public float rightLowerArmBlend;
    public float rightHandBlend;

    // The animator hips
    private Transform modelHips;

    // Awake is called before the first frame update
    public void Init(List<Transform> animatorTransforms)
    {
        // Get the transform in the model
        foreach (Transform transform in transform.GetComponentsInChildren<Transform>())
        {
            // Get the animator transforms
            foreach (Transform animTransform in animatorTransforms)
            {
                // If the transform names match - add to the list
                if (transform.name == animTransform.name)
                {
                    // Get the model transforms
                    modelTransforms.Add(transform);
                }
            }

            // Get the hips
            if (transform.name.Contains("Hips"))
                modelHips = transform;
        }
    }

    // Set the hips position
    void SetHipsPositionRotation(Transform hips)
    {
        // Set the hip positions
        modelHips.position = hips.position;
        modelHips.rotation = hips.rotation;
    }

    // Update is called once per frame
    public void UpdateModelController(List<Transform> ragdollTransforms, List<Transform> animatorTransforms, Transform hips)
    {
        // Update in world space
        UpdateTransforms(ragdollTransforms, animatorTransforms);

        // Set the hips position
        SetHipsPositionRotation(hips);
    }

    // Update transform
    void UpdateTransforms(List<Transform> ragdollTransforms, List<Transform> animatorTransforms)
    {
        // Blend model transform localRotations
        int i = 0;

        // Left leg
        modelTransforms[i].rotation = Quaternion.Slerp(animatorTransforms[i].rotation, ragdollTransforms[i].rotation, leftUpperLegBlend); i++;
        modelTransforms[i].rotation = Quaternion.Slerp(animatorTransforms[i].rotation, ragdollTransforms[i].rotation, leftLowerLegBlend); i++;
        modelTransforms[i].rotation = Quaternion.Slerp(animatorTransforms[i].rotation, ragdollTransforms[i].rotation, leftFootBlend); i++;

        // Right leg
        modelTransforms[i].rotation = Quaternion.Slerp(animatorTransforms[i].rotation, ragdollTransforms[i].rotation, rightUpperLegBlend); i++;
        modelTransforms[i].rotation = Quaternion.Slerp(animatorTransforms[i].rotation, ragdollTransforms[i].rotation, rightLowerLegBlend); i++;
        modelTransforms[i].rotation = Quaternion.Slerp(animatorTransforms[i].rotation, ragdollTransforms[i].rotation, rightFootBlend); i++;

        // Spine blend
        modelTransforms[i].rotation = Quaternion.Slerp(animatorTransforms[i].rotation, ragdollTransforms[i].rotation, spineBlend); i++;

        // Left arm
        modelTransforms[i].rotation = Quaternion.Slerp(animatorTransforms[i].rotation, ragdollTransforms[i].rotation, leftUpperArmBlend); i++;
        modelTransforms[i].rotation = Quaternion.Slerp(animatorTransforms[i].rotation, ragdollTransforms[i].rotation, leftLowerArmBlend); i++;
        modelTransforms[i].rotation = Quaternion.Slerp(animatorTransforms[i].rotation, ragdollTransforms[i].rotation, leftHandBlend); i++;

        // Head blend
        modelTransforms[i].rotation = Quaternion.Slerp(animatorTransforms[i].rotation, ragdollTransforms[i].rotation, headBlend); i++;

        // Right arm
        modelTransforms[i].rotation = Quaternion.Slerp(animatorTransforms[i].rotation, ragdollTransforms[i].rotation, rightUpperArmBlend); i++;
        modelTransforms[i].rotation = Quaternion.Slerp(animatorTransforms[i].rotation, ragdollTransforms[i].rotation, rightLowerArmBlend); i++;
        modelTransforms[i].rotation = Quaternion.Slerp(animatorTransforms[i].rotation, ragdollTransforms[i].rotation, rightHandBlend); i++;
    }
}