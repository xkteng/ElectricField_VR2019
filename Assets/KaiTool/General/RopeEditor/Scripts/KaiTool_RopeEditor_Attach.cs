using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.RopeEditor
{
    public class KaiTool_RopeEditor_Attach : MonoBehaviour
    {
        public void DestoryAllColliders()
        {
            var colliders = GetComponentsInChildren<Collider>();
            for (int i = 0; i < colliders.Length; i++)
            {
                DestroyImmediate(colliders[i]);
            }
        }
        public void EnableAllColliders()
        {
            var colliders = GetComponentsInChildren<Collider>();
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = true;
            }
        }
        public void DisableAllColliders()
        {
            var colliders = GetComponentsInChildren<Collider>();
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = false;
            }
        }
        public void SetAllRigidbodyKinematic(bool boolean)
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < rigidbodies.Length; i++)
            {
                rigidbodies[i].isKinematic = boolean;
            }
        }
    }
}

