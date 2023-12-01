using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageSafe : MonoBehaviour
{
    public Transform patrolPoint;
    public LayerMask obstacleLayer;
    public float alertAngle = 90f;
    public float alertRadius = 5f;
    public float changeDirectionInterval = 3f;
    private Light sightLight;
    // Start is called before the first frame update
    void Start()
    {
        if (patrolPoint == null)
        {
            // 生成一個新的空物體
            GameObject newPatrolPoint = new GameObject("PatrolPoint_AutoGenerate");

            // 設置新物體的位置為開始一瞬間的位置
            newPatrolPoint.transform.position = transform.position;

            // 將新物體的 transform 賦值給 patrolPoint
            patrolPoint = newPatrolPoint.transform;
        }
        //timerWander = changeDirectionInterval;

        // 獲取子物件上的 Light 組件
        sightLight = GetComponentInChildren<Light>();
        if (sightLight != null)
        {
            sightLight.spotAngle = alertAngle + 10f;
            sightLight.range = alertRadius * 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
    }
    void CheckPlayer()
    {
        // 检测玩家是否在警戒圈内
        Collider[] colliders = Physics.OverlapSphere(transform.position, alertRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // 玩家在警戒圈内，发射射线检测是否在视野范围内
                Vector3 directionToPlayer = collider.transform.position - transform.position;

                // 使用 LayerMask 来忽略障碍物的层级
                RaycastHit hitObstacle;
                if (!Physics.Raycast(transform.position, directionToPlayer, out hitObstacle, alertRadius, obstacleLayer))
                {
                    // 没有障碍物，再次发射射线检测是否在视野范围内
                    RaycastHit hitPlayer;
                    if (Physics.Raycast(transform.position, directionToPlayer, out hitPlayer, alertRadius))
                    {
                        // 检查玩家是否在扇形范围内
                        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

                        if (angleToPlayer <= alertAngle / 2 && angleToPlayer >= -alertAngle / 2)
                        {
                            Rescue();
                            Debug.Log("Hostage is Safe");
                        }
                    }
                }
            }
        }
    }
    void Rescue()
    {
        GetComponent<MeshRenderer>().enabled = false;
        // GetComponent<Rigidbody>().iskinematic = true;
        GetComponent<HostageSafe>().enabled = false;
    }

}
