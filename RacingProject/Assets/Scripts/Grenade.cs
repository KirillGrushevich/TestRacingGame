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
        GameObject obj = Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(obj, 1f);

        var health = collision.gameObject.GetComponent<IHealth>();
        if(health != null)
        {
            health.GetDamage(Damage);

        }

        Destroy(gameObject);
	}
}
