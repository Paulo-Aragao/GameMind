using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _virtualCam1;
    [SerializeField] private GameObject _virtualCam2;
    [SerializeField] private float _timeCamera = 3f;


    private void OnTriggerEnter(Collider other) 
    {
        _virtualCam2.SetActive(true);
        _virtualCam1.SetActive(false);
        Invoke(nameof(EnableMainCam),_timeCamera-0.2f);
        Destroy(gameObject,_timeCamera);
    }

    private void EnableMainCam()
    {
        _virtualCam1.SetActive(true);
        _virtualCam2.SetActive(false);
    }
}
