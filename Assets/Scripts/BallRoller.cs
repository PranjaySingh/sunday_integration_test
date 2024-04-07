using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRoller : MonoBehaviour
{
    public int level = 1;
    public float torqueAmount;
    private Vector3 cameraOffset;

    private bool isPressing = false;
    private Vector2 originalPressPoint = Vector2.zero;

    private GameObject playerBall;                                              //pj was here

    private void Awake()
    {
        MyEventSystem.I.StartLevel(level);
        FirebaseEvents.Instance.StartLevel(level);                              //pj was here : triggering a firebase event.
    }

    private void Start()
    {
        playerBall = GameObject.Find("PlayerBall");                             //pj was here : reference the playerball at the start
        // cameraOffset = GameObject.Find("PlayerBall").transform.position - Camera.main.transform.position;
        cameraOffset = playerBall.transform.position - Camera.main.transform.position;                                               //pj was here
    }

    private void FixedUpdate()                                                  //pj was here : using FixedUpdate instead of Update so adding torque to the playerBall object would be consistent through different FPS.
    {
        if(!playerBall)
        {
            playerBall = GameObject.Find("PlayerBall");                         //pj was here : finding playerBall only when not already referenced instead of finding it every frame. Thus, avoiding performance issues.
        }

        // var ballRigidbody = GameObject.Find("PlayerBall").GetComponent<Rigidbody>();             //pj was here : finding a GameObject through the scene every frame hinders the performance of the game.

        if (Input.GetMouseButton(0))
        {
            if (!isPressing)
            {
                originalPressPoint = Input.mousePosition;
                isPressing = true;
            }
            else
            {
                Vector2 diff = (originalPressPoint - new Vector2(Input.mousePosition.x, Input.mousePosition.y)).normalized;
                
                // ballRigidbody.AddTorque((Vector3.forward * diff.x + Vector3.right * -diff.y) * torqueAmount, ForceMode.VelocityChange);
                playerBall.GetComponent<Rigidbody>().AddTorque((Vector3.forward * diff.x + Vector3.right * -diff.y) * torqueAmount, ForceMode.VelocityChange);                      //pj was here
            }
        }
        else
        {
            isPressing = false;
        }

        // Camera.main.transform.position = ballRigidbody.transform.position - cameraOffset;
        Camera.main.transform.position = playerBall.transform.position - cameraOffset;                  //pj was here
    }
}
