using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaiTool.Geometry;
namespace KaiTool.LightGenerator
{
    public class KaiTool_LightGenerator : MonoBehaviour
    {
        [Header("LightGenerator")]
        [SerializeField]
        public GameObject m_light;
        [SerializeField]
        private Transform[] m_lightAnchors;
        [SerializeField]
        private Vector3 m_relativePosition = Vector3.zero;
        [SerializeField]
        private Vector3 m_relativeEuler = Vector3.zero;

        [HideInInspector]
        [SerializeField]
        private GameObject[] m_objectsGenerated;

        [Header("Gizmos")]
        [SerializeField]
        [Range(0, 5f)]
        private float m_gizmosSize = 1f;

        [ContextMenu("Refresh")]
        public void GenerateLights()
        {
            DestroyAllObjectsGenerated();
            int length = 0;
            if (m_lightAnchors != null)
            {
                length = m_lightAnchors.Length;
            }
            m_objectsGenerated = new GameObject[length];
            for (int i = 0; i < length; i++)
            {
                m_objectsGenerated[i] = Instantiate(m_light, m_lightAnchors[i]);
                m_objectsGenerated[i].transform.position = m_lightAnchors[i].position + m_relativePosition;
                m_objectsGenerated[i].transform.rotation = Quaternion.Euler(m_relativeEuler) * m_lightAnchors[i].rotation;
                m_objectsGenerated[i].SetActive(true);
            }
        }
        private void DestroyAllObjectsGenerated()
        {
            if (m_objectsGenerated != null)
            {
                for (int i = 0; i < m_objectsGenerated.Length; i++)
                {
                    DestroyImmediate(m_objectsGenerated[i]);
                }
            }
        }
        private void OnDrawGizmos()
        {
            if (m_light != null)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < m_lightAnchors.Length; i++)
                {
                    Gizmos.DrawWireCube(m_lightAnchors[i].transform.position, Vector3.one * m_gizmosSize * 0.3f);
                }
                Gizmos.color = Color.red;
                for (int i = 0; i < m_lightAnchors.Length; i++)
                {
                    var points = new Vector3[4];
                    points[0] = m_lightAnchors[i].transform.position;
                    points[1] = points[0] + m_relativePosition.y * Vector3.up;
                    points[2] = points[1] + m_relativePosition.x * Vector3.right;
                    points[3] = points[2] + m_relativePosition.z * Vector3.forward;
                    for (int j = 0; j < points.Length - 1; j++)
                    {
                        Gizmos.DrawLine(points[j], points[j + 1]);
                    }
                    Gizmos.DrawSphere(m_lightAnchors[i].position + m_relativePosition, 0.05f * m_gizmosSize);
                }
                Gizmos.color = Color.yellow;
                var light = m_light.GetComponent<Light>();
                switch (light.type)
                {
                    case LightType.Area:
                        var areaSize = Vector2.one;//light.areaSize;
                        for (int i = 0; i < m_lightAnchors.Length; i++)
                        {
                            var forward = Quaternion.Euler(m_relativeEuler) * m_lightAnchors[i].transform.forward;
                            var up = Quaternion.Euler(m_relativeEuler) * m_lightAnchors[i].up;
                            var right = Quaternion.Euler(m_relativeEuler) * m_lightAnchors[i].right;
                            var center = m_lightAnchors[i].position + m_relativePosition;

                            var points = new Vector3[4];
                            points[0] = center - right * areaSize.x / 2f + up * areaSize.y / 2f;
                            points[1] = center + right * areaSize.x / 2f + up * areaSize.y / 2f;
                            points[2] = center + right * areaSize.x / 2f - up * areaSize.y / 2f;
                            points[3] = center - right * areaSize.x / 2f - up * areaSize.y / 2f;
                            for (int j = 0; j < points.Length; j++)
                            {
                                Gizmos.DrawLine(points[j], points[(j + 1) % 4]);
                            }
                            Gizmos.DrawLine(center, center + forward * m_gizmosSize);
                        }
                        break;
                    case LightType.Directional:
                        for (int i = 0; i < m_lightAnchors.Length; i++)
                        {
                            var forward = Quaternion.Euler(m_relativeEuler) * m_lightAnchors[i].transform.forward;
                            var up = KaiTool_BasicGraphy.GetPerpendicularVector(forward);
                            var right = Vector3.Cross(up, forward).normalized;
                            var center = m_lightAnchors[i].position + m_relativePosition;
                            var tempRadius = 1f * m_gizmosSize;
                            //Gizmos.DrawLine(center, center + 5f * forward * m_gizmosSize);
                            var step = 16;
                            for (int j = 0; j < step; j++)
                            {
                                var start = center + Quaternion.AngleAxis(360f / step * j, forward) * up * tempRadius;
                                var end = start + 5f * forward * m_gizmosSize;
                                Gizmos.DrawLine(start, end);
                            }
                            var circle = new KaiTool_SimpleCircle(center, forward, tempRadius);
                            circle.DrawGizmos(Color.yellow);
                        }
                        break;
                    case LightType.Point:
                        for (int i = 0; i < m_lightAnchors.Length; i++)
                        {
                            var redSphereCenter = m_lightAnchors[i].position + m_relativePosition;
                            Gizmos.DrawWireSphere(redSphereCenter, light.range);
                        }
                        break;
                    case LightType.Spot:
                        var halfSpotAngle = light.spotAngle / 2f;
                        var range = light.range;
                        var coneLength = range * Mathf.Cos(halfSpotAngle / 180 * Mathf.PI);
                        var radius = range * Mathf.Sin(halfSpotAngle / 180 * Mathf.PI);
                        for (int i = 0; i < m_lightAnchors.Length; i++)
                        {
                            var forward = Quaternion.Euler(m_relativeEuler) * m_lightAnchors[i].transform.forward;
                            var up = KaiTool_BasicGraphy.GetPerpendicularVector(forward);
                            var right = Vector3.Cross(up, forward).normalized;
                            var center = m_lightAnchors[i].position + m_relativePosition;

                            var points = new Vector3[4];
                            points[0] = center + Quaternion.AngleAxis(-halfSpotAngle, right) * forward * range;
                            points[1] = center + Quaternion.AngleAxis(halfSpotAngle, up) * forward * range;
                            points[2] = center + Quaternion.AngleAxis(halfSpotAngle, right) * forward * range;
                            points[3] = center + Quaternion.AngleAxis(-halfSpotAngle, up) * forward * range;
                            for (int j = 0; j < points.Length; j++)
                            {
                                Gizmos.DrawLine(center, points[j]);
                            }
                            var circle = new KaiTool_SimpleCircle(center + forward * coneLength, forward, (points[2] - points[0]).magnitude / 2f);
                            circle.DrawGizmos(Color.yellow);
                        }
                        break;
                }
            }
        }
        public void Reset()
        {
            m_gizmosSize = 1f;
            m_light = null;
            m_lightAnchors = null;
            m_relativePosition = Vector3.zero;
            m_relativeEuler = Vector3.zero;
            DestroyAllObjectsGenerated();

        }
    }
}