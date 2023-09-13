using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 에너미는 생성되면 그 자리에 가만히 있고
/// 공격받으면 공격한다.
/// 몬스터 종류에 따라 스탯이 다름
/// </summary>

public enum EnemyState
{
    Idle,
    Attack,
    Damaged,
    Die
}

public class EnemyController : MonoBehaviour
{
    [Header("EnemyState")]
    [SerializeField]
    EnemyState eState = EnemyState.Idle;


    [Header("EnemyStats")]
    [SerializeField]
    private float hp;
    public float MaxHp
    {
        get { return hp; }
        set { hp = value; }
    }

    [SerializeField]
    private float mp;
    public float MaxMp
    {
        get { return mp; }
        set { mp = value; }
    }

    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed
    { 
        get { return attackSpeed; } 
        set { attackSpeed = value; } 
    }

    [SerializeField]
    private float damage;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private void Start()
    {
        switch (this.transform.gameObject.layer)
        {
            case (6):
                EnemyStatsInitalize(10.0f, 10.0f, 0.5f, 1.0f);
                break;

            case (7):
                EnemyStatsInitalize(20.0f, 10.0f, 0.45f, 1.5f);
                break;

            case (8):
                EnemyStatsInitalize(25.0f, 10.0f, 0.4f, 2.0f);
                break;
        }
    }

    private void EnemyStatsInitalize(float hp, float mp, float attackSpeed, float damage)
    {
        MaxHp = hp;
        MaxMp = mp;
        AttackSpeed = attackSpeed;
        Damage = damage;
    }

    private void Update()
    {
        switch (eState)
        {
            case EnemyState.Idle:      Idle();      break;
            case EnemyState.Attack:    Attack();    break;
            case EnemyState.Damaged:   Damaged();   break;
            case EnemyState.Die:       Die();       break;
        }
    }

    private void Idle()
    {

    }

    private void Attack()
    {

    }

    private void Damaged()
    {

    }

    private void Die()
    {

    }
}
