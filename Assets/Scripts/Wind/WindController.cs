using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class WindController : MonoBehaviour
{
    [SerializeField] private float windChangeTime;
    [SerializeField] private List<WindTypeSO> windTypes;
    
    
    public UnityEvent<WindTypeSO> OnWindChanged;
    [HideInInspector] public Transform windTransform;
    [HideInInspector] public WindTypeSO windType;

    [SerializeField] private WindCenter windCenter;
    
    //public UnityEvent<Quaternion> OnWindDirectionChange;
    
    private void Awake()
    {
        //OnWindDirectionChange ??= new UnityEvent<Quaternion>();
        
        SetWindType(0);
        windTransform = transform;
        //StartCoroutine(WindChange());
    }

    public void SetWindType(int windTypeNum)
    {
        windType = windTypes[windTypeNum];
        SetRandomYRotation();
        //OnWindDirectionChange.Invoke(windTransform.rotation);
    }
    
    public void SetRandomYRotation()
    {
        // Generate a random value between 0 and 360
        float randomYRotation = Random.Range(0f, 360f);
        
        // Update the transform's rotation to the new random Y value while keeping the current X and Z rotation
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, randomYRotation, transform.eulerAngles.z);
        windCenter.UpdateRotation(Quaternion.Euler(transform.eulerAngles.x, randomYRotation, transform.eulerAngles.z));
    }
    
    public void SetWindCenter(Transform windCenterr)
    {
        this.windCenter = windCenterr.GetComponent<WindCenter>();
    }
}
