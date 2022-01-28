using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LevelState
{
    WaveStart,
    WaveActive,
    WaveCompleted,
    UpgradeScreen
}


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public Spawner spawner;
    //public UpgradeManager upgrade_manager;
    private LevelState state;
    public int wave_number { get; private set; }
    private float state_endtime;  // Not used for UpgradeScreen. For Active it ends after the time is up, and all enemies are dead.
    private float wave_length = 120f;  // This can be changed, in ActivateWave, if we need different wave lengths.

    private const float wave_start_animation_length = 3f;
    private const float wave_completed_animation_length = 1.5f;
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        wave_number = 0;
        WaveStart();
    }

    void Update()
    {
        switch(state)
        {
            case LevelState.WaveStart:
                if (Time.time > state_endtime)
                {
                    ActivateWave();
                }
                break;
            case LevelState.WaveActive:
                if(ShouldEndWave())
                {
                    WaveCompleted();
                }
                break;
            case LevelState.WaveCompleted:
                if (Time.time > state_endtime)
                {
                    ActivateUpgradeScreen();
                }
                break;
            case LevelState.UpgradeScreen:
                if(!upgrade_manager.is_upgrading)
                {
                    WaveStart();
                }
                break;
        }
    }

    // 3, 2 ,1 Go! screen before the level starts.
    private void WaveStart()
    {
        wave_number += 1;
        state = LevelState.WaveStart;
        state_endtime = Time.time + wave_start_animation_length;
        // Start animation here.
    }

    private void ActivateWave()
    {
        state = LevelState.WaveStart;
        state_endtime = Time.time + wave_length;
        StartSpawner();

    }

    // 3, 2 ,1 Go! wave completed screen.
    private void WaveCompleted()
    {
        state = LevelState.WaveCompleted;
        state_endtime = Time.time + wave_completed_animation_length;
        // Start animation here.
    }

    private bool ShouldEndWave()
    {
        if (Time.time < state_endtime)
            return false;

        if(spawner.GetEnemiesCount() > 0)
            return false;

        return true;
    }

    private void ActivateUpgradeScreen()
    {
        state = LevelState.UpgradeScreen;
        upgrade_manager.Upgrade(GameManager.Instance.player.CalculateLevelUps());
    }

    private void StartSpawner()
    {
        // Set waves and level weight here.
        int enemy_weight = 50;
        List<EnemyGroup> enemy_groups = new List<EnemyGroup>();
        enemy_groups.Add(new EnemyGroup(new List<EnemyType> { EnemyType.Brute }));
        enemy_groups.Add(new EnemyGroup(new List<EnemyType> { EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite, EnemyType.Parasite }));
        spawner.SetLevel(enemy_groups, enemy_weight);
        spawner.StartSpawning();
    }
}