using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class torsoController : MonoBehaviour
{
    [SerializeField] Vector3 holdPosition = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] List<GameObject> potentialPickups = new List<GameObject>();
    GameObject pickupItem = null;

    [SerializeField] GameObject pickupText = null;

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(holdPosition + transform.position, new Vector3(0.2f, 0.2f,0.2f));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Pickup")
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

        if(pickupItem == null)
        {
            pickupItem = potentialPickups[0];
            potentialPickups.Remove(pickupItem);

            pickupItem.transform.parent = transform;
            pickupItem.transform.position = holdPosition + transform.position;

            pickupItem.GetComponent<Rigidbody>().isKinematic = true;

            pickupText.GetComponent<TMP_Text>().text = "Press \"E\" to drop";
        }
        else
        {
            pickupItem.GetComponent<Rigidbody>().isKinematic = false;
            pickupItem.transform.parent = null;
            pickupItem = null;

            pickupText.GetComponent<TMP_Text>().text = "Press \"E\" to pick up";
        }
    }
}
