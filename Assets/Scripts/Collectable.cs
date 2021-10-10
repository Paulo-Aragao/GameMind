using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<PlayerController>().enabled)
            {
                other.gameObject.GetComponent<PlayerController>().Collect();
                Destroy(gameObject);
            }
        }
    }
}
