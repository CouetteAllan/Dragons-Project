using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICompanion
{
    public bool Shoot();
    public void Deliver(PlayerController player);
}
