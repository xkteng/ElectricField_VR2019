using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Machinery
{
    public class KaiTool_ArmRod : MonoBehaviour
    {
        [SerializeField]
        private Transform m_tip_0;
        [SerializeField]
        private Transform m_tip_1;

        public Transform Tip_0
        {
            get
            {
                return m_tip_0;
            }

            set
            {
                m_tip_0 = value;
            }
        }
        public Transform Tip_1
        {
            get
            {
                return m_tip_1;
            }

            set
            {
                m_tip_1 = value;
            }
        }

        public Vector3 Forward
        {
            get
            {
                var forward = (m_tip_1.position - m_tip_0.position).normalized;
                return forward;
            }
        }

        public float Length
        {
            get
            {
                var length = (m_tip_1.position - m_tip_0.position).magnitude;
                return length;
            }
        }
    }
}