using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.VR
{
    public sealed class KaiTool_GazedPoint_LoadingTipAttach : MonoBehaviour
    {
        private const string LOADINGTIP_PATH = "UI/Loading_Tip";
        private const float SIZE_FACTOR = 0.01f;
        private KaiTool_BasicGazedPoint m_gazed;

        private GameObject m_loadingTip = null;
        private void Awake()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            m_gazed = GetComponent<KaiTool_BasicGazedPoint>();
        }
        private void InitEvent()
        {
            m_gazed.m_gazerStay.AddListener((gazed) =>
            {
                if (m_gazed.IsValid)
                {
                    CreateLoadingTip();
                }
            });
            m_gazed.m_gazerExit.AddListener((gazed) =>
            {
                DestroyLoadingTip();
            });
            m_gazed.m_triggered.AddListener((gazed) =>
            {
                DestroyLoadingTip();
            });
        }

        private void CreateLoadingTip()
        {
            if (m_loadingTip == null)
            {
                var temp = Resources.Load<GameObject>(LOADINGTIP_PATH);
                if (temp == null)
                {
                    throw new Exception("The resources at the path is null:");
                }
                m_loadingTip = Instantiate(temp, transform.position, Quaternion.identity, transform.parent);
                var factor = SIZE_FACTOR / m_loadingTip.transform.lossyScale.x;
                m_loadingTip.transform.localScale = m_loadingTip.transform.localScale * factor;
            }
        }
        private void DestroyLoadingTip()
        {
            if (m_loadingTip)
            {
                DestroyImmediate(m_loadingTip);
            }
        }
    }
}