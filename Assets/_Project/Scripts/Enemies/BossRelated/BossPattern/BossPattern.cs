using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossPattern : ScriptableObject
{
    public AnimatorOverrideController BossAnimPattern;

    public abstract void ExecutePattern(Vector2 direction, EnemyController enemy);
}
