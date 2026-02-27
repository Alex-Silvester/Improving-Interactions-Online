using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] GameObject playerCamera;

    void Update()
    {
        transform.LookAt(playerCamera.transform); 
    }
}
