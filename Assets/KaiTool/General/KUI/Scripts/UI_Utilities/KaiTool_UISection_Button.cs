using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KaiTool.UI
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(KaiTool_BasicUIObject))]
    public class KaiTool_UISection_Button : MonoBehaviour
    {
        [SerializeField]
        private int m_index = 0;
        [SerializeField]
        private KaiTool_BasicUISection m_UISection;
        [SerializeField]
        private Color m_selectedColor = Color.red;

        private Selectable.Transition m_transition;

        private Button m_button;
        private Image m_image;
        private Color m_originalColor;
        private ColorBlock m_originalColorBlock;
        private KaiTool_BasicUIObject m_UIObject;

        public void OnSelected()
        {
            m_image.color = m_selectedColor;
            if (m_transition == Selectable.Transition.ColorTint)
            {
                var colorBlock = m_originalColorBlock;
                colorBlock.colorMultiplier = 1f;
                colorBlock.normalColor = Color.white;
                colorBlock.highlightedColor = Color.white;
                colorBlock.pressedColor = Color.white;
                colorBlock.disabledColor = Color.white;
                m_button.colors = colorBlock;
            }
            //m_button.transition = Selectable.Transition.None;
            //m_button.enabled = false;
        }
        public void OnUnselected()
        {
            m_image.color = m_originalColor;
            if (m_transition == Selectable.Transition.ColorTint)
            {
                m_button.colors = m_originalColorBlock;
            }
            // m_button.transition = Selectable.Transition.ColorTint;
            //m_button.enabled = true;
        }

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            InitVar();
            InitEvent();
        }
        private void InitVar()
        {
            m_button = GetComponent<Button>();
            m_transition = m_button.transition;
            m_originalColorBlock = m_button.colors;
            m_image = GetComponent<Image>();
            m_originalColor = m_image.color;
            m_UIObject = GetComponent<KaiTool_BasicUIObject>();
        }
        private void InitEvent()
        {
            m_button.onClick.AddListener(() =>
            {
                m_UISection.ShowContent(m_index);

            });
            m_UISection.ShowContentEvent.AddListener((index) =>
            {
                if (index == m_index)
                {
                    OnSelected();
                }
                else// if (index! = -1)
                {
                    OnUnselected();
                }
            });
            m_UISection.HideAllContentsEvent.AddListener((index) =>
            {
                OnUnselected();
            });
            m_UIObject.HideUnityEvent.AddListener(() =>
            {
                //OnUnselected();
            });
        }


    }
}