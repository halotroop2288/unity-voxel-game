using UnityEngine;
using UnityEngine.UI;
using Minecraft;

#pragma warning disable CS0649

namespace UnifiedFactorization.GUI {
	public sealed class OptionsMenu : AbstractMenu {
        [Header("Inputs")]
        [SerializeField] private Slider m_RenderRadius;
        [SerializeField] private Slider m_HorizontalFOV;
        [SerializeField] private Slider m_MaxChunkCountInMemory;
        [SerializeField] private Slider m_MaxTaskCountPerFrame;
        [SerializeField] private Toggle m_EnableDestroyEffects;

        private void OnEnable() {
            m_RenderRadius.value = GlobalSettings.Instance.RenderChunkRadius;
            m_HorizontalFOV.value = GlobalSettings.Instance.HorizontalFOVInDEG;
            m_MaxChunkCountInMemory.value = GlobalSettings.Instance.MaxChunkCountInMemory;
            m_MaxTaskCountPerFrame.value = GlobalSettings.Instance.MaxTaskCountPerFrame;
            m_EnableDestroyEffects.isOn = GlobalSettings.Instance.EnableDestroyEffect;
        }

        public override void Close() {
            GlobalSettings.Instance.RenderChunkRadius = (int)m_RenderRadius.value;
            GlobalSettings.Instance.HorizontalFOVInDEG = m_HorizontalFOV.value;
            GlobalSettings.Instance.MaxChunkCountInMemory = (int)m_MaxChunkCountInMemory.value;
            GlobalSettings.Instance.MaxTaskCountPerFrame = (int)m_MaxTaskCountPerFrame.value;
            GlobalSettings.Instance.EnableDestroyEffect = m_EnableDestroyEffects.isOn;
            GlobalSettings.SaveSettings();

            base.Close();
        }
    }
}
