using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class gameOverMenu : MonoBehaviour
{
    private scoreHistory _scoreHistory;
    public GameObject endPannel;
    public TextMeshProUGUI actualScore;
    public TextMeshProUGUI lastScore;
    public TextMeshProUGUI bestScore;

    void Start()
    {
        _scoreHistory = GameObject.FindWithTag("veryimportant").GetComponent<scoreHistory>();
        endPannel.SetActive(false);
    }

    public void showEndPannel()
    {
        StartCoroutine(showEnd());
    }
    IEnumerator showEnd()
    {
        yield return new WaitForSeconds(1F);
        endPannel.SetActive(!endPannel.activeSelf);
        actualScore.text = "Actual score : " + _scoreHistory.currentScore;
        lastScore.text = "Last score : " + _scoreHistory.lastScore;
        bestScore.text = "Best score : " + _scoreHistory.bestScore;
        _scoreHistory.SaveScore();
    }
    public void replayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
