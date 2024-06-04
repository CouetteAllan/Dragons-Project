using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data",menuName = "Data/Player/General Player Data")]
public class PlayerData : ScriptableObject
{
    public float BaseSpeed = 15.0f;
    public float BaseDamage = 10.0f;
    public float BaseHealth = 100.0f;
    public float InvincibleTime = .5f;
}
