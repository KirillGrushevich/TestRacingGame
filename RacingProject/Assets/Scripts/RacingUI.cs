using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RacingUI : MonoBehaviour 
{

    [SerializeField]
    private Image SpeedImage;

    [SerializeField]
    private GameObject PausePanel;

    [SerializeField]
    private GameObject PauseButton;

	private void Start()
	{
        SimpleCar.UpdateSpeed += UpdateSpeedUI;
        if(PausePanel)
            PausePanel.SetActive(false);
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

    public void PauseOn()
    {
        PausePanel.SetActive(true);
        PauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        PausePanel.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void LoadLebel(string levelName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelName);
    }

}
