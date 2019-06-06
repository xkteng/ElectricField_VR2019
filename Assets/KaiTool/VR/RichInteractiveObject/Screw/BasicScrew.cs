using KaiTool.AssemblyPart;
using KaiTool.Utilities;
using UnityEngine;
namespace EntireCarAssembly
{
    public class BasicScrew : SteppedObject, IScrewedObject, IHighlightObject
    {
        protected GameObject arrow;
        public float maxAngle = 10f;
        public float distanceBetweenScrew2Arrow = 0.05f;
        private Material originalMat;
        public EnumScrewType _screwType;
        public GameObject[] highlightMeshArr;
        [SerializeField]
        private bool _isCanbeScrewed = true;
        [SerializeField]
        private bool _isScrewed = false;
        protected float currentStep = 0;
        protected float stepSum = 200;
        //protected Color previousColor;
        protected Color originalColor;
        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            if (highlightMeshArr.Length == 0)
            {
                originalMat = this.GetComponentInChildren<MeshRenderer>().material;
            }
            else
            {
                originalMat = highlightMeshArr[0].GetComponentInChildren<MeshRenderer>().material;
            }
            originalColor = originalMat.GetColor("_Color");
        }
        private void InitEvent()
        {
            m_steppedObjectStartEventHandle += (sender, e) =>
            {
                m_originalPos = transform.position;
                m_originalLocalRot = transform.localRotation;
                arrow = Instantiate(Resources.Load("Prefabs/Arrow") as GameObject, transform.position - transform.forward * distanceBetweenScrew2Arrow, transform.rotation);
            };
            m_steppedObjectFinishedEventHandle += (sender, e) =>
            {
                Destroy(arrow);
            };
        }
        public EnumScrewType ScrewType
        {
            get
            {
                return _screwType;
            }

            set
            {
                _screwType = value;
            }
        }
        public bool IsCanbeScrewed { get { return _isCanbeScrewed; } set { _isCanbeScrewed = value; } }
        public bool IsScrewed { get { return _isScrewed; } set { _isScrewed = true; } }
        public void ToggleHighlight(bool toggle)
        {

        }
        private void ChangeAllColor(GameObject target, Color color)
        {
            MeshRenderer[] mrArr = target.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in mrArr)
            {
                Material temp = mr.material;
                temp.SetColor("_EmissionColor", color);
                temp.SetColor("_Color", color);
            }
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
        }
        protected virtual void OnTriggerExit(Collider other)
        {
#if Debug
            print("TriggerExit");
#endif
            ScrewDriver screwDriver = other.GetComponentInParent<ScrewDriver>();
            if (screwDriver && screwDriver.ScrewType == this.ScrewType && IsStarted && !IsFinished)
            {
                ToggleHighlight(false);
            }
        }
        protected virtual void OnTriggerStay(Collider other)
        {
            ScrewDriver screwDriver = other.GetComponentInParent<ScrewDriver>();
            if (screwDriver && screwDriver.ScrewType == this.ScrewType)
            {
                if (IsStarted && !IsFinished)
                {
                    if (JudgeProbeDirection(other.transform, this.transform, maxAngle))
                    {
                        ToggleHighlight(true);
                    }
                    else
                    {
                        ToggleHighlight(false);
                    }
                }

            }

        }
        protected bool JudgeProbeDirection(Transform probe, Transform screw, float maxAngle)
        {
            var angle = Vector3.Angle(probe.forward, screw.forward);
            if (angle <= maxAngle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
