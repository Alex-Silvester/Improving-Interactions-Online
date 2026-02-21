using UnityEngine;
using UnityEngine.Events;

public class DropOffManager : MonoBehaviour
{
    [SerializeField] int requiredBoxes = 1;
    int collectedBoxes = 0;

    [SerializeField] UnityEvent completionEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Pickup" || collectedBoxes == requiredBoxes) return;

        collectedBoxes++;
        Destroy(other.gameObject);

        if(collectedBoxes == requiredBoxes)
        {
            completionEvent?.Invoke();
            Destroy(this.gameObject);
        }
        
    }
}
