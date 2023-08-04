using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters.Binary;
using System.Timers;
using UnityEngine;

public class RoundManager : TemporalSingleton<RoundManager>
{
    private int   m_actualRound = 0;
    private bool  m_lossesRound = false;
    private float m_timeLeft = 60f;
    private int   m_timerSpeed = 1;
    private int   m_personalBest = 0;

    private void Start()
    {
        m_actualRound = 0;
        GameManager.Instance.UpdateRound();

        if (FileExists())
        {
            LoadBin();
        }else
            SaveBin();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void StartRound()
    {
        nextRound();
        EnemyManager.Instance.initSpawnEnemies(m_actualRound-1);
        StartCoroutine(InitTimer());
    }

    public bool FileExists()
    {
        if(System.IO.Path.GetExtension(Application.persistentDataPath + "/GameData.aem").ToLower() == ".aem")
            if (System.IO.File.Exists(Application.persistentDataPath + "/GameData.aem"))
            {
                return true;
            }else
            {
                return false;
            }

        return false;
    }
    
    public void SaveBin()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream      file = File.Create(Application.persistentDataPath + "/GameData.aem");

        DataSaving dataAux = new DataSaving();
        dataAux.personalBest = m_personalBest;
        bf.Serialize(file, dataAux);
        file.Close();
    }

    public void LoadBin()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream      file = File.Open(Application.persistentDataPath + "/GameData.aem", FileMode.Open);

        //COMPROBAR SI EXISTE FICHERO
        DataSaving dataAux = (DataSaving)bf.Deserialize(file);
        m_personalBest = dataAux.personalBest;
        file.Close();
    }

    private IEnumerator InitTimer()
    {
        m_timeLeft = 60f;
        while (m_timeLeft > 0)
        {
            m_timeLeft -= (Time.deltaTime);
            GameManager.Instance.UpdateTime();
            yield return null;
        }

        if(m_timeLeft <= 0)
            GameManager.Instance.setRoundLost();
    }

    public void nextRound()
    {
        m_actualRound++;
        if(m_actualRound > m_personalBest)
            SaveBin();
        GameManager.Instance.UpdateRound();
    }

    public bool getLoosesRound()
    {
        return m_lossesRound;
    }

    public int getTimeLeft()
    {
        return (int)Mathf.Ceil(m_timeLeft);
    }

    public int getActualRound()
    {
        return m_actualRound;
    }
}