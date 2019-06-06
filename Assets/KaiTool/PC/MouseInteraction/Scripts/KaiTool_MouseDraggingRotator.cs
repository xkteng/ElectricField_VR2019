using UnityEngine;

namespace KaiTool.PC.MouseInteraction
{
    public enum Direction
    {
        X, Y, Z
    }
    [RequireComponent(typeof(KaiTool_MouseInteractiveObject))]
    public class KaiTool_MouseDraggingRotator : MonoBehaviour
    {
        [Header("DraggingRotator")]
        [SerializeField]
        private float m_rotateCoefficient = 1f;
        private KaiTool_MouseInteractiveObject m_mouseInteractiveObject;
        protected virtual void Init()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            m_mouseInteractiveObject = GetComponent<KaiTool_MouseInteractiveObject>();
        }
        private void InitEvent()
        {
            m_mouseInteractiveObject.Dragged += (sender, e) =>
            {
                var cameraForward = Camera.main.transform.forward;

                var axis = Vector3.Cross(m_mouseInteractiveObject.DraggingVelocity, cameraForward);
                var quaterion = Quaternion.AngleAxis(m_mouseInteractiveObject.DraggingSpeed * m_rotateCoefficient, axis);
                transform.rotation = quaterion * transform.rotation;
            };
        }

    }
}