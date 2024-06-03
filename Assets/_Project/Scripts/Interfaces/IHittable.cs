using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public void DealDamage(IHitSource source, float damage);
}

public interface IHitSource
{
    Transform Transform { get; }
}
