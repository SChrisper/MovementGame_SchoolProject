using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("CameraEffects")]
    public PlayerCam cam;
    public float dashFov;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.Q;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(dashKey))
            Dash();

        if(dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
        
    }

    private void Dash()
    {
        if(dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        cam.DoFov(dashFov);

        pm.dashing = true;

        Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;

        delayedForceToApply = forceToApply;

        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayedForceToApply;

    private void DelayedDashForce()
    {
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        cam.DoFov(85f);
        pm.dashing = false;

    }

    
}
