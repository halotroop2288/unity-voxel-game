using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

#pragma warning disable CS0649

namespace Minecraft {
    public sealed class SelectWorldMenu : AbstractMenu {
        private static string saveFolderPath;

        [Header("Menus")]
        [SerializeField] private AbstractMenu m_NewWorldMenu;

        [Header("World List")]
        [SerializeField] private Transform m_Content;
        [SerializeField] private GameObject m_WorldButtonTemplate;

        [Header("Buttons")]
        [SerializeField] public GameObject backButton;
        [SerializeField] public Button loadButton;
        [SerializeField] public Button deleteButton;
        [SerializeField] private List<GameObject> worldButtonList = new List<GameObject>();
        private GameObject selectedWorldButton;

        private void RefreshWorldList() {
            // Clear the list
            worldButtonList.ForEach(Destroy);

            saveFolderPath = Application.persistentDataPath + "/Worlds";

            // Create the directory if it doesn't exist
            if (!Directory.Exists(saveFolderPath))
                Directory.CreateDirectory(saveFolderPath);
            
            string[] folders = Directory.GetDirectories(saveFolderPath);
            Array.Sort(folders, (w1, w2) => File.GetLastWriteTime(w1) > File.GetLastWriteTime(w2) ? 1 : -1);

            // Add each world to the list
            foreach (string folderName in folders) {
                // Check if world settings exists
                if (!File.Exists(folderName + "/settings.json"))
                    return; // if not, return. This is not a valid world.

                GameObject worldButton = Instantiate(m_WorldButtonTemplate, m_Content, true);
                worldButton.GetComponentInChildren<TextMeshProUGUI>().text = Path.GetFileNameWithoutExtension(folderName);
                
                byte[] bytes = File.ReadAllBytes(folderName + "/Thumbnail.png");
                Texture2D thumbnail = new Texture2D(1920, 1080);
                thumbnail.LoadImage(bytes);
                worldButton.GetComponentInChildren<RawImage>().texture = thumbnail;

                worldButton.GetComponent<Button>().onClick.AddListener(() => {
                    worldButton.GetComponent<Button>().Select();
                    selectedWorldButton = worldButton;
                });

                worldButton.SetActive(true);
                worldButtonList.Add(worldButton);
            }
        }

		private void Update() {
            // Enable buttons if a world is selected
            bool worldSelected = (selectedWorldButton != null);
            loadButton.interactable = worldSelected;
            deleteButton.interactable = worldSelected;

        }

		public void NewWorld() {
            m_NewWorldMenu.Open();
        }

        public void DeleteWorld() {
            string name = selectedWorldButton.GetComponentInChildren<TextMeshProUGUI>().text;
            Directory.Delete(saveFolderPath + "/" + name, true);
			this.RefreshWorldList();
        }

        public void LoadWorld() {
            string name = selectedWorldButton.GetComponentInChildren<TextMeshProUGUI>().text;
            string json = File.ReadAllText(saveFolderPath + "/" + name + "/settings.json");
            WorldSettings.Instance = JsonUtility.FromJson<WorldSettings>(json);
            SceneManager.LoadScene(1);
        }

		private void OnEnable() {
            this.RefreshWorldList();
        }

		public void OnBackButtonClick() {
            selectedWorldButton = null;
            this.OnBackButtonPressed();
        }
    }
}