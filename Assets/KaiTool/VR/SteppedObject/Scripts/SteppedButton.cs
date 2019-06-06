//#define Debug
using KaiTool.Utilities;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
namespace KaiTool.Utilities
{
    public struct SteppedButtonArgs
    {
        public float intervalTime;
        public SteppedButtonArgs(float intervalTime)
        {
            this.intervalTime = intervalTime;
        }
    }
    public class SteppedButton : SteppedObject
    {
        [Header("Stepped Button")]
        public string textToChangeAfterClicked = null;
        public float intervalTime = 1f;
        public Text text;
        public bool isInactiveItselfAfterClicked = false;
        private bool _isClicked = false;
        public Action<UnityEngine.Object, SteppedButtonArgs> Clicked;
        private VRTK_InteractableObject interactableObject;
        public bool IsClicked
        {
            get
            {
                return _isClicked;
            }

            set
            {
                _isClicked = value;
            }
        }
        public void OnClicked(UnityEngine.Object sender, SteppedButtonArgs e)
        {
            if (Clicked != null)
            {
                Clicked(sender, e);
            }
        }
        private void Reset()
        {
            this.intervalTime = 1f;
        }
        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            interactableObject = GetComponent<VRTK_InteractableObject>();
        }
        private void InitEvent()
        {
            Clicked += (sender, e) =>
            {
                ChangeTextInTime(((SteppedButtonArgs)e).intervalTime);
                if (IsStarted)
                {
                    OnSteppedObjectFinishedInTime(((SteppedButtonArgs)e).intervalTime);
                    IsClicked = true;
                }
                if (isInactiveItselfAfterClicked)
                {
                    InactiveItselfInTime(((SteppedButtonArgs)e).intervalTime + 1f);
                }
            };


            //OnClick.AddListener(ChangeText);
            // OnClick.AddListener(OnSteppedObjectFinishedInTime);
        }
        private void OnSteppedObjectFinishedInTime(float time)
        {
            StartCoroutine(WaitAndOnSteppedObjectFinished(time));
        }
        IEnumerator WaitAndOnSteppedObjectFinished(float time)
        {
            yield return new WaitForSeconds(time);
            this.OnFinishSteppedObject(new SteppedObjectEventArgs());
        }
        private void ChangeTextInTime(float time)
        {
            StartCoroutine(ChangeTextIEnumerator(time));

        }
        IEnumerator ChangeTextIEnumerator(float time)
        {
            yield return new WaitForSeconds(time);
            if (text)
            {
                text.text = textToChangeAfterClicked;
            }
        }
        private void InactiveItselfInTime(float time)
        {
            StartCoroutine(InactiveItselfInTimeIEnumerator(time));
        }
        IEnumerator InactiveItselfInTimeIEnumerator(float time)
        {
            /*
            foreach (Collider coll in VRTK_DeviceFinder.GetControllerRightHand().GetComponentsInChildren<Collider>()) {
                coll.isTrigger = true;
            }
            foreach (Collider coll in VRTK_DeviceFinder.GetControllerLeftHand().GetComponentsInChildren<Collider>())
            {
                coll.isTrigger = true;
            }
            */
            yield return new WaitForSeconds(time);
            //transform.parent.gameObject.SetActive(false);
            foreach (Rigidbody rigid in GetComponentsInChildren<Rigidbody>())
            {
                rigid.isKinematic = true;
            }
            transform.parent.position += Vector3.up * 1000f;
            foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
            {
                mr.enabled = false;
            }
            foreach (Canvas canvas in GetComponentsInChildren<Canvas>())
            {
                canvas.enabled = false;
            }
        }
    }
}