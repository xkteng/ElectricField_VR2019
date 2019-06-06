using System.Text;
using UnityEngine;

namespace KaiTool.Json
{
    public class KT_JsonVector4
    {
        public float x;
        public float y;
        public float z;
        public float w;
        public KT_JsonVector4(float _x, float _y, float _z, float _w)
        {
            x = _x;
            y = _y;
            z = _z;
            w = _w;
        }
        public KT_JsonVector4(Vector4 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
            w = vector.w;
        }
        public string ToJson()
        {
            var sb = new StringBuilder("{\"x\":").Append(x).Append(",\"y\":").Append(y).Append(",\"z\":").Append(z).Append(",\"w\":").Append(w).Append("}");
            return sb.ToString();
        }
        public override string ToString()
        {
            return ToJson();
        }
    }
}