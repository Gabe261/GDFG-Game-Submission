using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirplaneMovement : MonoBehaviour
{
    [SerializeField] private Sliders airplaneUI;
    [SerializeField] private float glideSpeed = 2f; // Forward speed
    [SerializeField] private float rotationSpeed = 50f; // Rotate speed
    [SerializeField] private float fallSpeed = 5f; // 'Gravity'
    [SerializeField] private WindController windController; // Wind direction (could also be a Vector3)

    [SerializeField] private float climbSpeed = 15f;
    [SerializeField] private float slowMoveSpeed = 15f;

    [SerializeField] private Transform CameraFollow;

    [SerializeField] private Transform WindCenter;

    [SerializeField] private GameObject Target;
    

    
    public Transform ReturnCameraFollow()
    {
        return CameraFollow;
    }
    
    public GameObject ReturnMailboxTarget()
    {
        return Target;
    }
    
    public Transform ReturnWindCenter()
    {
        return WindCenter;
    }
    
    private float leftWingInput = 0f; // Left wing input (-50 to 50)
    private float rightWingInput = 0f; // Right wing input (-50 to 50)
    private float wingTurnSpeed = 1f; // Speed at which the wing values return to 0 when no input is pressed
    private float wingReturnSpeed = 5f;
    private float downwardMultiplier = 8f;
    
    [SerializeField] private Transform planeTransform;  // The plane's transform for presentation
    private Quaternion initialRotation;  // The initial forward-facing rotation of the plane

    public UnityEvent<float> OnLeftWingMove;
    public UnityEvent<float> OnRightWingMove;

    private bool enableMovement = false;

    public void EnableMovement(bool toEnable)
    {
        enableMovement = toEnable;
    }
    
    private void Start()
    {
        OnLeftWingMove ??= new UnityEvent<float>();
        OnRightWingMove ??= new UnityEvent<float>();
        initialRotation = planeTransform.rotation;  // Store the initial rotation when the game starts
    }


    
    
    void Update()
    {
        if (enableMovement)
        {
            HandlePlayerInput(); // Handle the player's input for wing control
            HandleAirplaneRotation(); // Handle airplane rotation based on wing inputs
            ApplyWindEffect(); // Apply wind effect to the airplane's movement
        }

    }

    // ----------------------------------------------------------------------------------------------------------------- handling player's input for wing control
    private void HandlePlayerInput()
    {
        
        // Update left wing input (Q = up, A = down)
        if (Input.GetKey(KeyCode.Q))
        {
            leftWingInput = Mathf.Clamp(leftWingInput - wingTurnSpeed * Time.deltaTime, -2f, 2f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            leftWingInput = Mathf.Clamp(leftWingInput + wingTurnSpeed * Time.deltaTime, -2f, 2f);
        }
        else
        {
            // Gradually return to 0 when no input
            leftWingInput = Mathf.MoveTowards(leftWingInput, 0f, wingReturnSpeed * Time.deltaTime);
        }

        // Update right wing input (E = up, D = down)
        if (Input.GetKey(KeyCode.E))
        {
            rightWingInput = Mathf.Clamp(rightWingInput - wingTurnSpeed * Time.deltaTime, -2f, 2f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rightWingInput = Mathf.Clamp(rightWingInput + wingTurnSpeed * Time.deltaTime, -2f, 2f);
        }
        else
        {
            // Gradually return to 0 when no input
            rightWingInput = Mathf.MoveTowards(rightWingInput, 0f, wingReturnSpeed * Time.deltaTime);
        }
        
        airplaneUI.UpdateSliders(leftWingInput, rightWingInput);
    }

    // ----------------------------------------------------------------------------------------------------------------- airplane rotation based on player input
    private void HandleAirplaneRotation()
    {
        if (leftWingInput > 0.1f || rightWingInput > 0.1f)
        {
            if (leftWingInput > 0.1f && rightWingInput > 0.1f)
            {
                transform.Translate(Vector3.down * 15f * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.down * 5f * Time.deltaTime);
            }
        }
        // Combine the wing inputs to determine the turn direction
        float turnInput = rightWingInput - leftWingInput;

        // Handle rotation (turn around the Y-axis)
        transform.Rotate(0f, turnInput * rotationSpeed * Time.deltaTime, 0f);
    }

    // -----------------------------------------------------------------------------------------------------------------  applying wind effect to the airplane's movement
    private void ApplyWindEffect()
    {
        // Switch statement for handling different wind types
        switch (windController.windType.WindTypeName)
        {
            case "Breeze":
                ApplyBreezeEffect();   // Apply wind effect for "Breeze"
                break;
            case "Moderate Gale":
                ApplyModerateGaleEffect();   // Empty for now
                break;
            case "Monsoon":
                ApplyMonsoonEffect();   // Empty for now
                break;
            default:
                break;
        }
    }

    // Apply wind effect for "Breeze"
    private void ApplyBreezeEffect()
    {
        // Get the Y-rotation (eulerAngles.y) of both the plane and the wind
        float planeYRotation = transform.eulerAngles.y;
        float windYRotation = windController.windTransform.eulerAngles.y;

        // Calculate the difference in their Y-rotation values
        float angleDifference = Mathf.DeltaAngle(planeYRotation, windYRotation);

        //Debug.Log("Breeze Angle Difference: " + angleDifference);

        // If the plane's forward direction is within the positive 180-degree range of the wind's direction (i.e., angleDifference between -90 and 90)
        if (angleDifference >= -90f && angleDifference <= 90f)
        {
            // Facing the wind, or within the positive 180-degree range
            float adjustedFallSpeed = 0.1f;  // Small constant gravity effect when facing the wind

            // The forward speed is affected by how closely the player's rotation matches the wind's Y-axis
            float windFactor = 1.5f - Mathf.Abs(angleDifference) / 90f;  // Wind factor based on angle difference
            
            float adjustedGlideSpeed = glideSpeed + (windController.windType.WindStrength * windFactor);

            transform.Translate(Vector3.down * adjustedFallSpeed * Time.deltaTime);  // Falling effect (gravity)
            transform.Translate(Vector3.forward * adjustedGlideSpeed * Time.deltaTime);  // Adjusted forward speed with wind
        }
        else
        {
            // Wind is opposite to the plane's forward direction (negative 180 degrees)
            // Set a constant slow speed and a fast falling effect
            float adjustedFallSpeed = fallSpeed;  // Greater constant gravity effect when facing opposite to the wind
            float adjustedGlideSpeed = glideSpeed * slowMoveSpeed;  // Slow forward speed when facing opposite direction

            transform.Translate(Vector3.down * adjustedFallSpeed * Time.deltaTime);  // Constant fast falling
            transform.Translate(Vector3.forward * adjustedGlideSpeed * Time.deltaTime);  // Constant slow forward speed
        }
    }

    // Apply wind effect for "Moderate Gale"
    private void ApplyModerateGaleEffect()
    {
        // Get the Y-rotation (eulerAngles.y) of both the plane and the wind
        float planeYRotation = transform.eulerAngles.y;
        float windYRotation = windController.windTransform.eulerAngles.y;

        // Calculate the difference in their Y-rotation values
        float angleDifference = Mathf.DeltaAngle(planeYRotation, windYRotation);

        // Debug log to check angle and direction
        //Debug.Log("Updraft Angle Difference: " + angleDifference);

        // If the plane's forward direction is within the positive 180-degree range of the wind's direction (i.e., angleDifference between -90 and 90)
        if (angleDifference >= -90f && angleDifference <= 90f)
        {
            // Facing the wind, or within the positive 180-degree range
            float adjustedFallSpeed = 0.1f;  // Small constant gravity effect when facing the wind

            // The forward speed is affected by how closely the player's rotation matches the wind's Y-axis
            float windFactor = 1.5f - Mathf.Abs(angleDifference) / 90f;  // Wind factor based on angle difference
            float adjustedGlideSpeed = glideSpeed + (windController.windType.WindStrength * windFactor);

            transform.Translate(Vector3.down * adjustedFallSpeed * Time.deltaTime);  // Falling effect (gravity)
            transform.Translate(Vector3.forward * adjustedGlideSpeed * Time.deltaTime);  // Adjusted forward speed with wind

        }
        // If the plane is facing within the backwards 60 degrees (exact opposite direction)
        else if (angleDifference >= 140f || angleDifference <= -140f)
        {
            // In the backward-facing 60-degree range, the plane moves upward quickly but forward slowly (updraft effect)
            float adjustedFallSpeed = climbSpeed;  // Reduced fall speed (updraft)
            float adjustedGlideSpeed = glideSpeed * slowMoveSpeed * 1.5f;  // Slow forward speed in this region

            transform.Translate(Vector3.up * adjustedFallSpeed * Time.deltaTime);  // Updraft effect (gravity)
            transform.Translate(Vector3.forward * adjustedGlideSpeed * Time.deltaTime);  // Slow forward speed in updraft
        }
        else
        {
            // Wind is opposite to the plane's forward direction (negative 180 degrees)
            // Set a constant slow speed and a fast falling effect
            float adjustedFallSpeed = fallSpeed;  // Greater constant gravity effect when facing opposite to the wind
            float adjustedGlideSpeed = glideSpeed * slowMoveSpeed;  // Slow forward speed when facing opposite direction

            transform.Translate(Vector3.down * adjustedFallSpeed * Time.deltaTime);  // Constant fast falling
            transform.Translate(Vector3.forward * adjustedGlideSpeed * Time.deltaTime);  // Constant slow forward speed
        }
    }

    // Apply wind effect for "Monsoon"
    private void ApplyMonsoonEffect()
    {
        // Get the Y-rotation (eulerAngles.y) of both the plane and the wind
        float planeYRotation = transform.eulerAngles.y;
        float windYRotation = windController.windTransform.eulerAngles.y;

        // Calculate the difference in their Y-rotation values
        float angleDifference = Mathf.DeltaAngle(planeYRotation, windYRotation);

        //Debug.Log("Monsoon Angle Difference: " + angleDifference);

        // If the plane's forward direction is within the new reduced range of -45 to 45 degrees of the wind's direction
        if (angleDifference >= -45f && angleDifference <= 45f)
        {
            // Facing the wind, or within the positive 180-degree range
            float adjustedFallSpeed = 0.1f;  // Small constant gravity effect when facing the wind

            // The forward speed is affected by how closely the player's rotation matches the wind's Y-axis
            float windFactor = 1.5f - Mathf.Abs(angleDifference) / 90f;  // Wind factor based on angle difference
            float adjustedGlideSpeed = glideSpeed + (windController.windType.WindStrength * windFactor);

            transform.Translate(Vector3.down * adjustedFallSpeed * Time.deltaTime);  // Falling effect (gravity)
            transform.Translate(Vector3.forward * adjustedGlideSpeed * Time.deltaTime);  // Adjusted forward speed with wind
        }
        else
        {
            // Wind is opposite to the plane's forward direction (negative 180 degrees)
            // Set a constant slow speed and a fast falling effect
            float adjustedFallSpeed = fallSpeed;  // Greater constant gravity effect when facing opposite to the wind
            float adjustedGlideSpeed = glideSpeed * slowMoveSpeed;  // Slow forward speed when facing opposite direction

            transform.Translate(Vector3.down * adjustedFallSpeed * Time.deltaTime);  // Constant fast falling
            transform.Translate(Vector3.forward * adjustedGlideSpeed * Time.deltaTime);  // Constant slow forward speed
        }
    }
}
