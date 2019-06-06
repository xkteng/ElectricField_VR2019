using UnityEngine;
namespace KaiTool.VR.VRTK_Assistant
{
    public class VRTK_TranslateObjectKaiKnob : VRTK_GenerializedKaiKnob
    {
        [Header("TranslateObjectFields", order = 3)]
        [SerializeField]
        protected Transform target;
        [SerializeField]
        protected EnumDirectionType m_translateDirection=EnumDirectionType.Z;
        [SerializeField]
        protected float m_translateDistanceEachRound = 1f;
        protected float m_currentTranslateValue=0f;
        protected float m_previousTranslateValue=0f;
        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }
        private void InitVar() {
        }
        private void InitEvent() {
            m_valueChanged += (sender,e) => {
                m_currentTranslateValue = ((KaiKnobEventArgs)e).m_currentValue;
                TranslateTargetByValue(m_currentTranslateValue,m_previousTranslateValue);
                m_previousTranslateValue = m_currentTranslateValue;
            };
        }
        private void TranslateTargetByValue(float currentValue,float previousValue) {
            switch (m_translateDirection) {
                case EnumDirectionType.X:
                    target.transform.position = target.transform.position + target.transform.right * (currentValue-previousValue)* m_translateDistanceEachRound/ 360f;
                    break;
                case EnumDirectionType.Y:
                    target.transform.position = target.transform.position + target.transform.up * (currentValue-previousValue) * m_translateDistanceEachRound / 360f;
                    break;
                case EnumDirectionType.Z:
                    target.transform.position = target.transform.position + target.transform.forward * (currentValue-previousValue) * m_translateDistanceEachRound / 360f;
                    break;
                default:
                    break;
            }
        }
    }
}