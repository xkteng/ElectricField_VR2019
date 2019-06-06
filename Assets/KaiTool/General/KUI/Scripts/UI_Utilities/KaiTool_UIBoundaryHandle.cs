using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace KaiTool.UI
{
    public struct UIBoundaryEventArgs
    {

    }
    [ExecuteInEditMode]
    public class KaiTool_UIBoundaryHandle : MonoBehaviour
    {
        [SerializeField]
        private float m_intervalTime = 0.05f;
        private Vector3 m_lastPosition = Vector3.zero;
        private event Action<System.Object, UIBoundaryEventArgs> m_positionChanged;
        public event Action<System.Object, UIBoundaryEventArgs> PositionChanged
        {
            add
            {
                m_positionChanged += value;
            }
            remove
            {
                m_positionChanged -= value;
            }
        }

        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            if (m_translateJudgmentCoroutine != null)
            {
                StopCoroutine(m_translateJudgmentCoroutine);
            }
            m_translateJudgmentCoroutine = StartCoroutine(TranslateIEnumerator());
        }
        protected virtual void Init()
        {
            m_lastPosition = transform.position;
        }
        private void OnPositionChanged(UIBoundaryEventArgs e)
        {
            if (m_positionChanged != null)
            {
                m_positionChanged(this, e);
            }
        }
        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
        }
        private Coroutine m_translateJudgmentCoroutine;
        private IEnumerator TranslateIEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            while (true)
            {
                if (m_lastPosition != transform.position)
                {
                    OnPositionChanged(new UIBoundaryEventArgs());
                }
                m_lastPosition = transform.position;
                yield return wait;
            }
        }
    }
}