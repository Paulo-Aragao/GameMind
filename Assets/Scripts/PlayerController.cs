using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    
    float translation;
    float strafe;
 
     void FixedUpdate () {
        if(Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f )
        {
            transform.Translate(Vector3.forward * (moveSpeed) * Time.deltaTime);
        }
    }
}
