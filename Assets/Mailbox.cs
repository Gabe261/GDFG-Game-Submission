using System;
using UnityEngine;

public class Mailbox : MonoBehaviour
{
    public bool hasBeenDelivered = false;
    private MailboxAlso marker;


    private void Start()
    {
        marker = GetComponentInChildren<MailboxAlso>();
    }

    private void Update()
    {
        if (hasBeenDelivered)
        {
            marker.gameObject.SetActive(false);
        }   
    }
}
