using System;
using System.Collections.Generic;
using UnityEngine;

// List here new enemies.
public enum EnemyType
{
    Brute,
    Parasite,
    Ranger,
    Drone,
    Psycho,
    BigMama,
    ChunkyBoi

}

[Serializable]
public class EnemyStats
{
    public int Damage;
    public int Health;
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

    void Start()
    {
        state = EnemyState.Idle;
    }

    void Update()
    {
        // State machine:
        switch (state)
        {
            case EnemyState.Walking:
                break;
            case EnemyState.Attacking:
                AttackUpdate();
                break;
            case EnemyState.Dying:
                DyingUpdate();
                break;
        }
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
        Destroy(gameObject);
    }
}
