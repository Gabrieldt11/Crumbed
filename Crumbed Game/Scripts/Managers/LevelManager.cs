using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int pointCounter;
    public int phasePoints;

    float timeOfGame;
    public float maxTime;

    public GameObject youWinPanel;
    public GameObject youLosePanel;
    public GameObject avioes;

    public Image medal;
    public Sprite[] medals;

    public Text totalTime;
    public Text timeForLoss;

    public string nextScene;
    public string previousScene;

    public int levelToUnlock;

    public Transform wallL;
    public Transform wallR;
    public Transform wallT;
    public Transform wallB;

    void Start()
    {
        WallAdjustment();
        Time.timeScale = 1;
        timeForLoss.text = maxTime.ToString();
    }

    void Update()
    {
        timeOfGame += Time.deltaTime;
        maxTime -= Time.deltaTime;
        timeForLoss.text = maxTime.ToString();
        if (pointCounter >= phasePoints)
        {
            Time.timeScale = 0;
            if (timeOfGame <= 15)
            {
                medal.sprite = medals[0];
            }
            else if (timeOfGame <= 30)
            {
                medal.sprite = medals[1];
            }
            else if (timeOfGame <= 60)
            {
                medal.sprite = medals[2];
            }

            PlayerPrefs.SetInt("levelReached", levelToUnlock);
            avioes.SetActive(false);
            youWinPanel.SetActive(true);
            totalTime.text = "Tempo: " + Mathf.Round(timeOfGame);
        }
        if (maxTime <= 0)
        {
            Time.timeScale = 0;
            avioes.SetActive(false);
            youLosePanel.SetActive(true);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void GoToPhases()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("PhaseSelectScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void PreviousScene()
    {
        SceneManager.LoadScene(previousScene);
    }

    public void WinPoints()
    {
        pointCounter++;
    }

    public void LossPoints()
    {
        pointCounter--;
        if (pointCounter <= 0)
        {
            pointCounter = 0;
        }
    }

    void WallAdjustment()
    {
        wallL.position = new Vector3(GameManager.instance.leftBottom.x - wallL.GetComponent<Collider2D>().bounds.extents.x, 0, wallL.position.z);
        wallR.position = new Vector3(GameManager.instance.rightTop.x + wallR.GetComponent<Collider2D>().bounds.extents.x, 0, wallR.position.z);
        wallT.position = new Vector3(0, GameManager.instance.rightTop.y + wallT.GetComponent<Collider2D>().bounds.extents.y, wallT.position.z);
        wallB.position = new Vector3(0, GameManager.instance.leftBottom.y - wallB.GetComponent<Collider2D>().bounds.extents.y, wallB.position.z);
    }
}
