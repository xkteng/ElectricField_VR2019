using KaiTool.AssemblyPart;
using KaiTool.Utilities;
using UnityEngine;
namespace EntireCarAssembly
{
    public class ScrewRotating : BasicScrew
    {
        [SerializeField] private float _rotatingAngleTotal = 1080f;
        
        public float RotatingAngleTotal
        {
            get
            {
                return _rotatingAngleTotal;
            }

            set
            {
                _rotatingAngleTotal = value;
            }
        }

        protected override void OnTriggerStay(Collider other)
        {
            base.OnTriggerStay(other);
            if (IsStarted) {
                var euler = m_originalLocalRot.eulerAngles;
                if (currentStep < stepSum)
                {
                    ScrewDriver screwDriver = other.GetComponentInParent<ScrewDriver>();
                    if (screwDriver && screwDriver.ScrewType == this.ScrewType && screwDriver.InterObject.IsUsing()&&JudgeProbeDirection(other.transform,this.transform,maxAngle))
                    {
                        currentStep++;
                        transform.localRotation = Quaternion.Euler(euler.x,euler.y,euler.z-RotatingAngleTotal*currentStep/stepSum);

                    }
                }
                if (currentStep == stepSum)
                {
                    currentStep++;
                    IsScrewed = true;
                    OnFinishSteppedObject( new SteppedObjectEventArgs());
                }
            }
        }
       
    }
}