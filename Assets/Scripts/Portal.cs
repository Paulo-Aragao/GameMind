using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<PlayerMovement>() != null)
            {
                if(other.gameObject.GetComponent<PlayerMovement>().enabled)
                {
                    SceneManager.LoadScene("02", LoadSceneMode.Single);
                }
            }
        }
    }
}
