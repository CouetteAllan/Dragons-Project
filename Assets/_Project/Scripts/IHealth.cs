using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public float MaxHealth { get; }
    public float CurrentHealth { get;}
    public void ChangeHealth(float healthChange);
}
