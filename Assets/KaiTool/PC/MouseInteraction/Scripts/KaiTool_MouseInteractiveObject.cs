using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace KaiTool.PC.MouseInteraction
{
    public class KaiTool_MouseInteractiveObject : MonoBehaviour, IMouseInteractiveObject, ILeftButtonInteractiveObject, IRightButtonInteractiveObject
    {
        [Header("MouseInteractiveObject", order = 1)]
        [SerializeField]
        protected bool m_isLeftButtonInteractive = true;
        [SerializeField]
        protected bool m_isRightButtonInteractive = true;
        [Header("Status", order = 2)]
        [SerializeField]
        private bool m_isHovered = false;
        [SerializeField]
        private bool m_isLeftButtonSelected = false;
        [SerializeField]
        private bool m_isRightButtonSelected = false;
        [SerializeField]
        private bool m_isDragged = false;

        protected float m_draggingSpeed = 0f;
        private Vector3 m_draggingVelocity = Vector3.zero;
        protected float m_draggingDistance = 0f;
        protected Vector3 m_draggingVector = Vector3.zero;

        protected float m_intervalTime = 0.03f;
        protected float m_previousSelectedTime = 0f;
        protected float m_previousAimedTime = 0f;

        protected float m_leftButtonDoubleClickTimer = 0f;
        protected float m_rightButtonDoubleClickTimer = 0f;
        protected float m_doubleClickWaitingTime;


        [SerializeField]
        private UnityEvent m_hoveredIn;
        [SerializeField]
        private UnityEvent m_hoveredOver;
        [SerializeField]
        private UnityEvent m_hoveredOut;
        [SerializeField]
        private UnityEvent m_leftButtonPressed;
        [SerializeField]
        private UnityEvent m_leftButtonReleased;
        [SerializeField]
        private UnityEvent m_leftButtonDoubleClicked;
        [SerializeField]
        private UnityEvent m_rightButtonPressed;
        [SerializeField]
        private UnityEvent m_rightButtonRelease;
        [SerializeField]
        private UnityEvent m_rightButtonDoubleClicked;
        [SerializeField]
        private UnityEvent m_dragged;

        protected Action<UnityEngine.Object, MouseInteractEventArgs> m_hoveredInEventHandle;
        protected Action<UnityEngine.Object, MouseInteractEventArgs> m_hoveredOverEventHandle;
        protected Action<UnityEngine.Object, MouseInteractEventArgs> m_hoveredOutEventHandle;
        protected Action<UnityEngine.Object, MouseInteractEventArgs> m_leftButtonPressedEventHandle;
        protected Action<UnityEngine.Object, MouseInteractEventArgs> m_leftButtonReleasedEventHandle;
        protected Action<UnityEngine.Object, MouseInteractEventArgs> m_leftButtonDoubleClickedEventHandle;

        protected Action<UnityEngine.Object, MouseInteractEventArgs> m_rightButtonPressedEventHandle;
        protected Action<UnityEngine.Object, MouseInteractEventArgs> m_rightButtonReleasedEventHandle;
        protected Action<UnityEngine.Object, MouseInteractEventArgs> m_rightButtonDoubleClickedEventHandle;

        protected Action<UnityEngine.Object, MouseInteractEventArgs> m_draggedEventHandle;

        private Coroutine m_dragCoroutine;



        #region PUBLIC PROPERTIES
        public Action<UnityEngine.Object, MouseInteractEventArgs> HoveredIn
        {
            get
            {
                return m_hoveredInEventHandle;
            }

            set
            {
                m_hoveredInEventHandle = value;
            }
        }
        public Action<UnityEngine.Object, MouseInteractEventArgs> HoveredOver
        {
            get
            {
                return m_hoveredOutEventHandle;
            }
            set
            {
                m_hoveredOutEventHandle = value;
            }
        }
        public Action<UnityEngine.Object, MouseInteractEventArgs> HoveredOut
        {
            get { return m_hoveredOutEventHandle; }
            set { m_hoveredOutEventHandle = value; }
        }
        public Action<UnityEngine.Object, MouseInteractEventArgs> LeftButtonPressed
        {
            get { return m_leftButtonPressedEventHandle; }
            set { m_leftButtonPressedEventHandle = value; }
        }
        public Action<UnityEngine.Object, MouseInteractEventArgs> LeftButtonReleased
        {
            get { return m_leftButtonReleasedEventHandle; }
            set { m_leftButtonReleasedEventHandle = value; }
        }
        public Action<UnityEngine.Object, MouseInteractEventArgs> LeftButtonDoubleClicked
        {
            get
            {
                return m_leftButtonDoubleClickedEventHandle;
            }

            set
            {
                m_leftButtonDoubleClickedEventHandle = value;
            }
        }
        public Action<UnityEngine.Object, MouseInteractEventArgs> RightButtonPressed
        {
            get { return m_rightButtonPressedEventHandle; }
            set { m_rightButtonPressedEventHandle = value; }
        }
        public Action<UnityEngine.Object, MouseInteractEventArgs> RightButtonReleased
        {
            get { return m_rightButtonReleasedEventHandle; }
            set { m_rightButtonReleasedEventHandle = value; }
        }
        public Action<UnityEngine.Object, MouseInteractEventArgs> RightButtonDoubleClicked
        {
            get
            {
                return m_rightButtonDoubleClickedEventHandle;
            }

            set
            {
                m_rightButtonDoubleClickedEventHandle = value;
            }
        }
        public float PreviousAimedTime
        {
            get
            {
                return m_previousAimedTime;
            }
        }
        public float PreviousSelectedTime
        {
            get
            {
                return m_previousSelectedTime;
            }
        }
        public Action<UnityEngine.Object, MouseInteractEventArgs> Dragged
        {
            get
            {
                return m_draggedEventHandle;
            }

            set
            {
                m_draggedEventHandle = value;
            }
        }
        public bool IsHovered
        {
            get { return m_isHovered; }
        }
        public bool IsLeftButtonSelected
        {
            get { return m_isLeftButtonSelected; }
        }
        public bool IsRightButtonSelected
        {
            get { return m_isRightButtonSelected; }
        }
        public bool IsDragged
        {
            get
            {
                return m_isDragged;
            }
        }
        public float DraggingSpeed
        {
            get
            {
                return m_draggingSpeed;
            }
        }
        public float DraggingDistance
        {
            get
            {
                return m_draggingDistance;
            }
        }
        public Vector3 DraggingVector
        {
            get
            {
                return m_draggingVector;
            }
        }
        public Vector3 DraggingVelocity
        {
            get
            {
                return m_draggingVelocity;
            }
        }
        #endregion

        #region PROTECTED METHOD
        protected virtual void OnHoveredIn(MouseInteractEventArgs e)
        {
            KaiTool_MouseInputManager.Instance.OnHoveringInObject(this, new MouseInputManagerEventArgs(e.m_time));
            m_isHovered = true;
            if (m_hoveredIn != null)
            {
                m_hoveredIn.Invoke();
            }
            if (m_hoveredInEventHandle != null)
            {
                m_hoveredInEventHandle(this, e);
            }
        }
        protected virtual void OnHoveredOver(MouseInteractEventArgs e)
        {
            KaiTool_MouseInputManager.Instance.OnHoveringOverObject(this, new MouseInputManagerEventArgs(e.m_time));
            if (m_hoveredOver != null)
            {
                m_hoveredOver.Invoke();
            }
            if (m_hoveredOverEventHandle != null)
            {
                m_hoveredOverEventHandle(this, e);
            }
        }
        protected virtual void OnHoveredOut(MouseInteractEventArgs e)
        {
            KaiTool_MouseInputManager.Instance.OnHoveringOutObject(this, new MouseInputManagerEventArgs(e.m_time));
            m_isHovered = false;
            if (m_hoveredOut != null)
            {
                m_hoveredOut.Invoke();
            }
            if (m_hoveredOutEventHandle != null)
            {
                m_hoveredOutEventHandle(this, e);
            }
        }
        protected virtual void OnLeftButtonPressed(MouseInteractEventArgs e)
        {
            KaiTool_MouseInputManager.Instance.OnSelectingObject(this, new MouseInputManagerEventArgs(e.m_time));
            m_previousSelectedTime = e.m_time;
            m_isLeftButtonSelected = true;
            if (m_leftButtonPressed != null)
            {
                m_leftButtonPressed.Invoke();
            }
            if (m_leftButtonPressedEventHandle != null)
            {
                m_leftButtonPressedEventHandle(this, e);
            }
        }
        protected virtual void OnLeftButtonReleased(MouseInteractEventArgs e)
        {
            KaiTool_MouseInputManager.Instance.OnUnselectingObject(this, new MouseInputManagerEventArgs(e.m_time));
            m_isLeftButtonSelected = false;
            if (m_leftButtonReleased != null)
            {
                m_leftButtonReleased.Invoke();
            }
            if (m_leftButtonReleasedEventHandle != null)
            {
                m_leftButtonReleasedEventHandle(this, e);
            }
        }
        protected virtual void OnLeftButtonDoubleClicked(MouseInteractEventArgs e)
        {
            if (m_leftButtonDoubleClicked != null)
            {
                m_leftButtonDoubleClicked.Invoke();
            }
            if (m_leftButtonDoubleClickedEventHandle != null)
            {
                m_leftButtonDoubleClickedEventHandle(this, e);
            }
        }
        protected virtual void OnRightButtonPressed(MouseInteractEventArgs e)
        {
            KaiTool_MouseInputManager.Instance.OnAimingObject(this, new MouseInputManagerEventArgs(e.m_time));
            m_isRightButtonSelected = true;
            if (m_rightButtonPressed != null)
            {
                m_rightButtonPressed.Invoke();
            }
            if (m_rightButtonPressedEventHandle != null)
            {
                m_rightButtonPressedEventHandle(this, e);
            }
        }
        protected virtual void OnRightButtonReleased(MouseInteractEventArgs e)
        {
            KaiTool_MouseInputManager.Instance.OnUnaimingObject(this, new MouseInputManagerEventArgs(e.m_time));
            m_isRightButtonSelected = false;
            if (m_rightButtonRelease != null)
            {
                m_rightButtonRelease.Invoke();
            }
            if (m_rightButtonReleasedEventHandle != null)
            {
                m_rightButtonReleasedEventHandle(this, e);
            }
        }
        protected virtual void OnRightButtonDoubleClicked(MouseInteractEventArgs e)
        {
            if (m_rightButtonDoubleClicked != null)
            {
                m_rightButtonDoubleClicked.Invoke();
            }

            if (m_rightButtonDoubleClickedEventHandle != null)
            {
                m_rightButtonDoubleClickedEventHandle(this, e);
            }
        }
        protected virtual void OnDragged(MouseInteractEventArgs e)
        {
            if (m_dragged != null)
            {
                m_dragged.Invoke();
            }
            if (m_draggedEventHandle != null)
            {
                m_draggedEventHandle(this, new MouseInteractEventArgs());
            }
        }
        #endregion

        private void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {
            InitVar();
            InitEvent();
        }
        protected void InitVar()
        {
            m_doubleClickWaitingTime = KaiTool_MouseInputManager.Instance.DoubleClickedTime;
        }
        private void InitEvent()
        {
   
        }
        private void OnStartBeingDragged()
        {
            m_isDragged = true;
            if (m_dragCoroutine != null)
            {
                StopCoroutine(m_dragCoroutine);
            }
            m_dragCoroutine = StartCoroutine(BeingDraggedEnumerator());
        }
        private void OnStopBeingDragged()
        {
            m_draggingSpeed = 0f;
            m_draggingVelocity = Vector3.zero;
            m_draggingDistance = 0f;
            m_draggingVector = Vector3.zero;
            m_isDragged = false;
            if (m_dragCoroutine != null)
            {
                StopCoroutine(m_dragCoroutine);
            }
        }
        private IEnumerator BeingDraggedEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            var tempMousePosition = Input.mousePosition;
            var originalMousePosition = tempMousePosition;
            var previousMousePosition = tempMousePosition;
            var currentMousePostion = tempMousePosition;
            while (true)
            {
                OnDragged(new MouseInteractEventArgs(Time.time));
                m_draggingVector = currentMousePostion - originalMousePosition;
                m_draggingDistance = m_draggingVector.magnitude;
                m_draggingSpeed = (currentMousePostion - previousMousePosition).magnitude / m_intervalTime;
                m_draggingVelocity = (currentMousePostion - previousMousePosition) / m_intervalTime;
                previousMousePosition = currentMousePostion;
                currentMousePostion = Input.mousePosition;
                yield return wait;
            }
        }
        private void OnMouseEnter()
        {
            if (m_isLeftButtonInteractive || m_isRightButtonInteractive)
            {
                OnHoveredIn(new MouseInteractEventArgs(Time.time));
            }
        }
        private void OnMouseOver()
        {
            if (m_isLeftButtonInteractive || m_isRightButtonInteractive)
            {
                OnHoveredOver(new MouseInteractEventArgs(Time.time));
            }
            if (Input.GetMouseButtonDown(0) && m_isLeftButtonInteractive)
            {
                OnLeftButtonPressed(new MouseInteractEventArgs(Time.time));
                if (m_leftButtonDoubleClickTimer <= 0f)
                {
                    m_leftButtonDoubleClickTimer = m_doubleClickWaitingTime;
                }
                else
                {
                    OnLeftButtonDoubleClicked(new MouseInteractEventArgs(Time.time));
                    m_leftButtonDoubleClickTimer = 0f;
                }
                OnStartBeingDragged();
            }
            if (Input.GetMouseButtonDown(1) && m_isRightButtonInteractive)
            {
                OnRightButtonPressed(new MouseInteractEventArgs(Time.time));
                if (m_rightButtonDoubleClickTimer <= 0f)
                {
                    m_rightButtonDoubleClickTimer = m_doubleClickWaitingTime;
                }
                else
                {
                    OnRightButtonDoubleClicked(new MouseInteractEventArgs(Time.time));
                    m_rightButtonDoubleClickTimer = 0f;
                }
            }
        }
        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && m_isLeftButtonInteractive)
            {

                var selectedObject = KaiTool_MouseInputManager.Instance.ObjectSelected;
                if (selectedObject != null)
                {
                    var interactiveObject = selectedObject.GetComponent<KaiTool_MouseInteractiveObject>();
                    interactiveObject.OnLeftButtonReleased(new MouseInteractEventArgs(Time.time));
                    interactiveObject.OnStopBeingDragged();
                }
            }
            if (Input.GetMouseButtonUp(1) && m_isRightButtonInteractive)
            {
                var aimedObject = KaiTool_MouseInputManager.Instance.ObjectAimed;
                if (aimedObject != null)
                {
                    var interactiveObject = aimedObject.GetComponent<KaiTool_MouseInteractiveObject>();
                    interactiveObject.OnRightButtonReleased(new MouseInteractEventArgs(Time.time));
                }
            }
        }
        private void FixedUpdate()
        {
            if (m_leftButtonDoubleClickTimer > 0f)
            {
                m_leftButtonDoubleClickTimer -= Time.deltaTime;
            }
            if (m_rightButtonDoubleClickTimer > 0f)
            {
                m_rightButtonDoubleClickTimer -= Time.deltaTime;
            }
        }
        private void OnMouseExit()
        {
            if (m_isLeftButtonInteractive || m_isRightButtonInteractive)
            {
                OnHoveredOut(new MouseInteractEventArgs(Time.time));
            }
        }
    }
}