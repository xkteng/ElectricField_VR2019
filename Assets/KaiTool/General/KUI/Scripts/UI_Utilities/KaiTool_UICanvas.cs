using KaiTool.Geometry;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.UI
{
    [RequireComponent(typeof(KaiTool_BoundedUIObject))]
    [ExecuteInEditMode]
    public class KaiTool_UICanvas : MonoBehaviour
    {
        [Header("Coroutine")]
        [SerializeField]
        private float m_intervalTime = 0.02f;
        private Coroutine m_RelocateCoroutine;
        [SerializeField]
        [HideInInspector]
        private KaiTool_BoundedUIObject m_UIObject;

        private void Reset()
        {
            m_UIObject = GetComponent<KaiTool_BoundedUIObject>();
        }
        private void Start()
        {
            if (m_RelocateCoroutine != null)
            {
                StopCoroutine(m_RelocateCoroutine);
            }
            m_RelocateCoroutine = StartCoroutine(RenderEnumerator());
        }
        private IEnumerator RenderEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            while (true)
            {
               // m_UIObject.OnRendered(new BoundedUIObjectEventArgs());
                yield return wait;
            }
        }

    }
}