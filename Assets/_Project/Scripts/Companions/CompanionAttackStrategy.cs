using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompanionAttackStrategy : ScriptableObject, ICompanionStrategy
{
    public static Action OnCompanionShoot;
    public float TimeBetweenAttacks = 5.0f;
    public float TimeBetweenShots => TimeBetweenAttacks;

    public abstract bool ShootStrategy();
}
