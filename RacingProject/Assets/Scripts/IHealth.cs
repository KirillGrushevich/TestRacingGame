using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    int HP { get; set; }
    void GetDamage(int damage);
    void Die();
}
