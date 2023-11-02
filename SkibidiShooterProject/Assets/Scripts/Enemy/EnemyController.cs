using Controller;
using Manager;
using SoundManage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour,IHitable
{
    [Header("Behave")]
    private NavMeshAgent m_Agent;
    private Animator m_Animator;
    [SerializeField] private float _damage;
    [SerializeField] private GameObject m_target;
    [SerializeField] private int attackDistance;
    bool canAttack = true;
    [SerializeField] private List<int> attackanimations = new List<int>();
    [SerializeField] private bool isBoss;
    [Header("HEALTH")]
    [SerializeField] private float health;
    public float EnemyHealth { get { return health; } }

    public float Health => health;

    [SerializeField] private Slider healthScroll;
    private bool dead = false;

    [Header("Sounds")]
    [SerializeField] private AudioSource source;
    private SoundManagment SoundManagment;

    void Start()
    {
        SoundManagment = GetComponent<SoundManagment>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        if (!LevelManager.Instance) return;
       if(!isBoss)  UpdateHealth(LevelManager.Instance.GetEnemeyMaxHealth);
        healthScroll.maxValue = health;
        m_target = GameManager.Instance.GetRandomFakePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance && GameManager.Instance.CurrentGameState == GameState.GS_Running)
        {
            if (health <= 0 && !dead)
            {
                if (GameManager.Instance && GameManager.Instance.GetLeftEnimes() > 0 && !isBoss) //boss not counted
                {
                    GameManager.Instance.EnemeyDied();
                }
                if (m_Agent)
                {
                    m_Agent.isStopped = true;
                }
                m_Animator.Play("die");
                StartCoroutine(DestroyEnemey());
                dead = true;
            }
            else
            {
                
                ChasePlayer();
            }
        }
        else
        {
            if (GameManager.Instance.CurrentPlayer && GameManager.Instance.CurrentPlayer.Health <= 0)
            {
                if (m_Agent)
                {
                    m_Agent.isStopped = true;
                }
                UpdateStateAnimation(4);
            }

        }
    }

    #region EnemeyBehavior
    private void ChasePlayer()
    {
        if(GameManager.Instance && m_target && !IsTargetClose())
        {
            UpdateStateAnimation(1);
            canAttack = true;
            //  Debug.Log("Chase");
            m_Agent.SetDestination(m_target.transform.position);
            
        }
        //  Debug.Log(DistanceWithPlayer());
        if (IsRealPlayerClose())
        {
            UpdateTarget(GameManager.Instance.CurrentPlayer.gameObject);
          
        }
        if( m_target ==null && !IsRealPlayerClose())
        {
            m_target = GameManager.Instance.GetRandomFakePlayer();
        }
        if(m_target == null && GameManager.Instance.GetRandomFakePlayer()==null)
        {
            UpdateTarget(GameManager.Instance.CurrentPlayer.gameObject);
        }else if(m_target == null && GameManager.Instance.GetRandomFakePlayer() == null && GameManager.Instance.CurrentPlayer.Health <= 0)
        {
            m_target = null;
        }
        if (m_target&&IsTargetClose() && canAttack)
        {
            LookAtTarget();
            Attack();
            canAttack = false;
        }

    }
    void Attack()
    {
        int rand = Random.Range(0, attackanimations.Count);
        UpdateStateAnimation(attackanimations[rand]);
     
    }
    public void UpdateTarget(GameObject newtarget)
    {
        m_target = newtarget;
    }
    public void ApplyDamage()
    {
        if (!m_target) return;
        if (!IsTargetClose()) return;
        var damagableobj =  m_target.TryGetComponent(out IHitable hitable);
        if (hitable !=null && hitable.Health >0)
        {
            hitable.TakeHit(_damage);
        }
        if(hitable != null && hitable.Health <= 0)
        {

            UpdateStateAnimation(4);
        }
        if (SoundManagment && m_target.GetComponent<FPSController>())
        {
            SoundManagment.PlayOnShot(source, "attack");
        }
    }
    public void CanAttack()
    {
        canAttack = true;
    }
 
    bool IsRealPlayerClose()
    {
      
        return (GameManager.Instance.CurrentPlayer.transform.position - transform.position).magnitude <= attackDistance;
    }
    private void LookAtTarget()
    {
        if (!m_target) return;
        transform.LookAt(m_target.transform, Vector3.up);
    }
    float DistanceWithTarget()
    {
       
        return (m_target.transform.position - transform.position).magnitude;
    }
    bool IsTargetClose()
    {
        return DistanceWithTarget() <= m_Agent.stoppingDistance;
    }
    private void UpdateStateAnimation(int val)
    {
        m_Animator.SetInteger("State", val);
    }
    #endregion
    #region Damage&Health
    public void TakeHit(float damage)
    {
        if (health > 0)
        {
            UpdateHealth(health - damage);
            //only player who shot focus player

            UpdateTarget(GameManager.Instance.CurrentPlayer.gameObject);
        }
    }
    private void UpdateHealth(float val)
    {
        health = val;
        healthScroll.value = health;
    }
    /**
     * Death
    /*/
    IEnumerator DestroyEnemey()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    #endregion

}
