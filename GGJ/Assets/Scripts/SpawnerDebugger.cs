using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDebugger : MonoBehaviour
{
    public Spawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        List<EnemyGroup> enemy_groups = new List<EnemyGroup>();
        enemy_groups.Add(new EnemyGroup(new List<EnemyType> { EnemyType.Brute }));
        enemy_groups.Add(new EnemyGroup(new List<EnemyType> { EnemyType.Parasite }));
        enemy_groups.Add(new EnemyGroup(new List<EnemyType> { EnemyType.Brute, EnemyType.Parasite }));
        enemy_groups.Add(new EnemyGroup(new List<EnemyType> { EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite }));
        spawner.SetLevel(enemy_groups, 30);
        spawner.StartSpawning();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
