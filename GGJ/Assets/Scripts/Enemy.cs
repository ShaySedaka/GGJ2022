using System;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// List here new enemies.
public enum EnemyType
{
    Brute,
    Parasite,
    Ranger,
    Drone,
    Psycho,
    ChunkyBoi
}

[Serializable]
public class EnemyStats
{
    public int Damage;
    public int Health;
    public int XP;
}


public class EnemyGroup
{
    public List<EnemyType> enemy_type_list;
    public EnemyGroup(List<EnemyType> _enemy_type_list)
    {
        enemy_type_list = _enemy_type_list;
    }
}

public enum EnemyState
{
    Walking,
    Attacking,
    Dying,
    Idle
}

public abstract class Enemy : MonoBehaviour
{
    public EnemyType enemy_type;
    public int weight;
    [SerializeField]
    public EnemyStats stats;
    public GameObject PopMaster;
    protected EnemyState state;

    protected Animator anim;
    protected bool facingR = true;
    
    void Start()
    {
        state = EnemyState.Idle;
        GetComponent<AIDestinationSetter>().target = GameManager.Instance.Player.transform;
        anim = GetComponent<Animator>();
    }

    protected abstract void AttackUpdate();
    protected abstract void DyingUpdate(); // animation + destroy?

    public void GetAttacked(int damage)
    {
        stats.Health -= damage;
        GameObject go = Instantiate(PopMaster, transform.position, Quaternion.identity);
        go.GetComponent<DamagePopup>().PopDmg(damage);
        Debug.Log("enemy takes " + damage);

        if (GetComponent<Flash>())
        {
            GetComponent<Flash>().GetHit();
        }
        if (stats.Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.Instance.levelManager.GainXP(stats.XP);
        Destroy(gameObject);
    }

    
}
