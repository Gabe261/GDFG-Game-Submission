using UnityEngine;

public class MailboxAlso : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;  // Speed of rotation (degrees per second)
    [SerializeField] private float bounceHeight = 1f;  // Height of the bounce
    [SerializeField] private float bounceSpeed = 2f;   // Speed of the bounce

    private Vector3 startPosition;  // To store the object's initial position

    void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotate the object on the Y-axis
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        // Bounce the object up and down using a sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        
        // Update the position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
