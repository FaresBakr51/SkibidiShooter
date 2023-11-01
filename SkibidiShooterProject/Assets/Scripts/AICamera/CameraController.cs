using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AICam
{
    public class CameraController : MonoBehaviour
    {
        [Header("Behave")]
        private NavMeshAgent m_Agent;
        private Animator m_Animator;


        [Header("HEALTH")]
        [SerializeField] private float health;


        private void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            m_Animator = GetComponent<Animator>();
            UpdatePath();
        }
        private void Update()
        {
            if (GameManager.Instance && GameManager.Instance.CurrentGameState == GameState.GS_Running)
            {
                CheckNextPoint();
            }
            else
            {
                if (m_Agent.hasPath)
                {
                    m_Agent.isStopped = true;
                    UpdateANimation(1);
                }
            }
        }
        private void CheckNextPoint()
        {
            if (m_Agent != null)
            {
                if (m_Agent.hasPath)
                {
                    if(m_Agent.remainingDistance <= 3)
                    {
                        UpdatePath();
                    }
                }
            }
        }
        private void UpdatePath()
        {
            Debug.Log("UPDATE pATH");
            if (m_Agent != null)
            {
                m_Agent.SetDestination(GameManager.Instance.GetRandomPatrolPoint().position);
            }
        }

       private void UpdateANimation(int val)
        {
            m_Animator.SetInteger("State", val);
        }
    }
}
