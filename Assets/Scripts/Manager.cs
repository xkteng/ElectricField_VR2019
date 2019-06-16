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

        private List<GameObject> m_platesList = new List<GameObject>();


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
            m_platesList.Add(m_gameObjectLoader.LoadGameObject("Plates", m_platesSpawn.position, m_platesSpawn.rotation, m_platesSpawn));
            m_electricityManager.Reset();
        }
        private void LoadRoundPlates()
        {
            m_platesList.Add(m_gameObjectLoader.LoadGameObject("RoundPlates", m_roundplatesSpawn.position, m_roundplatesSpawn.rotation, m_roundplatesSpawn));
            m_electricityManager.Reset();
        }
        private void LoadPlatesOneVSTwo()
        {
            m_platesList.Add(m_gameObjectLoader.LoadGameObject("PlatesOneVSTwo", m_platesSpawn.position, m_platesSpawn.rotation, m_platesSpawn));
            m_electricityManager.Reset();
        }
        private void LoadRoundPlatesOneVSTwo()
        {
            m_platesList.Add(m_gameObjectLoader.LoadGameObject("RoundPlatesOneVSTwo", m_roundplatesSpawn.position, m_roundplatesSpawn.rotation, m_roundplatesSpawn));
            m_electricityManager.Reset();
        }
        private void LoadPositivePlate()
        {
            m_platesList.Add(m_gameObjectLoader.LoadGameObject("PositivePlate", m_newPlateSpawn.position, m_newPlateSpawn.rotation, m_newPlateSpawn));
            m_electricityManager.Reset();
        }
        private void LoadNegativePlate()
        {
            m_platesList.Add(m_gameObjectLoader.LoadGameObject("NegativePlate", m_newPlateSpawn.position, m_newPlateSpawn.rotation, m_newPlateSpawn));
            m_electricityManager.Reset();
        }
        private void LoadPositiveRoundPlate()
        {
            m_platesList.Add(m_gameObjectLoader.LoadGameObject("PositiveRoundPlate", m_newPlateSpawn.position, m_newPlateSpawn.rotation, m_newPlateSpawn));
            m_electricityManager.Reset();
        }
        private void LoadNegativeRoundPlate()
        {
            m_platesList.Add(m_gameObjectLoader.LoadGameObject("NegativeRoundPlate", m_newPlateSpawn.position, m_newPlateSpawn.rotation, m_newPlateSpawn));
            m_electricityManager.Reset();
        }

        private void UnloadPlates()
        {
            if (m_platesList.Count > 0)
            {
                foreach (var item in m_platesList)
                {
                    DestroyImmediate(item);
                }
                m_platesList.Clear();
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
            LoadPlates();
        }
        [Button("SetRoundPlates")]
        public void ResetRoundPlates()
        {
            UnloadPlates();
            LoadRoundPlates();
        }
        [Button("SquarePlates1vs2")]
        public void ResetPlatesOneVSTwo()
        {
            UnloadPlates();
            LoadPlatesOneVSTwo();
        }
        [Button("RoundPlates1vs2")]
        public void ResetRoundPlatesOneVSTwo()
        {
            UnloadPlates();
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


