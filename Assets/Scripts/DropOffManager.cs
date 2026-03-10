using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropOffManager : MonoBehaviour
{
    [SerializeField] int requiredBoxes = 1;
    int collectedBoxes = 0;

    [SerializeField] List<string> pickupBoxTypes = new List<string>();

    [SerializeField] UnityEvent completionEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (!pickupBoxTypes.Contains(other.gameObject.tag) || collectedBoxes == requiredBoxes) return;

        collectedBoxes++;
        Destroy(other.gameObject);

        if(collectedBoxes == requiredBoxes)
        {
            completionEvent?.Invoke();
            Destroy(this.gameObject);
        }
        
    }
}
