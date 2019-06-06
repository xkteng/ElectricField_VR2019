using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace KaiTool.Utilities
{

    public class KT_LevelLoader : MonoBehaviour, ILevelLoader
    {
        private const float LOAD_COMPLETE_DELAY = 0.5f;

        [SerializeField]
        private KT_LevelAssembly m_assembly;
        private Coroutine m_loadCoroutine;
        private LevelName m_currenLevelName = LevelName.None;

        private float m_progress_current = 0f;
        private float m_progress_target = 0f;
        private AsyncOperation m_asyncOp = null;

        private LevelLoadEvent m_levelLoaded = new LevelLoadEvent();
        private FloatValueEvent m_updateLoadingProgress = new FloatValueEvent();

        public LevelLoadEvent LevelLoaded => m_levelLoaded;

        public FloatValueEvent UpdateLoadingProgress => m_updateLoadingProgress;

        public void LoadLevel(LevelName levelName, bool useLoadingPanel)
        {

            if (m_loadCoroutine != null)
            {
                return;
            }
            m_loadCoroutine = StartCoroutine(LoadLevelEnumerator(levelName));
            if (useLoadingPanel)
            {
                StartCoroutine(SetLoadingPanelProgress(levelName, m_assembly.m_levelAssemblyDic[levelName].Length));
            }
        }
        public void LoadLevel(LevelName levelName)
        {
            LoadLevel(levelName, false);
        }

        #region PRIVATE_PROTECTED_METHODS
        private IEnumerator LoadLevelEnumerator(LevelName levelName)
        {
            if (!m_assembly.m_levelAssemblyDic.ContainsKey(levelName))
            {
                throw new System.Exception("There is no such level name in the dictionary.");
            }
            yield return new WaitForSeconds(1f);
            if (m_assembly.m_levelAssemblyDic.ContainsKey(m_currenLevelName) && m_currenLevelName != LevelName.None)
            {
                yield return UnloadScenes(m_assembly.m_levelAssemblyDic[m_currenLevelName]);
            }
            yield return LoadScenes(m_assembly.m_levelAssemblyDic[levelName]);
            m_currenLevelName = levelName;
            var x = 0f;
            DOTween.To(() => x, (v) => x = v, 0f, LOAD_COMPLETE_DELAY).OnComplete(() =>
            {
                m_levelLoaded.Invoke(levelName);
                m_loadCoroutine = null;
            });

        }
        private IEnumerator LoadScenes(string[] sceneNames)
        {
            if (sceneNames == null)
            {
                throw new System.Exception("scenes array loaded can not be null;");
            }
            for (int i = 0; i < sceneNames.Length; i++)
            {
                yield return SceneManager.LoadSceneAsync(sceneNames[i], LoadSceneMode.Additive);
                m_progress_target = (float)(i + 2) / sceneNames.Length;
            }

        }
        private IEnumerator UnloadScenes(string[] sceneNames)
        {
            if (sceneNames == null)
            {
                throw new System.Exception("scenes array unloaded can not be null;");
            }
            foreach (var item in sceneNames)
            {
                yield return SceneManager.UnloadSceneAsync(item);
            }
        }
        private IEnumerator SetLoadingPanelProgress(LevelName levelName, int length)
        {
            m_progress_target = 1f / length;
            m_progress_current = 0f;
            while (m_progress_target <= 1f)
            {
                m_progress_current = Mathf.Lerp(m_progress_current, m_progress_target, 0.02f);
                //GameEvent.Local_Invoke<float>(CommandType.UpdateLoadingProgress, m_progress_current);
                //GameEvent.UpdateLoadingProgress.Invoke(m_progress_current);
                m_updateLoadingProgress.Invoke(m_progress_current);

                yield return new WaitForEndOfFrame();
            }
            //GameEvent.UpdateLoadingProgress.Invoke(1f);
            m_updateLoadingProgress.Invoke(1f);
        }
        #endregion
    }
}