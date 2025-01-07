using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using SimpleFileBrowser;
using UnityEditor;

namespace CharacterStudio
{
    public class CSSettingPanel : MonoBehaviour
    {
        [SerializeField] CanvasGroup _canvasGroup;
        [SerializeField] Button _backButton;
        [SerializeField] Button _choosePathButton;
        [SerializeField] TMP_Text _pathText;
        [SerializeField] Button _removeButton;
        public bool HasDataFolderPath
        {
            get 
            {
                return !string.IsNullOrEmpty( DataManager.Instance?.SaveData?.DataFolderPath );
            }
        }
        public void Setup()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            _choosePathButton.onClick.AddListener( OnClickChoosePath );
            _removeButton.onClick.AddListener( OnClickRemove );
            _backButton.onClick.AddListener(OnBackButtonClicked);
            UpdatePathText();
        }

        private void OnBackButtonClicked()
        {
            EventBus.Instance.Publish(new OnBackToPreviousLayoutArg());
        }

        public void Unsetup()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            // DataManager.Instance.Save();
            _choosePathButton.onClick.RemoveAllListeners();
            _removeButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }

        private void OnClickChoosePath()
        {
// #if UNITY_EDITOR
//             string selectedPath = EditorUtility.OpenFolderPanel( "Choose DATA folder", Application.dataPath, "" );
//             if ( !string.IsNullOrEmpty( selectedPath ) )
//             {
//                 DataManager.Instance.SaveData.DataFolderPath = selectedPath;
//                 DataManager.Instance.Save();
//                 UpdatePathText();
//             }
// #else
            FileBrowser.ShowSaveDialog( OnBrowseSuccess, OnBrowseCancel, FileBrowser.PickMode.Folders, initialPath: Application.dataPath, title: "Choose DATA folder" );
// #endif
        }

        private void OnBrowseCancel()
        {
        }

        private void OnBrowseSuccess(string[] paths)
        {
            if (paths.Length > 0)
            {
                DataManager.Instance.SaveData.DataFolderPath = paths[0];
                DataManager.Instance.Save();
                UpdatePathText();
            }
        }

        private void OnClickRemove()
        {
            DataManager.Instance.SaveData.DataFolderPath = "";
            DataManager.Instance.Save();
            UpdatePathText();
        }

        void UpdatePathText()
        {
            var userData = DataManager.Instance.SaveData;
            _pathText.text = userData?.DataFolderPath ?? "...";
        }
    }
}
