using EntireCarAssembly;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace KaiTool.VR.VRUI
{
    public class LeftHandUI : BasicHandUI
    {
        [Header("UpPanel")]
        public GameObject[] upPanelArr;
        private Hashtable upPanelTable = new Hashtable();
        [Header("MiddlePanel")]
        public GameObject[] middlePanelArr;
        private Hashtable middlePanelTable = new Hashtable();
        [Header("DownPanel")]
        public GameObject[] downPanelArr;
        private Hashtable downPanelTable = new Hashtable();
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        [Header("LeftHand UI")]
        public Text timeText;

        public override void ResetHandUILayout()
        {
            base.ResetHandUILayout();
            ShowNonePanelOfUpPanel();
        }
        #region UpPanel Method
        public void ShowNonePanelOfUpPanel() {
            ShowPanelFromPanelArr((string)upPanelTable[EnumUpPanelType.None],upPanelArr);
        }
        
        public void ShowStartButtonPanelOfUpPanel() {
            ShowPanelFromPanelArr((string)upPanelTable[EnumUpPanelType.StartButtonPanel],upPanelArr);
        }
        public void ShowStartButtonPanelOfUpPanelInTime(float time) {
            ShowPanelFromPanelArrInTime((string)upPanelTable[EnumUpPanelType.StartButtonPanel], upPanelArr, time);
        }
        public void ShowTimePanelOfUpPanel() {
            ShowPanelFromPanelArr((string)upPanelTable[EnumUpPanelType.TimePanel],upPanelArr);
        }

        #endregion
        #region MiddlePanel Method
        public void ShowPromptPanelOfMiddlePanel() {
            ShowPanelFromPanelArr((string)middlePanelTable[EnumMiddlePanelType.PromptPanel],middlePanelArr);
        }
        public void ShowPromptPanelOfMiddlePanelInTime(float time) {
            ShowPanelFromPanelArrInTime((string)middlePanelTable[EnumMiddlePanelType.PromptPanel], middlePanelArr,time);
        }

        public void ShowLoadScenePanel() {
            ShowPanelFromPanelArr((string)middlePanelTable[EnumMiddlePanelType.LoadScenePanel], middlePanelArr);
        }

        #endregion
        #region DownPanel Method
        public void ShowSettingPanelOfDownPane() {
            ShowPanelFromPanelArr((string)downPanelTable[EnumDownPanelType.SettingPanel], downPanelArr);
        }
        public void ShowVolumeSettingPanelOfDownPanel() {
            ShowPanelFromPanelArr((string)downPanelTable[EnumDownPanelType.VolumeSettingPanel],downPanelArr);
        }
        #endregion

        protected override void Awake()
        {
            base.Awake();
            Init();
        }
        protected override void Start()
        {
            base.Start();
            StartCoroutine(PrintRecordTime());
        }
        protected override void Update()
        {
            base.Update();
        }
        private void Init() {
            InitVar();
            InitEvent();
        }
        private void InitVar() {
            //UpPanel
            upPanelTable.Add(EnumUpPanelType.None, "NonePanel");
            upPanelTable.Add(EnumUpPanelType.StartButtonPanel, "StartButtonPanel");
            upPanelTable.Add(EnumUpPanelType.TimePanel, "TimePanel");
            //MiddlePanel
            middlePanelTable.Add(EnumMiddlePanelType.None, "NonePanel");
            middlePanelTable.Add(EnumMiddlePanelType.LoadScenePanel, "LoadScenePanel");
            middlePanelTable.Add(EnumMiddlePanelType.PromptPanel, "PromptPanel");
            //DownPanel
            downPanelTable.Add(EnumDownPanelType.None, "NonePanel");
            downPanelTable.Add(EnumDownPanelType.VolumeSettingPanel, "VolumeSettingPanel");
            downPanelTable.Add(EnumDownPanelType.SettingPanel, "SettingPanel");


        }
        private void InitEvent() { }
        IEnumerator PrintRecordTime() {
            while (true) {
                if (gameObject.activeInHierarchy) {
                }
                yield return wait;
            }
        }
        /// <summary>
        /// Invoke this when StartButton On the LeftHandUI Clicked.
        /// </summary>
        public void OnHandUIStartButtonClicked()
        {
            ShowPanelFromPanelArr((string)upPanelTable[EnumUpPanelType.TimePanel], upPanelArr);

        }
        private  void ShowPanelFromPanelArr(string panelName,GameObject[] panelArr) {
            foreach (GameObject obj in panelArr) {
                if (obj.name != panelName)
                {
                    obj.SetActive(false);
                    continue;
                }
                else {
                    obj.SetActive(true);
                }
            }
        }
        private  void ShowPanelFromPanelArrInTime(string panelName, GameObject[] panelArr,float time) {
            StartCoroutine(WaitAndShowPanelFromArr(panelName,panelArr,time));
        }
        IEnumerator WaitAndShowPanelFromArr(string panelName, GameObject[] panelArr,float time) {
            WaitForSeconds wait = new WaitForSeconds(time);
            yield return wait;
            ShowPanelFromPanelArr(panelName,panelArr);
        }
    }
    public enum EnumUpPanelType
    {
        None,
        StartButtonPanel,
        TimePanel
    }
    public enum EnumMiddlePanelType
    {
        None,
        LoadScenePanel,
        PromptPanel
    }
    public enum EnumDownPanelType
    {
        None,
        VolumeSettingPanel,
        SettingPanel
    }
}