using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.VR.VRTK_Assistant
{
    public class VRTK_RotateObjectKaiKnob : VRTK_GenerializedKaiKnob
    {

        [Header("RotateObjectFields", order = 3)]
        [SerializeField]
        protected Transform target;
        [SerializeField]
        protected EnumDirectionType m_rotateDirection = EnumDirectionType.Z;
        [SerializeField]
        protected float m_rotateDegreeEachRound = 360f;
        protected float m_currentRotateValue = 0f;
        protected float m_previousRotateValue = 0f;

        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }
        private void InitVar() { }
        private void InitEvent()
        {
            m_valueChanged += (sender, e) =>
            {
                m_currentRotateValue = ((KaiKnobEventArgs)e).m_currentValue;
                RotateTargetByValue(m_currentRotateValue, m_previousRotateValue);
                m_previousRotateValue = m_currentRotateValue;
            };
        }
        private void RotateTargetByValue(float currentValue, float previousValue)
        {
            switch (m_rotateDirection)
            {
                case EnumDirectionType.X:
                    target.transform.localRotation = Quaternion.Euler((currentValue - previousValue) * m_rotateDegreeEachRound / 360f, 0, 0) * target.transform.localRotation;
                    break;
                case EnumDirectionType.negative_X:
                    target.transform.localRotation = Quaternion.Euler(-(currentValue - previousValue) * m_rotateDegreeEachRound / 360f, 0, 0) * target.transform.localRotation;
                    break;
                case EnumDirectionType.Y:
                    target.transform.localRotation = Quaternion.Euler(0, (currentValue - previousValue) * m_rotateDegreeEachRound / 360f, 0) * target.transform.localRotation;
                    break;
                case EnumDirectionType.negative_Y:
                    target.transform.localRotation = Quaternion.Euler(0, -(currentValue - previousValue) * m_rotateDegreeEachRound / 360f, 0) * target.transform.localRotation;
                    break;
                case EnumDirectionType.Z:
                    target.transform.localRotation = target.transform.localRotation* Quaternion.Euler(0, 0, (currentValue - previousValue) * m_rotateDegreeEachRound / 360f);
                    break;
                case EnumDirectionType.negative_Z:
                    target.transform.localRotation = target.transform.localRotation * Quaternion.Euler(0, 0,-(currentValue - previousValue) * m_rotateDegreeEachRound / 360f);
                    break;
                default:
                    break;
            }
        }

    }
}