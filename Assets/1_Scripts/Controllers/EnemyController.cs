using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// 에너미는 생성되면 그 자리에 가만히 있고
/// 공격받으면 공격한다.
/// 몬스터 종류에 따라 스탯이 다름
/// </summary>

public enum EnemyState
{
    Attack,
    Damaged,
    Die
}

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float originHP;
    private float curTime;

    PlayerController player;

    [Header("EnemyState")]
    [SerializeField]
    EnemyState eState = EnemyState.Damaged;


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

    private void Awake()
    {
        EnemySeparate(this.transform.gameObject.layer);

        FindPlayer();
    }

    private void EnemySeparate(int layer)
    {
        switch (layer)
        {
            case (6):
                EnemyStatsInitalize(10.0f, 10.0f, 5.0f, 1.0f);
                break;

            case (7):
                EnemyStatsInitalize(20.0f, 10.0f, 4.5f, 1.5f);
                break;

            case (8):
                EnemyStatsInitalize(25.0f, 10.0f, 4.0f, 2.0f);
                break;
        }
    }

    private void EnemyStatsInitalize(float hp, float mp, float attackSpeed, float damage)
    {
        MaxHp = hp;
        MaxMp = mp;
        AttackSpeed = attackSpeed;
        Damage = damage;

        originHP = hp;
    }

    private void FindPlayer()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        switch (eState)
        {
            case EnemyState.Attack:    Attack();    break;
            case EnemyState.Damaged:   Damaged();   break;
        }

        Die();
    }

    private void Damaged()
    {
        Debug.Log("eState.Damaged is now playing");
        if (hp != originHP)
        {
            eState = EnemyState.Attack;
            Debug.Log("eState = Damaged -> Attack");
        }
    }

    private void Attack()
    {
        Debug.Log("eState.Attack is now playing");

        curTime += Time.deltaTime;

        if (curTime >= attackSpeed)
        {
            player.MaxHp -= damage;
            curTime = 0.0f;
        }
    }

    private void Die()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
