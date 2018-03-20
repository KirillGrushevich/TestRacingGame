using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class InputManager : CreateSingletonGameObject<InputManager>
{
    public Action<object, InputType, Vector2, float> InputAction = (sender, type, vec, param) => { /*sender to input log, debug or replay*/ };

    public enum InputType
    {
        movement,
        tower,
        fire
    }

    private Vector2 axis;

    private void Update()
    {
        //Movement Axis
        InputAction(this, InputType.movement, GetAxis("Horizontal", "Vertical"), 0);    

        //Tower Axis
        InputAction(this, InputType.tower, GetAxis("Mouse X", "Mouse Y"), 0);    


        //Shoot button
        if(Input.GetKeyDown(KeyCode.Space))
        {
            InputAction(this, InputType.fire, Vector2.zero, 0);
        }

    }

    private Vector2 GetAxis(string axisX, string axisY)
    {
        axis.x = Input.GetAxis(axisX);
        if (Mathf.Abs(axis.x) < 0.01f)
        {
            axis.x = CrossPlatformInputManager.GetAxis(axisX);
        }

        axis.y = Input.GetAxis(axisY);
        if (Mathf.Abs(axis.y) < 0.01f)
        {
            axis.y = CrossPlatformInputManager.GetAxis(axisY);
        }
        return axis;
    }

}

