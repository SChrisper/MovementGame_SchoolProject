using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSavedTime : MonoBehaviour
{
    [Header("Reference")]
    private Timer ti;
    // Start is called before the first frame update
    void Start()
    {
        ti.SaveTime();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
