using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupController : MonoBehaviour
{
    [SerializeField] Transform holdPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] List<GameObject> potentialPickups = new List<GameObject>();
    GameObject pickupItem = null;

    public bool holdingBox => pickupItem != null;

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(holdPosition.position, new Vector3(0.2f, 0.2f,0.2f));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Interactable" && !holdingBox)
        {
            if (!collision.gameObject.GetComponent<InteractableBoxControl>().canPickUp) return;

            if (!potentialPickups.Contains(collision.gameObject))
            {
                potentialPickups.Add(collision.gameObject); 
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(potentialPickups.Contains(collision.gameObject))
        {
            potentialPickups.Remove(collision.gameObject);
        }
    }

    public  void OnPickupInteract()
    {
        torsoInteract();
    }

    public void torsoInteract()
    {
        if (potentialPickups.Count == 0 && pickupItem == null) return;

        if (pickupItem == null)
        {
            GetComponent<SpringCharacterController>().slowDown();

            pickupItem = potentialPickups[0];
            potentialPickups.RemoveAt(0);

            pickupItem.transform.parent = transform;
            pickupItem.transform.position = holdPosition.position;

            pickupItem.GetComponent<Rigidbody>().isKinematic = true;
            pickupItem.GetComponent<InteractableBoxControl>().canPickUp = false;
        }
        else
            dropBox();        
    }

    public void dropBox(bool closeText = false)
    {
        GetComponent<SpringCharacterController>().speedUp();

        pickupItem.GetComponent<Rigidbody>().isKinematic = false;
        pickupItem.GetComponent<InteractableBoxControl>().canPickUp = true;
        pickupItem.transform.parent = null;
        pickupItem = null;
    }

}
