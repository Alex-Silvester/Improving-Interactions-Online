using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InBetween : MonoBehaviour
{
    [SerializeField] List<GameObject> gameObjects = new List<GameObject>();

    void FixedUpdate()
    {
        Vector3 pos = Vector3.zero;

        foreach(GameObject obj in gameObjects)
        {
            pos += obj.transform.position;
        }

        pos /= gameObjects.Count;

        transform.position = pos;
    }
}
