using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
namespace Electricity
{
    [RequireComponent(typeof(VRTK_InteractableObject))]
    public class InteractableObject_Attach : MonoBehaviour
    {
        private VRTK_InteractableObject m_interObj;

        private void Awake()
        {
            m_interObj = GetComponent<VRTK_InteractableObject>();
            m_interObj.InteractableObjectGrabbed += OnGrabbed;
            m_interObj.InteractableObjectUngrabbed += OnUngrabbed;
        }

        private void OnGrabbed(object sender, InteractableObjectEventArgs e) {
            ElectricityManager.Instance.HideFieldLine();
        }
        private void OnUngrabbed(object sender, InteractableObjectEventArgs e) {
            ElectricityManager.Instance.Refresh();
            ElectricityManager.Instance.ShowFieldLine();
        }
    }
}
