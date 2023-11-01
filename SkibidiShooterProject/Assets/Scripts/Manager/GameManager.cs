using System.Collections.Generic;
using UnityEngine;
using Patterns;
using UnityEngine.UI;
using UnityEngine.Events;
using Controller;

namespace Manager
{
    public enum GameState
    {
        GS_Running, GS_Finished
    }
    public class GameManager : Singelton<GameManager>
    {

        [Header("GameRules")]
        [SerializeField] private float gameTimer;


        //[Header("UI")]
        //[SerializeField] private Text timerText;


        [Header("GamePlay")]
        [SerializeField] private FPSController currentController;
        [SerializeField] private GameState gameState;
        [SerializeField] private List<GameObject> gameWeapons = new List<GameObject>();
        [SerializeField] private List<Transform> weaponTransformPoints = new List<Transform>();

        [SerializeField] private List<Transform> playerSpawnPoint = new List<Transform>();
        [SerializeField] private List<Transform> enimeySpawnPoints = new List<Transform>();
        [SerializeField] private int currentEnimesCount;

        [Header("Events")]
        public static UnityEvent GameStartEvent = new UnityEvent();
        public static UnityEvent<string> GameFinishedEvent = new UnityEvent<string>();

        [SerializeField] private GameObject cameraPlayer;
        [SerializeField] private List<GameObject> cameraPlayers = new List<GameObject>();
        [SerializeField] private int cameraCounts;
        [SerializeField] private List<Transform> patrolPoints = new List<Transform>();
        public GameState CurrentGameState { get { return gameState; } }
        public FPSController CurrentPlayer { get { return currentController; } }

        private void OnEnable()
        {
           
            GameStartEvent.AddListener(SpawnCameraPlayers);
            GameStartEvent.AddListener(SpawnEnimes);
            GameFinishedEvent.AddListener(UpdateHud);
            GameFinishedEvent.AddListener(UpdateLeveL);
        }
        private void OnDisable()
        {
            GameStartEvent.RemoveAllListeners();
            GameFinishedEvent.RemoveAllListeners();
        }
        private void Start()
        {


            int rand = Random.Range(0, gameWeapons.Count);
            GameObject randWeapon = Instantiate(gameWeapons[rand], weaponTransformPoints[Random.Range(0, weaponTransformPoints.Count)].position, gameWeapons[rand].transform.rotation);
            currentEnimesCount = LevelManager.Instance.GetEnemiesCount;

            currentController = FindAnyObjectByType<FPSController>();
            GameStartEvent?.Invoke();
        }
        private void Update()
        {
            if (gameState == GameState.GS_Running)
            {
                if (gameTimer > 0)
                {

                    gameTimer -= Time.deltaTime;
                    HandleTimer();
                    if (currentEnimesCount <= 0)
                    {
                        //win con
                        GameFinishedEvent?.Invoke("win");
                        gameState = GameState.GS_Finished;
                    }
                }
                else
                {
                    //lose cond
                    GameFinishedEvent?.Invoke("lose");
                    gameState = GameState.GS_Finished;
                }
                if(currentController.Health <= 0)
                {
                    //lose cond
                    GameFinishedEvent?.Invoke("lose");
                    gameState = GameState.GS_Finished;
                }
            }
       
        }


        private void HandleTimer()
        {
            if (gameTimer > 0)
            {
                float minutes = Mathf.FloorToInt(gameTimer / 60);
                float seconds = Mathf.FloorToInt(gameTimer % 60);
                UIManager.Instance.UpdateTiemrTxt(string.Format("{0:00}:{1:00}", minutes, seconds));
             
            }
            else
            {
                UIManager.Instance.UpdateTiemrTxt(string.Format("{0:00}:{1:00}", 0, 0));
            }
        }
        public int GetLeftEnimes()
        {
            return currentEnimesCount;
        }

        public void EnemeyDied()
        {
            currentEnimesCount--;
            UIManager.Instance.UpdateEnimesTxt(currentEnimesCount);
        }

        #region CameraAI
        public Transform GetRandomPatrolPoint()
        {
            int ran = Random.Range(0, patrolPoints.Count);
            return patrolPoints[ran];
        }

        public GameObject GetRandomFakePlayer()
        {
            int rand = Random.Range(0, cameraPlayers.Count);
            return cameraPlayers[rand];
        }
        #endregion

        #region StartEvents
        private void SpawnCameraPlayers() {

            Debug.Log("Spawncamera");
            cameraCounts = Random.Range(1, GameGlobalData.Instance.MaxCameraCount + 1);
            UIManager.Instance.UpdatecameraHud(cameraCounts);
            for (int i = 0; i < cameraCounts; i++)
            {
                GameObject camera = Instantiate(cameraPlayer,currentController.transform.position,cameraPlayer.transform.rotation);
                cameraPlayers.Add(camera);

            }


        }

        private void SpawnEnimes()
        {
            UIManager.Instance.UpdateEnimesTxt(currentEnimesCount);
            for (int i =0;i< currentEnimesCount;i++)
            {
                Transform randPoint = GetRandomFromList(enimeySpawnPoints);
                GameObject enemy = Instantiate(GetRandomFromList(GameGlobalData.Instance.EnemiesPrefab), randPoint.position, randPoint.rotation);
            }
            if (LevelManager.Instance.GetBoss())
            {
                Transform randPoint = GetRandomFromList(enimeySpawnPoints);
                GameObject enemy = Instantiate(GetRandomFromList(GameGlobalData.Instance.EnemiesBossPrefab), randPoint.position, randPoint.rotation);
            }
         
        }

        #endregion
        #region FinishEvents
        private void UpdateHud(string cond)
        {
            UIManager.Instance.UpdateFinishHud(cond);
        }

        private void UpdateLeveL(string cond)
        {
            if(cond == "win")
            {
                LevelManager.Instance.UpdateLevel();
            }
        }
        #endregion

        public static T GetRandomFromList<T>(List<T> list)
        {
            int rand = Random.Range(0,list.Count);
            return list[rand];
        }

    }
}