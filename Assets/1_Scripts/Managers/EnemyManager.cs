using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ��ġ��
/// Enemy ����
/// </summary>

public class EnemyManager : MonoBehaviour
{

    private void Start()
    {
        GameObject enemy = Resources.Load("Prefabs/Enemy") as GameObject;
    }
}
