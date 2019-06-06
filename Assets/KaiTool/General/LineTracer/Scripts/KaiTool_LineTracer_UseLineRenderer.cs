using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.LineTracer
{

    public class KaiTool_LineTracer_UseLineRenderer : KaiTool_BasicLineTracer
    {
        public override void OnCreateLine()
        {
            base.OnCreateLine();
            m_objectCreated = new GameObject("LineRenderers");
            m_objectCreated.transform.SetParent(transform);
            for (int i = 0; i < m_links.Length; i++)
            {
                var obj = new GameObject("Link_Rendered(" + i + ")", typeof(LineRenderer));
                obj.transform.SetParent(m_objectCreated.transform);
                var lineRender = obj.GetComponent<LineRenderer>();
                lineRender.startColor = m_color;
                lineRender.endColor = m_color;
                lineRender.startWidth = m_width;
                lineRender.endWidth = m_width;
                lineRender.material = m_material;
                lineRender.positionCount = m_links[i].PointsCount;
                lineRender.SetPositions(m_links[i].GetPositions());
            }
        }

        public override void OnDestroyLine()
        {
            base.OnDestroyLine();
            if (m_objectCreated != null)
            {
                Destroy(m_objectCreated);
            }
        }

        public override void OnUpdateLine()
        {
            base.OnUpdateLine();
        }

    }
}