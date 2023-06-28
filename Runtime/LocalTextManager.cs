using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wsh.LocalText {

    public class LocalTextManager {

        public LanguageType LanguageType { get { return m_languageType; } }

        private LanguageType m_languageType;
        private Dictionary<string, LocalTextDataDefineClass> m_dicLocalText;

        public void SetLanguage(LanguageType languageType) {
            m_languageType = languageType;
        }

        public void Init(LocalTextConfigDefine define, LanguageType languageType) {
            InitDicLocalText(define);
            SetLanguage(languageType);
            LocalTextComponent.SetLocalTextManager(this);
        }

        public void InitAsync(LocalTextConfigDefine define, LanguageType languageType, Action onComplete) {
            InitDicLocalText(define);
            SetLanguage(languageType);
            LocalTextComponent.SetLocalTextManager(this);
            onComplete?.Invoke();
        }
        
        private void InitDicLocalText(LocalTextConfigDefine define) {
            m_dicLocalText = new Dictionary<string, LocalTextDataDefineClass>();
            for(int i = 0; i < define.LocalTextDataDefine.Count; i++) {
                var dataDefine = define.LocalTextDataDefine[i];
                if(m_dicLocalText.ContainsKey(dataDefine.key)) {
                    Debug.LogError($"LocalTextConfigDefine contain the same key '{dataDefine.key}'");
                } else {
                    m_dicLocalText.Add(dataDefine.key, dataDefine);
                }
            }
        }

        public void DeinitAsync(Action onComplete) {
            
        }
        
        public string GetText(string localTextKey, params object[] args) {
            return string.Format(GetText(localTextKey), args);
        }

        public string GetText(string localTextKey) {
            if(m_dicLocalText == null) {
                Debug.LogError("LocalTextManager dont init success");
            }
            if(m_dicLocalText.ContainsKey(localTextKey)) {
                switch(m_languageType) {
                    case LanguageType.CN:
                        return m_dicLocalText[localTextKey].cn.text;
                    case LanguageType.EN:
                        return m_dicLocalText[localTextKey].en.text;
                    default:
                        return "";
                }
            } else {
                Debug.LogError($"LocalTextConfigDefine dont define the key '{localTextKey}'");
            }
            return string.Empty;
        }

        private void SetTextComponentInfo(Text textComponent, string text) {
            textComponent.text = text;
        }

        public void SetText(Text textComponent, string localTextKey) {
            if(m_dicLocalText.ContainsKey(localTextKey)) {
                var define = m_dicLocalText[localTextKey];
                switch(m_languageType) {
                    case LanguageType.CN:
                        SetTextComponentInfo(textComponent, define.cn.text);
                    break;
                    case LanguageType.EN:
                        SetTextComponentInfo(textComponent, define.en.text);
                    break;
                    default:
                        
                    break;
                }
            } else {
                Debug.LogError($"LocalTextDefine dont define the key {localTextKey}");
            }
        }
    }
}
