using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField]
    private int MaxHp = 2;

	private void Start()
	{
        HP = MaxHp;
	}

	public int HP
    {
        get; set;
    }

    public void Die()
    {
        Destroy(gameObject);
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
