using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiTool.Utilities
{
    public abstract class GameEventMonoBehaviour : SerializedMonoBehaviour
    {
        protected virtual void Awake() { }
        protected virtual void Start() { }
        protected virtual void Update() { }
        protected virtual void FixedUpdate() { }

        protected virtual void OnEnable() { SubscribeEvent(); }
        protected virtual void OnDisable() { UnsubscribeEvent(); }
        protected virtual void OnDestroy() { UnsubscribeEvent(); }

        protected abstract void SubscribeEvent();
        protected abstract void UnsubscribeEvent();
    }
}