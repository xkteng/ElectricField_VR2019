using UnityEngine;
namespace KaiTool.TrackRenderer
{
    [RequireComponent(typeof(LineRenderer))]
    public class KaiTool_TrackRendererUsingLineRenderer : KaiTool_TrackRenderer
    {
        #region LineRenderer
        [SerializeField]
        private LineRenderer m_lineRenderer;
        [SerializeField]
        private bool m_isShowLineRendererProperties = false;
        [Header("LineRenderer")]
        [SerializeField]
        private float m_startWidth = 1f;
        [SerializeField]
        private float m_endWidth = 1f;
        [SerializeField]
        private Color m_startColor = Color.white;
        [SerializeField]
        private Color m_endColor = Color.white;
        #endregion
        public bool IsShowLineRendererProperties
        {
            get
            {
                return m_isShowLineRendererProperties;
            }
            set {
                m_isShowLineRendererProperties = value;
            }
        }
        protected override void Init()
        {
            InitVar();
            base.Init();
        }
        private void InitVar()
        {
            m_lineRenderer = GetComponent<LineRenderer>();
        }
        protected override void Restore()
        {
            base.Restore();
            if (m_trackPoints.Length > 1)
            {
                m_nextPointTrackLength = (m_trackPoints[1].position - m_trackPoints[0].position).magnitude;
            }
            m_lineRenderer.positionCount = 2;
            m_lineRenderer.SetPosition(0, m_trackPoints[0].position);
            m_lineRenderer.SetPosition(1, m_trackPoints[0].position);
        }
        protected override void Render()
        {
            if (m_currentTime < m_duration)
            {
                var delta = m_nextPointTrackLength - m_currentTime / m_duration * m_trackLength;
                if (delta <= 0)
                {
                    m_lastPointTrackLength = m_nextPointTrackLength;
                    if (m_trackPointsDrawnNum < m_trackPoints.Length - 1)
                    {
                        m_nextPointTrackLength += (m_trackPoints[m_trackPointsDrawnNum + 1].position - m_trackPoints[m_trackPointsDrawnNum].position).magnitude;
                        m_lineRenderer.positionCount++;
                        m_lineRenderer.SetPosition(m_trackPointsDrawnNum, m_trackPoints[m_trackPointsDrawnNum].position);
                        m_lineRenderer.SetPosition(m_trackPointsDrawnNum + 1, m_trackPoints[m_trackPointsDrawnNum].position);
                        m_trackPointsDrawnNum++;
                    }
                }
                else
                {
                    m_lineRenderer.SetPosition(m_trackPointsDrawnNum,
                        Vector3.Lerp(m_trackPoints[m_trackPointsDrawnNum - 1].position, m_trackPoints[m_trackPointsDrawnNum].position, 1 - (delta / (m_nextPointTrackLength - m_lastPointTrackLength))));
                }
            }
            else
            {
                m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, m_trackPoints[m_trackPoints.Length - 1].position);
            }
        }
        /// <summary>
        /// Use this to bind lineRenderer to track position.
        /// </summary>
        public override void UpdateTrackPosition()
        {
            SetTrackLength();
            SetLastTrackPoint();
            //m_nextPointTrackLength = m_lastPointTrackLength+(m_trackPoints[m_trackPointsDrawnNum].position - m_trackPoints[m_trackPointsDrawnNum-1].position).magnitude;
            SetNextTrackPoint();
            for (int i = 0; i < m_trackPointsDrawnNum; i++)
            {
                m_lineRenderer.SetPosition(i, m_trackPoints[i].position);
            }
            if (m_lineRenderer.positionCount > m_trackPointsDrawnNum)
            {
                var delta = m_nextPointTrackLength - m_currentTime / m_duration * m_trackLength;
                m_lineRenderer.SetPosition(m_trackPointsDrawnNum,
                    Vector3.Lerp(m_trackPoints[m_trackPointsDrawnNum - 1].position, m_trackPoints[m_trackPointsDrawnNum].position, 1 - (delta / (m_nextPointTrackLength - m_lastPointTrackLength))));
            }
        }
        protected override void Reset()
        {
            base.Reset();
            m_lineRenderer = GetComponent<LineRenderer>();
            if (m_lineRenderer == null)
            {
                m_lineRenderer = gameObject.AddComponent<LineRenderer>();
            }
        }
        protected override void OnValidate()
        {
            base.OnValidate();
            ResetLineRenderer();
        }
        private void ResetLineRenderer()
        {
            if (m_lineRenderer == null)
            {
                m_lineRenderer = GetComponent<LineRenderer>();
            }
            if (m_lineRenderer == null)
            {
                m_lineRenderer = gameObject.AddComponent<LineRenderer>();
            }
            m_lineRenderer.startWidth = m_startWidth;
            m_lineRenderer.endWidth = m_endWidth;
            m_lineRenderer.startColor = m_startColor;
            m_lineRenderer.endColor = m_endColor;
        }
    }
}