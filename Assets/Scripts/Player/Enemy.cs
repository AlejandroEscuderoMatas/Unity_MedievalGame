using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private EnemyManager  m_enemyManager;

    
    public EnemyList      m_enemyStats;
    
    public float         m_strenght;
    public float         m_damage;
    
    private float         m_fieldOfView = 60f;
    private float         m_hearingDistance = 25f;

    private GameObject    m_player;
    
    private Transform     m_playerTransform;
    public Animator       m_animator;
    public NavMeshAgent   m_meshAgent;
    public Transform[]    m_wayPoints;
    public int            m_nextPoint;
    private bool          m_hasHitted = false;
    
    //ESTADOS
    private StateMachine  m_stateMachine;
    private IdleState     m_idle;
    private PatrolState   m_patrol;
    private ChaseState    m_chase;
    private AttackState   m_attack;
    private HittedState   m_hitted;
    private DeadState     m_dead;


    void Start()
    {
        m_enemyManager = EnemyManager.Instance;
        m_player = GameObject.FindWithTag("Player");
        m_playerTransform = m_player.transform;
        m_meshAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        m_nextPoint = -1;
        m_meshAgent.autoBraking = false;

        m_stateMachine = GetComponent<StateMachine>();
        m_idle = GetComponent<IdleState>();
        m_patrol = GetComponent<PatrolState>();
        m_chase = GetComponent<ChaseState>();
        m_attack = GetComponent<AttackState>();
        m_hitted = GetComponent<HittedState>();
        m_dead = GetComponent<DeadState>();

        m_idle.Init(this, m_stateMachine);
        m_patrol.Init(this, m_stateMachine);
        m_chase.Init(this, m_stateMachine);
        m_attack.Init(this, m_stateMachine);
        m_hitted.Init(this, m_stateMachine);
        m_dead.Init(this, m_stateMachine);
        m_stateMachine.Init(m_patrol);
    }

    public void adaptStats(int difficultyMultiplier)
    {
        m_damage = m_enemyStats.enemyList[difficultyMultiplier].damage;
        m_strenght = m_enemyStats.enemyList[difficultyMultiplier].strength;;
    }

    public void NextWaypoint()
    {
        if (m_wayPoints.Length == 0)
                 return;
        
        m_nextPoint = (m_nextPoint + 1) % m_wayPoints.Length;
        m_meshAgent.destination = m_wayPoints[m_nextPoint].position;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IWeapon>() != null)
        {
            Sword sword = other.GetComponent<Sword>();
            m_strenght -= sword.getDamage();
            
            checkCollision();

            //PARA QUE SALGA SANGRE O ALGO ASI
            //Instantiate(m_rcvDamage, transform.position, Quaternion.identity);
        }
        else if (other.GetComponent<Player>() != null && m_hasHitted)
        {
            other.GetComponent<Player>().setStrength(-m_damage);
            m_hasHitted = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Arrow"))
        {
            Arrow arrow = other.gameObject.GetComponent<Arrow>();
            m_strenght -= arrow.getDamage();
            other.gameObject.SetActive(false);
            
            checkCollision();
        }
    }

    private void checkCollision()
    {
        if (IsDead())
        {
            m_animator.SetTrigger("Dies");
            this.GetComponent<Collider>().enabled = false;
            StartCoroutine(WaitForDeath());
            
        }
        else
        {
            m_animator.SetTrigger("Hitted");
            StartCoroutine(ResetHittedCooldown());
        }
    }

    private IEnumerator ResetHittedCooldown()
    {
        yield return new WaitForSeconds(1f);
    }
    
    private IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(3f);
        m_enemyManager.enemyDies(gameObject);
    }

    public bool IsDead()
    {
        if (m_strenght <= 0) return true;
        else return false;
    }

    public void SetWayPoints(Transform[] enemyRoute)
    {
        for (int i = 0; i < m_wayPoints.Length; i++)
        {
            m_wayPoints[i] = enemyRoute[i];
        }
    }

    public Transform getPlayerTransform()
    {
        return m_playerTransform;
    }
    public GameObject getPlayer()
    {
        return m_player;
    }

    public void setHasHitted(bool aux)
    {
        m_hasHitted = aux;
    }

    public int getNextWaypoint()
    {
        return m_nextPoint;
    }
    public NavMeshAgent getNavMeshAgent()
    {
        return m_meshAgent;
    }
    public StateMachine getSM()
    {
        return m_stateMachine;
    }
    public State getIdle()
    {
        return m_idle;
    }
    public State getPatrol()
    {
        return m_patrol;
    }
    public State getChase()
    {
        return m_chase;
    }
    public State getAttack()
    {
        return m_attack;
    }

    public State getHitted()
    {
        return m_hitted;
    }

    public float getFieldOfView()
    {
        return m_fieldOfView;
    }
    
    public float getHearingDistance()
    {
        return m_hearingDistance;
    }
}