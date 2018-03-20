using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    [Header("Tower Settings")]

    [SerializeField]
    private Transform Tower;

    [Range(1f, 10f)]
    [SerializeField]
    private float TowerSpeed = 3f;

    [Space]
    [Header("Gun Settings")]

    [SerializeField]
    private Transform Gun;

    [Range(1f, 10f)]
    [SerializeField]
    private float GunSpeed = 3f;

    [Range(0, 25)]
    [SerializeField]
    private float maxDownAngle = 15;

    [Range(0f, 60)]
    [SerializeField]
    private float maxUpAngle = 25;

    private float gunRotation;


	private void Start()
	{
        InputManager.Instance.InputAction += Rotate;
	}

	private void OnDisable()
	{
        InputManager.Instance.InputAction -= Rotate;
	}


	public void Rotate(object obj, InputManager.InputType type, Vector2 axes, float val)
    {
        if (type != InputManager.InputType.tower)
            return;

        RotateTower(axes.x);
        RotateGun(axes.y);
    }

    private void RotateTower(float axisValue)
    {
        Tower.Rotate(0, axisValue * TowerSpeed, 0);
    }

    private void RotateGun(float axisValue)
    {
        gunRotation = gunRotation + axisValue * GunSpeed;
        gunRotation = Mathf.Clamp(gunRotation, -maxUpAngle, maxDownAngle);

        Quaternion rotation = Quaternion.Euler(gunRotation, 0, 0);

        Gun.localRotation = rotation;

    }
}
