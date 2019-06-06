using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.TrackRenderer
{
    public class KaiTool_TrackRendererController : MonoBehaviour
    {
        [SerializeField]
        private KaiTool_TrackRenderer[] m_orginalTrackes;
        private KaiTool_TrackRenderer[] m_allTracks;
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            InitVar();
        }
        private void InitVar()
        {
            m_allTracks = GetComponentsInChildren<KaiTool_TrackRenderer>();
        }
        public void Play()
        {
            for (int i = 0; i < m_orginalTrackes.Length; i++)
            {
                m_orginalTrackes[i].OnPlay();
            }
        }
        public void Restart()
        {
            for (int i = 0; i < m_orginalTrackes.Length; i++)
            {
                m_orginalTrackes[i].OnRestart();
            }
        }
        public void UpdateAllTrackFigure()
        {
            for (int i = 0; i < m_allTracks.Length; i++)
            {
                m_allTracks[i].UpdateTrackPosition();
            }
        }
    }
}