//***********************************************
//By Kai
//Description：
//
//
//***********************************************
using SeperateUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KaiTool.Utilities
{
    public class PlayerLoadGameLevelStartEvent : UnityEvent<LevelName, bool> { }
    public class PlayerLoadGameLevelEndEvent : UnityEvent<LevelName> { }
    public class FloatValueEvent : UnityEvent<float> { }
    public class Manager : Singleton<Manager>
    {
        [SerializeField]
        private LevelName m_defaultName;
        [SerializeField]
        private ILevelLoader m_levelLoader;
        [SerializeField]
        private IGameObjectLoader m_gameObjectLoader;
        [SerializeField]
        private IUIController m_uiController;
        [HideInInspector]
        public PlayerLoadGameLevelStartEvent PlayerLoadGameLevel_Start = new PlayerLoadGameLevelStartEvent();
        [HideInInspector]
        public PlayerLoadGameLevelEndEvent PlayerLoadGameLevel_End = new PlayerLoadGameLevelEndEvent();
        [HideInInspector]
        public FloatValueEvent UpdateLoadingProgress = new FloatValueEvent();

        public GameObject LoadGameObject(string key, Vector3 position, Quaternion rotation, Transform parent)
        {
            return m_gameObjectLoader.LoadGameObject(key, position, rotation, parent);
        }
        public GameObject LoadGameObject(string key, Transform spawnPoint)
        {
            return m_gameObjectLoader.LoadGameObject(key, spawnPoint);
        }

        protected override void Start()
        {
            base.Start();
            m_levelLoader.LoadLevel(m_defaultName);
        }
        protected override void SubscribeEvent()
        {
            base.SubscribeEvent();
            m_levelLoader.LevelLoaded.AddListener(OnLevelLoaderLoadLevel);
            m_levelLoader.UpdateLoadingProgress.AddListener(OnLevelLoaderUpdateLoadingProgress);
            PlayerLoadGameLevel_Start.AddListener(OnPlayerLoadGameLevel_Start);
            PlayerLoadGameLevel_End.AddListener(OnPlayerLoadGameLevel_End);

        }
        protected override void UnsubscribeEvent()
        {
            base.UnsubscribeEvent();
            m_levelLoader.LevelLoaded.RemoveListener(OnLevelLoaderLoadLevel);
            m_levelLoader.UpdateLoadingProgress.RemoveListener(OnLevelLoaderUpdateLoadingProgress);
            PlayerLoadGameLevel_Start.RemoveListener(OnPlayerLoadGameLevel_Start);
            PlayerLoadGameLevel_End.RemoveListener(OnPlayerLoadGameLevel_End);
        }
        private void OnLevelLoaderLoadLevel(LevelName levelName)
        {
            PlayerLoadGameLevel_End.Invoke(levelName);
        }
        private void OnLevelLoaderUpdateLoadingProgress(float progress)
        {
            UpdateLoadingProgress.Invoke(progress);
        }
        private void OnPlayerLoadGameLevel_Start(LevelName levelName, bool useLoadingPanel)
        {
            m_levelLoader.LoadLevel(levelName, useLoadingPanel);
            m_uiController.OnPlayerLoadGameLevel_Start(levelName, useLoadingPanel);
        }
        private void OnPlayerLoadGameLevel_End(LevelName levelName)
        {
            m_uiController.OnPlayerLoadGameLevel_End(levelName);
        }

    }
}