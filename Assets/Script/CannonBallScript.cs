using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    public float moveSpeed = 100f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroyAfterDelay(3f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(Vector3.up) * moveSpeed * Time.deltaTime);
    }
        IEnumerator DestroyAfterDelay(float delay)
    {
        // 等待指定的秒數
        yield return new WaitForSeconds(delay);

        // 等待完成後，銷毀物體
        Destroy(gameObject);
    }
}
