using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DeliveryController : MonoBehaviour
{
    private List<Mailbox> mailboxList;

    [SerializeField] private List<GameObject> deliveryList;
    private int deliveryIndex;
    
    [SerializeField] private GameOver gameOver;
    
    public UnityEvent<Transform> OnNextPlane;
    public UnityEvent<Transform> OnNextWindCenter;
    public UnityEvent<GameObject> OnNewPlane;
    public UnityEvent<GameObject> OnNewTarget;
    
    private void Awake()
    {
        OnNextPlane ??= new UnityEvent<Transform>();
        OnNextWindCenter ??= new UnityEvent<Transform>();
        OnNewPlane ??= new UnityEvent<GameObject>();
        OnNewTarget??= new UnityEvent<GameObject>();
        
        mailboxList = FindObjectsOfType<Mailbox>().ToList();
        
        foreach (GameObject plane in deliveryList)
        {
            plane.TryGetComponent<AirplaneMovement>(out AirplaneMovement airplaneMovement);
            plane.TryGetComponent<AirPlaneCollision>(out AirPlaneCollision airPlaneCollision);
            
            airplaneMovement.enabled = false;
            airPlaneCollision.enabled = false;
        }
        deliveryIndex = 0;
        
        GoNextPlane();
    }

    public void GoNextPlane()
    {

        if (deliveryIndex >= deliveryList.Count)
        {
            if (RemainingMailboxes() == 0)
            {
                gameOver.UpdateScreen($"You Won!", $"You got {RemainingMailboxes()} mailboxes out of {deliveryList.Count}");
            }
            else
            {
                gameOver.UpdateScreen($"You Lose", $"You got {RemainingMailboxes()} mailboxes out of {deliveryList.Count}");
            }
            return;
        }

        
        
        deliveryList[deliveryIndex].TryGetComponent<AirplaneMovement>(out AirplaneMovement airplaneMovementFirst);
        deliveryList[deliveryIndex].TryGetComponent<AirPlaneCollision>(out AirPlaneCollision airPlaneCollisionFirst);
        
        airplaneMovementFirst.enabled = true;
        airPlaneCollisionFirst.enabled = true;
        
        if (deliveryIndex != 0)
        {
            deliveryList[deliveryIndex-1].SetActive(false);
            airplaneMovementFirst.EnableMovement(true);
        }
        
        
        OnNextPlane?.Invoke(airplaneMovementFirst.ReturnCameraFollow());
        OnNewPlane?.Invoke(airplaneMovementFirst.gameObject);
        OnNewTarget?.Invoke(airplaneMovementFirst.ReturnMailboxTarget());
        
        deliveryIndex++;
    }

    private int RemainingMailboxes()
    {
        int i = 0;
        foreach (Mailbox mailbox in mailboxList)
        {
            if (mailbox.hasBeenDelivered)
            {
                i++;
            }
        }
        return i;
    }
}
