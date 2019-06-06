using KaiTool.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Painting
{
    public struct PaintEventArgs
    {

    }
    public class KaiTool_PaintingManager : Singleton<KaiTool_PaintingManager>
    {
        [Header("PaintingFields", order = 1)]
        [SerializeField]
        protected Material m_paintingMaterial;
        [SerializeField]
        protected float m_paintRadius = 0.1f;
        [SerializeField]
        protected List<Vector4> m_pointList = new List<Vector4>();
        [SerializeField]
        protected float m_paintingCoeffcient = 1f;
        [SerializeField]
        protected float m_intervalTime = 0.03f;
        public Action<UnityEngine.Object, PaintEventArgs> m_painting;

        protected override void Awake()
        {
            base.Awake();
            InitVar();
            OnPainting(this, new PaintEventArgs());
        }
        private void InitVar()
        {
            m_pointList.Add(new Vector4(0, 0, 0, 0));
        }
        public Material PaintingMaterial
        {
            get
            {
                return m_paintingMaterial;
            }
        }
        private IEnumerator PaintingEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            while (true)
            {
                OnPainting(this, new PaintEventArgs());
                yield return wait;
            }
        }
        private void OnPainting(UnityEngine.Object sender, PaintEventArgs e)
        {
            UpdatePainting();
            if (m_painting != null)
            {
                m_painting(sender, e);
            }
        }
        private void UpdatePainting()
        {
            m_paintingMaterial.SetInt("_PointLength", m_pointList.Count);
            m_paintingMaterial.SetVectorArray("_Points", m_pointList.ToArray());
            m_paintingMaterial.SetFloat("_PaintingCoeffcient", m_paintingCoeffcient);
            m_paintingMaterial.SetFloat("_PaintRadius", m_paintRadius);
        }
    }
}