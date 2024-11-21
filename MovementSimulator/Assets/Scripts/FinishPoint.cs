using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    [Header("References")]
    private Timer ti;
    private void Start()
    {
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
            {   
                SceneManager.LoadScene(2);
                ti.SaveTime();
            }
    }
}
