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
    public UpgradeManager upgrade_manager;
    private LevelState state;
    public int wave_number { get; private set; }
    private float state_endtime;  // Not used for UpgradeScreen. For Active it ends after the time is up, and all enemies are dead.
    private float wave_length = 20f;  // This can be changed, in ActivateWave, if we need different wave lengths.

    private const float wave_start_animation_length = 3f;
    private const float wave_completed_animation_length = 1.5f;

    //leveling
    public int currentXP;
    public int XPtoLevel;
    public int XPtoLevelIncrement = 0;
    

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
        currentXP = 0;
        XPtoLevel = 100;

        wave_number = 0;
        Debug.Log("WaveStart_root");
        WaveStart();
    }
        
    void Update()
    {
        switch(state)
        {
            case LevelState.WaveStart:
                if (Time.time > state_endtime)
                {
                    Debug.Log("ActivateWave");
                    ActivateWave();
                }
                break;
            case LevelState.WaveActive:
                if(ShouldEndWave())
                {
                    Debug.Log("CompletedWave");
                    WaveCompleted();
                }
                break;
            case LevelState.WaveCompleted:
                if (Time.time > state_endtime)
                {
                    Debug.Log("ActivateUpgradeScreen");
                    ActivateUpgradeScreen();
                }
                break;
            case LevelState.UpgradeScreen:
                if(!upgrade_manager.is_upgrading)
                {
                    Debug.Log("WaveStart");
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
        state = LevelState.WaveActive;
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

        if (spawner.spawning)
        {
            // Probably should be called in the outer func. YOLO
            spawner.StopSpawning();
        }
        if (spawner.GetEnemiesCount() > 0)
            return false;

        return true;
    }

    private void ActivateUpgradeScreen()
    {
        state = LevelState.UpgradeScreen;
        upgrade_manager.Upgrade(CalculateLevelsGained()); 
    }

    private void StartSpawner()
    {
        // Set waves and level weight here.
        int enemy_weight = 50;
        List<EnemyGroup> enemy_groups = new List<EnemyGroup>();
        enemy_groups.Add(new EnemyGroup(new List<EnemyType> { EnemyType.Brute }));
        enemy_groups.Add(new EnemyGroup(new List<EnemyType> { EnemyType.Ranger, EnemyType.Ranger }));
        enemy_groups.Add(new EnemyGroup(new List<EnemyType> { EnemyType.Psycho, EnemyType.Brute }));
        spawner.SetLevel(enemy_groups, enemy_weight);
        spawner.StartSpawning();
    }

    public void GetXP(int gained_xp)
    {
        currentXP += gained_xp;
    }

    private int CalculateLevelsGained()
    {
        int levels_gained = 0;
        while (currentXP >= XPtoLevel)
        {
            currentXP -= XPtoLevel;
            XPtoLevel += XPtoLevelIncrement;
            levels_gained++;
        }
        return levels_gained;
    }

}
