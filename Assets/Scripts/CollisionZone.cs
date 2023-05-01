using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionZone : MonoBehaviour
{
    public UnityEvent PackageDestroyedEvent;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Package"))
        {
            PackageDestroyedEvent.Invoke();
            Destroy(collision.gameObject);
        }
    }
}
