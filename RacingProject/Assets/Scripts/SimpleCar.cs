using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCar : MonoBehaviour
{

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
    private float maxSteerAngle = 45f;
    [SerializeField]
    private Transform[] WheelMesh = new Transform[4];
    [SerializeField]
    private WheelCollider[] WheelCollider = new WheelCollider[4];

    private Rigidbody Rig;

    int fromWheelsUodate;
    int toWheelsUpdate = 4;


    float steer;
    float torque;

    private void Start()
    {
        InputManager.Instance.InputAction += UpdateAxes;

        if (CarDriveType == DriveType.RearWheelDrive)
            fromWheelsUodate = 2;
        if (CarDriveType == DriveType.FrontWheelDrive)
            toWheelsUpdate = 2;

        Rig = GetComponent<Rigidbody>();
        Rig.centerOfMass = CenterOfMass.localPosition;
    }

	private void OnDestroy()
	{
        InputManager.Instance.InputAction -= UpdateAxes;
	}

    private void UpdateAxes(object obj, InputManager.InputType type, Vector2 vec, float param)
    {
        if (type != InputManager.InputType.movement)
            return;

        steer = vec.x * maxSteerAngle;
        torque = vec.y * maxTorque;
    }


	public void Update()
    {
        UpdateMeshes();
    }

    public void FixedUpdate()
    {
        WheelCollider[0].steerAngle = steer;
        WheelCollider[1].steerAngle = steer;

        for (int i = fromWheelsUodate; i < toWheelsUpdate; i++)
        {
            WheelCollider[i].motorTorque = torque;
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
