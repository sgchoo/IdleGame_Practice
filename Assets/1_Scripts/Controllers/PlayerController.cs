using System.Collections;
using System.Collections.Generic;
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
    Damaged,
    Die
}

public class PlayerController : MonoBehaviour
{
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

    private void Awake()
    {
        MaxHp = 100.0f;
        MaxMp = 100.0f;
        MoveSpeed = 0.8f;
        AttackSpeed = 0.5f;
        Damage = 5.0f;
    }

    private void Update()
    {
        switch (pState) 
        {
            case PlayerState.Idle:      Idle();      break;
            case PlayerState.Move:      Move();      break;
            case PlayerState.Attack:    Attack();    break;
            case PlayerState.Damaged:   Damaged();   break;
            case PlayerState.Die:       Die();       break;
        }
    }

    private void Idle()
    {
        DetectTarget();
    }

    private void DetectTarget()
    {
        Debug.Log("pState.Idle isPlaying");

        float detectDistance = 100.0f;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(Camera.main.transform.position, Vector2.one * detectDistance, 0.0f, 1 << 6);

        if (colliders != null)
        {
            target = colliders[0].transform.gameObject;
            pState = PlayerState.Move;
            Debug.Log("pState = Idle -> Move");
        }
    }

    private void Move()
    {
        Debug.Log("pState.Move isPlaying");

        if (target != null)
        {
            if (Vector2.Distance(this.transform.position, target.transform.position) > 0.8f)
            {
                Vector2 dir = (target.transform.position - this.transform.position).normalized;
                this.transform.Translate(dir * Time.deltaTime * moveSpeed);
            }

            else
            {
                pState = PlayerState.Attack;
                Debug.Log("pState = Move -> Attack");
            }
        }
    }

    private void Attack()
    {
        Debug.Log("pState.Attack isPlaying");

        if (Vector2.Distance(this.transform.position, target.transform.position) < 0.8f)
        {

        }

        else
        {
            Debug.Log("pState = Attack -> Idle");
            pState = PlayerState.Idle;
        }

        if (target == null)
        {
            Debug.Log("isRunning?");
            pState = PlayerState.Idle;
        }
    }

    //private void AttackEnemy()
    //{
    //    Debug.Log("Enemy Attack");
    //}

    private void Damaged()
    {

    }

    private void Die()
    {
        isDead = true;
    }
}
