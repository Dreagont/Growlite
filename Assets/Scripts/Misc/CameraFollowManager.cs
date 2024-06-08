using UnityEngine;
using Cinemachine;

public class CameraFollowManager : MonoBehaviour
{
    public string followTargetTag = "Player";
    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        FindAndSetFollowTarget();
    }

    private void FindAndSetFollowTarget()
    {
        GameObject followTarget = GameObject.FindGameObjectWithTag(followTargetTag);
        if (followTarget != null)
        {
            virtualCamera.Follow = followTarget.transform;
        }
        else
        {
            Debug.LogError("Follow target not found!");
        }
    }

    // Optionally, call this method if needed to re-assign target dynamically
    public void ReassignFollowTarget()
    {
        FindAndSetFollowTarget();
    }
}
