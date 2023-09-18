using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 플레이어는 몬스터를 향해 돌아다닌다.
/// Collider2D 클래스를 사용하여 먼저 입력되는 collider의 오브젝트를 타겟으로 정한다.
/// 타겟으로 일정 거리만큼 이동 후
/// 몇 초당 한대씩 때린다.
/// 몬스터를 잡으면 다음 타겟을 설정한다.
/// 
/// </summary>

public enum PlayerState
{
    Idle,
    FoundTarget,
    Move,
    Attack,
    Die
}

public class PlayerController : MonoBehaviour
{
    private float originHP;
    private float curTime;

    [Header("PlayerState")]
    [SerializeField]
    private PlayerState pState = PlayerState.Idle;
    [SerializeField]
    private bool isDead;

    [Header("PlayerStats")]
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
    private float moveSpeed;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
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

    [Header("Target")]
    [SerializeField]
    private GameObject target;
    private GameObject enemy;

    private void Awake()
    {
        PlayerStatsInit();
    }

    private void PlayerStatsInit()
    {
        MaxHp = 100.0f;
        MaxMp = 100.0f;
        MoveSpeed = 0.8f;
        AttackSpeed = 5.0f;
        Damage = 5.0f;

        originHP = hp;
    }

    private void Update()
    {
        switch (pState) 
        {
            case PlayerState.Idle:      Idle();      break;
            case PlayerState.Move:      Move();      break;
            case PlayerState.Attack:    Attack();    break;
            case PlayerState.Die:       Die();       break;
        }

        PlayerDamaged();
        PlayerDead();
    }

    private void Idle()
    {
        enemy = GameObject.FindWithTag("Enemy");

        if (enemy == null)      return;
        else                    DetectTarget();
    }

    private void DetectTarget()
    {
        float detectDistance = 100.0f;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(Camera.main.transform.position, Vector2.one * detectDistance, 0.0f, 1 << 6);

        if (colliders != null)
        {
            target = colliders[0].transform.gameObject;
            pState = PlayerState.Move;
        }
    }

    private void Move()
    {
        if (target != null)
        {
            if (Vector2.Distance(this.transform.position, target.transform.position) > 0.8f)
            {
                Vector2 dir = (target.transform.position - this.transform.position).normalized;
                this.transform.Translate(dir * Time.deltaTime * moveSpeed);

                if (dir.x > 0)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
            }

            else
            {
                pState = PlayerState.Attack;
            }
        }
    }

    private void Attack()
    {
        if (Vector2.Distance(this.transform.position, target.transform.position) < 0.8f)
        {
            if (target == null)
            {
                pState = PlayerState.Idle;
            }

            AttackEnemy(attackSpeed);
        }
    }

    private void AttackEnemy(float delayTime)
    {
        EnemyController enemy = target.GetComponent<EnemyController>();

        curTime += Time.deltaTime;
        
        if (curTime > delayTime)
        {
            enemy.MaxHp -= damage;
            curTime = 0.0f;
        }

        if (enemy.MaxHp <= 0)
        {
            pState = PlayerState.Idle;
        }
    }

    private void PlayerDamaged()
    {
        if (hp != originHP)
        {
            StartCoroutine("ChangeColor");
            originHP = hp;
        }
    }

    IEnumerator ChangeColor()
    {
        SpriteRenderer render = this.GetComponent<SpriteRenderer>();
        render.color = new Color32(54, 86, 226, 150);

        yield return new WaitForSeconds(0.5f);

        render.color = new Color32(54, 86, 226, 255);
    }

    private void Die()
    {
        if (isDead == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void PlayerDead()
    {
        if (hp <= 0)
        {
            isDead = true;
            pState = PlayerState.Die;
        }
    }
}
