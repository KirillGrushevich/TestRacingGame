using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class SimpleCar : MonoBehaviour
{
    public static Action<object, float, float> UpdateSpeed = (obj, speed, maxSpeed) => { };

    public enum DriveType
    {
        FrontWheelDrive,
        RearWheelDrive,
        FourWheelDrive
    }

    [SerializeField]
    private DriveType CarDriveType;

    [SerializeField]
    private Transform CenterOfMass;
    [SerializeField]
    private float maxTorque = 500f;
    [SerializeField]
    private float BrackeTorque = 1000f;
    [SerializeField]
    private float MaxSpeed = 20f;
    private float speed;
    private float accelerator;
    [SerializeField]
    private float maxSteerAngle = 45f;
    [SerializeField]
    private Transform[] WheelMesh = new Transform[4];
    [SerializeField]
    private WheelCollider[] WheelCollider = new WheelCollider[4];

    [SerializeField]
    private Rigidbody Rig;

    int fromWheelsUodate;
    int toWheelsUpdate = 4;


    float steer;
    float torque;
    float brakeTorque;

	private void Reset()
	{
        Rig = GetComponent<Rigidbody>();
	}

	private void Start()
    {
        InputManager.InputAction += UpdateAxes;

        if (CarDriveType == DriveType.RearWheelDrive)
            fromWheelsUodate = 2;
        if (CarDriveType == DriveType.FrontWheelDrive)
            toWheelsUpdate = 2;

        Rig.centerOfMass = CenterOfMass.localPosition;
    }


	private void OnDestroy()
	{
        InputManager.InputAction -= UpdateAxes;
	}

    private void UpdateAxes(object obj, InputManager.InputType type, Vector2 vec, float param)
    {
        if (type != InputManager.InputType.movement)
            return;

        steer = vec.x * maxSteerAngle;

        accelerator = Mathf.Lerp(accelerator, vec.y, Time.deltaTime * 2f);
        accelerator = vec.y > 0 ? accelerator : accelerator * -1;

        if (Vector3.Dot(transform.forward, Rig.velocity.normalized) < 0 ) 
        {
            print("back");
            //torque = 0;
            //brakeTorque = BrackeTorque;
            //return;
        }

        if (speed >= MaxSpeed)
        {
            accelerator = Mathf.Lerp(accelerator, 0, Time.deltaTime * 10f);
        }

        torque = vec.y * maxTorque * accelerator;
    }


	public void Update()
    {
        UpdateMeshes();
        speed = Rig.velocity.magnitude;
        if (speed > 0.01f)
            UpdateSpeed(this, speed, MaxSpeed);
    }

    public void FixedUpdate()
    {
        WheelCollider[0].steerAngle = steer;
        WheelCollider[1].steerAngle = steer;

        for (int i = fromWheelsUodate; i < toWheelsUpdate; i++)
        {
            WheelCollider[i].motorTorque = torque;
            WheelCollider[i].brakeTorque = brakeTorque;
        }
    }
    public void UpdateMeshes()
    {
        for (int i = 0; i < WheelMesh.Length; i++)
        {
            Quaternion quat;
            Vector3 pos;
            WheelCollider[i].GetWorldPose(out pos, out quat);
            WheelMesh[i].position = pos;
            WheelMesh[i].rotation = quat;
        }
    }
}
