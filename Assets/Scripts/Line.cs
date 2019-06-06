using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField]
    private LineRenderer m_lineRenderer;
   

    private void Start()
    {
        DrawCurve();
    }
    public void DrawLine(Vector3[] points)
    {
        m_lineRenderer.positionCount = points.Length;
        m_lineRenderer.SetPositions(points);
    }
    public void SetWidth(float width)
    {
        m_lineRenderer.startWidth = width;
        m_lineRenderer.endWidth = width;
    }
    public void DrawCurve()
    {
        var count = 100;
        var step = (2*Mathf.PI) / count;
        var points = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            points[i] = Vector3.zero + Vector3.right * i * step + Vector3.up * Mathf.Sin(i * step);
        }


        DrawLine(points);
        SetWidth(0.1f);
    }
}
