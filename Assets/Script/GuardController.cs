using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    public Transform patrolPoint;
    public LayerMask obstacleLayer;
    public float maxPatrolDistance = 10f; 
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float changeDirectionInterval = 3f;
    public float watchAroundInterval = 30f;
    public float alertAngle = 45f;
    public float alertRadius = 5f;
    private float timerWander = 0f;
    private float timerStop = 0f;
    private Vector3 targetPosition;
    private Vector3 direction;
    private Light sightLight;

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
        timerWander = changeDirectionInterval;
        
        // 獲取子物件上的 Light 組件
        sightLight = GetComponentInChildren<Light>();
        if (sightLight != null)
        {
            sightLight.spotAngle = alertAngle+10f;
            sightLight.range = alertRadius*2;
        }
    }

    void Update()
    {
        
        Wandering();
        CheckAlert();
    }

    void Wandering()
    {
        float distance = Vector3.Distance(transform.position, patrolPoint.position);
        timerWander += Time.deltaTime;
        timerStop += Time.deltaTime;
        if (timerWander >= changeDirectionInterval || distance >= maxPatrolDistance)
        {
            // 在指定半徑內隨機選擇一個目標點
            Vector2 randomPoint = Random.insideUnitCircle.normalized * maxPatrolDistance;
            targetPosition = new Vector3(patrolPoint.position.x + randomPoint.x, transform.position.y, patrolPoint.position.z + randomPoint.y);      
            
            // 計算目標點的方向
            direction = targetPosition - transform.position;

            timerWander = 0f;
        }

        // 轉動警衛朝向目標點的方向
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        // 讓警衛朝著目標點移動
        if (timerStop > watchAroundInterval && distance < maxPatrolDistance)
        {
            if (timerStop > watchAroundInterval+5){timerStop = 0f;} // 可以改數字用來挑整停留的區間
        }
        else{transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);}
        
    }

    //     void OnDrawGizmosSelected()
    // {
    //     if (patrolPoint != null)
    //     {
    //         // 以不同顏色繪製移動範圍
    //         Gizmos.color = Color.blue;
    //         Gizmos.DrawWireSphere(patrolPoint.position, maxPatrolDistance);
    //     }
    //     else
    //     {
    //         // 以不同顏色繪製移動範圍
    //         Gizmos.color = Color.blue;
    //         Gizmos.DrawWireSphere(transform.position, maxPatrolDistance);
    //     }

    //     // 移動目標
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireSphere(targetPosition, 0.5f);

    //     // 繪製扇形區域
    //     Gizmos.color = Color.red;
    //     DrawWireArc(transform.position, transform.up, transform.forward, alertAngle / 2, alertRadius, 45);
    //     DrawWireArc(transform.position, transform.up, transform.forward, -alertAngle / 2, alertRadius, 45);
    // }

    void DrawWireArc(Vector3 position, Vector3 up, Vector3 forward, float angle, float radius, int segments)
    {
        float angleStep = angle / segments;
        Quaternion rotation = Quaternion.AngleAxis(-angle / 2, up);

        Vector3 lastPoint = position + rotation * forward * radius;

        for (int i = 0; i < segments; i++)
        {
            Vector3 currentPoint = position + rotation * forward * radius;

            // 只在循環的最後一次繪製線條，連接首尾形成扇形的外邊緣
            if (i == segments-1)
            {
                Gizmos.DrawLine(position, currentPoint);
            }

            if (i > 0 && i < segments)
            {
                Vector3 start = position + rotation * forward * radius;
                rotation = Quaternion.AngleAxis(i * angleStep, up);
                Vector3 end = position + rotation * forward * radius;

                Gizmos.DrawLine(start, end);
            }
            
            lastPoint = currentPoint;
            rotation = Quaternion.AngleAxis(i * angleStep, up);
        }
    }

    void CheckAlert()
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
                            Debug.Log("Game Over");
                        }
                    }
                }
            }
        }
    }








}
