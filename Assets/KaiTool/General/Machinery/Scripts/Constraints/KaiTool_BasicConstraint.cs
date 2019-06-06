using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Machinery
{
    [ExecuteInEditMode]
    public abstract class KaiTool_BasicConstraint : MonoBehaviour
    {
        [Header("Contraint")]
        [SerializeField]
        private bool m_isDrawGizmos = true;
        [SerializeField]
        protected Color m_gizmosColor = Color.green;
        [SerializeField]
        protected float m_gizmosSize = 1f;
        private void Awake()
        {
            Init();
            LaterInit();
        }
        protected virtual void Init() { }
        protected virtual void LaterInit() { }

        private void Update()
        {
            ResetPosition();
        }
        protected abstract void ResetPosition();
        protected abstract void DrawGizmos();
        private void OnDrawGizmos()
        {
            if (m_isDrawGizmos)
            {
                DrawGizmos();
            }
        }
    }
}