//#define Debug
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
namespace KaiTool.VR.VRUI
{
    public struct HandUIEventArgs {

    }
    public abstract class BasicHandUI : MonoBehaviour
    {
        public GameObject hand;
        public float maxShowingAngle = 30f;
        [Range(0f, 1f)] public float scaleChangeDuration = 0.2f;
        [Range(10, 30)] public int scaleChangeSmooth = 20;
        public delegate void HandUIEventHandle(UnityEngine.Object sender, HandUIEventArgs e);
        public HandUIEventHandle GazePointEnter;
        public HandUIEventHandle GazePointStay;
        public HandUIEventHandle GazePointExist;
        private Transform childTranArr;
        private Coroutine currentScaleChangingCoroutine;
        private WaitForSeconds waitForJudge = new WaitForSeconds(0.1f);

        public virtual void ResetHandUILayout() { }

        protected virtual void Awake()
        {
            Init();
        }
        protected virtual void Update() {
#if Debug
            if (Input.GetKeyDown(KeyCode.O)) {
                Appear();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                DisAppear();
            }
#endif
        }
        protected virtual void Start() { }


        private void Init() {
            InitVar();
            InitEvent();
        }
        private void InitVar() { }
        private void InitEvent() { }
        public void OnGazePointEnter(UnityEngine.Object sender, HandUIEventArgs e) {
            if (GazePointEnter != null) {
                GazePointEnter(sender, e);
            }
        }
        public void OnGazePointStay(UnityEngine.Object sender, HandUIEventArgs e) {
            if (GazePointStay != null) {
                GazePointStay(sender, e);
            }
        }
        public void OnGazePointExist(UnityEngine.Object sender, HandUIEventArgs e) {
            if (GazePointExist != null) {
                GazePointExist(sender, e);
            }
        }

        private IEnumerator JudgeEvent() {
            var HeadSet = VRTK_DeviceFinder.HeadsetCamera();
            while (true) {

                yield return waitForJudge;
            }
        }

        private void ToggleActive(bool toggle) {
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(toggle);
            }
        }
        private void Appear() {
            ScaleTransit(Vector3.one,scaleChangeDuration,transform);
        }
        private void DisAppear() {
            ScaleTransit(Vector3.zero, scaleChangeDuration, transform);
        }
        private void ScaleTransit(Vector3 finalScale, float duration, Transform target) {
            if(currentScaleChangingCoroutine!=null){
                StopCoroutine(currentScaleChangingCoroutine);
            }
            currentScaleChangingCoroutine=StartCoroutine(ScaleTransitSubroutine(finalScale,duration, target));
        }
        private IEnumerator ScaleTransitSubroutine( Vector3 finalScale, float duration,Transform target) {
            int times = scaleChangeSmooth;
            var originalScale = target.localScale;
            WaitForSeconds wait = new WaitForSeconds(duration/times);
            for (int i=0;i<times;i++) {
                var temp = Vector3.Lerp(originalScale,finalScale,(float)i/(times-1));
                target.localScale = temp;
                yield return wait;
            }
        }
    }
}