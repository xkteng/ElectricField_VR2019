using System.Text;
using UnityEngine;

namespace KaiTool.Json
{
    public class KT_JsonVector2 
    {
        public float x;
        public float y;
        public KT_JsonVector2(float _x, float _y)
        {
            x = _x;
            y = _y;
        }
        public KT_JsonVector2(Vector2 vector)
        {
            x = vector.x;
            y = vector.y;
        }
        public string ToJson()
        {
            var sb = new StringBuilder("{\"x\":").Append(x).Append(",\"y\":").Append(y).Append("}");
            return sb.ToString();
        }
        public override string ToString()
        {
            return ToJson();
        }
    }
}