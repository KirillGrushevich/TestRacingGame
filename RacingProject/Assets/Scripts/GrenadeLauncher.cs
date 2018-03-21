using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    [Range(0.01f, 5f)]
    [SerializeField]
    private float NextShotTimer = 1f;

    [SerializeField]
    private float Impulse = 35f;

    [SerializeField]
    private GameObject Grenade;

    [SerializeField]
    private Transform FirePoint;

    public Coroutine WaitProcess { get; set; }


	private void Start()
	{
        InputManager.InputAction += Shoot;
	}

	private void OnDestroy()
	{
        InputManager.InputAction -= Shoot;
	}


	public void Shoot(object obj, InputManager.InputType type, Vector2 axes, float val)
    {
        if (type != InputManager.InputType.fire)
            return;

        Shot(FirePoint.position, FirePoint.forward);
    }


    public void Shot(Vector3 position, Vector3 direction)
    {
        if (WaitProcess != null)
            return;

        GameObject obj = Instantiate(Grenade, position, Quaternion.LookRotation(direction));
        obj.GetComponent<Rigidbody>().AddForce(direction * Impulse, ForceMode.Impulse);

        WaitProcess = StartCoroutine(WaitCoroutine(NextShotTimer));
    }

    public IEnumerator WaitCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        WaitProcess = null;
    }

}
