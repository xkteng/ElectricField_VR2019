using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace KaiTool.Timeline
{
    public struct PlayableBehaviourEventArgs
    {
        public Playable m_playable;
        public FrameData m_info;
    }

    public class KaiTool_PlayableBehaviour : PlayableBehaviour
    {
        public event Action<System.Object, PlayableBehaviourEventArgs> m_graphStartEventHandle;
        public event Action<System.Object, PlayableBehaviourEventArgs> m_graphStopEventHandle;
        public event Action<System.Object, PlayableBehaviourEventArgs> m_behaviourPlayEventHandle;
        public event Action<System.Object, PlayableBehaviourEventArgs> m_behaviourPauseEventHandle;
        public event Action<System.Object, PlayableBehaviourEventArgs> m_prepareFrameEventHandle;




        // Called when the owning graph starts playing
        public override void OnGraphStart(Playable playable)
        {
            // Debug.Log("GraphStart");
            if (m_graphStartEventHandle != null)
            {
                var args = new PlayableBehaviourEventArgs();
                args.m_playable = playable;
                m_graphStartEventHandle(this, args);
            }
        }

        // Called when the owning graph stops playing
        public override void OnGraphStop(Playable playable)
        {
            // Debug.Log("GraphStop");
            if (m_graphStopEventHandle != null)
            {
                var args = new PlayableBehaviourEventArgs();
                args.m_playable = playable;
                m_graphStopEventHandle(this, args);
            }
        }

        // Called when the state of the playable is set to Play
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            // Debug.Log("BehaviourPlay");
            if (m_behaviourPlayEventHandle != null)
            {
                var args = new PlayableBehaviourEventArgs();
                args.m_playable = playable;
                args.m_info = info;
                m_behaviourPlayEventHandle(this, args);
            }
        }

        // Called when the state of the playable is set to Paused
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            //Debug.Log("BehaviourPause");
            if (m_behaviourPauseEventHandle != null)
            {
                var args = new PlayableBehaviourEventArgs();
                args.m_playable = playable;
                args.m_info = info;
                m_behaviourPauseEventHandle(this, args);
            }
        }

        // Called each frame while the state is set to Play
        public override void PrepareFrame(Playable playable, FrameData info)
        {
            if (m_prepareFrameEventHandle != null)
            {
                var args = new PlayableBehaviourEventArgs();
                args.m_playable = playable;
                args.m_info = info;
                m_prepareFrameEventHandle(this, args);
            }
        }
    }
}
