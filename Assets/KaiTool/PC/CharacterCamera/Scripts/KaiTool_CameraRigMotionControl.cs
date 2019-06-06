using System.Collections;
using UnityEngine;
namespace KaiTool.PC
{
    public class KaiTool_CameraRigMotionControl : MonoBehaviour
    {
        [SerializeField]
        protected float m_intervalTime = 0.03f;
        private Transform m_camera;
        private Coroutine m_translateCoroutine;
        private Coroutine m_rotateCoroutine;
        private void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {
            InitVar();
        }
        private void Start()
        {
            m_translateCoroutine = StartCoroutine(TranslateEnumerator());
            m_rotateCoroutine = StartCoroutine(RotateEnumerator());
        }
        private void InitVar()
        {
            m_camera = Camera.main.transform;
        }
        private IEnumerator TranslateEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            while (true)
            {

                yield return wait;
            }
        }
        private IEnumerator RotateEnumerator()
        {
            var wait = new WaitForSeconds(m_intervalTime);
            while (true)
            {

                yield return wait;
            }
        }


    }
}