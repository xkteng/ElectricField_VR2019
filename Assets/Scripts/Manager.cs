using Electricity;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR2019
{
    public class Manager : SerializedMonoBehaviour
    {
        [SerializeField]
        private IGameObjectLoader m_gameObjectLoader;
        [SerializeField]
        private IElectricityManager m_electricityManager;

        [SerializeField]
        private Transform m_playerSpawn;
        [SerializeField]
        private Transform m_platesSpawn;
        [SerializeField]
        private Transform m_roundplatesSpawn;

        private GameObject m_plates = null;
        private GameObject m_rplates = null;

        private Manager() { }
        private static Manager s_instance = null;
        

        public static Manager Instance
        {
            get
            {
                return s_instance;
            }
        }
        private void Awake()
        {
            s_instance = this;
        }
        private void Start()
        {
            m_gameObjectLoader.LoadGameObject("PlayerRig", m_playerSpawn.position, m_playerSpawn.rotation, m_playerSpawn);
            LoadPlates();
        }
        private void LoadPlates()
        {
            m_plates = m_gameObjectLoader.LoadGameObject("Plates", m_platesSpawn.position, m_platesSpawn.rotation, m_platesSpawn);
            m_electricityManager.Reset();
        }
        private void LoadRoundPlates()
        {
            m_rplates = m_gameObjectLoader.LoadGameObject("RoundPlates", m_roundplatesSpawn.position, m_roundplatesSpawn.rotation, m_roundplatesSpawn);
            m_electricityManager.Reset();
        }
        private void UnloadPlates()
        {
            if (m_plates)
            {
                DestroyImmediate(m_plates);
            }
        }
        private void UnloadRoundPlates()
        {
            if (m_rplates)
            {
                DestroyImmediate(m_rplates);
            }
        }
        [Button("RefreshPlates")]
        public void RefreshPlates()
        {
            m_electricityManager.Refresh();
        }
        [Button("SetPlates")]
        public void ResetPlates()
        {
            UnloadPlates();
            UnloadRoundPlates();
            LoadPlates();
        }
        [Button("SetRoundPlates")]
        public void ResetRoundPlates()
        {
            UnloadPlates();
            UnloadRoundPlates();
            LoadRoundPlates();
        }
    }
}


