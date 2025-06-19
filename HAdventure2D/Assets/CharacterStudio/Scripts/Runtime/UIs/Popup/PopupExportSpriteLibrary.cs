using System.IO;
#if UNITY_EDITOR
#endif
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class PopupExportSpriteLibrary : PopupBase
    {
        [SerializeField] Button _explorerButton;
        [SerializeField] Button _exportButton;
        override public ePopupType PopupType => ePopupType.ExportSpriteLibrary;
        public override void Show()
        {
            base.Show();

            _exportButton.onClick.AddListener(OnExportButtonClicked);
        }

        private void OnExportButtonClicked()
        {
            string path = DataManager.Instance.GetExportedFolderPath();
            if (!Directory.Exists(path))
                return;
            if (string.IsNullOrEmpty(_name.text))
                return;
            string exportFolder = Path.Combine(path, _name.text);
            if (!Directory.Exists(exportFolder))
                Directory.CreateDirectory(exportFolder);
            EventBus.Instance.Publish(new SpriteLibraryExportArg(exportFolder, _name.text));
        }
    }
}
