using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData",menuName = "Data/Projectiles/Projectile Data")]
public class ProjectileData : ScriptableObject
{
    public float ProjectileSpeed = 20.0f;
    public float ProjectileDamage = 10.0f;
    public Projectile ProjectilePrefab;
    public float ProjectileDuration = 2.0f;
}
