using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    [SerializeField] GameObject playerCamera;

    void Start()
    {
        playerCamera.transform.parent = null;
    }
}
