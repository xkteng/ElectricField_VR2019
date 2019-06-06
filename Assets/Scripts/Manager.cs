using Electricity;
using Sirenix.OdinInspector;
using System;
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
        [SerializeField]
        private Transform m_newPlateSpawn;

        private GameObject m_plates = null;
        private GameObject m_rplates = null;
        private GameObject m_plates12 = null;
        private GameObject m_rplates12 = null;
        private GameObject m_pplate = null;
        private GameObject m_nplate = null;
        private GameObject m_prplate = null;
        private GameObject m_nrplate = null;



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
        private void LoadPlatesOneVSTwo()
        {
            m_plates12 = m_gameObjectLoader.LoadGameObject("PlatesOneVSTwo", m_platesSpawn.position, m_platesSpawn.rotation, m_platesSpawn);
            m_electricityManager.Reset();
        }
        private void LoadRoundPlatesOneVSTwo()
        {
            m_rplates12 = m_gameObjectLoader.LoadGameObject("RoundPlatesOneVSTwo", m_roundplatesSpawn.position, m_roundplatesSpawn.rotation, m_roundplatesSpawn);
            m_electricityManager.Reset();
        }
        private void LoadPositivePlate()
        {
            m_pplate = m_gameObjectLoader.LoadGameObject("PositivePlate", m_newPlateSpawn.position, m_newPlateSpawn.rotation, m_newPlateSpawn);
            m_electricityManager.Reset();
        }
        private void LoadNegativePlate()
        {
            m_nplate = m_gameObjectLoader.LoadGameObject("NegativePlate", m_newPlateSpawn.position, m_newPlateSpawn.rotation, m_newPlateSpawn);
            m_electricityManager.Reset();
        }
        private void LoadPositiveRoundPlate()
        {
            m_prplate = m_gameObjectLoader.LoadGameObject("PositiveRoundPlate", m_newPlateSpawn.position, m_newPlateSpawn.rotation, m_newPlateSpawn);
            m_electricityManager.Reset();
        }
        private void LoadNegativeRoundPlate()
        {
            m_nrplate = m_gameObjectLoader.LoadGameObject("NegativeRoundPlate", m_newPlateSpawn.position, m_newPlateSpawn.rotation, m_newPlateSpawn);
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
        private void UnloadPlates12()
        {
            if (m_plates12)
            {
                DestroyImmediate(m_plates12);
            }
        }
        private void UnloadRoundPlates12()
        {
            if (m_rplates12)
            {
                DestroyImmediate(m_rplates12);
            }
        }
        private void UnloadPositivePlate()
        {
            if (m_pplate)
            {
                DestroyImmediate(m_pplate);
            }
        }
        private void UnloadNegativePlate()
        {
            if (m_nplate)
            {
                DestroyImmediate(m_nplate);
            }
        }
        private void UnloadPositiveRoundPlate()
        {
            if (m_prplate)
            {
                DestroyImmediate(m_prplate);
            }
        }
        private void UnloadNegativeRoundPlate()
        {
            if (m_nrplate)
            {
                DestroyImmediate(m_nrplate);
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
            UnloadPlates12();
            UnloadRoundPlates12();
            UnloadPositivePlate();
            UnloadNegativePlate();
            UnloadPositiveRoundPlate();
            UnloadNegativeRoundPlate();
            LoadPlates();
        }
        [Button("SetRoundPlates")]
        public void ResetRoundPlates()
        {
            UnloadPlates();
            UnloadRoundPlates();
            UnloadPlates12();
            UnloadRoundPlates12();
            UnloadPositivePlate();
            UnloadNegativePlate();
            UnloadPositiveRoundPlate();
            UnloadNegativeRoundPlate();
            LoadRoundPlates();
        }
        [Button("SquarePlates1vs2")]
        public void ResetPlatesOneVSTwo()
        {
            UnloadPlates();
            UnloadRoundPlates();
            UnloadPlates12();
            UnloadRoundPlates12();
            UnloadPositivePlate();
            UnloadNegativePlate();
            UnloadPositiveRoundPlate();
            UnloadNegativeRoundPlate();
            LoadPlatesOneVSTwo();
        }
        [Button("RoundPlates1vs2")]
        public void ResetRoundPlatesOneVSTwo()
        {
            UnloadPlates();
            UnloadRoundPlates();
            UnloadPlates12();
            UnloadRoundPlates12();
            UnloadPositivePlate();
            UnloadNegativePlate();
            UnloadPositiveRoundPlate();
            UnloadNegativeRoundPlate();
            LoadRoundPlatesOneVSTwo();
        }
        [Button("AddPositivePlate")]
        public void AddPositivePlate()
        {
            LoadPositivePlate();
        }
        [Button("AddNegativePlate")]
        public void AddNegativePlate()
        {
            LoadNegativePlate();
        }
        [Button("AddPositiveRoundPlate")]
        public void AddPositiveRoundPlate()
        {
            LoadPositiveRoundPlate();
        }
        [Button("AddNegativeRoundPlate")]
        public void AddNegativeRoundPlate()
        {
            LoadNegativeRoundPlate();
        }

    }
}


