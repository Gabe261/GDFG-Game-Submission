using UnityEngine;

public class WindFollow : MonoBehaviour
{

    [SerializeField] private GameObject plane, wind;

    // Update is called once per frame
    void Update()
    {
        transform.position = plane.transform.position;
        
        transform.forward = wind.transform.forward;
    }

    public void UpdatePlane(Transform plane)
    {
        this.plane = plane.gameObject;
    }
}
