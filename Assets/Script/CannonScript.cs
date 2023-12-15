using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectilePrefab; // 砲彈預置物
    public Transform firePoint; // 砲口位置
    private GameObject playerBody;

    void Start()
    {
        // playerBody = GameObject.Find("PlayerBody");

        // // 檢查是否找到了物件
        // if (playerBody == null)
        // {
        //     Debug.LogError("Could not find a GameObject named 'PlayerBody'. Make sure the object exists in the scene.");
        // }
        // else
        // {
        //     Vector3 localOffset = playerBody.transform.TransformDirection(Vector3.right + Vector3.forward);
        //     transform.localPosition = playerBody.transform.localPosition + localOffset;
        // }
    }

    void Update()
    {
        // 檢測左鍵按下
        if (Input.GetButtonDown("Fire1"))
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        // 檢查砲口和砲彈預置物是否已指定
        if (firePoint != null && projectilePrefab != null)
        {
            // 在砲口位置生成砲彈
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}
