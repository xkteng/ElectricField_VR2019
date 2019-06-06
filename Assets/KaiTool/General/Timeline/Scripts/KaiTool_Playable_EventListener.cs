using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace KaiTool.Timeline
{
    [Serializable]
    public class PlayableEvent : UnityEvent
    {

    }
    public struct Listener_EventArgs
    {
        public Playable m_playable;
        public FrameData m_info;
    }
    public class KaiTool_Playable_EventListener : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField]
        private PlayableEvent m_behaviourPlay;
        [SerializeField]
        private PlayableEvent m_behaviourPause;
        [SerializeField]
        private PlayableEvent m_graphStart;
        [SerializeField]
        private PlayableEvent m_graphStop;
        [SerializeField]
        private PlayableEvent m_prepareFrame;

        //Event Handles
        public Action<System.Object, Listener_EventArgs> m_behaviourPlayEventHandle;
        public Action<System.Object, Listener_EventArgs> m_behaviourPauseEventHandle;
        public Action<System.Object, Listener_EventArgs> m_graphStartEventHandle;
        public Action<System.Object, Listener_EventArgs> m_graphStopEventHandle;
        public Action<System.Object, Listener_EventArgs> m_prepareFrameEventHandle;

        public void OnBehaviourPlay(Listener_EventArgs e)
        {
            if (m_behaviourPlay != null)
            {
                m_behaviourPlay.Invoke();
            }
            if (m_behaviourPlayEventHandle != null)
            {
                m_behaviourPlayEventHandle(this, e);
            }
        }

        public void OnBehaviourPause(Listener_EventArgs e)
        {
            if (m_behaviourPause != null)
            {
                m_behaviourPause.Invoke();
            }
            if (m_behaviourPauseEventHandle != null)
            {
                m_behaviourPauseEventHandle(this, e);
            }
        }

        public void OnGraphStart(Listener_EventArgs e)
        {
            if (m_graphStart != null)
            {
                m_graphStart.Invoke();
            }
            if (m_graphStartEventHandle != null)
            {
                m_graphStartEventHandle(this, e);
            }
        }
        public void OnGraphStop(Listener_EventArgs e)
        {
            if (m_graphStop != null)
            {
                m_graphStop.Invoke();
            }
            if (m_graphStopEventHandle != null)
            {
                m_graphStopEventHandle(this, e);
            }
        }

        public void OnPrepareFrame(Listener_EventArgs e)
        {
            if (m_prepareFrame != null)
            {
                m_prepareFrame.Invoke();
            }
            if (m_prepareFrameEventHandle != null)
            {
                m_prepareFrameEventHandle(this, e);
            }
        }
    }
}