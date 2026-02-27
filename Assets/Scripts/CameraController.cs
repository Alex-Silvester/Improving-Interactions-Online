using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject followingObject;
    [SerializeField] Transform lookingAtObject;

    [Range(0f, 1f)]
    [SerializeField] float lerpValue = 0.01f;

    [SerializeField] bool lookAtPlayer = false;

    [SerializeField] Vector3 followingOffset = Vector3.zero;

    [SerializeField] Color gizmoColor;

    [SerializeField] Vector3 transformPosition;

    // Update is called once per frame
    private void FixedUpdate()
    {
        transformPosition = followingObject.transform.position;

        transform.position = Vector3.Lerp(
            transform.position,
            followingObject.transform.position + followingOffset,
            lerpValue * (1 + Vector3.Distance(transform.position, followingObject.transform.position + followingOffset)));

        float distDifference =
            Vector3.Distance(transform.position, lookingAtObject.position) -
            Vector3.Distance(followingObject.transform.position + followingOffset, lookingAtObject.position);

        Vector3 direction = (lookingAtObject.position - transform.position).normalized;
        direction.y = 0;

        transform.localPosition += direction * distDifference;

        if(lookAtPlayer)
            transform.LookAt(lookingAtObject.position);
    }

    public void setFollowing(GameObject pos)
    {
        followingObject = pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(followingObject.transform.position + followingOffset, new Vector3(0.2f,0.2f,0.2f));
    }
}
