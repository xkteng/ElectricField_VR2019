using Sirenix.OdinInspector;
using System;
using UnityEngine;
namespace KaiTool.Utilities
{
    public class Singleton<T> : SerializedMonoBehaviour where T : Singleton<T>
    {
        protected static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    T[] instanceArray = FindObjectsOfType<T>();
                    if (instanceArray.Length > 1)
                    {
                        throw new MoreThanOneSingletonException();
                    }
                    else
                    {
                        try
                        {
                            _instance = instanceArray[0];
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            print(e.Message);
                        }

                    }
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        protected virtual void Awake()
        {
        }
        protected virtual void OnEnable()
        {
            SubscribeEvent();
        }
        protected virtual void OnDestroy()
        {
            SetInstanceNull();
            UnsubscribeEvent();
        }
        protected virtual void OnDisable()
        {
            UnsubscribeEvent();
        }
        protected virtual void Start() { }
        private void SetInstanceNull()
        {
            _instance = null;
        }

        protected virtual void SubscribeEvent() { }
        protected virtual void UnsubscribeEvent() { }

    }
    public class MoreThanOneSingletonException : Exception
    {
        public MoreThanOneSingletonException() : base("There is more than one singleton in the scene.")
        {
        }
        public MoreThanOneSingletonException(string Message) : base(Message)
        {

        }
    }
}
