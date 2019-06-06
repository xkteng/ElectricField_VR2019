//#define Debug
using KaiTool.AssemblyPart;
using KaiTool.Utilities;
using UnityEngine;
namespace EntireCarAssembly
{
    public class ScrewDrilling : BasicScrew
    {

        [SerializeField]
        protected float _depth = 0.01f;
        public float Depth { get { return _depth; } set { _depth = value; } }
        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {

        }
        private void InitEvent()
        {

        }

        protected override void OnTriggerStay(Collider other)
        {
            base.OnTriggerStay(other);
            if (IsStarted)
            {
                if (currentStep < stepSum)
                {
                    ScrewDriver screwDriver = other.GetComponentInParent<ScrewDriver>();
                    if (screwDriver && screwDriver.ScrewType == this.ScrewType && screwDriver.InterObject.IsUsing() && JudgeProbeDirection(other.transform, this.transform, maxAngle))
                    {
                        currentStep++;
                        transform.position = m_originalPos + transform.forward * Mathf.Lerp(0, Depth, (float)currentStep / stepSum);
                    }
                }
                if (currentStep == stepSum)
                {
                    currentStep++;
                    IsScrewed = true;
                    OnFinishSteppedObject(new SteppedObjectEventArgs());
                }
            }
        }
        [ContextMenu("SetDistance")]
        public void SetDistance()
        {
            foreach (ScrewDrilling sd in FindObjectsOfType<ScrewDrilling>())
            {
                sd.distanceBetweenScrew2Arrow = 0.1f;
            }
        }
    }
}
