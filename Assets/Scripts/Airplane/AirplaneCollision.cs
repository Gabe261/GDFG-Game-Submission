using UnityEngine;
using UnityEngine.Events;

public class AirPlaneCollision : MonoBehaviour
{
    public UnityEvent OnEnvironmentHit;
    public UnityEvent OnMailBoxHit;
    public UnityEvent OnCoinHit;
    public UnityEvent OnEtceteraHit;
    
    private void Awake()
    {
        OnEnvironmentHit ??= new UnityEvent();
        OnMailBoxHit ??= new UnityEvent();
        OnCoinHit ??= new UnityEvent();
        OnEtceteraHit ??= new UnityEvent();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Mailbox>(out Mailbox mailbox)) // Mailbox (LevelComplete)
        {
            mailbox.hasBeenDelivered = true;
            OnMailBoxHit?.Invoke();
            return;
        }
        
        if (other.gameObject.TryGetComponent<MailboxAlso>(out MailboxAlso mailboxAlso)) // Mailbox (LevelComplete)
        {
            mailboxAlso.gameObject.GetComponentInParent<Mailbox>().hasBeenDelivered = true;
            OnMailBoxHit?.Invoke();
            return;
        }
        
        if (other.gameObject.TryGetComponent<Collider>(out Collider collider)) // GameObject with Collider
        {
            OnEnvironmentHit?.Invoke();
        } 
        else if (other.gameObject.TryGetComponent<Collider>(out Collider collider2)) // Mailbox (LevelComplete)
        {
            OnMailBoxHit?.Invoke();
        }
        else if (other.gameObject.TryGetComponent<Collider>(out Collider collider3)) // Coin collection
        {
            OnCoinHit?.Invoke();
        }
        else // Other object TODO
        {
            OnEtceteraHit?.Invoke();
        }
    }
}
