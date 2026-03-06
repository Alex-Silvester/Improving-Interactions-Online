using System.Collections.Generic;
using UnityEngine;


//Used to sort a list of gameObjects based on their distance to a transform
public class DistanceCompare : IComparer<GameObject>
{
    private Transform positionTransform;

    public DistanceCompare(Transform transform)
    {
        positionTransform = transform;

    }

    // Compares by Height, Length, and Width.
    public int Compare(GameObject x, GameObject y)
    {
        Vector3 posA = x.transform.position;
        Vector3 posB = y.transform.position;

        return Vector3.Distance(posA, positionTransform.position).CompareTo(Vector3.Distance(posB, positionTransform.position));
    }
}

public class KickControl : MonoBehaviour
{
    [SerializeField] float kickForce = 500f;
    [SerializeField] private List<GameObject> boxes = new List<GameObject>();
    DistanceCompare distanceCompare;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distanceCompare = new DistanceCompare(transform);
    }

    public void OnInteract()
    {
        if (boxes.Count == 0) return;

        GameObject box = boxes[0];
        Rigidbody boxRB = box.GetComponent<Rigidbody>();

        boxRB.AddExplosionForce(kickForce, transform.position, GetComponent<SphereCollider>().radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Kick box") return;

        boxes.Add(other.gameObject);

        boxes.Sort(distanceCompare);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Kick box") return;

        if (boxes.Contains(other.gameObject)) boxes.Remove(other.gameObject);
    }
}
