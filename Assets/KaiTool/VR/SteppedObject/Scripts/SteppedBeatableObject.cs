//#define Debug
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
namespace KaiTool.Utilities
{
    public class SteppedBeatableObject : SteppedObject, IBeatableObject
    {
        private GameObject arrow;
        private bool _isBeaten = false;
        public float distanceBetweenObjectAndArrow = 0.05f;
        public float beatRange = 0.05f;
        public float beatVelocity = 0.05f;

        public bool IsBeaten
        {
            get
            {
                return _isBeaten;
            }

            set
            {
                _isBeaten = value;
            }
        }

        public event Action<UnityEngine.Object, BeatableObjectArgs> Beaten;
        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }
#if Debug
        protected override void Update()
        {
            base.Update();
            if (IsStarted)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    OnBeaten(this, new BeatableObjectArgs());
                }
            }
        }
#endif
        private void InitVar()
        {
        }
        private void InitEvent()
        {
            m_steppedObjectStartEventHandle += (sender, e) =>
            {
                ShowUpArrow();
                SearchForHandController();
            };
            Beaten += (sender, e) =>
            {
                if (!IsBeaten)
                {
                    IsBeaten = true;
                    HideArrow();
                    Invoke("OnSteppedObjectFinished", 0.5f);
                }
            };
        }
        private void OnSteppedObjectFinished()
        {
            OnFinishSteppedObject(new SteppedObjectEventArgs());
        }
        public void OnBeaten(UnityEngine.Object sender, BeatableObjectArgs e)
        {
            if (Beaten != null)
            {
                Beaten(sender, e);
            }
        }
        /// <summary>
        /// Show up arrow after the start method invoked.
        /// </summary>
        private void ShowUpArrow()
        {
            arrow = GameObject.Instantiate(Resources.Load("Prefabs/Arrow") as GameObject
                , transform.position - transform.forward * distanceBetweenObjectAndArrow, transform.rotation);
        }
        /// <summary>
        /// Make arrow disappear.
        /// </summary>
        private void HideArrow()
        {
            Destroy(arrow);
        }

        private void SearchForHandController()
        {
            StartCoroutine(SearchForHandControllerCoroutine());
        }
        private IEnumerator SearchForHandControllerCoroutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.1f);
            while (!IsBeaten)
            {
                Collider[] colliderArr = Physics.OverlapSphere(transform.position, beatRange, 1 << LayerMask.NameToLayer("PLayerController"));
                // print(colliderArr.Length);
                foreach (Collider coll in colliderArr)
                {
                    VRTK_ControllerEvents controllerEvents = coll.GetComponent<VRTK_ControllerEvents>();

                    if (controllerEvents != null)
                    {
                        VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(controllerEvents.gameObject);

                        var controllerVelocity = VRTK_DeviceFinder.GetControllerVelocity(controllerReference);
                        // print(controllerVelocity);
                        //print(controllerVelocity.magnitude);
                        if (controllerVelocity.magnitude > beatVelocity)
                        {
                            OnBeaten(this, new BeatableObjectArgs());
                            break;
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        OnBeaten(this, new BeatableObjectArgs());
                        break;
                    }
                }
                yield return wait;
            }
        }
        [ContextMenu("ResetAllBeatableObject")]
        private void ResetAllBeatableObject()
        {
            foreach (SteppedBeatableObject beat in FindObjectsOfType<SteppedBeatableObject>())
            {
                beat.beatRange = 0.05f;
                beat.beatVelocity = 1f;
            }
        }
    }
}