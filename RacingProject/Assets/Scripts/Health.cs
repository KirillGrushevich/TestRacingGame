using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField]
    private int MaxHp = 2;

    public void Start()
	{
        HP = MaxHp;
	}

	public int HP
    {
        get; set;
    }

    public void Die()
    {
        gameObject.AddComponent<ObjectReconstruction>();
        GetComponent<Rigidbody>().isKinematic = true;
        foreach (Transform transf in transform)
        {
            transf.gameObject.SetActive(false);

        }
    }

    public void GetDamage(int damage)
    {
        HP -= damage;
        if(HP <= 0)
        {
            Die();
        }
    }
}
