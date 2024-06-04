using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyConfig _datas;

    public void Initialize(EnemyConfig datas)
    {
        _datas = datas;
    }

    //TODO: Enemy walk in range, then attack => repeat
}
