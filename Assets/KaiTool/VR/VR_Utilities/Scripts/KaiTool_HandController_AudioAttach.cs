using KaiTool.Audio;
using UnityEngine;
using VRTK;

namespace KaiTool.VR.VRUtilities
{
    public class KaiTool_HandController_AudioAttach : MonoBehaviour
    {
        [Header("Interact")]
        [SerializeField]
        private KaiTool_AudioPlayer m_touch_Audio;
        [SerializeField]
        private KaiTool_AudioPlayer m_grab_Audio;
        [Header("Teleport")]
        [SerializeField]
        private AudioSource m_teleport_Start;
        [SerializeField]
        private AudioSource m_teleport_Loop;
        [SerializeField]
        private AudioSource m_teleport_Go;
        [SerializeField]
        private AudioSource m_teleport_GoodPlace;
        [SerializeField]
        private AudioSource m_teleport_BadPlace;

        VRTK_Pointer m_pointer;
        VRTK_PlayAreaCursor m_playAreaCursor;
        VRTK_InteractTouch m_interTouch;
        VRTK_InteractGrab m_interGrab;

        private void Awake()
        {
            InitVar();
            InitEvent();
        }
        void InitVar()
        {
            m_pointer = GetComponent<VRTK_Pointer>();
            m_playAreaCursor = GetComponent<VRTK_PlayAreaCursor>();
            m_interTouch = GetComponent<VRTK_InteractTouch>();
            m_interGrab = GetComponent<VRTK_InteractGrab>();
        }
        void InitEvent()
        {
            #region INTERACTION
            m_interTouch.ControllerTouchInteractableObject += (sender, e) =>
            {
                if (m_touch_Audio)
                {
                    m_touch_Audio.PlayOneShot();
                }
            };
            m_interGrab.ControllerGrabInteractableObject += (sender, e) =>
            {
                if (m_grab_Audio)
                {
                    m_grab_Audio.PlayOneShot();
                }
            };
            #endregion
            #region TELEPORT
            m_pointer.ActivationButtonPressed += (sender, e) =>
            {
                if (!m_interGrab.GetGrabbedObject())
                {
                    m_teleport_Start.Play();
                    m_teleport_Loop.Play();
                }
            };
            m_pointer.ActivationButtonReleased += (sender, e) =>
            {
                m_teleport_Loop.Pause();
                if (m_pointer.IsStateValid())
                {
                    m_teleport_Go.Play();
                }
            };
            m_pointer.PointerStateValid += (sender, e) =>
            {
                m_teleport_GoodPlace.Play();
            };
            m_pointer.PointerStateInvalid += (sender, e) =>
            {
                m_teleport_BadPlace.Play();
            };
            #endregion
        }

    }

}
