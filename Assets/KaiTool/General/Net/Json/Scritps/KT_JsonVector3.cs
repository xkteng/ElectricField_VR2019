using System.Text;
using UnityEngine;
namespace KaiTool.Json
{
    public class KT_JsonVector3
    {
        public float x;
        public float y;
        public float z;
        public KT_JsonVector3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
        public KT_JsonVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }
        public string ToJson()
        {
            var sb = new StringBuilder("{\"x\":").Append(x).Append(",\"y\":").Append(y).Append(",\"z\":").Append(z).Append("}");
            return sb.ToString();
        }
        public override string ToString()
        {
            return ToJson();
        }
    }
}