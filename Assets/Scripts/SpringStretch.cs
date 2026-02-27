using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Animations;

public class SpringStretch : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject end;

    [SerializeField] Axis axis;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(
            transform.localScale.x,
            Vector3.Distance(start.transform.position, end.transform.position)/2f,
            transform.localScale.z); 

        transform.LookAt(start.transform);
        transform.Rotate(90, 0, 0);
    }
}
