using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public void ReceiveDamage(IHitSource source, float damage);
}

public interface IHitSource
{
    Transform Transform { get; }
}
