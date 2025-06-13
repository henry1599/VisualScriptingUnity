using System.IO;
using NaughtyAttributes;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class PopupExportSpriteSheet : PopupBase
    {
        [SerializeField] Button _explorerButton;
        [SerializeField] Button _exportButton;
        [SerializeField] Toggle _autoSliceToggle;
        [ShowNativeProperty] override public ePopupType PopupType => ePopupType.ExportSpriteSheet;
        public override void Show()
        {
            base.Show();

            _exportButton.onClick.AddListener( OnExportButtonClicked );
        }

        private void OnExportButtonClicked()
        {
            string path = DataManager.Instance.GetExportedFolderPath();
            if ( !Directory.Exists( path ) )
                return;
            if (string.IsNullOrEmpty(_name.text))
                return;
            string exportFolder = Path.Combine(path, _name.text);
            if (!Directory.Exists(exportFolder))
                Directory.CreateDirectory(exportFolder);
            EventBus.Instance.Publish( new SpritesheetExportArg( exportFolder, _autoSliceToggle.isOn, _name.text ) );
        }
    }
}
