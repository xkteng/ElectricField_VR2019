//#define Debug
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.TrackRenderer
{
    public struct TrackRendererEventArgs
    {

    }
    public abstract class KaiTool_TrackRenderer : MonoBehaviour
    {
        [Header("Render")]
        [SerializeField]
        protected float m_duration = 1f;
        [SerializeField]
        protected float m_delay = 0f;
        [Header("Status")]
        [SerializeField]
        private bool m_isStartedPlaying = false;
        [SerializeField]
        private bool m_isPlaying = false;
        [SerializeField]
        private bool m_isEnded = false;
        [Header("Events")]
        public UnityEvent m_play;
        public UnityEvent m_pause;
        public UnityEvent m_resume;
        public UnityEvent m_stop;
        public UnityEvent m_restart;
        [Header("Loop")]
        [SerializeField]
        private float m_intervalTime = 0.03f;
        [Header("TrackPoints")]
        [SerializeField]
        protected float m_trackLength = 0f;
        [SerializeField]
        protected Transform[] m_trackPoints;
        [Header("Gizmos")]
        [SerializeField]
        private bool m_isDrawGizmos = true;
        [SerializeField]
        private Color m_gizmosColor = Color.green;
        [SerializeField]
        [Range(0f, 1f)]
        private float m_gizmosSize = 0.5f;

        [SerializeField]
        private bool m_isShowStatusProperties = false;
        [SerializeField]
        private bool m_isShowEventsProperties = false;
        [SerializeField]
        private bool m_isShowGizmosProperties = false;


        public Action<UnityEngine.Object, TrackRendererEventArgs> m_playEventHandle;
        public Action<UnityEngine.Object, TrackRendererEventArgs> m_pauseEventHandle;
        public Action<UnityEngine.Object, TrackRendererEventArgs> m_resumeEventHandle;
        public Action<UnityEngine.Object, TrackRendererEventArgs> m_stopEventHandle;
        public Action<UnityEngine.Object, TrackRendererEventArgs> m_restartEventHandle;

        private Coroutine m_renderingCoroutine;
        //[SerializeField]
        protected int m_trackPointsDrawnNum = 1;
        protected float m_lastPointTrackLength = 0f;
        protected float m_nextPointTrackLength = 0f;
        protected float m_currentTime = 0f;

        public bool IsShowStatusProperties
        {
            get
            {
                return m_isShowStatusProperties;
            }

            set
            {
                m_isShowStatusProperties = value;
            }
        }

        public bool IsShowEventsProperties
        {
            get
            {
                return m_isShowEventsProperties;
            }

            set
            {
                m_isShowEventsProperties = value;
            }
        }

        public bool IsShowGizmosProperties
        {
            get
            {
                return m_isShowGizmosProperties;
            }

            set
            {
                m_isShowGizmosProperties = value;
            }
        }

        private void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {
            InitVar();
            Restore();
        }
        private void InitVar()
        {
            SetTrackLength();
        }
        public void OnPlay()
        {
            OnPlay(new TrackRendererEventArgs());
        }
        public void OnStop()
        {
            OnStop(new TrackRendererEventArgs());
        }
        public void OnPause()
        {
            OnPause(new TrackRendererEventArgs());
        }
        public void OnRestart()
        {
            OnRestart(new TrackRendererEventArgs());
        }
        public void TogglePause()
        {
            TogglePause(new TrackRendererEventArgs());
        }
        public void OnPlay(TrackRendererEventArgs e)
        {
            if (!m_isStartedPlaying)
            {
                m_isStartedPlaying = true;
                m_isPlaying = true;
                if (m_play != null)
                {
                    m_play.Invoke();
                }
                if (m_playEventHandle != null)
                {
                    m_playEventHandle(this, e);
                }
                if (m_renderingCoroutine != null)
                {
                    StopCoroutine(m_renderingCoroutine);
                }
                m_renderingCoroutine = StartCoroutine(RenderingEnumerator());
            }
        }
        protected void OnStop(TrackRendererEventArgs e)
        {
            if (m_isStartedPlaying && m_isPlaying && !m_isEnded)
            {
                m_isEnded = true;
                m_isPlaying = false;
                if (m_stop != null)
                {
                    m_stop.Invoke();
                }
                if (m_stopEventHandle != null)
                {
                    m_stopEventHandle(this, e);
                }
            }
        }
        public void OnPause(TrackRendererEventArgs e)
        {
            if (m_isPlaying && m_isStartedPlaying && !m_isEnded)
            {
                m_isPlaying = false;
                if (m_pause != null)
                {
                    m_pause.Invoke();
                }
                if (m_pauseEventHandle != null)
                {
                    m_pauseEventHandle(this, e);
                }
            }
        }
        public void OnResume(TrackRendererEventArgs e)
        {
            if (!m_isPlaying && m_isStartedPlaying && !m_isEnded)
            {
                m_isPlaying = true;
                if (m_resume != null)
                {
                    m_resume.Invoke();
                }
                if (m_resumeEventHandle != null)
                {
                    m_resumeEventHandle(this, e);
                }
            }
        }
        public void OnRestart(TrackRendererEventArgs e)
        {
            if (m_isStartedPlaying)
            {
                if (m_restart != null)
                {
                    m_restart.Invoke();
                }
                if (m_restartEventHandle != null)
                {
                    m_restartEventHandle(this, e);
                }
                Restore();
                if (m_renderingCoroutine != null)
                {
                    StopCoroutine(m_renderingCoroutine);
                }
            }
        }
        public void TogglePause(TrackRendererEventArgs e)
        {
            if (m_isPlaying)
            {
                OnPause(e);
            }
            else
            {
                OnResume(e);
            }
        }
        public abstract void UpdateTrackPosition();
        private IEnumerator RenderingEnumerator()
        {
            yield return new WaitForSeconds(m_delay);
            var wait = new WaitForSeconds(m_intervalTime);
            while (m_currentTime < m_duration + m_intervalTime)
            {
                if (m_isPlaying)
                {
                    Render();
                    m_currentTime += m_intervalTime;
                }
                yield return wait;
            }
            var args = new TrackRendererEventArgs();
            OnStop(args);
        }
        protected abstract void Render();
        protected virtual void Restore()
        {
            m_isStartedPlaying = false;
            m_isPlaying = false;
            m_isEnded = false;

            m_trackPointsDrawnNum = 1;
            m_lastPointTrackLength = 0f;
            m_nextPointTrackLength = 0f;
            m_currentTime = 0f;
        }
        public void SetTrackLength()
        {
            var sum = 0f;
            if (m_trackPoints != null)
            {
                for (int i = 0; i < m_trackPoints.Length - 1; i++)
                {
                    sum += (m_trackPoints[i + 1].position - m_trackPoints[i].position).magnitude;
                }
            }
            m_trackLength = sum;
        }
        protected void SetNextTrackPoint()
        {
            var sum = 0f;
            for (int i = 0; i < m_trackPointsDrawnNum; i++)
            {
                sum += (m_trackPoints[i + 1].position - m_trackPoints[i].position).magnitude;
            }
            m_nextPointTrackLength = sum;
        }
        protected void SetLastTrackPoint()
        {
            var sum = 0f;
            for (int i = 0; i < m_trackPointsDrawnNum - 1; i++)
            {
                sum += (m_trackPoints[i + 1].position - m_trackPoints[i].position).magnitude;
            }
            m_nextPointTrackLength = sum;
        }
        protected virtual void OnDrawGizmos()
        {
            if (m_isDrawGizmos)
            {
                Gizmos.color = m_gizmosColor;
                if (m_trackPoints.Length > 0)
                {
                    for (int i = 0; i < m_trackPoints.Length - 1; i++)
                    {
                        Gizmos.DrawLine(m_trackPoints[i].position, m_trackPoints[i + 1].position);
                    }
                    for (int i = 0; i < m_trackPoints.Length; i++)
                    {
                        Gizmos.DrawWireSphere(m_trackPoints[i].position, 0.2f * m_gizmosSize);
                    }
                }
            }
        }
        protected virtual void Reset()
        {

        }
        protected virtual void OnValidate()
        {
            SetTrackLength();
        }
#if Debug
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                var args = new TrackRendererEventArgs();
                OnPlaying(args);
                print("Start!");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                var args = new TrackRendererEventArgs();
                OnRestart(args);
                print("ReStart!");
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                var args = new TrackRendererEventArgs();
                OnPause(args);
                print("Pause");
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                var args = new TrackRendererEventArgs();
                OnResume(args);
                print("Resume");
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                var args = new TrackRendererEventArgs();
                TogglePause(args);
                print("TogglePause");
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                UpdateTrackPosition();
                print("UpdateTrackPosition");
            }
        }
#endif 

    }
}