using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth{
    float MaxHealth { get; }
    float CurrentHealth { get; }

    void TakeDamage(float damageAmount);
    void onDeath(); 
}
