using System.Collections;
using UnityEngine;

namespace KaiTool.ElectricCircuit
{
    public class DDKZ_CircuitManager : KaiTool_BasicCircuitManager
    {
        [Header("QS")]
        [SerializeField]
        private KaiTool_SingleInputSwitch m_QS;
        [Header("SB")]
        [SerializeField]
        private KaiTool_SingleInputSwitch m_SB1;
        [SerializeField]
        private KaiTool_SingleInputSwitch m_SB2;
        [SerializeField]
        private KaiTool_SingleInputSwitch m_SB3;
        [SerializeField]
        private KaiTool_SingleInputSwitch m_SB4;
        [SerializeField]
        private KaiTool_SingleInputSwitch m_SB5;
        [SerializeField]
        private KaiTool_SingleInputSwitch m_SB6;
        [SerializeField]
        private KaiTool_SingleInputSwitch m_SB7;
        [Header("KM")]
        [SerializeField]
        private KaiTool_SingleInputSwitch[] m_KMs;
        [Header("UI")]
        [SerializeField]
        private GameObject m_QS_Cue;

        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }
        private void InitVar() { }
        private void InitEvent()
        {
            m_QS.TurnedOn.AddListener(() =>
            {
                m_QS_Cue.SetActive(false);
            });
            m_QS.TurnedOff.AddListener(() =>
            {
                m_QS_Cue.SetActive(true);
            });
        }

        public void TurnOn(string name)
        {
            switch (name)
            {
                case "SB1":
                    m_SB1.TurnOn();
                    break;
                case "SB2":
                    m_SB2.TurnOn();
                    break;
                case "SB3":
                    m_SB3.TurnOn();
                    break;
                case "SB4":
                    m_SB4.TurnOn();
                    break;
                case "SB5":
                    m_SB5.TurnOn();
                    break;
                case "SB6":
                    m_SB6.TurnOn();
                    break;
                case "SB7":
                    m_SB7.TurnOn();
                    break;
                case "QS":
                    m_QS.TurnOn();
                    break;
                default:
                    break;
            }
        }
        public void TurnOff(string name)
        {
            switch (name)
            {
                case "SB1":
                    m_SB1.TurnOff();
                    break;
                case "SB2":
                    m_SB2.TurnOff();
                    break;
                case "SB3":
                    m_SB3.TurnOff();
                    break;
                case "SB4":
                    m_SB4.TurnOff();
                    break;
                case "SB5":
                    m_SB5.TurnOff();
                    break;
                case "SB6":
                    m_SB6.TurnOff();
                    break;
                case "SB7":
                    m_SB7.TurnOff();
                    break;
                case "QS":
                    m_QS.TurnOff();
                    break;
                default:
                    break;
            }
        }

        public void ToggleAllKM(bool toggle)
        {
            StartCoroutine(ToggleAllKMEnumerator(toggle));
        }
        private IEnumerator ToggleAllKMEnumerator(bool toggle)
        {
            foreach (var item in m_KMs)
            {
                if (toggle)
                {
                    item.TurnOn();
                }
                else
                {
                    // yield return new WaitForSeconds(0.3f);
                    item.TurnOff();
                }
            }
            yield return null;
        }
    }
}