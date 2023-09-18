using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 랜덤한 위치에
/// Enemy 생성
/// </summary>

public class EnemyManager : MonoBehaviour
{

    private void Start()
    {
        GameObject enemy = Resources.Load("Prefabs/Enemy") as GameObject;
    }
}
