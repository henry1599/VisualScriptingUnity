using System.Collections;
using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
using SimpleFileBrowser;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class PopupExportSpriteLibrary : PopupBase
    {
        [SerializeField] Button _explorerButton;
        [SerializeField] TMP_Text _pathText;
        [SerializeField] Button _exportButton;
        [ShowNativeProperty] override public ePopupType PopupType => ePopupType.ExportSpriteLibrary;
        public override void Show()
        {
            base.Show();

            _exportButton.onClick.AddListener(OnExportButtonClicked);
            _explorerButton.onClick.AddListener(OnExplorerButtonClicked);
        }

        private void OnExportButtonClicked()
        {
            if (!Directory.Exists(_pathText.text))
                return;
            if (string.IsNullOrEmpty(_name.text))
                return;
            string exportFolder = Path.Combine(_pathText.text, _name.text);
            if (!Directory.Exists(exportFolder))
                Directory.CreateDirectory(exportFolder);
            EventBus.Instance.Publish(new SpriteLibraryExportArg(exportFolder));
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
                _pathText.text = selectedPath;
            }
#else
            _pathSelected = FileBrowser.ShowSaveDialog(OnBrowseSuccess, OnBrowseCancel, FileBrowser.PickMode.Folders, initialPath: initialPath, title: "Choose save folder");
#endif
        }
    }
}
