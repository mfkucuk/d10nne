using System;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private LockObject lockObject;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySoundEffect("KeyPickup");
            lockObject.Unlock();
            Destroy(this.gameObject);
        }
    }
}
