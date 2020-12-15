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
        [SerializeField] private GameObject m_WorldTemplate;

        [Header("Buttons")]
        [SerializeField] public GameObject backButton;
        [SerializeField] public GameObject loadButton;
        [SerializeField] public GameObject deleteButton;
        [SerializeField] private List<GameObject> worldObjectList = new List<GameObject>();
        private GameObject selectedWorldObject;

        private void RefreshWorldList() {
            // Clear the list
            worldObjectList.ForEach(Destroy);

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

                GameObject worldObj = Instantiate(m_WorldTemplate, m_Content, true);
                worldObj.GetComponentInChildren<TextMeshProUGUI>().text = Path.GetFileNameWithoutExtension(folderName);
                
                byte[] bytes = File.ReadAllBytes(folderName + "/Thumbnail.png");
                Texture2D thumbnail = new Texture2D(1920, 1080);
                thumbnail.LoadImage(bytes);
                worldObj.GetComponentInChildren<RawImage>().texture = thumbnail;

                worldObj.GetComponent<Button>().onClick.AddListener(() => {
                    selectedWorldObject = worldObj;
                });

                worldObj.SetActive(true);
                worldObjectList.Add(worldObj);
            }
        }

		private void Update() {
            // Enable world buttons if one is selected
            if (selectedWorldObject != null) {
                loadButton.SetActive(true);
                deleteButton.SetActive(true);
            } else {
                loadButton.SetActive(false);
                deleteButton.SetActive(false);
            }
		}

		public void NewWorld() {
            m_NewWorldMenu.Open();
        }

        public void DeleteWorld() {
            string name = selectedWorldObject.GetComponentInChildren<TextMeshProUGUI>().text;
            Directory.Delete(saveFolderPath + "/" + name, true);
			this.RefreshWorldList();
        }

        public void LoadWorld() {
            string name = selectedWorldObject.GetComponentInChildren<TextMeshProUGUI>().text;
            string json = File.ReadAllText(saveFolderPath + "/" + name + "/settings.json");
            WorldSettings.Instance = JsonUtility.FromJson<WorldSettings>(json);
            SceneManager.LoadScene(1);
        }

		private void OnEnable() {
            this.RefreshWorldList();
        }

		public void OnBackButtonClick() {
            selectedWorldObject = null;
            this.OnBackButtonPressed();
        }
    }
}