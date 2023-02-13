using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class InputManager : MonoBehaviour
{
    public GameObject settingsPannel;
    public static InputManager instance;
    public List<KeyCode> allKeyCode;

    public KeyCode up; //0
    public KeyCode down; //1
    public KeyCode left; //2
    public KeyCode right;

    public KeyCode Skill1;
    public KeyCode Skill2;
    public KeyCode Skill3;
    public KeyCode Skill4; //7


    void Start()
    {
        Scene scene = SceneManager.GetSceneByBuildIndex(0);
        GameObject[] allObject = scene.GetRootGameObjects();
        int i = 0;
        while (i < allObject.Length && allObject[i].name != "Canvas")
        {
            i++;
        }
        settingsPannel = allObject[i].transform.Find("Settings").gameObject;
        if (settingsPannel != null)
            settingsPannel.SetActive(false);

        up = KeyCode.Z;
        down = KeyCode.S;
        left = KeyCode.Q;
        right = KeyCode.D;

        Skill1 = KeyCode.E;
        Skill2 = KeyCode.F;
        Skill3 = KeyCode.LeftShift;
        Skill4 = KeyCode.R;

        allKeyCode.Add(up);
        allKeyCode.Add(down);
        allKeyCode.Add(left);
        allKeyCode.Add(right);
        allKeyCode.Add(Skill1);
        allKeyCode.Add(Skill2);
        allKeyCode.Add(Skill3);
        allKeyCode.Add(Skill4);

        allKeyCode.Add(KeyCode.Mouse0);
        allKeyCode.Add(KeyCode.Space);
        allKeyCode.Add(KeyCode.Escape);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
        Scene scene = SceneManager.GetSceneByBuildIndex(0);
        GameObject[] allObject = scene.GetRootGameObjects();
        int i = 0;
        while (i < allObject.Length && allObject[i].name != "Canvas")
        {
            i++;
        }
        settingsPannel = allObject[i].transform.Find("Settings").gameObject;
        if (settingsPannel != null)
            settingsPannel.SetActive(false);
    }

    void Update()
    {
        if (settingsPannel == null)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(0);
            GameObject[] allObject = scene.GetRootGameObjects();
            int i = 0;
            while (i < allObject.Length && allObject[i].name != "Canvas")
            {
                i++;
            }
            settingsPannel = allObject[i].transform.Find("Settings").gameObject;
            if (settingsPannel != null)
                settingsPannel.SetActive(false);
        }
    }

}
