using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICompanionStrategy 
{
    public float TimeBetweenShots { get; }
    public bool ShootStrategy();
}
