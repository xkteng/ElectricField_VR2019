using EntireCarAssembly;
using KaiTool.Utilities;
using KaiTool.VR;
using System.Collections;
using UnityEngine;
using VRTK;
namespace KaiTool.AssemblyPart
{
    public class ScrewDriver : VRTK_InteractiveAssistant, IScrewingObject
    {

        [SerializeField]
        private EnumScrewType _screwType;
        private WaitForSeconds waitForJudgeIfBeingUsed;
        [Header("Haptic Variables")]
        public float hapticPulseStrength = 1f;
        public float hapticPulseDuration = 0.2f;
        public float hapticPulseInterval = 0.01f;
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
        protected override void Init()
        {
            base.Init();
            InitVar();
            InitEvent();
        }
        private void InitVar() {
            waitForJudgeIfBeingUsed = new WaitForSeconds(hapticPulseDuration);

        }
        private void InitEvent() {

            InterObject.InteractableObjectUsed += (sender, e) => {
                StopAllCoroutines();
                GameObject usingObj = InterObject.GetUsingObject();
               // print(usingObj);
            };
        }
        IEnumerator StartHapticUntilUnused(GameObject usingObj) {
            VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(usingObj);
            while (InterObject.IsUsing()) {
                VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, hapticPulseStrength, hapticPulseDuration, hapticPulseInterval);
                    yield return waitForJudgeIfBeingUsed;
                }

                var lastTime = 5;
            for (int i = 0; i <= lastTime; i++)
            {
                float currentStrengh = Mathf.Lerp(hapticPulseStrength, 2*hapticPulseStrength/3, (float)i / lastTime);
                VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, currentStrengh, hapticPulseDuration, hapticPulseInterval);
                yield return new WaitForSeconds(0.05f);
            }
            //--------------------------------------------
            for (int i=0;i<=lastTime;i++) {
                float currentStrengh = Mathf.Lerp(2 * hapticPulseStrength / 3 ,0,(float)i/lastTime);
                VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, currentStrengh, hapticPulseDuration, hapticPulseInterval);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}