using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Machinery
{
    public class KaiTool_Spring : MonoBehaviour
    {
        [Header("Spring")]
        [SerializeField]
        private Transform m_tip_0;
        [SerializeField]
        private Transform m_tip_1;
        [SerializeField]
        private Transform m_springMesh;
        [Header("Connect")]
        [SerializeField]
        private Transform m_connect_0;
        [SerializeField]
        private Transform m_connect_1;

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
        }
        private void InitEvent() { }

        private void FixedUpdate()
        {
            UpdatePosition();
            UpdateRotation();
            UpdateLength();

        }

        private void UpdateLength()
        {
            var connectDistance = (m_connect_1.position - m_connect_0.position).magnitude;
            var currentLength = (m_tip_1.position - m_tip_0.position).magnitude;
            var times = connectDistance / currentLength;
            var scale = m_springMesh.localScale;
            m_springMesh.localScale = new Vector3(scale.x, scale.y, scale.z * times);
        }

        private void UpdatePosition()
        {
            m_springMesh.position = (m_connect_0.position + m_connect_1.position) / 2;
        }

        private void UpdateRotation()
        {
            m_springMesh.rotation = Quaternion.FromToRotation(m_springMesh.forward, m_connect_0.position - m_connect_1.position) * m_springMesh.rotation;
        }
        private void OnValidate()
        {
            if (m_tip_0 && m_tip_1 && m_connect_0 && m_connect_1 && m_springMesh)
            {
                UpdatePosition();
                UpdateRotation();
                UpdateLength();
            }
        }
        private void Reset()
        {
            if (m_tip_0 && m_tip_1 && m_connect_0 && m_connect_1 && m_springMesh)
            {
                UpdatePosition();
                UpdateRotation();
                UpdateLength();
            }
        }

    }
}