using System.Text;
using UnityEditor;
using UnityEngine;

namespace Wsh.LocalText {

    [CustomEditor(typeof(LocalTextConfigDefine))]
    public class LocalTextConfigDefineEditor : Editor {
        
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

            if(GUILayout.Button("文本另存为...")) {
                SaveAs();
            }
        }

        private void SaveFileAs(string value) {
            string path = EditorUtility.SaveFilePanel("本地化文件另存为：", Application.dataPath, "LocalTextDefine", "txt");
            System.IO.File.WriteAllText(path, value);
        }

        private void SaveAs() {
            StringBuilder stringBuilder = new StringBuilder();
            for(int i = 0; i < define.LocalTextDataDefine.Count; i++) {
                stringBuilder.Append((i+1).ToString() + ". ");
                stringBuilder.Append(define.LocalTextDataDefine[i].key);
                stringBuilder.Append("\n    cn: ");
                stringBuilder.Append(define.LocalTextDataDefine[i].cn.text);
                stringBuilder.Append("\n    en: ");
                stringBuilder.Append(define.LocalTextDataDefine[i].en.text);
                stringBuilder.Append("\n");
            }
            SaveFileAs(stringBuilder.ToString());
        }
    }

}
