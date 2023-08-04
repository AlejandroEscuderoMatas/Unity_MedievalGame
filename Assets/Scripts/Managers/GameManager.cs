using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = System.Random;

public class GameManager : TemporalSingleton<GameManager>
{
    private DataSaving   m_dataSaving = new DataSaving();
    
    public UIController  m_UIcontroller;
    public Player        m_player;

    public Transform     m_itemSpawn;
    public Item[]        m_itemVector;
    private bool         m_roundFinished = false;
    private bool         m_roundLost = false;
    private int          m_personalBest = 0;

    private void Start()
    {
        UpdateHealth(m_player.getStrength());
        UpdateKills();
        NewRound();
    }

    private void Update()
    {
        if (m_roundLost)
        {
            m_UIcontroller.setPlayerDead(true);
            //HAS PERDIDO, PANTALLA DE GAME OVER
        }

        if (m_roundFinished && m_player.getStrength() > 0)
        {
            SpawnRoundItem();
            m_roundFinished = false;
            StartCoroutine(WaitForNextRound());
        }
    }

    private void NewRound()
    {
        RoundManager.Instance.StartRound();
    }

    private void SpawnRoundItem()
    {
        Random randomNum = new Random();
        int roundItem = 0;
        
        roundItem = randomNum.Next(0, 3);

        Item newItem = Instantiate(m_itemVector[roundItem], Vector3.zero, Quaternion.identity);
        newItem.transform.position = m_itemSpawn.position;
        newItem.transform.rotation = m_itemSpawn.rotation;
        newItem.gameObject.SetActive(true);
    }

    private IEnumerator WaitForNextRound()
    {
        NewRound();
        yield return new WaitForSeconds(3f);
    }

    public void UpdateHealth(float health)
    {
        m_UIcontroller.setHealthText(Mathf.Ceil(health));
    }

    public void UpdateKills()
    {
        m_UIcontroller.setKillsText(EnemyManager.Instance.getPlayerKills());
    }

    public void UpdateRound()
    {
        m_UIcontroller.setRoundText(RoundManager.Instance.getActualRound());
    }

    public void UpdateTime()
    {
        m_UIcontroller.setTimeText(RoundManager.Instance.getTimeLeft());
    }

    public void setRoundFinished()
    {
        m_roundFinished = true;
    }
    public void setRoundLost()
    {
        m_roundLost = true;
    }

    
}
