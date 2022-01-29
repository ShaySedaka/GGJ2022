using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// List here new enemies.
public enum EnemyType
{
    Brute,
    Parasite,
    Ranger, 
    Drone, 
    Psycho,
    BigMama

}

[Serializable]
public class EnemyStats
{
    public int Speed;
    public int Damage;
    public int Range;
    public int AttackSpeed;
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
    
    protected EnemyState state;
<<<<<<< Updated upstream
    
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
=======
    protected Animator anim;
    bool facingR = true;

    void Start()
    {
        state = EnemyState.Idle;
        GetComponent<AIDestinationSetter>().target = GameManager.Instance.Player.transform;
        anim = GetComponent<Animator>();
>>>>>>> Stashed changes
    }

    protected abstract void AttackUpdate();
    protected abstract void DyingUpdate(); // animation + destroy?

    public void GetAttacked(int damage)
    {
        stats.Health -= damage;
        if(stats.Health <= 0)
        {
            Die();
        }
    }
    
<<<<<<< Updated upstream
    void Die()
    {
        state = EnemyState.Dying;
=======
    protected void Flip()
    {
        if ((facingR && GameManager.Instance.Player.transform.position.x >= transform.position.x) 
            || (!facingR && GameManager.Instance.Player.transform.position.x < transform.position.x))
        {
            facingR = !facingR;
            transform.Rotate(new Vector3(0,180,0));
        }
    }


    void Die()
    {
        //GameManager.Instance.levelManager.GainXP(stats.XP);
        Destroy(gameObject);
>>>>>>> Stashed changes
    }
}
