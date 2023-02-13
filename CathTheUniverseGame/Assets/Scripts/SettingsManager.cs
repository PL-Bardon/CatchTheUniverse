using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public List<TextMeshProUGUI> texts;
    InputManager input;
    bool changeKey;
    int currentIndex;
    private TextMeshProUGUI currentTxt;

    void Start()
    {
        input = GameObject.FindWithTag("veryimportant").GetComponent<InputManager>();
        changeKey = false;
        StartCoroutine(lateStart());
    }
    IEnumerator lateStart()
    {
        yield return new WaitForSeconds(0.1F);
        setKeybinds();
    }
    void setKeybinds()
    {
        texts[0].text = "" + input.up;
        texts[1].text = "" + input.down;
        texts[2].text = "" + input.left;
        texts[3].text = "" + input.right;
        texts[4].text = "" + input.Skill1;
        texts[5].text = "" + input.Skill2;
        texts[6].text = "" + input.Skill3;
        texts[7].text = "" + input.Skill4;
    }
    void Update()
    {
        //KeyPressed
        if (Input.anyKeyDown && changeKey)
        {
            KeyCode pressed = getCurrentKeyPressed();
            if (input.allKeyCode.Contains(pressed))
            {
                currentTxt.text = input.allKeyCode[Int32.Parse(currentTxt.name)] + "";
                Debug.Log("Déja use");
            }
            else
            {
                currentTxt.text = pressed + "" ;
                replaceKey(pressed);
            }
        }

        //SETTIGNS
        if (input.settingsPannel != null && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)))
        {
            input.settingsPannel.SetActive(false);
        } 
    }
    public void changeKeybind(TextMeshProUGUI txtButton)
    {
        changeKey = true;
        currentTxt = txtButton;
        txtButton.text = "< Press a key >";
        currentIndex = Int32.Parse(txtButton.name);
    }
    public void replaceKey(KeyCode newK)
    {
        switch (currentIndex)
        {
            case 0:
                input.up = newK;
                break;
            case 1:
                input.down = newK;
                break;
            case 2:
                input.left = newK;
                break;
            case 3:
                input.right = newK;
                break;
            case 4:
                input.Skill1 = newK;
                break;
            case 5:
                input.Skill2 = newK;
                break;
            case 6:
                input.Skill3 = newK;
                break;
            case 7:
                input.Skill4 = newK;
                break;
            default:
                break;
        }
        input.allKeyCode[currentIndex] = newK;
    }
    public KeyCode getCurrentKeyPressed()
    {
        foreach(KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if(Input.GetKey(vKey))
            {
                changeKey = false;
                return vKey;
            }
        }
        return KeyCode.None;
    }
}
