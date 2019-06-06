using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Machinery
{
    [ExecuteInEditMode]
    public class KaiTool_BasicConnectRod : MonoBehaviour
    {
        [Header("ConnectRod")]
        [SerializeField]
        private Transform m_start;
        [SerializeField]
        private Transform m_end;
        private bool m_isInitialized = false;
        private Vector3 MiddlePoint
        {
            get
            {
                return (m_start.position + m_end.position) / 2;
            }
        }
        private Vector3 Direction
        {
            get
            {
                return m_end.position - m_start.position;
            }
        }
        private void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {
            InitVar();
        }
        private void InitVar()
        {
            m_isInitialized = true;
        }
        private void LateUpdate()
        {
            if (m_isInitialized)
            {
                ResetPosition();
                ResetRotation();
            }
        }
        private void ResetPosition()
        {
            transform.position = MiddlePoint;
        }

        private void ResetRotation()
        {
            transform.rotation = Quaternion.FromToRotation(transform.forward, Direction) * transform.rotation;
        }

        [ContextMenu("Connect")]
        private void Connect()
        {
            ResetPosition();
            ResetRotation();
        }
        private void OnValidate()
        {
            Connect();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            if (m_start != null && m_end != null)
            {
                Gizmos.DrawSphere(MiddlePoint, 0.2f);
            }
        }
    }
}