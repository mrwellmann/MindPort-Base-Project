using UnityEditor;
using UnityEngine;
using System;

[CustomPropertyDrawer(typeof(SerializableGuid))]
public class GuidDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		SerializedProperty serializedGuid = property.FindPropertyRelative("serializedGuid");

		EditorGUI.BeginProperty(position, label, serializedGuid);

		// Ensure the byte array is the correct size for a Guid
		if (serializedGuid.arraySize == 16)
		{
			byte[] guidBytes = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				SerializedProperty byteProperty = serializedGuid.GetArrayElementAtIndex(i);
				guidBytes[i] = (byte)byteProperty.intValue;
			}

			Guid guidValue = new Guid(guidBytes);

			// Display the Guid as a string in a label field, making it not editable
			EditorGUI.LabelField(position, label, new GUIContent(guidValue.ToString()));
		}
		else
		{
			// Display a warning or handle the unexpected array size accordingly
			EditorGUI.LabelField(position, label, new GUIContent("Invalid GUID"));
		}

		EditorGUI.EndProperty();
	}
}
