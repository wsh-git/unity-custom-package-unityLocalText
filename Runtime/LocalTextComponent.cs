using UnityEngine;
using UnityEngine.UI;

namespace Wsh.LocalText {

	[RequireComponent(typeof(Text))]
    public class LocalTextComponent : MonoBehaviour {

        private static LocalTextManager m_localTextManager;

        public string localType;

        void Start() {
            if(!string.IsNullOrEmpty(localType)) {
                m_localTextManager.SetText(transform.GetComponent<Text>(), localType);
            }
        }

        public static void SetLocalTextManager(LocalTextManager localTextManager) {
            m_localTextManager = localTextManager;
        }
    }
}
