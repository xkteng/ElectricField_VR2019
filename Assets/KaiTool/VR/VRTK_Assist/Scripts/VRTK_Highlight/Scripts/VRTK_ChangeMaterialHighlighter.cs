using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.VR.VRTK_Assistant
{

    public sealed class VRTK_ChangeMaterialHighlighter : VRTK_BasicInteractableObjectHighlighter
    {
        [Header("ChangeMaterial")]
        [SerializeField]
        private Material m_HighlightMaterial;
        private GameObject m_highlightCopy;
        protected override void Init()
        {
            base.Init();
            InitVar();
        }
        private void InitVar()
        {
            CreateHighlightCopy();
        }
        protected override void OnHighlighted(Object sender, HighlighterEventArgs e)
        {
            base.OnHighlighted(sender, e);
            m_highlightCopy.SetActive(true);
        }
        protected override void OnUnhighlighted(Object sender, HighlighterEventArgs e)
        {
            base.OnUnhighlighted(sender, e);
            m_highlightCopy.SetActive(false);
        }
        private void CreateHighlightCopy()
        {
            m_highlightCopy = Instantiate(this.gameObject, transform.position, transform.rotation);
            m_highlightCopy.SetActive(false);
            m_highlightCopy.transform.SetParent(transform);
            m_highlightCopy.transform.localScale = Vector3.one * 1.02f;
            foreach (var item in m_highlightCopy.GetComponentsInChildren<MonoBehaviour>())
            {
                // DestroyImmediate(item);
                item.enabled = false;
            }
            foreach (var item in m_highlightCopy.GetComponentsInChildren<Collider>())
            {
                DestroyImmediate(item);

            }
            foreach (var item in m_highlightCopy.GetComponentsInChildren<Rigidbody>())
            {
                // DestroyImmediate(item);
                item.isKinematic = true;
            }
            if (m_HighlightMaterial != null)
            {
                var meshRenderers = m_highlightCopy.GetComponentsInChildren<MeshRenderer>();
                for (int i = 0; i < meshRenderers.Length; i++)
                {
                    meshRenderers[i].material = m_HighlightMaterial;
                }
            }
        }
    }
}