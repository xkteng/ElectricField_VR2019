using System;
using System.Collections;
using UnityEngine;

namespace KaiTool.Machinery
{
    [Flags]
    public enum EnumDirection
    {
        X = 1,
        Y = 2,
        Z = 4
    }

    public enum EnumMotionMode
    {
        Free,
        Limited,
        Locked
    }

    public abstract class KaiTool_BasicJoint : MonoBehaviour
    {
        [Header("Joint")]
        [SerializeField]
        protected Color m_gizmosColor = Color.green;
        [SerializeField]
        protected float m_gizmosSize = 1f;
        [SerializeField]
        private float m_intervalTime = 0.02f;
        private Coroutine m_updateCoroutine;

        private void Awake()
        {
            Init();
            LaterInit();
        }
        protected virtual void Init()
        {
            InitVar();
            InitEvent();
        }
        protected void InitVar() { }
        protected void InitEvent()
        {
        }
        protected virtual void LaterInit()
        {
            m_updateCoroutine = StartCoroutine(UpdateIEnumerator());
        }
        private IEnumerator UpdateIEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            while (true)
            {
                ResetTranform();
                yield return wait;
            }
        }

        protected abstract void ResetTranform();
        protected abstract void OnDrawGizmos();
    }
}