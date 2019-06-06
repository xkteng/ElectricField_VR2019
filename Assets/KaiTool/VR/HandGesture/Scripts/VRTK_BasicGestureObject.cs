using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.VR.HandGesture
{
    public class VRTK_BasicGestureObject : MonoBehaviour
    {
        [Header("Default_Hand_Gestures")]
        public EnumHandGestureType m_defaulttouchGesture = EnumHandGestureType.Stretch;
        public EnumHandGestureType m_defaultGrabGesture = EnumHandGestureType.Grasp;
        public EnumHandGestureType m_defaultUseGesture = EnumHandGestureType.PullTrigger;
        [Header("Special_Hand_Gestures")]
        [SerializeField]
        private VRTK_BasicGesture m_specialTouchGesture_lefthand;
        [SerializeField]
        private VRTK_BasicGesture m_specialGrabGesture_lefthand;
        [SerializeField]
        private VRTK_BasicGesture m_specialUseGesture_lefthand;


        protected virtual void Awake() { }

        private void Init()
        {
            InitVar();
            InitEvent();
            InitComponent();
        }
        private void InitVar()
        {
        }
        private void InitEvent() { }
        private void InitComponent()
        {
        }
        public VRTK_BasicGesture LeftTouchGesture
        {
            get
            {
                return m_specialTouchGesture_lefthand;
            }
        }

        public VRTK_BasicGesture LeftGrabGesture
        {
            get
            {
                return m_specialGrabGesture_lefthand;
            }
        }

        public VRTK_BasicGesture LeftUseGesture
        {
            get
            {
                return m_specialUseGesture_lefthand;
            }
        }

        public void ToggleTouchGesture(EnumHandType handType, bool toggle)
        {

            LeftTouchGesture.gameObject.SetActive(toggle);
            var originalScale = LeftTouchGesture.transform.localScale;
            if (toggle)
            {
                switch (handType)
                {
                    case EnumHandType.LeftHand:
                        LeftTouchGesture.transform.localScale = originalScale;
                        break;
                    case EnumHandType.RightHand:
                        var reflectedScale = Vector3.Reflect(originalScale, Vector3.right);
                        LeftTouchGesture.transform.localScale = reflectedScale;
                        break;
                    default:
                        LeftTouchGesture.transform.localScale = originalScale;
                        break;
                }
            }
            else
            {
                var tempScale = LeftTouchGesture.transform.localScale;
                LeftTouchGesture.transform.localScale = new Vector3(Mathf.Abs(tempScale.x), tempScale.y, tempScale.z);
            }
        }
        public void ToggleGrabGesture(EnumHandType handType, bool toggle)
        {
            LeftGrabGesture.gameObject.SetActive(toggle);
            var originalScale = LeftGrabGesture.transform.localScale;
            if (toggle)
            {
                switch (handType)
                {
                    case EnumHandType.LeftHand:
                        LeftGrabGesture.transform.localScale = originalScale;
                        break;
                    case EnumHandType.RightHand:
                        var reflectedScale = Vector3.Reflect(originalScale, Vector3.right);
                        LeftGrabGesture.transform.localScale = reflectedScale;
                        break;
                    default:
                        LeftGrabGesture.transform.localScale = originalScale;
                        break;
                }
            }
            else
            {
                var tempScale = LeftGrabGesture.transform.localScale;
                LeftGrabGesture.transform.localScale = new Vector3(Mathf.Abs(tempScale.x), tempScale.y, tempScale.z);
            }
        }
        public void ToggleUseGesture(EnumHandType handType, bool toggle)
        {
            LeftUseGesture.gameObject.SetActive(toggle);
            var originalScale = LeftUseGesture.transform.localScale;
            if (toggle)
            {
                switch (handType)
                {
                    case EnumHandType.LeftHand:
                        LeftUseGesture.transform.localScale = originalScale;
                        break;
                    case EnumHandType.RightHand:
                        var reflectedScale = Vector3.Reflect(originalScale, Vector3.right);
                        LeftUseGesture.transform.localScale = reflectedScale;
                        break;
                    default:
                        LeftUseGesture.transform.localScale = originalScale;
                        break;
                }
            }
            else
            {
                var tempScale = LeftUseGesture.transform.localScale;
                LeftUseGesture.transform.localScale = new Vector3(Mathf.Abs(tempScale.x), tempScale.y, tempScale.z);
            }
        }

    }
}