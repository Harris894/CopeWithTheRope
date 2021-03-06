﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProUserInput : MonoBehaviour {

    public int playerNumber;

    ProPlayerController proPlayerController;

    //Inputs
    float moveForward, moveRight;
    float jump;
    bool primaryAction, secondaryAction;
    bool startButton;

	// Use this for initialization
	void Start () {
        proPlayerController = GetComponent<ProPlayerController>();
	}
	
	// Update is called once per frame
	void Update () {


        //Reset values back to 0 so that it doesn't stack up.
        jump = 0;
        moveForward = 0;
        moveRight = 0;
        primaryAction = false;
        secondaryAction = false;
        

        #region Keyboard
        jump += Input.GetAxis(string.Format("Jump{0}", playerNumber));
        moveForward += Input.GetAxis(string.Format("Vertical{0}", playerNumber));
        moveRight += Input.GetAxis(string.Format("Horizontal{0}", playerNumber));
        primaryAction = Input.GetButtonDown(string.Format("Primary Action{0}", playerNumber));
        secondaryAction = Input.GetButtonDown(string.Format("Secondary Action{0}", playerNumber));

        if(playerNumber == 1)
        startButton = Input.GetButtonDown("Start Button");
        #endregion

        #region Joystick
        jump += Input.GetAxis(string.Format("A{0}", playerNumber));
        moveForward += Input.GetAxis(string.Format("Left Stick Y {0}", playerNumber));
        moveRight += Input.GetAxis(string.Format("Left Stick X {0}", playerNumber));


        if (!startButton && playerNumber == 1)
            startButton = Input.GetButtonDown(string.Format("Start {0}", 1));
        if (!primaryAction)
        primaryAction = Input.GetButtonDown(string.Format("X{0}", playerNumber));
        if(!secondaryAction)
        secondaryAction = Input.GetButtonDown(string.Format("B{0}", playerNumber));
        #endregion


        float lastTriggerTime = 0;
        #region HandleSomeInput
        if (startButton && PauseMenu.instance != null && Time.time > lastTriggerTime + 2f)
        {
            lastTriggerTime = Time.time;
            if (PauseMenu.instance.gameObject.activeInHierarchy)
            {
                PauseMenu.instance.SetActive(false);
            }
            else
            {
                PauseMenu.instance.SetActive(true);
            }
                        
        }
        #endregion



        //Limit all input between -1 and 1 since we are summing up all the values from different input methods.
        jump = Mathf.Clamp(jump, -1, 1);
        moveForward = Mathf.Clamp(moveForward, -1, 1);
        moveRight = Mathf.Clamp(moveRight, -1, 1);

        //Debug.Log(startButton);
        //Debug.LogFormat("Forward: {0} + Right: {1}", moveForward, moveRight);
        //Debug.Log(Input.GetJoystickNames().Length);
        proPlayerController.SendInput(jump, moveForward, moveRight, primaryAction, secondaryAction);
    }
}
