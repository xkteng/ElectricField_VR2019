using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
namespace KaiTool.VR.RichInteract
{
    public class RichInteractableObject : VRTK_InteractiveAssistant
    {
        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
            InitComponent();
        }
        private void InitVar()
        {
            this.RigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
        private void InitEvent()
        {
            this.InterObject.InteractableObjectGrabbed += (sender, e) =>
            {
                RemoveFixJoint();
                this.RigidBody.constraints = RigidbodyConstraints.None;

            };
            this.InterObject.InteractableObjectUngrabbed += (sender, e) =>
            {
                this.RigidBody.constraints = RigidbodyConstraints.FreezeAll;
                AddFixJoint();
            };
        }
        private void InitComponent()
        {
            AddFixJoint();
        }
        private void AddFixJoint()
        {
            gameObject.AddComponent<FixedJoint>().connectedBody = transform.parent.GetComponentInParent<Rigidbody>();
        }
        private void RemoveFixJoint()
        {
            foreach (FixedJoint fj in GetComponents<FixedJoint>())
            {
                Destroy(fj);
            }
        }
    }
}