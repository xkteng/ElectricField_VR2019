using KaiTool.Utilities;
using System;
using UnityEngine;
using UnityEngine.Events;
using VRTK;
namespace KaiTool.VR.VRTK_Assistant
{
    public class VRTK_GenerializedKaiKnob : VRTK_BasicKaiKnob
    {
        [Header("GenerializedFields", order = 2)]

        [SerializeField]
        protected float m_hapticCoefficient = 1f;
        protected override void Init()
        {
            base.Init();
            InitEvent();
        }
        private void InitEvent()
        {
        }

        public override void OnValueChanged(UnityEngine.Object sender, KaiKnobEventArgs e)
        {
            base.OnValueChanged(sender, e);
            var controllerReference = VRTK_ControllerReference.GetControllerReference(m_interUse.gameObject);
            VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, Mathf.Abs(m_currentValue - m_previousValue) * m_hapticCoefficient);
        }


    }
}