using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace KaiTool.KaiTool_Editor
{
    public class EnumFlags : PropertyAttribute { }
    public class EnumFlagsAttributeDrawe : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //base.OnGUI(position, property, label);
            property.intValue = EditorGUI.MaskField(position, label, property.intValue
                                               , property.enumNames);
        }
    }
}