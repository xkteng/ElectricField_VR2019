using UnityEngine;
using KaiTool.Geometry;
namespace KaiTool.GizmosUtilities
{
    public class KaiTool_Gizmos
    {
        public static void DrawGizmos(KaiTool_BasicGraphy graphy, Color color)
        {
            graphy.DrawGizmos(color);
        }
        public static void DrawBox(Vector3 center, Vector3 size, Color color)
        {
            var box = new KaiTool_SimpleBox(center, size);
            box.DrawGizmos(color);
        }
        public static void DrawBox(Transform center, Vector3 size, Color color)
        {
            var box = new KaiTool_Box(center, size);
            box.DrawGizmos(color);
        }
        public static void DrawSphere(Transform center, float radius, Color color)
        {
            var sphere = new KaiTool_Sphere(center, radius);
            sphere.DrawGizmos(color);
        }
        public static void DrawCircle(Transform center, float radius, Color color)
        {
            var circle = new KaiTool_Circle(center, radius);
            circle.DrawGizmos(color);
        }
        public static void DrawCircle(Vector3 center, Vector3 normal, float radius, Color color)
        {
            var circle = new KaiTool_SimpleCircle(center, normal, radius);
            circle.DrawGizmos(color);

        }
        public static void DrawCylinder(Transform axisStart, Transform axisEnd, float radius, Color color)
        {
            var cylinder = new KaiTool_Cylinder(new KaiTool_Line(axisStart, axisEnd), radius);
            cylinder.DrawGizmos(color);
        }
        public static void DrawLine(Transform start, Transform end, Color color)
        {
            var line = new KaiTool_Line(start, end);
            line.DrawGizmos(color);
        }
        public static void DrawSegment(Transform start, Transform end, Color color)
        {
            var segment = new KaiTool_Segment(start, end);
            segment.DrawGizmos(color);
        }
        public static void DrawTrack(Transform[] wayPoints,float pointSize, Color color)
        {
            var track = new KaiTool_Track(wayPoints,pointSize);
            track.DrawGizmos(color);
        }

    }
}