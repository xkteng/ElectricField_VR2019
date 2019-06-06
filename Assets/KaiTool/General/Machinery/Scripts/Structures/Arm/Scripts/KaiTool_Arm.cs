using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Machinery
{
    [ExecuteInEditMode]
    public class KaiTool_Arm : MonoBehaviour
    {


        [SerializeField]
        private KaiTool_ArmRod m_rod_0;
        [SerializeField]
        private KaiTool_ArmJoint m_joint;
        [SerializeField]
        private KaiTool_ArmRod m_rod_1;
        [SerializeField]
        private Transform m_driver;

        private void Update()
        {
            if (m_driver)
            {
                UpateNormal();
                UpdateArm_0();
                UpdateJoint();
                UpdateArm_1();
            }
        }

        private void UpateNormal()
        {
            var dir = m_driver.position - m_rod_0.Tip_0.position;
            var targetNormal = Vector3.ProjectOnPlane(Vector3.Cross(dir, transform.up), transform.up);
            var q = Quaternion.FromToRotation(transform.right, targetNormal);
            transform.rotation = q * transform.rotation;
        }
        private void UpdateArm_0()
        {
            var tip_0_Pos = m_rod_0.Tip_0.position;
            var dir = m_driver.position - m_rod_0.Tip_0.position;
            var C = dir.magnitude;
            var A = m_rod_0.Length;
            var B = m_rod_1.Length;
            var cos = Mathf.Clamp(((Mathf.Pow(C, 2) + Mathf.Pow(A, 2) - Mathf.Pow(B, 2)) / (2 * A * C)), -1, 1);
            var theta = Mathf.Acos(cos) * Mathf.Rad2Deg;
            var normal = transform.right;
            var q = Quaternion.AngleAxis(theta, normal);
            m_rod_0.transform.LookAt(m_rod_0.transform.position + q * dir);
            m_rod_0.transform.position += tip_0_Pos - m_rod_0.Tip_0.position;
        }
        private void UpdateJoint()
        {
            m_joint.transform.position = m_rod_0.Tip_1.position;
            m_joint.transform.LookAt(m_joint.transform.position + transform.right);
        }
        private void UpdateArm_1()
        {
            var dir = m_driver.position - m_rod_0.Tip_0.position;
            var C = dir.magnitude;
            var A = m_rod_0.Length;
            var B = m_rod_1.Length;
            var cos = Mathf.Clamp(((Mathf.Pow(A, 2) + Mathf.Pow(B, 2) - Mathf.Pow(C, 2)) / (2 * A * B)), -1, 1);
            var beta = Mathf.Acos(-cos) * Mathf.Rad2Deg;
            var normal = transform.right;
            var q = Quaternion.AngleAxis(-beta, normal);
            m_rod_1.transform.LookAt(m_rod_1.transform.position + q * m_rod_0.Forward, m_rod_0.transform.up);
            m_rod_1.transform.position += (m_rod_0.Tip_1.position - m_rod_1.Tip_0.position);
        }
    }
}
