using System;
using System.Collections;
using System.Collections.Generic;
using SimpleFileBrowser;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class CSPresettingLayout : MonoBehaviour
    {
        [SerializeField] Button _choosePathButton;
        [SerializeField] TMP_Text _pathText;
        [SerializeField] Button _removeButton;
        [SerializeField] Button _confirmButton;
        public bool HasDataFolderPath
        {
            get 
            {
                return !string.IsNullOrEmpty( DataManager.Instance?.SaveData?.DataFolderPath );
            }
        }
        public void Setup()
        {
            _choosePathButton.onClick.AddListener( OnClickChoosePath );
            _removeButton.onClick.AddListener( OnClickRemove );
            _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
            UpdatePathText();
        }

        private void OnConfirmButtonClicked()
        {
            if (HasDataFolderPath)
            {
                DataManager.Instance.Save();
                DataManager.Instance.InitConfigs();
                EventBus.Instance.Publish(new TransitionArg("CharacterStudio"));
            }
        }
        public void Unsetup()
        {
            _choosePathButton.onClick.RemoveAllListeners();
            _removeButton.onClick.RemoveAllListeners();
        }

        private void OnClickChoosePath()
        {
            FileBrowser.ShowSaveDialog( OnBrowseSuccess, OnBrowseCancel, FileBrowser.PickMode.Folders, initialPath: Application.dataPath, title: "Choose DATA folder" );
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
