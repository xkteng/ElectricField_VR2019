using System;
using System.Collections;
using UnityEngine;
using VRTK;

namespace KaiTool.VR.HandGesture
{
    /// <summary>
    /// Put this component on the hand controller.
    /// </summary>
    public class VRTK_BasicHandGestureController : MonoBehaviour
    {

        public EnumHandType handType;
        public GameObject handModel;
        protected Animator handAnimator;
        protected VRTK_ControllerEvents controllerEvent;
        protected VRTK_InteractTouch interactTouch;
        protected VRTK_InteractGrab interactGrab;
        protected VRTK_InteractUse interactUsed;
        protected VRTK_Pointer pointer;
        protected virtual void Awake()
        {
        }
        protected virtual void Start()
        {
            Init();
        }
        private void Init()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            handAnimator = handModel.GetComponent<Animator>();
            controllerEvent = GetComponent<VRTK_ControllerEvents>();
            interactTouch = GetComponent<VRTK_InteractTouch>();
            interactGrab = GetComponent<VRTK_InteractGrab>();
            interactUsed = GetComponent<VRTK_InteractUse>();
            pointer = GetComponent<VRTK_Pointer>();
        }
        private void InitEvent()
        {
            InitGestureEvent();
        }
        private void InitGestureEvent()
        {
            //*********************************************************************************
            //possitive event
            pointer.ActivationButtonPressed += (sender, e) =>
            {
                if (interactTouch.GetTouchedObject() == null)
                {
                    //TransitToPointerGesture();
                    TransitToGestureByHandGestureType(EnumHandGestureType.Point);
                }
            };
            pointer.ActivationButtonReleased += (sender, e) =>
            {
                if (interactTouch.GetTouchedObject() == null)
                {
                    //TransitToIdleGesture();
                    TransitToGestureByHandGestureType(EnumHandGestureType.Idle);
                }
            };
            interactGrab.GrabButtonPressed += (sender, e) =>
            {
                if (interactTouch.GetTouchedObject() == null)
                {
                    TransitToGestureByHandGestureType(EnumHandGestureType.Grasp);
                    //TransitToGraspGesture();
                }
            };
            interactGrab.GrabButtonReleased += (sender, e) =>
            {
                if (interactTouch.GetTouchedObject() == null)
                {
                    TransitToGestureByHandGestureType(EnumHandGestureType.Idle);
                    //TransitToIdleGesture();
                }
            };
            //**********************************************************************************
            //passive event
            interactTouch.ControllerTouchInteractableObject += (sender, e) =>
            {
                VRTK_BasicGestureObject objectWithGesture = ((ObjectInteractEventArgs)e).target.GetComponent<VRTK_BasicGestureObject>();
                if (objectWithGesture)
                {
                    if (objectWithGesture.LeftTouchGesture != null)
                    {
                        objectWithGesture.ToggleTouchGesture(handType, true);
                        ToggleHandModelActive(false);
                    }
                    else
                    {
                        if (interactGrab.GetGrabbedObject() == null)
                        {
                            TransitToGestureByHandGestureType(objectWithGesture.m_defaulttouchGesture);
                        }
                    }
                }
                else
                { //NO VRTK_BasicGestureObject
                    if (interactGrab.GetGrabbedObject() == null)
                    {
                        //TransitToGestureByHandGestureType(EnumHandGestureType.Stretch);
                    }
                }
            };
            interactTouch.ControllerUntouchInteractableObject += (sender, e) =>
            {
                VRTK_BasicGestureObject objectWithGesture = ((ObjectInteractEventArgs)e).target.GetComponent<VRTK_BasicGestureObject>();
                if (objectWithGesture)
                {
                    if (objectWithGesture.LeftTouchGesture != null)
                    {
                        objectWithGesture.ToggleTouchGesture(handType, false);
                        ToggleHandModelActive(true);
                    }
                    else
                    {
                        if (interactGrab.GetGrabbedObject() == null)
                        {
                            //TransitToIdleGestureLater();
                            TransitToGestureByHandGestureType(EnumHandGestureType.Idle);//*********************
                        }
                    }
                }
                else
                { //NO VRTK_BasicGestureObject
                    if (interactGrab.GetGrabbedObject() == null)
                    {
                        //TransitToIdleGesture();
                        TransitToIdleGestureLater();
                    }
                }
                /*
                foreach (Collider coll in GetComponentsInChildren<Collider>() ) {
                    coll.isTrigger = true;
                }
                */
            };
            interactGrab.ControllerGrabInteractableObject += (sender, e) =>
            {
                VRTK_BasicGestureObject objectWithGesture = ((ObjectInteractEventArgs)e).target.GetComponent<VRTK_BasicGestureObject>();
                if (objectWithGesture)
                {
                    if (objectWithGesture.LeftGrabGesture != null)
                    {
                        if (objectWithGesture.LeftTouchGesture != null)
                        {
                            objectWithGesture.ToggleTouchGesture(handType, false);
                        }
                        objectWithGesture.ToggleGrabGesture(handType, true);
                        ToggleHandModelActive(false);
                    }
                    else
                    {
                        TransitToGestureByHandGestureType(objectWithGesture.m_defaultGrabGesture);
                    }
                }
                else
                { //NO VRTK_BasicGestureObject
                  //TransitToGraspGesture();
                    TransitToGestureByHandGestureType(EnumHandGestureType.Grasp);
                }
            };
            interactGrab.ControllerUngrabInteractableObject += (sender, e) =>
            {
                VRTK_BasicGestureObject objectWithGesture = ((ObjectInteractEventArgs)e).target.GetComponent<VRTK_BasicGestureObject>();
                if (objectWithGesture)
                {
                    if (objectWithGesture.LeftGrabGesture != null)
                    {
                        objectWithGesture.ToggleGrabGesture(handType, false);
                        if (objectWithGesture.LeftTouchGesture != null)
                        {
                            objectWithGesture.ToggleTouchGesture(handType, true);
                        }
                        else
                        {
                            ToggleHandModelActive(true);
                        }
                    }
                    else
                    {
                        TransitToGestureByHandGestureType(EnumHandGestureType.Idle);
                    }
                }
                else
                { //NO VRTK_BasicGestureObject

                    // TransitToIdleGesture();
                    TransitToGestureByHandGestureType(EnumHandGestureType.Idle);
                }
            };

            interactUsed.ControllerUseInteractableObject += (sender, e) =>
            {
                VRTK_BasicGestureObject objectWithGesture = ((ObjectInteractEventArgs)e).target.GetComponent<VRTK_BasicGestureObject>();
                if (objectWithGesture)
                {
                    if (objectWithGesture.LeftUseGesture != null)
                    {
                        if (objectWithGesture.LeftGrabGesture != null)
                        {
                            objectWithGesture.ToggleGrabGesture(handType, false);
                        }
                        objectWithGesture.ToggleUseGesture(handType, true);
                        ToggleHandModelActive(false);

                    }
                    else
                    {
                        TransitToGestureByHandGestureType(objectWithGesture.m_defaultUseGesture);
                    }
                }
                else
                { //NO VRTK_BasicGestureObject
                    //TransitToPullTriggerGresture();
                    TransitToGestureByHandGestureType(EnumHandGestureType.PullTrigger);
                }
            };
            interactUsed.ControllerUnuseInteractableObject += (sender, e) =>
            {
                try
                {
                    VRTK_BasicGestureObject objectWithGesture = ((ObjectInteractEventArgs)e).target.GetComponent<VRTK_BasicGestureObject>();
                    if (objectWithGesture)
                    {
                        if (objectWithGesture.LeftUseGesture != null)
                        {
                            objectWithGesture.ToggleUseGesture(handType, false);
                            if (objectWithGesture.LeftGrabGesture != null)
                            {
                                objectWithGesture.ToggleGrabGesture(handType, true);
                            }
                            else
                            {
                                ToggleHandModelActive(true);
                            }
                        }
                        else
                        {
                            TransitToGestureByHandGestureType(objectWithGesture.m_defaultGrabGesture);
                        }
                    }
                    else
                    { //NO VRTK_BasicGestureObject
                        //TransitToGraspGesture();
                        TransitToGestureByHandGestureType(EnumHandGestureType.Grasp);
                    }
                }
                catch (NullReferenceException e1)
                {
                    print(e1.Message);
                }

            };
        }
        public void TransitToGestureByHandGestureType(EnumHandGestureType gestureType)
        {
            switch (gestureType)
            {
                case EnumHandGestureType.Idle:
                    handAnimator.SetInteger("State", 0);
                    break;
                case EnumHandGestureType.Point:
                    handAnimator.SetInteger("State", 1);
                    break;
                case EnumHandGestureType.Grasp:
                    handAnimator.SetInteger("State", 2);
                    break;
                case EnumHandGestureType.Stretch:
                    handAnimator.SetInteger("State", 3);
                    break;
                case EnumHandGestureType.PullTrigger:
                    handAnimator.SetInteger("State", 4);
                    break;
                case EnumHandGestureType.ThumbUp:
                    handAnimator.SetInteger("State", 5);
                    break;
                default:
                    break;
            }
        }
        private void TransitToIdleGestureLater()
        {
            StartCoroutine(TransitToIdleGestureLaterIEnumerator());
        }
        IEnumerator TransitToIdleGestureLaterIEnumerator()
        {
            yield return new WaitForSeconds(0.3f);
            if (interactTouch.GetTouchedObject() == null)
            {
                TransitToGestureByHandGestureType(EnumHandGestureType.Idle);
                //TransitToIdleGesture();
            }
        }
        /*
        private void TransitToPointerGesture()
        {
            handAnimator.SetInteger("State", 1);
        }
        private void TransitToGraspGesture()
        {
            handAnimator.SetInteger("State", 2);
        }
        private void TransitToStretchGesture()
        {
            handAnimator.SetInteger("State", 3);
        }
        private void TransitToPullTriggerGresture()
        {
            handAnimator.SetInteger("State", 4);
        }
        */
        private void ToggleHandModelActive(bool toggle)
        {
            handModel.SetActive(toggle);
        }
    }
    public enum EnumHandType
    {
        None,
        RightHand,
        LeftHand
    }
    public enum EnumHandGestureType
    {
        Idle,
        Point,
        Grasp,
        Stretch,
        PullTrigger,
        ThumbUp
    }
}