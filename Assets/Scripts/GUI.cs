using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("01", LoadSceneMode.Single);
    }
}
