using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(SoundPlaylist))]
public class SoundPlaylistInspector : UnityEditor.Editor
{
    private ReorderableList sounds;
    private SerializedProperty _data;

    protected void OnEnable()
    {
        _data = serializedObject.FindProperty("Sounds");
        sounds = new ReorderableList(serializedObject, _data, true, true, true, true);

        sounds.drawHeaderCallback = (Rect rect) =>
        {
            rect.x += 14f;
            rect.width -= 14f;

            float x = rect.x;
            float w = (rect.width - 4f);

            GUI.Label(new Rect(x, rect.y, w * 0.15f, rect.height), "Type");
            x += w * 0.15f + 4f;
            GUI.Label(new Rect(x, rect.y, w * 0.85f, rect.height), "Src");                
        };

        sounds.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var data = _data.GetArrayElementAtIndex(index);

            rect.y += 2;
            rect.height = EditorGUIUtility.singleLineHeight;


            float x = rect.x;
            float w = (rect.width - 4f);

            EditorGUI.PropertyField(new Rect(x, rect.y, w * 0.15f, rect.height), data.FindPropertyRelative("Type"), GUIContent.none);
            x += w * 0.15f + 4f;
            EditorGUI.PropertyField(new Rect(x, rect.y, w * 0.85f, rect.height), data.FindPropertyRelative("Name"), GUIContent.none);
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        sounds.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }
}