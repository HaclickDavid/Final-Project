using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_Point : MonoBehaviour
{
    public float maxPatrolDistance = 10f;
        void OnDrawGizmosSelected()
    {
        // 以不同顏色繪製移動範圍
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxPatrolDistance);
    }
}
