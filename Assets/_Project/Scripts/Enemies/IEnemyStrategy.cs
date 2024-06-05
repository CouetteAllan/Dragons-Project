using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStrategy
{
    public void DoWalkInRange();
    public void DoAttack(Vector2 attackDirection);
    public AnimatorOverrideController ChoseAttack();
    public void SpawnBehaviour(Action OnFinishSpawnCallBack);
}
