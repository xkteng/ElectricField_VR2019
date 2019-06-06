//#define Debug
using EntireCarAssembly;
using KaiTool.Utilities;
using UnityEngine;
namespace KaiTool.AssemblyPart
{
    public class Screw : SteppedObject, IScrewedObject, IHighlightObject
    {
        public GameObject arrow;
        private Material originalMat;
        public EnumScrewType _screwType;
        [SerializeField]
        private bool _isCanbeScrewed = true;
        [SerializeField]
        private bool _isScrewed = false;
        [SerializeField]
        private float _depth = 0.01f;
        private float currentStep = 0;
        private float stepSum = 200;
        // private Vector3 originalPos;
        private Color previousColor;
        //private Color previousEmissionColor;
        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }

        private void InitVar()
        {
            // transform.position = originalPos + transform.forward * 1f;//

            originalMat = this.GetComponentInChildren<MeshRenderer>().material;
            if (arrow)
            {
                arrow.SetActive(false);
            }
        }
        private void InitEvent()
        {
            m_steppedObjectStartEventHandle += (sender, e) =>
            {
                m_originalPos = transform.position;
                if (arrow)
                {
                    arrow.SetActive(true);
                }
            };
            m_steppedObjectFinishedEventHandle += (sender, e) =>
             {
                 if (arrow)
                 {
                     arrow.SetActive(false);
                 }
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
        public float Depth { get { return _depth; } set { _depth = Depth; } }

        public void ToggleHighlight(bool toggle)
        {

            if (toggle)
            {
                //previousColor = GetComponentInChildren<MeshRenderer>().material.GetColor("_Color");
                Color highlightColor = new Color(0f, 1f, 0f, 1f);
                ChangeAllColor(highlightColor);
            }
            else
            {

                Color normalColor = new Color(0, 1f, 1f, 1f);
                // Color normalColor = previousColor;
                ChangeAllColor(normalColor);


            }
        }
        private void ChangeAllColor(Color color)
        {
            MeshRenderer[] mrArr = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in mrArr)
            {
                Material temp = mr.material;
                temp.SetColor("_EmissionColor", color);
                temp.SetColor("_Color", color);
            }
        }
        protected void OnTriggerEnter(Collider other)
        {
#if Debug
            print("TriggerEnter");
#endif
            ScrewDriver screwDriver = other.GetComponentInParent<ScrewDriver>();
            if (screwDriver && screwDriver.ScrewType == this.ScrewType && IsStarted && !IsFinished)
            {
                ToggleHighlight(true);
            }
        }
        protected void OnTriggerExit(Collider other)
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
        protected void OnTriggerStay(Collider other)
        {
#if Debug
            //print("TriggerStay");
#endif
            if (currentStep < stepSum)
            {
                ScrewDriver screwDriver = other.GetComponentInParent<ScrewDriver>();
                if (screwDriver && screwDriver.ScrewType == this.ScrewType && screwDriver.InterObject.IsUsing())
                {
                    currentStep++;
                    transform.position = m_originalPos + transform.forward * Mathf.Lerp(0, Depth, (float)currentStep / stepSum);
                }
            }
            if (currentStep >= stepSum)
            {
                // IsFinished = true;
                IsScrewed = true;
                OnFinishSteppedObject(new SteppedObjectEventArgs());
            }
        }

    }
}
