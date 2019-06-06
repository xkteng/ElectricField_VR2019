using DG.Tweening;
using KaiTool.ToolTip;
using KaiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.VR
{
    public sealed class KaiTool_GazedPoint_ToolTipAttach : MonoBehaviour
    {
        [SerializeField]
        private string m_text = "Default";
        [SerializeField]
        private int m_frontSize = 50;
        //[SerializeField]
        //private float m_remainTime = 2f;

        private KaiTool_BasicGazedPoint m_gazed;
        private KaiTool_ToolTip_Generator m_toolTipGenerator;

        public void SetArgs(string text, int frontSize)
        {
            m_text = text;
            m_frontSize = frontSize;
        }
        private void Awake()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            m_gazed = transform.GetComponent<KaiTool_BasicGazedPoint>();
            m_toolTipGenerator = transform.GetSurvivalType<KaiTool_ToolTip_Generator>();
            m_toolTipGenerator.SetText(m_text);
            m_toolTipGenerator.SetFontSize(m_frontSize);
        }
        private void InitEvent()
        {
            m_gazed.m_triggered.AddListener((gazer) =>
            {
                m_toolTipGenerator.CreateToolTip();
            });
            m_gazed.m_gazerExit.AddListener((gazer) =>
            {
                var x = 0;
                m_toolTipGenerator.DestroyToolTip();
            });
        }

    }
}