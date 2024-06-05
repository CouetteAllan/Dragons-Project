﻿using UnityEngine;

[CreateAssetMenu(fileName = "BossDash",menuName = "Data/Enemy/Boss Pattern/Dash")]
public class BossDashRush : BossPattern
{
    public float DashSpeed = 10.0f;
    public override void ExecutePattern(Vector2 direction, EnemyController enemy)
    {
        //Dash in the PlayerDirection
        enemy.GetRB().AddForce(direction * DashSpeed * 8.0f, ForceMode2D.Impulse);
    }
}