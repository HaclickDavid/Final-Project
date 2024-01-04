using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 玩家角色的Transform
    public float smoothSpeed = 0.1f; // 相機跟隨的平滑速度
    public Vector3 offset; // 相機與玩家的偏移量

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 playerPosition = target.transform.position;
            transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);

            // transform.LookAt(target.position);
        }
    }
}

