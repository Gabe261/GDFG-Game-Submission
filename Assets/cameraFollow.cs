using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;  // The object to follow (e.g., an empty object)
    [SerializeField] private float lerpSpeed = 5f;  // Speed of the camera lerp movement
    
    void Start()
    {
        // Ensure target transform is set (e.g., by dragging the empty object in the inspector)
        if (targetTransform == null)
        {
            Debug.LogError("Target Transform not set for CameraFollow.");
        }
    }

    void LateUpdate()
    {
        // If the target transform is assigned, move and rotate the camera to match it
        if (targetTransform != null)
        {
            // Smoothly move the camera to match the target's position
            transform.position = Vector3.Lerp(transform.position, targetTransform.position, lerpSpeed * Time.deltaTime);

            // Smoothly match the rotation of the target object (camera follows the rotation as well)
            transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform.rotation, lerpSpeed * Time.deltaTime);
        }
    }

    public void SetTargetTransform(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }
}
