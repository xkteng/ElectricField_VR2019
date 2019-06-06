using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.FSM
{
    [CreateAssetMenu(menuName = "KaiTool/PluggableAI/State")]
    [Serializable]
    public sealed class State : ScriptableObject
    {
        public Action[] m_actions;
        public Transition[] m_transitions;
        public Color m_gizmosColor = Color.gray;
        public void UpdateState(KaiTool_StateController controller)
        {
            DoActions(controller);
            CheckTransition(controller);
        }
        private void DoActions(KaiTool_StateController controller)
        {
            for (int i = 0; i < m_actions.Length; i++)
            {
                m_actions[i].Act(controller);
            }
        }
        private void CheckTransition(KaiTool_StateController controller)
        {
            for (int i = 0; i < m_transitions.Length; i++)
            {
                if (m_transitions[i].m_decition.Decide(controller))
                {
                    controller.TransitionToState(m_transitions[i].m_trueState);
                }
                else
                {
                    controller.TransitionToState(m_transitions[i].m_falseState);
                }
            }
        }

        
    }
}

