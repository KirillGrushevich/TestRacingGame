using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private int Damage = 1;

    [SerializeField]
    private GameObject Explosion;

	private void OnCollisionEnter(Collision collision)
	{
        GameObject obj = ObjectsPool.Instance.GetObject(Explosion);
        obj.transform.position = transform.position;

        var health = collision.gameObject.GetComponent<IHealth>();
        if(health != null)
        {
            health.GetDamage(Damage);

        }

        gameObject.SetActive(false);
	}
}
