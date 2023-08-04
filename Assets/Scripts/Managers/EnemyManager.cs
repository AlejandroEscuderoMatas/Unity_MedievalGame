using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : TemporalSingleton<EnemyManager>
{
    public Enemy          enemy;
    private List<Enemy>   m_enemyVector = new List<Enemy>();
    private int           m_playerKills = 0;

    private int           m_roundKills;

    public Transform[]    m_path1_1;
    public Transform[]    m_path1_2;
    public Transform[]    m_path2_1;
    public Transform[]    m_path2_2;
    private Transform[][] m_allPaths;
    
    public Transform[]    m_spawns;

    private int           initNumEnemies = 4;

    private void Awake()
    {
        initAllPaths();
        createEnemies();
    }

    void createEnemies()
    {
        for (int i = 0; i < initNumEnemies; i++)
        {
            Enemy enemyAux = Instantiate(enemy, Vector3.zero, Quaternion.identity);
            enemyAux.gameObject.SetActive(false);
            m_enemyVector.Add(enemyAux);

            m_enemyVector[i].SetWayPoints(m_allPaths[i]);
        }
    }

    public void initSpawnEnemies(int difficultyMultiplier)
    {
        m_roundKills = 0;
        for (int i = 0; i < initNumEnemies; i++)
        {
            //NOS DEVUELVE FALSE SI NO ESTA ACTIVO 
            if (!m_enemyVector[i].gameObject.activeSelf)
            {
                m_enemyVector[i].adaptStats(difficultyMultiplier);
                m_enemyVector[i].transform.position = m_spawns[i].position;
                m_enemyVector[i].transform.rotation = m_spawns[i].rotation;
                m_enemyVector[i].GetComponent<Collider>().enabled = true;
                m_enemyVector[i].gameObject.SetActive(true);
            }
        }
    }

    void initAllPaths()
    {
        m_allPaths = new Transform[4][];

        m_allPaths[0] = m_path1_1;
        m_allPaths[1] = m_path1_2;
        m_allPaths[2] = m_path2_1;
        m_allPaths[3] = m_path2_2;
    }

    public void enemyDies(GameObject eGameObject)
    {
        eGameObject.SetActive(false);
        m_playerKills += 1;
        m_roundKills++;

        if (m_roundKills == m_enemyVector.Count)
        {
            GameManager.Instance.setRoundFinished();
        }

        GameManager.Instance.UpdateKills();
    }

    public int getNumEnemies()
    {
        return m_enemyVector.Count;
    }

    public int getRoundKills()
    {
        return m_roundKills;
    }

    public int getPlayerKills()
    {
        return m_playerKills;
    }
}
