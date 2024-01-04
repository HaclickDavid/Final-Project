using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageSafe : MonoBehaviour
{
    public Transform checkPoint;
    public GameObject checkPlayerAreaPrefab;
    public Transform wanderPoint;
    public float changeDirectionInterval = 2f;
    public float watchAroundInterval = 30f;
    public float maxWanderDistance = 10f;
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    private GameObject lightObject;
    private GameObject checkPlayerHereArea;
    private CheckPlayerArea collisionScript;
    private float timerWander = 0f;
    private float timerStop = 0f;
    private Vector3 targetPosition;
    private Vector3 direction;
    private bool isCrash = false;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LightGenerate();
        AreaGenerate();   
        if (wanderPoint == null)
        {
            // 生成一個新的空物體
            GameObject newWanderPoint = new GameObject("PatrolPoint_AutoGenerate");

            // 設置新物體的位置為開始一瞬間的位置
            newWanderPoint.transform.position = transform.position;

            // 將新物體的 transform 賦值給 patrolPoint
            wanderPoint = newWanderPoint.transform;
        }   
        timerWander = changeDirectionInterval;
    }

    void Update()
    {
        if(collisionScript.CheckPlayerHere()){Rescue();}
        Wandering();
    }

    void Wandering()
    {
        float distance = Vector3.Distance(transform.position, wanderPoint.position);
        timerWander += Time.deltaTime;
        timerStop += Time.deltaTime;
        if (timerWander >= changeDirectionInterval || distance >= maxWanderDistance || isCrash)
        {
            // 在指定半徑內隨機選擇一個目標點
            Vector2 randomPoint = Random.insideUnitCircle.normalized * maxWanderDistance;
            targetPosition = new Vector3(wanderPoint.position.x + randomPoint.x, transform.position.y, wanderPoint.position.z + randomPoint.y);      
            
            // 計算目標點的方向
            direction = targetPosition - transform.position;

            timerWander = 0f;
        }

        // 轉動警衛朝向目標點的方向
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        // 讓警衛朝著目標點移動
        if (timerStop > watchAroundInterval && distance < maxWanderDistance)
        {
            if (timerStop > watchAroundInterval+2){timerStop = 0f;} // 可以改數字用來挑整停留的區間
        }
        //else{transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);}

        // 使用物理引擎的力来移动物体
        else{rb.MovePosition(rb.position + transform.TransformDirection(Vector3.forward) * moveSpeed * Time.deltaTime);}
        
    }

    private void AreaGenerate()
    {
        if (checkPoint == null)
        {
            // 生成一個新的空物體
            GameObject newCheckPoint = new GameObject("CheckPoint_AutoGenerate");
            // 設置新物體的位置為開始一瞬間的位置
            newCheckPoint.transform.position = transform.position;
            // 將新物體的 transform 賦值給 patrolPoint
            checkPoint = newCheckPoint.transform;
        }
        checkPlayerHereArea = Instantiate(checkPlayerAreaPrefab, checkPoint.position, Quaternion.identity);
        collisionScript = checkPlayerHereArea.GetComponent<CheckPlayerArea>();
    }

    private void LightGenerate()
    {
        lightObject = new GameObject("Ring_AutoGenerate");
        
        if (checkPoint == null)
        {
            lightObject.transform.position = new Vector3 (transform.position.x, transform.position.y+0.3f, transform.position.z);
        }
        else
        {
            lightObject.transform.position = new Vector3 (checkPoint.position.x, transform.position.y+0.3f, checkPoint.position.z);
        }

        Light lightComponent = lightObject.AddComponent<Light>();
        lightObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        lightComponent.type = LightType.Spot; 
        lightComponent.intensity = 100.0f; 
        lightComponent.spotAngle = 120f; 
        lightComponent.color = Color.green;
        lightComponent.shadows = LightShadows.None; 
    }

    private void Rescue()
    {
        Destroy(lightObject);
        Destroy(checkPlayerHereArea);
        Destroy(gameObject);
    }
    
    void OnDrawGizmosSelected()
    {
        if (wanderPoint != null)
        {
            // 以不同顏色繪製移動範圍
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(wanderPoint.position, maxWanderDistance);
        }
        else
        {
            // 以不同顏色繪製移動範圍
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, maxWanderDistance);
        }

        // 移動目標
        // Gizmos.color = Color.green;
        // Gizmos.DrawWireSphere(targetPosition, 0.5f);
    }

}
