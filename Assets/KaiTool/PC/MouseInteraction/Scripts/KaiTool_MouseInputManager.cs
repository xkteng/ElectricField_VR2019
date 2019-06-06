//#define Debug
using KaiTool.Utilities;
using System;
using UnityEngine;
namespace KaiTool.PC.MouseInteraction
{

    public interface IMouseInteractiveObject
    {
        Action<UnityEngine.Object, MouseInteractEventArgs> HoveredIn { get; set; }
        Action<UnityEngine.Object, MouseInteractEventArgs> HoveredOver { get; set; }
        Action<UnityEngine.Object, MouseInteractEventArgs> HoveredOut { get; set; }
        GameObject gameObject { get; }
        bool IsHovered { get; }
    }
    public interface IRightButtonInteractiveObject
    {
        Action<UnityEngine.Object, MouseInteractEventArgs> RightButtonPressed { get; set; }
        Action<UnityEngine.Object, MouseInteractEventArgs> RightButtonReleased { get; set; }
        float PreviousAimedTime
        {
            get;
        }
        GameObject gameObject { get; }
        bool IsRightButtonSelected { get; }
    }
    public interface ILeftButtonInteractiveObject
    {
        float PreviousSelectedTime { get; }
        Action<UnityEngine.Object, MouseInteractEventArgs> Dragged { get; set; }
        Action<UnityEngine.Object, MouseInteractEventArgs> LeftButtonPressed { get; set; }
        Action<UnityEngine.Object, MouseInteractEventArgs> LeftButtonReleased { get; set; }
        GameObject gameObject { get; }
        float DraggingSpeed { get; }
        float DraggingDistance { get; }
        Vector3 DraggingVector { get; }
        bool IsLeftButtonSelected { get; }
        bool IsDragged { get; }
    }
    public struct MouseInteractEventArgs
    {
        public float m_time;
        public MouseInteractEventArgs(float time)
        {
            m_time = time;
        }
    }
    public struct MouseInputManagerEventArgs
    {
        float m_time;
        public MouseInputManagerEventArgs(float time)
        {
            m_time = time;
        }
    }
    public sealed class KaiTool_MouseInputManager : Singleton<KaiTool_MouseInputManager>
    {
        [Header("MouseInputManager")]
        [SerializeField]
        private GameObject m_objectHovered;
        [SerializeField]
        private GameObject m_objectSelected;
        [SerializeField]
        private GameObject m_objectAimed;
        [SerializeField]
        private float m_doubleClickedTime = 0.3f;
        public Action<IMouseInteractiveObject, MouseInputManagerEventArgs> m_hoveringInObject;
        public Action<IMouseInteractiveObject, MouseInputManagerEventArgs> m_hoveringOverObject;
        public Action<IMouseInteractiveObject, MouseInputManagerEventArgs> m_hoveringOutObject;
        public Action<ILeftButtonInteractiveObject, MouseInputManagerEventArgs> m_selectingObject;
        public Action<ILeftButtonInteractiveObject, MouseInputManagerEventArgs> m_unselectingObject;

        public Action<IRightButtonInteractiveObject, MouseInputManagerEventArgs> m_aimingObject;
        public Action<IRightButtonInteractiveObject, MouseInputManagerEventArgs> m_unaimingObject;

        protected override void Awake()
        {
            base.Awake();
            InitEvent();
        }
        private void InitEvent()
        {
#if Debug
            m_hoveringInObject += (sender, e) =>
            {
                print("HoveringIn!");
            };
            m_hoveringOverObject += (sender, e) =>
            {
               // print("HoveringOver!");
            };
            m_hoveringOutObject += (sender, e) =>
            {
                print("HoveringOut!");
            };

            m_selectingObject += (sender, e) =>
            {
                print("SelectingObject");
            };

            m_unselectingObject += (sender, e) =>
            {
                print("UnselectingObject");
            };
            m_aimingObject+=(sender,e)=>{
                print("AimingObject");
            };
            m_unaimingObject += (sender, e) =>
            {
                print("UnaimingObject");
            };
#endif
        }
        public void OnHoveringInObject(IMouseInteractiveObject sender, MouseInputManagerEventArgs e)
        {
            m_objectHovered = sender.gameObject;
            if (m_hoveringInObject != null)
            {
                m_hoveringInObject(sender, e);
            }
        }
        public void OnHoveringOverObject(IMouseInteractiveObject sender, MouseInputManagerEventArgs e)
        {
            if (m_hoveringOverObject != null)
            {
                m_hoveringOverObject(sender, e);
            }
        }
        public void OnHoveringOutObject(IMouseInteractiveObject sender, MouseInputManagerEventArgs e)
        {
            m_objectHovered = null;
            if (m_hoveringOutObject != null)
            {
                m_hoveringOutObject(sender, e);
            }
        }
        public void OnSelectingObject(ILeftButtonInteractiveObject sender, MouseInputManagerEventArgs e)
        {
            m_objectSelected = sender.gameObject;
            if (m_selectingObject != null)
            {
                m_selectingObject(sender, e);
            }
        }
        public void OnUnselectingObject(ILeftButtonInteractiveObject sender, MouseInputManagerEventArgs e)
        {
            m_objectSelected = null;
            if (m_unselectingObject != null)
            {
                m_unselectingObject(sender, e);
            }
        }
        public void OnAimingObject(IRightButtonInteractiveObject sender, MouseInputManagerEventArgs e)
        {
            m_objectAimed = sender.gameObject;
            if (m_aimingObject != null)
            {
                m_aimingObject(sender, e);
            }
        }
        public void OnUnaimingObject(IRightButtonInteractiveObject sender, MouseInputManagerEventArgs e)
        {
            m_objectAimed = null;
            if (m_unaimingObject != null)
            {
                m_unaimingObject(sender, e);
            }
        }

        public GameObject HoveringObject
        {
            get
            {
                return m_objectHovered;
            }
        }
        public GameObject ObjectSelected
        {
            get
            {
                return m_objectSelected;
            }
        }
        public GameObject ObjectAimed
        {
            get
            {
                return m_objectAimed;
            }
        }
        public float DoubleClickedTime
        {
            get
            {
                return m_doubleClickedTime;
            }
        }
    }
}
