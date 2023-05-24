using UnityEditor;

namespace Wsh.LocalText {

    [CustomEditor(typeof(LocalTextConfigDefine))]
    public class LocalTextConfigDefineEditor : UnityEditor.Editor {
        
        private LocalTextConfigDefine define;
        private SerializedProperty localTextDataDefine;

        private void OnEnable() {
            localTextDataDefine = serializedObject.FindProperty("LocalTextDataDefine");
            define = (LocalTextConfigDefine)target;
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(localTextDataDefine, true);
            if(EditorGUI.EndChangeCheck()) {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(define);
            }
        }

    }

}
