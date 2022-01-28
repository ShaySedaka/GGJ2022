using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // List here new enemies.
    public Enemy BrutePrefab;
    public Enemy ParasitePrefab;

    private List<EnemyGroup> enemy_groups_to_spawn;
    private List<Enemy> current_enemies = new List<Enemy>();
    public List<Vector3> spawn_positions;
    private float spawn_duration = 2;
    private float last_spawn_time = - 1000000; // lazy solution FTW
    private bool spawning = false;
    public int max_enemies_weight;
    
    void Start()
    {
    }

    void Update()
    {
        if(ShouldSpawn())
        {
            Spawn();
        }
    }

    public void StartSpawning()
    {
        if(spawning)
        {
            Debug.LogError("Can't start spawning! already spawning.");
        }
        spawning = true;
        last_spawn_time = -1000000;
    }

    public void StopSpawning()
    {
        if (!spawning)
        {
            Debug.LogError("Can't stop spawning! not spawning.");
        }
        spawning = false;
        // Delete all enemies here?
    }

    public void SetLevel(List<EnemyGroup> _enemy_groups, int _max_enemies_weight)
    {
        enemy_groups_to_spawn = new List<EnemyGroup>();
        foreach (EnemyGroup enemy_group in _enemy_groups)
        {
            enemy_groups_to_spawn.Add(enemy_group);
        }
        Debug.Log(enemy_groups_to_spawn.Count);
        max_enemies_weight = _max_enemies_weight;
    }

    private bool ShouldSpawn()
    {
        if (!spawning)
            return false;

        if (Time.time - last_spawn_time < spawn_duration)
        {
            Debug.Log(string.Format("Time not satisfied: {0}, {1}", Time.time, last_spawn_time));
            return false;
        }
        
        int current_weight = 0;
        foreach(Enemy enemy in current_enemies)
        {
            if (enemy != null)
            {
                current_weight += enemy.weight;
            }
            
        }
        if (current_weight > max_enemies_weight)
            return false;

        return true;
    }

    private void Spawn()
    {
        if(enemy_groups_to_spawn.Count == 0)
        {
            Debug.LogError("No Enemy Groups! Can't spawn.");
        }
        last_spawn_time = Time.time;
        Vector3 position = GetPosition();

        EnemyGroup random_enemy_group = enemy_groups_to_spawn[Random.Range(0, enemy_groups_to_spawn.Count)];

        foreach (EnemyType enemy_type in random_enemy_group.enemy_type_list)
        {
            Enemy enemy;
            switch (enemy_type)
            {
                // List here new enemies.
                case EnemyType.Brute:
                    enemy = Instantiate(BrutePrefab, position, Quaternion.identity);
                    break;
                case EnemyType.Parasite:
                    enemy = Instantiate(ParasitePrefab, position, Quaternion.identity);
                    break;
                default:
                    Debug.LogError(string.Format("Missing prefab for enemy_type {0}", enemy_type));
                    return;
            }
            current_enemies.Add(enemy);
        }
    }

    private Vector3 GetPosition()
    {
        int random_first_index = Random.Range(0, spawn_positions.Count);
        for(int i = 0; i < spawn_positions.Count; i++)
        {
            Vector3 position = spawn_positions[(random_first_index + i) % spawn_positions.Count];
            if (IsPositionValid(position))
            {
                return position;
            }
        }
        Debug.LogError("No valid position to spawn");
        return Vector3.zero;
    }

    private bool IsPositionValid(Vector3 position)
    {
        // Check that player is far enough? position not in camera?
        Vector3 screen_point = Camera.main.WorldToScreenPoint(position);
        return (screen_point.x < 0 || screen_point.y < 0 || screen_point.x > Camera.main.pixelWidth || screen_point.y > Camera.main.pixelHeight);
    }
}