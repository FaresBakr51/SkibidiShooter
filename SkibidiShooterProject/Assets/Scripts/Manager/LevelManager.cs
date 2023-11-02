using Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class LevelManager : Singelton<LevelManager>
    {
        [SerializeField] private int current_level;
        [SerializeField] private int currentEnemymaxHealth;
        [SerializeField] private int currentEnemiesCount;
        [SerializeField]  private int lastbosslevel;
        public int GetEnemeyMaxHealth { get { return currentEnemymaxHealth; } }
        public int GetEnemiesCount { get {  return currentEnemiesCount; } }
        void Awake()
        {
            LoadLevelData();
        }


        public void UpdateLevel()
        {
            if (GetBoss())
            {
                lastbosslevel = current_level + 3;
            }
            current_level++;
            if(currentEnemiesCount < GameGlobalData.Instance.MaxEnemeyCount)
            {
                currentEnemiesCount += Random.Range(0, GameGlobalData.Instance.MaxEnemyIncreaseRange+1);

            }
            if(currentEnemymaxHealth < GameGlobalData.Instance.MaxHealthEnemey)
            {
                currentEnemymaxHealth += Random.Range(0, GameGlobalData.Instance.MaxEnemyIncreaseHealth+1);
            }
           
            SaveLevelData();
        }

        public bool GetBoss()
        {
            if(current_level == lastbosslevel)
            {
               
                return true;
            }
            return false;
        }
        public int GetCurrentLevel()
        {
            return current_level +1;
        }
        private void LoadLevelData()
        {
            current_level =  PlayerPrefs.GetInt("currentlvl", 1);
            currentEnemymaxHealth = PlayerPrefs.GetInt("currnetmaxhealth", 100);
            currentEnemiesCount = PlayerPrefs.GetInt("currentEnemiesCount",4);
            lastbosslevel = PlayerPrefs.GetInt("currentbosslevel", 3);
        }
        private void SaveLevelData()
        {
            PlayerPrefs.SetInt("currentlvl", current_level);
            PlayerPrefs.SetInt("currnetmaxhealth", currentEnemymaxHealth);
            PlayerPrefs.SetInt("currentEnemiesCount", currentEnemiesCount);
            PlayerPrefs.SetInt("currentbosslevel", lastbosslevel);
            PlayerPrefs.Save();
        }
        private void OnApplicationQuit()
        {
            SaveLevelData();
        }

        public void OpenRandomLevel()
        {
            int rand = Random.Range(1, 3);
            SceneManager.LoadScene(rand);
        }
    }
}
