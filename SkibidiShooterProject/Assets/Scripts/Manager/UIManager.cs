using Patterns;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : Singelton<UIManager>
    {
        [Header("GamePlayHUD")]
        [SerializeField] private Text timerTxt;
        [SerializeField] private Text leftEnimesTxt;
        [SerializeField] protected Text currentKameraTxt;

        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private Text levelTxt;
        public void UpdateTiemrTxt(string val)
        {
            timerTxt.text = val;
        }
        public void UpdateEnimesTxt(int val)
        {
            leftEnimesTxt.text = val.ToString();
        }
        public void UpdatecameraHud(int val)
        {
            currentKameraTxt.text  = val.ToString();
        }

        public void UpdateFinishHud(string cond)
        {
            if (cond == "win")
            {
                winPanel.SetActive(true);
                levelTxt.text = " Next Level : " + LevelManager.Instance.GetCurrentLevel().ToString();
            }
            else
            {
                losePanel.SetActive(true);
            }
            StartCoroutine(nextLe());
        }
        IEnumerator nextLe()
        {
            yield return new WaitForSeconds(3f);
           LevelManager.Instance.OpenRandomLevel();
        }

        //private void NextLevel()
        //{
        //    SceneManager.LoadScene(0);
        //}

    }
}
