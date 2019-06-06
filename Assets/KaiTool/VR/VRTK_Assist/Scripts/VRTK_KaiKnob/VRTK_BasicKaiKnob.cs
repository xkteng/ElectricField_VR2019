using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using VRTK;
using UnityEngine.Events;

namespace KaiTool.VR.VRTK_Assistant
{
    public enum EnumDirectionType
    {
        X, Y, Z, negative_X, negative_Y, negative_Z, Custom
    }
    public struct KaiKnobEventArgs
    {
        public float m_currentValue;
        public KaiKnobEventArgs(float currentValue)
        {
            m_currentValue = currentValue;
        }
    }
    public interface IVRTK_KaiKnob
    {
        EnumDirectionType Direction { get; }
        float CurrentValue { get; set; }
        Action<UnityEngine.Object, KaiKnobEventArgs> ValueChange { get; set; }
    }
    [RequireComponent(typeof(VRTK_InteractableObject))]
    public abstract class VRTK_BasicKaiKnob : MonoBehaviour, IVRTK_KaiKnob
    {
        [Header("KnobFields", order = 1)]
        [SerializeField]
        protected EnumDirectionType m_direction = EnumDirectionType.Z;
        [SerializeField]
        protected float m_maxValue = 180f;
        [SerializeField]
        protected float m_minValue = -180f;
        [SerializeField]
        protected float m_currentValue = 0f;
        [SerializeField]
        private float m_rotateRatio = 1f;
        protected float m_previousValue = 0f;
        public Action<UnityEngine.Object, KaiKnobEventArgs> m_valueChanged;
        //  [SerializeField]
        // protected int m_round = 0;
        [SerializeField]
        protected float m_intervalTime = 0.03f;
        [SerializeField]
        private UnityEvent m_OnUsedEvent;
        [SerializeField]
        private UnityEvent m_OnUnUsedEvent;
        [SerializeField]
        protected UnityEvent m_OnValueChangedEvent;
        protected VRTK_InteractableObject m_interObj;
        protected VRTK_InteractUse m_interUse;
        private Vector3 m_originalEuler;
        private Coroutine m_usingCoroutine = null;
        private Coroutine m_detectValueChangeCoroutine = null;
        public EnumDirectionType Direction
        {
            get
            {
                return m_direction;
            }
        }
        public float CurrentValue
        {
            get { return m_currentValue; }
            set { m_currentValue = value; }
        }
        public Action<UnityEngine.Object, KaiKnobEventArgs> ValueChange
        {
            get
            {
                return m_valueChanged;
            }
            set
            {
                m_valueChanged = value;
            }
        }
        private void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            m_currentValue = 0f;
            m_previousValue = m_currentValue;
            m_originalEuler = transform.localRotation.eulerAngles;
            m_interObj = GetComponent<VRTK_InteractableObject>();
            //  m_previousLocalRot = transform.localRotation;
        }
        private void InitEvent()
        {
            m_interObj.InteractableObjectUsed += (sender, e) =>
            {
                m_interUse = ((InteractableObjectEventArgs)e).interactingObject.GetComponent<VRTK_InteractUse>();
                var pointerRenderer = m_interUse.GetComponent<VRTK_StraightPointerRenderer>();
                pointerRenderer.enabled = false;
                if (m_usingCoroutine != null)
                {
                    StopCoroutine(m_usingCoroutine);
                    m_usingCoroutine = StartCoroutine(UseEnumerator());
                }
                else
                {
                    m_usingCoroutine = StartCoroutine(UseEnumerator());
                }
                m_OnUsedEvent.Invoke();
            };
            m_interObj.InteractableObjectUnused += (sender, e) =>
            {
                //print("UnUse!");
                var pointerRenderer = m_interUse.GetComponent<VRTK_StraightPointerRenderer>();
                pointerRenderer.enabled = true;
                ToggleControllerColliders(true);
                m_interUse = null;
                if (m_usingCoroutine != null)
                {
                    StopCoroutine(m_usingCoroutine);
                }
                m_OnUnUsedEvent.Invoke();
            };
            m_interObj.InteractableObjectUntouched += (sender, e) =>
            {
                if (m_interUse != null)
                {
                    ToggleControllerColliders(false);
                }
            };
        }
        public virtual void OnValueChanged(UnityEngine.Object sender, KaiKnobEventArgs e)
        {
            if (m_valueChanged != null)
            {
                m_valueChanged(sender, e);
            }
            m_OnValueChangedEvent.Invoke();
        }
        protected IEnumerator UseEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            var controllerEvent = m_interUse.GetComponent<VRTK_ControllerEvents>();
            if (controllerEvent != null)
            {
                while (true)
                {
                    if (controllerEvent.touchpadTouched && m_detectValueChangeCoroutine == null)
                    {
                        m_detectValueChangeCoroutine = StartCoroutine(DetectValueChangeEnumerator());
                    }
                    if (!controllerEvent.touchpadTouched && m_detectValueChangeCoroutine != null)
                    {
                        StopCoroutine(m_detectValueChangeCoroutine);
                        m_detectValueChangeCoroutine = null;
                    }
                    if (m_interUse.IsUseButtonPressed() && !m_interObj.IsTouched())
                    {
                        m_interObj.ForceStopInteracting();
                        //m_interUse.enabled = true;
                        // m_interUse = null;
                        break;
                    }
                    yield return wait;
                }
            }
        }
        protected IEnumerator DetectValueChangeEnumerator()
        {
            WaitForSeconds wait = new WaitForSeconds(m_intervalTime);
            var controllerEvent = m_interUse.GetComponent<VRTK_ControllerEvents>();
            var currentTouchAngle = controllerEvent.GetTouchpadAxis();
            var previousTouchAngle = currentTouchAngle;
            while (true)
            {
                currentTouchAngle = controllerEvent.GetTouchpadAxis();
                SetCurrentValue(previousTouchAngle, currentTouchAngle);
                m_currentValue = Mathf.Clamp(m_currentValue, m_minValue, m_maxValue);
                if (m_currentValue != m_previousValue)
                {
                    OnValueChanged(this, new KaiKnobEventArgs(m_currentValue));
                    m_previousValue = m_currentValue;
                }
                SetRotationByValue(m_currentValue);
                previousTouchAngle = currentTouchAngle;
                yield return wait;
            }
        }
        private void SetCurrentValue(Vector3 previousAxis, Vector3 currentAxis)
        {
            Vector3 cross = Vector3.Cross(previousAxis, currentAxis);
            float angle = Mathf.Acos(Vector3.Dot(previousAxis, currentAxis) / (previousAxis.magnitude * currentAxis.magnitude)) / Mathf.PI * 180;
            if (!float.IsNaN(angle))
            {
                m_currentValue += -Mathf.Sign(cross.z) * angle;
            }
        }
        private void SetRotationByValue(float value)
        {
            switch (m_direction)
            {
                case EnumDirectionType.X:
                    transform.localEulerAngles = new Vector3(m_originalEuler.x + value, m_originalEuler.y, m_originalEuler.z);
                    break;
                case EnumDirectionType.Y:
                    transform.localEulerAngles = new Vector3(m_originalEuler.x, m_originalEuler.y + value, m_originalEuler.z);
                    break;
                case EnumDirectionType.Z:
                    transform.localEulerAngles = new Vector3(m_originalEuler.x, m_originalEuler.y, m_originalEuler.z + value);
                    break;
                case EnumDirectionType.negative_X:
                    transform.localEulerAngles = new Vector3(m_originalEuler.x - value, m_originalEuler.y, m_originalEuler.z);
                    break;
                case EnumDirectionType.negative_Y:
                    transform.localEulerAngles = new Vector3(m_originalEuler.x, m_originalEuler.y - value, m_originalEuler.z);
                    break;
                case EnumDirectionType.negative_Z:
                    transform.localEulerAngles = new Vector3(m_originalEuler.x, m_originalEuler.y, m_originalEuler.z - value);
                    break;
                default:
                    break;
            }
        }
        private void ToggleControllerColliders(bool toggle)
        {
            if (m_interUse != null)
            {
                foreach (var item in m_interUse.GetComponentsInChildren<Collider>())
                {
                    item.enabled = toggle;
                }
            }
        }
        protected void OnValidate()
        {
            foreach (var item in GetComponents<VRTK_BasicKaiKnob>())
            {
                item.m_maxValue = m_maxValue;
                item.m_minValue = m_minValue;
            }
        }
    }
}