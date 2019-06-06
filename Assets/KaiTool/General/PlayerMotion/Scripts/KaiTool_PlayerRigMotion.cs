#define Debug
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.PlayerMotion
{
    public struct PlayerRigMotionEventArgs
    {
        public Vector3 m_direction;
        public float m_forceIndensity;
        public float m_impulseTime;
    }
    [RequireComponent(typeof(Rigidbody))]
    public class KaiTool_PlayerRigMotion : MonoBehaviour
    {
        [SerializeField]
        private float m_moveCoefficient = 10f;
        private Rigidbody m_rigidbody;
        [SerializeField]
        private UnityEvent m_playerMove;
        [SerializeField]
        private UnityEvent m_playerBrake;
        [SerializeField]
        private float m_intervalTime = 0.02f;
        public Action<UnityEngine.Object, PlayerRigMotionEventArgs> m_playerMoveEventHandle;
        public Action<UnityEngine.Object, PlayerRigMotionEventArgs> m_playerBrakeEventHandle;

        public void OnPlayerMove(PlayerRigMotionEventArgs e)
        {
            if (m_playerMove != null)
            {
                m_playerMove.Invoke();
            }
            if (m_playerMoveEventHandle != null)
            {
                m_playerMoveEventHandle(this, e);
            }
            StartCoroutine(MoveEnumerator(e.m_direction, e.m_forceIndensity, e.m_impulseTime));
        }
        public void OnPlayerBrake(PlayerRigMotionEventArgs e)
        {
            if (m_playerBrake != null)
            {
                m_playerBrake.Invoke();
            }
            if (m_playerBrakeEventHandle != null)
            {
                m_playerBrakeEventHandle(this, e);
            }
        }
        private void Awake()
        {
            Init();
        }
        protected virtual void Init()
        {
            InitVar();
        }
        private void InitVar()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }
        private IEnumerator MoveEnumerator(Vector3 m_direction, float m_forceIndensity, float m_impulseTime)
        {
            var wait = new WaitForSeconds(m_intervalTime);
            var steps = m_impulseTime / m_intervalTime;
            steps = steps > 1 ? steps : 1;
            for (int i = 0; i < steps; i++)
            {
                m_rigidbody.AddForce(m_direction * m_forceIndensity);
                yield return wait;
            }
        }
        private void OnValidate()
        {

        }
#if Debug
        private void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            if (x != 0 || y != 0)
            {
                var args = new PlayerRigMotionEventArgs();
                var vec = new Vector3(x, 0, y);
                args.m_direction = vec.normalized;
                args.m_forceIndensity = m_moveCoefficient * vec.magnitude;
              //  args.m_impulseTime = m_intervalTime;
                OnPlayerMove(args);
            }
        }
#endif

    }
}