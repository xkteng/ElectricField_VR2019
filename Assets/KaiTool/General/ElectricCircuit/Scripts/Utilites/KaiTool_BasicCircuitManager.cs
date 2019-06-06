using System;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.ElectricCircuit
{
    [Serializable]
    public class CircuiEvent : UnityEvent { }
    public abstract class KaiTool_BasicCircuitManager : MonoBehaviour
    {
        private void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {

        }

        protected void RegisterDevice_Switch_Event(KaiTool_CircuitDevice device, KaiTool_SingleInputSwitch[] switches, bool toggle)
        {
            if (toggle)
            {
                device.m_electrifiedEventHandle += (sender, e) =>
                {
                    foreach (var item in switches)
                    {
                        item.TurnOn();
                    }
                };
                device.m_unelectrifiedEventHandle += (sender, e) =>
                {
                    foreach (var item in switches)
                    {
                        item.TurnOff();
                    }
                };
            }
            else
            {
                device.m_electrifiedEventHandle += (sender, e) =>
                {
                    foreach (var item in switches)
                    {
                        item.TurnOff();
                    }
                };
                device.m_unelectrifiedEventHandle += (sender, e) =>
                {
                    foreach (var item in switches)
                    {
                        item.TurnOn();
                    }
                };
            }
        }

        public virtual void ToggleSwitch(string message)
        {

        }

        protected void SetCueActive(GameObject cue, bool toggle)
        {
            if (cue)
            {
                cue.SetActive(toggle);
            }
        }
    }
}