using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    #region Variables

    private float _mapSpawnCooldown;
    private float _alienSpawnCooldown;

    #endregion

    #region Components

    public GameObject Alien;
    public GameObject IslandPrefab;
    public TextMeshProUGUI MapSpeed;

    #endregion

    #region OnSceneStart

    void Start()
    {
        // Map's First Spawn Cooldown
        _mapSpawnCooldown = 2;
        // Alien's First Spawn Cooldown
        _alienSpawnCooldown = 10;
    }

    #endregion

    #region OnEachFrame

    void Update()
    {
        _mapSpawnCooldown -= Time.unscaledDeltaTime;
        _alienSpawnCooldown -= !Config.IsDead ? Time.unscaledDeltaTime : 0;
        MapSpeed.text = CurrentMapSpeed();

        if (_mapSpawnCooldown <= 0)
        {
            _mapSpawnCooldown = 4;

            // Spawns the Platform in the World
            Instantiate(IslandPrefabGen(), new Vector3(25, -5, 0), Quaternion.Euler(0, 0, 0));
        }

        if (_alienSpawnCooldown <= 0)
        {
            // Spawns the GameObject in the World
            Instantiate(Alien, new Vector3(25, Random.Range(-3.5f, 2), 0), Quaternion.Euler(0, 0, 0));

            // Sets the Next Spawn Cooldown
            _alienSpawnCooldown = SpawnCooldown();
        }
    }

    #endregion

    #region Methods

    float SpawnCooldown()
    {
        // Spawns the Platform based in the Player's Score
        if (Player.Score < 5)
        {
            return Random.Range(4, 10);
        }
        else if (Player.Score < 10)
        {
            return Random.Range(3, 8);
        }
        else if (Player.Score < 20)
        {
            return Random.Range(3, 6);
        }
        else if (Player.Score < 30)
        {
            return Random.Range(2, 4);
        }
        return 2;
    }

    GameObject IslandPrefabGen()
    {
        // Defines the Platform X Size
        float xSize = Random.Range(10, 20);
        // Sets the X Size to the GameObject
        IslandPrefab.transform.localScale = new Vector3(xSize, 2, 1);

        return IslandPrefab;
    }

    string CurrentMapSpeed()
    {
        // Defines the Speed Text
        if (Player.Score > 40)
        {
            return "X3";
        }
        else if (Player.Score > 30)
        {
            return "X2.5";
        }
        else if (Player.Score > 20)
        {
            return "X2";
        }
        else if (Player.Score > 10)
        {
            return "X1.5";
        }
        return "X1";
    }

    #endregion
}
