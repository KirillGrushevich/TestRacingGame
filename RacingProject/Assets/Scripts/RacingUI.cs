using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacingUI : MonoBehaviour 
{

    [SerializeField]
    private Image SpeedImage;

	private void Start()
	{
        SimpleCar.UpdateSpeed += UpdateSpeedUI;
	}

	private void OnDestroy()
	{
        SimpleCar.UpdateSpeed -= UpdateSpeedUI;
	}

	public void FireButton()
    {
        InputManager.InputAction(this, InputManager.InputType.fire, Vector2.zero, 0);
    }


    private void UpdateSpeedUI(object obj, float speed, float maxSpeed)
    {
        SpeedImage.fillAmount = speed / maxSpeed;
    }

}
