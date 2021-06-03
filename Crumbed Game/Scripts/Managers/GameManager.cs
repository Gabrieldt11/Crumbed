using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Vector2 leftBottom;
    public Vector2 rightTop;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Camera cam = Camera.main;
        leftBottom = cam.ScreenToWorldPoint(new Vector3(300, 0, 0));
        rightTop = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }
}
