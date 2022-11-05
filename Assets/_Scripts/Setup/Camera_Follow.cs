using UnityEngine;

public class Camera_Follow : StaticInstance<Camera_Follow>
{
    public Transform target;
    public Vector3 _cameraoffset;
    [Range(0.01f, 1.0f)]
    public float smoothness = 0.128f;

    void LateUpdate()
    {
        if (!target) target = CheckpointManager.Instance.currentCheckpoint.transform;
        Vector3 newPos = target.position + _cameraoffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
    }
}
