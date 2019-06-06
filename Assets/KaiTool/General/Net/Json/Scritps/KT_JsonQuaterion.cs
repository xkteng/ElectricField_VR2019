using System.Text;
using UnityEngine;
namespace KaiTool.Json
{
    public class KT_JsonQuaterion
    {
        public float x;
        public float y;
        public float z;
        public float w;
        public KT_JsonQuaterion(float _x, float _y, float _z, float _w)
        {
            x = _x;
            y = _y;
            z = _z;
            w = _w;
        }
        public KT_JsonQuaterion(Quaternion quaternion)
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
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