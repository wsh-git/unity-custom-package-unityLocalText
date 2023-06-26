using System.Text;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Wsh.LocalText {

    [CustomEditor(typeof(LocalTextConfigDefine))]
    public class LocalTextConfigDefineEditor : Editor {
        
        private LocalTextConfigDefine define;
        private SerializedProperty generateCSharpPath;
        private SerializedProperty localTextDataDefine;

        private void OnEnable() {
            generateCSharpPath = serializedObject.FindProperty("GenerateCSharpPath");
            localTextDataDefine = serializedObject.FindProperty("LocalTextDataDefine");
            define = (LocalTextConfigDefine)target;
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(generateCSharpPath);
            EditorGUILayout.PropertyField(localTextDataDefine, true);

            if(EditorGUI.EndChangeCheck()) {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(define);
            }
            if(GUILayout.Button("Auto Generate (SoundId) CSharp Script")) {
                Generate();
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

        private void Generate() {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("//Automatically generated, do not manually modify it!!!\n\n");
            stringBuilder.Append("namespace Wsh.LocalText {\n\n");
            stringBuilder.Append("    public class LocalTextPath {\n");
            
            for(int i = 0; i < define.LocalTextDataDefine.Count; i++) {
                GenerateVar(stringBuilder, define.LocalTextDataDefine[i].key);
            }
            stringBuilder.Append("    }\n\n");
            stringBuilder.Append("}");
            string path = Path.Combine(Application.dataPath, define.GenerateCSharpPath, "LocalTextPath.cs");
            File.WriteAllText(path, stringBuilder.ToString());
            AssetDatabase.Refresh();
        }

        private void GenerateVar(StringBuilder stringBuilder, string key) {
            stringBuilder.Append("        ");
            stringBuilder.Append("public const string ");
            stringBuilder.Append(key.ToUpper());
            stringBuilder.Append(" = ");
            stringBuilder.Append("\"");
            stringBuilder.Append(key);
            stringBuilder.Append("\"");
            stringBuilder.Append(";");
            stringBuilder.Append("\n");
        }
    }

}
