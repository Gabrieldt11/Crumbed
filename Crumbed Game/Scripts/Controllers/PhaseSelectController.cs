using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhaseSelectController : MonoBehaviour
{
    public Button[] lvlButtons;
    public Image[] lockImage;

    // Start is called before the first frame update
    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                lvlButtons[i].interactable = false;
                lockImage[i].gameObject.SetActive(true);
            }
        }
    }

    public void GoToPhase(string Phase)
    {
        SceneManager.LoadScene(Phase);
    }

    public void Return(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}