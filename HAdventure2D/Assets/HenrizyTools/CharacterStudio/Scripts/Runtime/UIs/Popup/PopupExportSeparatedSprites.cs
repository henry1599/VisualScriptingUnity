using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
#endif
using System.IO;

namespace CharacterStudio
{
    public class PopupExportSeparatedSprites : PopupBase
    {
        [SerializeField] Button _explorerButton;
        [SerializeField] Button _exportButton;
        override public ePopupType PopupType => ePopupType.ExportSeparatedSprites;
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
            EventBus.Instance.Publish(new SeparatedSpritesExportArg(exportFolder, _name.text));
        }
    }
}
