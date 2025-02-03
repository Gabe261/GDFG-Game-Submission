using UnityEngine;
public class MoveAndRotateTowardsVelocity : MonoBehaviour
{
    private Vector3 previousPosition; // To store the previous position
    [SerializeField] private float rotationSpeed = 5f;  // Serialized field to control the rotation speed for testing
    [SerializeField] private float maxRotation = 30f;  // Max rotation angle for up or down based on speed
    private float currentRotation;  // To store the current rotation value
    
    [SerializeField] private Transform copyObject;
    
    void Start()
    {
        previousPosition = copyObject.transform.position;  // Initialize the previous position at the start
        currentRotation = copyObject.transform.eulerAngles.z;  // Initialize the current rotation (Z-axis)
    }

    void Update()
    {
        // Get the current position of the object
        Vector3 currentPosition = transform.position;

        // Calculate the vertical speed (change in Y position)
        float verticalSpeed = currentPosition.y - previousPosition.y;  // Vertical movement: positive for up, negative for down

        // Debug log to see how much the plane is moving up or down
        verticalSpeed *= 100;
        
        if (verticalSpeed > -1 && verticalSpeed < 1)
        {
            RotatePlane(0);
        }
        else if (verticalSpeed < 0)
        {
            // Calculate rotation for downward movement
            // Allowing negative rotation for downward tilt
            float rotationAmount = Mathf.Clamp(-verticalSpeed * maxRotation, -maxRotation, 0);  // Negative for downward rotation
            RotatePlane(-20);  // Rotate downwards
        }
        // If the object is moving upwards (positive vertical speed)
        else if (verticalSpeed > 0)
        {
            // Calculate rotation for upward movement
            // Allowing positive rotation for upward tilt
            float rotationAmount = Mathf.Clamp(verticalSpeed * maxRotation, 0, maxRotation);  // Positive for upward rotation
            RotatePlane(20);  // Rotate upwards
        }

        // Update the previous position for the next frame
        previousPosition = currentPosition;
    }

    // Method to rotate the plane smoothly on the Z-axis
    private void RotatePlane(float rotationAmount)
    {
        // Smoothly apply the rotation to the Z-axis based on the calculated amount
        currentRotation = Mathf.Lerp(currentRotation, rotationAmount, rotationSpeed * Time.deltaTime);

        // Apply the new rotation to the plane
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, currentRotation);
    }
}