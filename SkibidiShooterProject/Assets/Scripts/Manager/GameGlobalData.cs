using Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Manager
{

    [CreateAssetMenu(fileName = "GameGlobalData", menuName = "GameGlobalData")]
    public class GameGlobalData : Singelton<GameGlobalData>
    {






        private static GameGlobalData instance;
        private static bool isCaching = false;
        public static bool isDataCached = false;
        [Header("GameObjects")]
        [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
        [SerializeField] private List<GameObject> enemybossPrefabs = new List<GameObject>();
        public List<GameObject> EnemiesPrefab { get { return enemyPrefabs; } }
        public List<GameObject> EnemiesBossPrefab { get { return enemybossPrefabs; } }

        [Header("GameRules")]
        [Range(2, 8)]
        [SerializeField] private int maxCameracount;
        [SerializeField] private int maxHealthEnemey;
        [SerializeField] private int maxEnemeyCount;
        [Range(0,3)]
        [SerializeField] private int maxenemyIncreaseRange;
        [Range(0,20)]
        [SerializeField] private int maxenemyincreaseHealth;
        public int MaxCameraCount { get { return maxCameracount; } }

        public int MaxHealthEnemey { get { return maxHealthEnemey; } }
        public int MaxEnemeyCount { get { return maxEnemeyCount; } }
        public int MaxEnemyIncreaseRange { get { return maxenemyIncreaseRange; } }
        public int MaxEnemyIncreaseHealth { get { return maxenemyincreaseHealth; } }

    }

}