using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // List here new enemies.
    public Enemy BrutePrefab;
    public Enemy ParasitePrefab;
    public Enemy RangerPrefab;
    public Enemy DronePrefab;
    public Enemy PsychoPrefab;
    public Enemy ChunkyBoyPrefab;

    private List<EnemyGroup> enemy_groups_to_spawn;
    private List<Enemy> current_enemies = new List<Enemy>();
    public List<Vector3> spawn_positions;
    private float spawn_duration = 2;
    private float last_spawn_time = - 1000000; // lazy solution FTW
    public bool spawning = false;
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
    }

    public void SetLevel(List<EnemyGroup> _enemy_groups, int _max_enemies_weight)
    {
        enemy_groups_to_spawn = new List<EnemyGroup>();
        foreach (EnemyGroup enemy_group in _enemy_groups)
        {
            enemy_groups_to_spawn.Add(enemy_group);
        }
        max_enemies_weight = _max_enemies_weight;
    }

    private bool ShouldSpawn()
    {
        if (!spawning)
            return false;

        if (Time.time - last_spawn_time < spawn_duration)
        {
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
                    enemy = Instantiate(BrutePrefab, position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
                    break;
                case EnemyType.Parasite:
                    enemy = Instantiate(ParasitePrefab, position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
                    break;
                case EnemyType.Ranger:
                    enemy = Instantiate(RangerPrefab, position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
                    break;
                case EnemyType.Drone:
                    enemy = Instantiate(DronePrefab, position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
                    break;
                case EnemyType.Psycho:
                    enemy = Instantiate(PsychoPrefab, position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
                    break;
                case EnemyType.ChunkyBoi:
                    enemy = Instantiate(ChunkyBoyPrefab, position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
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
        List<Vector3> valid_spawn_positions = new List<Vector3>();
        for(int i = 0; i < spawn_positions.Count; i++)
        {
            Vector3 position = spawn_positions[i];
            if (IsPositionValid(position))
            {
                valid_spawn_positions.Add(position);
            }
        }
        if(valid_spawn_positions.Count == 0)
        {
            Debug.LogError("No valid position to spawn");
            return Vector3.zero;
        }

        return valid_spawn_positions[Random.Range(0, valid_spawn_positions.Count)];
    }

    private bool IsPositionValid(Vector3 position)
    {
        // Check that player is far enough? position not in camera?
        Vector3 screen_point = Camera.main.WorldToScreenPoint(position);
        return (screen_point.x < 0 || screen_point.y < 0 || screen_point.x > Camera.main.pixelWidth || screen_point.y > Camera.main.pixelHeight);
    }

    public int GetEnemiesCount()
    {
        // This is not optimized at all. preferably we can keep count of enemies + weights, and whenever an enemy dies he'll inform the spawner, to keep count.
        int enemy_count = 0;
        foreach (Enemy enemy in current_enemies)
        {
            if (enemy != null)
            {
                enemy_count += 1;
            }
        }
        return enemy_count;
    }
}
