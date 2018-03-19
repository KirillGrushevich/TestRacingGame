using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InputManager : CreateSingletonGameObject<InputManager>
{
    public Action<object, InputType, Vector2, float> InputAction = (sender, type, vec, param) => { };

    public enum InputType
    {
        movement,
        tower,
        fire
    }


    private Vector2 movementAxis;
    private Vector2 towerAxis;



#if UNITY_EDITOR

    private void Update()
    {
        //Movement Axis
        movementAxis.x = Input.GetAxis("Horizontal");
        movementAxis.y = Input.GetAxis("Vertical");

        if (Mathf.Abs(movementAxis.x) > 0.01f || Mathf.Abs(movementAxis.y) > 0.01f)
        {
            InputAction(this, InputType.movement, movementAxis, 0);
        }


        //Tower Axis
        towerAxis.x = Input.GetAxis("Mouse X");
        //towerAxis.y = Input.GetAxis("Mouse Y");
        if (Mathf.Abs(towerAxis.x) > 0.01f)
        {
            InputAction(this, InputType.tower, towerAxis, 0);
        }


        //Shoot button
        if(Input.GetKeyDown(KeyCode.Space))
        {
            InputAction(this, InputType.fire, Vector2.zero, 0);
        }

    }

#endif
}

