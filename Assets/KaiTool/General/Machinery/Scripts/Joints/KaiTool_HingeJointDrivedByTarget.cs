using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Machinery
{
    public class KaiTool_HingeJointDrivedByTarget : KaiTool_BasicHingeJoint
    {
        [Header("Target")]
        [SerializeField]
        protected Transform m_target;
        private Vector3 m_originalRelativePos;
        private Quaternion m_originalRot;
        private Vector3 m_originalTargetDirection;
        protected override void Init()
        {
            base.Init();
            InitVar();
        }
        private void InitVar()
        {
            m_originalRelativePos = transform.position - m_aixs.m_point0.position;
            m_originalRot = transform.rotation;
            var vec = m_target.position - m_aixs.m_point0.position;
            m_originalTargetDirection = Vector3.ProjectOnPlane(vec, m_aixs.Direction);
        }
        protected override void ResetTranform()
        {
            var vec = m_target.position - m_aixs.m_point0.position; ;
            var currentTargetDirection = Vector3.ProjectOnPlane(vec, m_aixs.Direction);
            var deltaQuaternion = Quaternion.FromToRotation(m_originalTargetDirection, currentTargetDirection);
            transform.position = deltaQuaternion * m_originalRelativePos + m_aixs.m_point0.position;
            transform.rotation = deltaQuaternion * m_originalRot;
        }

        protected override void OnDrawGizmos()
        {
            if (m_aixs != null)
            {
                m_aixs.DrawGizmos(m_gizmosColor);
            }
            if (m_target != null)
            {
                Gizmos.color = m_gizmosColor;
                Gizmos.DrawSphere(m_target.position, 0.2f * m_gizmosSize);
            }
        }
    }
}