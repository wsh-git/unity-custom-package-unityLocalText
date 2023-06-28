using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wsh.LocalText {

    [Serializable]
    public class LocalTextContent {
        public string text;
    }

    [Serializable]
    public class LocalTextDataDefineClass {
        public string key;
        public LocalTextContent cn;
        public LocalTextContent en;
    }

    [CreateAssetMenu(fileName = "LocalTextConfigDefine", menuName = "Custom/ScriptableObject/LocalTextConfigDefine")]
    public class LocalTextConfigDefine : ScriptableObject {
        public string GenerateCSharpPath;
        public List<LocalTextDataDefineClass> LocalTextDataDefine = new List<LocalTextDataDefineClass>();
    }
}
