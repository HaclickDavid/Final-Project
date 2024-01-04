using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerArea : MonoBehaviour
{
    
    private bool checkPlayer = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkPlayer = true;
            Debug.Log("HERE");
            Destroy(gameObject);
        }
    }

    public bool CheckPlayerHere()
    {
        return checkPlayer;
    }
}
