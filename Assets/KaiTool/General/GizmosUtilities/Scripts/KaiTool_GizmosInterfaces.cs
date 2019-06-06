using UnityEngine;

namespace KaiTool.GizmosUtilities
{
    public interface IComponentWithGizmos
    {
        bool IsDrawGizmos
        {
            get; set;
        }
        Color GizmosColor { get; set; }
        float GizmosSize { get; set; }
    }
}