using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class torsoController : MonoBehaviour
{
    [SerializeField] Transform holdPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] List<GameObject> potentialPickups = new List<GameObject>();
    GameObject pickupItem = null;

    [SerializeField] GameObject pickupText = null;

    public bool holdingBox => pickupItem != null;

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(holdPosition.position, new Vector3(0.2f, 0.2f,0.2f));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Pickup" && !holdingBox)
        {
            if (!potentialPickups.Contains(collision.gameObject))
            {
                pickupText.SetActive(true);
                potentialPickups.Add(collision.gameObject); 
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(potentialPickups.Contains(collision.gameObject))
        {
            pickupText.SetActive(false);
            potentialPickups.Remove(collision.gameObject);
        }
    }

    public void torsoInteract()
    {
        if (potentialPickups.Count == 0) return;

        if (pickupItem == null)
        {
            pickupItem = potentialPickups[0];
            potentialPickups.RemoveAt(0);

            pickupItem.transform.parent = transform;
            pickupItem.transform.position = holdPosition.position;

            pickupItem.GetComponent<Rigidbody>().isKinematic = true;

            pickupText.GetComponent<TMP_Text>().text = "Press \"E\" to drop";
        }
        else
            dropBox();

        
    }

    public void dropBox(bool closeText = false)
    {
        pickupItem.GetComponent<Rigidbody>().isKinematic = false;
        pickupItem.transform.parent = null;
        pickupItem = null;

        pickupText.GetComponent<TMP_Text>().text = "Press \"E\" to pick up";

        if (closeText && potentialPickups.Count == 0) pickupText.SetActive(false);
    }

}
