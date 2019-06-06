using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace KaiTool.FSM
{
    public class KaiTool_StateController : MonoBehaviour
    {
        [SerializeField]
        private EnemyStats m_enemyStats;
        [SerializeField]
        private Transform m_eyes;

        public State m_state;


        private NavMeshAgent m_navMeshAgent;

        private void Awake()
        {
            InitVar();
            InitEvent();
        }

        private void InitVar()
        {
            m_navMeshAgent = GetComponent<NavMeshAgent>();
        }
        private void InitEvent() { }
        public NavMeshAgent NavMeshAgent
        {
            get
            {
                return m_navMeshAgent;
            }
        }
        public void TransitionToState(State state)
        {
            m_state = state;
        }
        private void Update()
        {
            m_state.UpdateState(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = m_state.m_gizmosColor;
            if (m_eyes)
            {
                Gizmos.DrawWireSphere(m_eyes.position, m_enemyStats.m_lookSphereCastRadius);
            }
        }
    }
}