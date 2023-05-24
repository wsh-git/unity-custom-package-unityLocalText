using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wsh.LocalText {

    [Serializable]
    public class LocalTextContent {
        public string text;
        public int fontSize;
    }

    [Serializable]
	public class LocalTextDataOldDefineClass {
        public string cn;
        public int cnFontSize;
        public string en;
        public int enFontSize;
    }

    [Serializable]
    public class LocalTextDataDefineClass {
        public string key;
        public LocalTextContent cn;
        public LocalTextContent en;
    }

    [CreateAssetMenu(fileName = "LocalTextConfigDefine", menuName = "Custom/ScriptableObject/LocalTextConfigDefine")]
    public class LocalTextConfigDefine : ScriptableObject {
        public List<LocalTextDataDefineClass> LocalTextDataDefine = new List<LocalTextDataDefineClass>();
    }
}
