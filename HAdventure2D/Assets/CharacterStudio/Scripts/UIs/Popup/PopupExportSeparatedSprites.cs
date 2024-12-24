using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor;
using SimpleFileBrowser;
using NaughtyAttributes;
using System.IO;

namespace CharacterStudio
{
    public class PopupExportSeparatedSprites : PopupBase
    {
        [SerializeField] Button _explorerButton;
        [SerializeField] TMP_Text _pathText;
        [SerializeField] Button _exportButton;
        [ShowNativeProperty] override public ePopupType PopupType => ePopupType.ExportSeparatedSprites;
        bool _pathSelected = false;
        public override void Show()
        {
            base.Show();

            _exportButton.onClick.AddListener(OnExportButtonClicked);
            _explorerButton.onClick.AddListener(OnExplorerButtonClicked);
        }

        private void OnExportButtonClicked()
        {
            if (!_pathSelected)
                return;
            if (!Directory.Exists(_pathText.text))
                return;
            EventBus.Instance.Publish(new ExportArg(eExportType.SeparatedSprites, _pathText.text));
        }

        private void OnBrowseCancel()
        {
        }

        private void OnBrowseSuccess(string[] paths)
        {
            if (paths.Length > 0)
                _pathText.text = paths[0];
        }

        private void OnExplorerButtonClicked()
        {
            if ( FileBrowser.IsOpen )
            {
                return;
            }
            string initialPath = string.IsNullOrEmpty( _pathText.text ) ? Application.dataPath + "/CharacterStudio/" : _pathText.text;
#if UNITY_EDITOR
            string selectedPath = EditorUtility.OpenFolderPanel( "Choose save folder", initialPath, "" );
            if ( !string.IsNullOrEmpty( selectedPath ) )
            {
                _pathSelected = true;
                _pathText.text = selectedPath;
            }
            else
            {
                _pathSelected = false;
            }
#else
            _pathSelected = FileBrowser.ShowSaveDialog(OnBrowseSuccess, OnBrowseCancel, FileBrowser.PickMode.Folders, initialPath: initialPath, title: "Choose save folder");
#endif
        }
    }
}
