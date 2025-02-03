using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MapVisualizer : MonoBehaviour
{
    public Transform northBoundary, southBoundary, eastBoundary, westBoundary;
    public GameObject plane;
    public GameObject target;
    public RectTransform planeIcon, pinIcon;
    public RectTransform topBoundary, bottomBoundary, leftBoundary, rightBoundary;

    public Vector2 ratio, ratio2;
    public Vector3 targetPos, targetPos2;
    public float moveOnX, moveOnY, moveOnX2, moveOnY2;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        planeIcon.transform.localEulerAngles = new Vector3(0, 0, -1*plane.transform.localEulerAngles.y); //rotates the plane icon on the map
        
        //this section calculates the distance between bounds in the 3D space and moves the plane icon accordingly
        ratio = new Vector2((plane.transform.position.x - southBoundary.position.x) / (northBoundary.position.x - southBoundary.position.x), 
            (plane.transform.position.z - westBoundary.position.z) / (eastBoundary.position.z - westBoundary.position.z));
        moveOnX = -(leftBoundary.localPosition.x - rightBoundary.localPosition.x) * ratio.x;
        moveOnY = -(bottomBoundary.localPosition.y - topBoundary.localPosition.y) * ratio.y;
        targetPos = new Vector3(leftBoundary.localPosition.x + moveOnX, bottomBoundary.localPosition.y + moveOnY, 0);
        
        planeIcon.transform.localPosition = targetPos;
        
        //this section calculates the distance between bounds in the 3D space and moves the pin icon accordingly
        ratio2 = new Vector2((target.transform.position.x - southBoundary.position.x) / (northBoundary.position.x - southBoundary.position.x), 
            (target.transform.position.z - westBoundary.position.z) / (eastBoundary.position.z - westBoundary.position.z));
        moveOnX2 = -(leftBoundary.localPosition.x - rightBoundary.localPosition.x) * ratio2.x;
        moveOnY2 = -(bottomBoundary.localPosition.y - topBoundary.localPosition.y) * ratio2.y;
        targetPos2 = new Vector3(leftBoundary.localPosition.x + moveOnX2, bottomBoundary.localPosition.y + moveOnY2, 0);
        
        pinIcon.transform.localPosition = targetPos2;
    }

    public void UpdateActivePlane(GameObject plane)
    {
        this.plane = plane;
    }

    public void UpdateActiveTarget(GameObject target)
    {
        this.target = target;
    }
}
