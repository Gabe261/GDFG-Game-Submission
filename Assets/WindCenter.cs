using UnityEngine;

public class WindCenter : MonoBehaviour
{
    public void UpdateRotation(Quaternion newRotation)
    {
        transform.rotation = newRotation;
    }
}
