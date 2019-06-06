using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace KaiTool.Timeline
{

    public class KaiTool_PlayableAsset : PlayableAsset
    {
        [SerializeField]
        private ExposedReference<KaiTool_Playable_EventListener> m_eventListener;
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<KaiTool_PlayableBehaviour>.Create(graph);
            var listener = m_eventListener.Resolve(graph.GetResolver());
            playable.GetBehaviour().m_behaviourPlayEventHandle += (sender, e) =>
            {

                if (listener)
                {
                    var args = new Listener_EventArgs();
                    args.m_playable = e.m_playable;
                    args.m_info = e.m_info;
                    listener.OnBehaviourPlay(args);
                }
            };
            playable.GetBehaviour().m_behaviourPauseEventHandle += (sender, e) =>
            {
                // var listener = m_eventListener.Resolve(graph.GetResolver());
                if (listener != null)
                {
                    var args = new Listener_EventArgs();
                    args.m_playable = e.m_playable;
                    args.m_info = e.m_info;
                    listener.OnBehaviourPause(args);
                }
            };
            playable.GetBehaviour().m_graphStartEventHandle += (sender, e) =>
            {
                // var listener = m_eventListener.Resolve(graph.GetResolver());
                if (listener)
                {
                    var args = new Listener_EventArgs();
                    args.m_playable = e.m_playable;
                    args.m_info = e.m_info;
                    listener.OnGraphStart(args);
                }
            };
            playable.GetBehaviour().m_graphStopEventHandle += (sender, e) =>
            {
                //var listener = m_eventListener.Resolve(graph.GetResolver());
                if (listener)
                {
                    var args = new Listener_EventArgs();
                    args.m_playable = e.m_playable;
                    args.m_info = e.m_info;
                    listener.OnGraphStop(args);
                }
            };
            playable.GetBehaviour().m_prepareFrameEventHandle += (sender, e) =>
            {
                // var listener = m_eventListener.Resolve(graph.GetResolver());
                if (listener)
                {
                    var args = new Listener_EventArgs();
                    args.m_playable = e.m_playable;
                    args.m_info = e.m_info;
                    listener.OnPrepareFrame(args);
                }
            };
            return playable;
        }
    }
}