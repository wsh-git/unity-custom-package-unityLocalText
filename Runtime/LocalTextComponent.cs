using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wsh.LocalText {

	[RequireComponent(typeof(Text))]
    public class LocalTextComponent : MonoBehaviour {

        [Serializable]
        public class FontCustom {
            public LanguageType languageType;
            public Font font;
            public bool isStatic;
            public float fontSize;
        }

        private static LocalTextManager m_localTextManager;

        public string localType;
        public List<FontCustom> fontCustoms;
        private Text m_text;

        void Start() {
            m_text = transform.GetComponent<Text>();
            InitFont();
            if(!string.IsNullOrEmpty(localType)) {
                m_localTextManager.SetText(m_text, localType);
            }
        }

        private void InitFont() {
            if(m_localTextManager != null && fontCustoms.Count > 0) {
                var fontCustom = fontCustoms.Find(x => x.languageType == m_localTextManager.LanguageType);
                if(fontCustom != null) {
                    m_text.font = fontCustom.font;
                    if(fontCustom.isStatic) {
                        if(fontCustom.fontSize != 0) {
                            transform.localScale = new Vector3(fontCustom.fontSize, fontCustom.fontSize, fontCustom.fontSize);
                        }
                    } else {
                        m_text.fontSize = (int)fontCustom.fontSize;
                    }
                }
            }
        }

        public void ResetFont() {
            InitFont();
        }

        public static void SetLocalTextManager(LocalTextManager localTextManager) {
            m_localTextManager = localTextManager;
        }
    }
}
