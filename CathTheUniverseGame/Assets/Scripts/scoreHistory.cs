using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreHistory : MonoBehaviour
{
    public int currentScore;
    public int lastScore;
    public int bestScore;
    void Start()
    {
        LoadScore();
    }
    void Awake()
    {
        if (GameObject.FindWithTag("veryimportant") != this.gameObject)
            Destroy(this.gameObject);
    }
    public void checkScore(int newScore)
    {
        currentScore = newScore;
        if (newScore > bestScore)
            bestScore = newScore;
    }

    public void LoadScore() 
    {
        if (!File.Exists("_score_data.txt"))
        {
            StreamWriter sw = new StreamWriter("_score_data.txt");
            sw.WriteLine(0);
            sw.WriteLine(0);
        }
        StreamReader sr = new StreamReader("_score_data.txt");
        string fileContent = sr.ReadToEnd();
        sr.Close();
        string[] f = fileContent.Split('\n');

        if (f.Length >= 2)
        { 
            if (f[0] != "")
                lastScore = Int32.Parse(f[0]);
            else
                lastScore = 0;
            if (f[1] != "")
                bestScore = Int32.Parse(f[1]);
            else
                bestScore = 0;
        }
        else
        {
            lastScore = 0;
            bestScore = 0;
        }
    }
    public void SaveScore()
    {
        StreamWriter sw = new StreamWriter("_score_data.txt");
        sw.AutoFlush = true;
        lastScore = currentScore;
        sw.WriteLine(lastScore.ToString());
        sw.WriteLine(bestScore.ToString());
    }
}
