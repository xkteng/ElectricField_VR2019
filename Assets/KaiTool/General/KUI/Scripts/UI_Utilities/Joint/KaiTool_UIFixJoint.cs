using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.UI
{
    public class KaiTool_UIFixJoint : KaiTool_UIBasicJoint
    {
        [SerializeField]
        private Transform m_anchor;

        private Vector3 m_relativePos;
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            m_relativePos = transform.position - m_anchor.position;
        }

        private void InitEvent() { }
        private void Update()
        {
            Fix();
        }
        private void Fix()
        {
            transform.position = m_anchor.position + m_relativePos;
        }

    }
}