using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace KaiTool.Timeline
{
    [RequireComponent(typeof(PlayableDirector))]
    public class KaiTool_PlayableDirectorAttach : MonoBehaviour
    {
        [SerializeField]
        private float m_playSpeed = 1f;

        [HideInInspector]
        [SerializeField]
        private PlayableDirector m_playableDirector;

        private float m_intervalTime = 0.02f;
        private Coroutine m_evaluateCoroutine;

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar() { }
        private void InitEvent() { }


        private void OnEnable()
        {
            if (m_playableDirector.timeUpdateMode == DirectorUpdateMode.Manual && m_playableDirector.playOnAwake)
            {
                m_playableDirector.Play();
                OnPlayForward();
            }
        }
        public void OnPlayForward()
        {
            if (m_evaluateCoroutine != null)
            {
                StopCoroutine(m_evaluateCoroutine);
            }
            m_evaluateCoroutine = StartCoroutine(PlayForwardEnumerator());
        }
        public void OnPlayBackward()
        {
            if (m_evaluateCoroutine != null)
            {
                StopCoroutine(m_evaluateCoroutine);
            }
            m_evaluateCoroutine = StartCoroutine(PlayBackwardEnumerator());
        }
        public void OnRewind()
        {
            m_playableDirector.time = m_playableDirector.initialTime;
        }


        private IEnumerator PlayForwardEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            m_playableDirector.time += 1f;
            while (m_playableDirector.time < m_playableDirector.duration)
            {
                m_playableDirector.time += m_intervalTime * m_playSpeed;
                m_playableDirector.Evaluate();
                if (m_playableDirector.time > m_playableDirector.duration && m_playableDirector.extrapolationMode == DirectorWrapMode.Loop)
                {
                    m_playableDirector.time = 0f;
                }
                yield return wait;
            }
            if (m_playableDirector.extrapolationMode == DirectorWrapMode.None)
            {
                m_playableDirector.Stop();
            }
            m_evaluateCoroutine = null;
        }
        private IEnumerator PlayBackwardEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            while (m_playableDirector.time > 0)
            {
                m_playableDirector.time -= m_intervalTime * m_playSpeed;
                m_playableDirector.Evaluate();
                if (m_playableDirector.time < 0 && m_playableDirector.extrapolationMode == DirectorWrapMode.Loop)
                {
                    m_playableDirector.time = m_playableDirector.duration;
                }
                yield return wait;
            }
            m_evaluateCoroutine = null;
        }
        private void Reset()
        {
            m_playableDirector = GetComponent<PlayableDirector>();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                OnPlayForward();
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                OnPlayBackward();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                OnRewind();
            }

        }
    }
}