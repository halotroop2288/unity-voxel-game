using TMPro;
using UnityEngine;

#pragma warning disable CS0649

namespace Minecraft
{
	public sealed class LoadingMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_Text;
        private float m_Time = 100;
        private uint m_Count = 0;

		private void Start() {
            m_Text.text = "Loading";
        }

		private void Update() {
			m_Time += Time.deltaTime;

            if (m_Time < 1f)
                return;

            m_Time = 0;

            if (m_Count == 50)
                m_Text.text = "Still loading...";
            else if (m_Count == 100)
                m_Text.text = "Loading is taking a long time";
            else
                m_Text.text += ".";

            m_Count++;
        }
    }
}