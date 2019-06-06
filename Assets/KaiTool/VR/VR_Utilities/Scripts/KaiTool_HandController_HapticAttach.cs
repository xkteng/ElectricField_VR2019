using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace KaiTool.VR.VRUtilities
{
    public class KaiTool_HandController_HapticAttach : MonoBehaviour
    {

        [SerializeField]
        [Range(0, 1f)]
        private float m_strength = 0.5f;
        [Range(0, 0.5f)]
        [SerializeField]
        private float m_duration = 0.1f;
        [SerializeField]
        [Range(0, 0.1f)]
        private float m_pulseInterval = 0.05f;
        private KaiTool_VRPlayerRig m_VRplayRig;
        private VRTK_InteractTouch m_interTouch;
        private VRTK_InteractGrab m_interGrab;
        private VRTK_ControllerReference m_controllerReference;

        private bool IsHaptic
        {
            get
            {
                return m_VRplayRig.IsHaptic;
            }
        }
        protected virtual void Awake()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            m_VRplayRig = GetComponentInParent<KaiTool_VRPlayerRig>();
            m_interTouch = GetComponent<VRTK_InteractTouch>();
            m_interGrab = GetComponent<VRTK_InteractGrab>();
            m_controllerReference = VRTK_ControllerReference.GetControllerReference(gameObject);
        }
        private void InitEvent()
        {
            m_interTouch.ControllerTouchInteractableObject += (sender, e) =>
            {
                if (IsHaptic)
                {
                    var controllerReference = VRTK_ControllerReference.GetControllerReference(gameObject);
                    VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, m_strength, m_duration, m_pulseInterval);
                }
            };
        }
    }
}