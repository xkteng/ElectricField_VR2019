using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KaiTool.UI
{
    public enum SliderCueType
    {
        Continuous,
        Discrete
    }
    public class KaiTool_SliderCue : MonoBehaviour
    {
        [SerializeField]
        private Slider m_targetSlider;
        [SerializeField]
        private Text m_text;
        [SerializeField]
        private bool m_isInverse = false;
        [SerializeField]
        private SliderCueType m_cueType = SliderCueType.Continuous;
        [SerializeField]
        private float m_segement = 0.1f;

        private void Awake()
        {
            Init();
            SetText(m_targetSlider.value);
        }

        private void Init()
        {
            InitVar();
            InitEvent();
        }

        private void InitVar() { }
        private void InitEvent()
        {
            m_targetSlider.onValueChanged.AddListener((value) =>
            {
                SetText(value);
            });
        }

        private void SetText(float value)
        {
            value = Mathf.Clamp01(value);
            switch (m_cueType)
            {
                case SliderCueType.Continuous:
                    if (m_isInverse)
                    {
                        m_text.text = Mathf.CeilToInt((1 - value) * 100).ToString() + "%";
                    }
                    else
                    {
                        m_text.text = Mathf.CeilToInt((value) * 100).ToString() + "%";
                    }
                    break;
                case SliderCueType.Discrete:
                    if (m_isInverse)
                    {
                        m_text.text = Mathf.CeilToInt((1 - value) / m_segement).ToString();
                    }
                    else
                    {
                        m_text.text = Mathf.CeilToInt(value / m_segement).ToString();
                    }
                    break;
            }
        }

    }
}