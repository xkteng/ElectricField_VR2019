using UnityEditor;
using UnityEngine;

namespace KaiTool
{
    [CustomPropertyDrawer(typeof(EnumFlags))]
    public class EnumFlagsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            /*����ö�ٸ�ѡ�� �� 0-Nothing��-1-Everything,������ö��֮��
            ö��ֵ��2��x���ݣ���2��0����=1��2��1����=2��2��2����=4��8��16...
            */
            property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
        }
    }
}