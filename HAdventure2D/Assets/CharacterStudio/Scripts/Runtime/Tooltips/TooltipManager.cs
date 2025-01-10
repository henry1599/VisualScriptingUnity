using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class TooltipManager : MonoSingleton<TooltipManager>
    {
        [SerializeField] Tooltip tooltipPrefab;
        private Tooltip tooltipObject;
        private Canvas mainCanvas
        {
            get
            {
                if (_mainCanvas == null)
                {
                    _mainCanvas = FindObjectOfType<Canvas>();
                }
                return _mainCanvas;
            }
        } private Canvas _mainCanvas;
        private Vector2 mouseScreenPosition
        {
            get
            {
                return Input.mousePosition;
            }
        }
        protected override bool Awake()
        {
            if (tooltipObject == null)
            {
                tooltipObject = Instantiate(tooltipPrefab, mainCanvas.transform);
            }
            return base.Awake();
        }
        private void Start()
        {
            tooltipObject.gameObject.SetActive(false);
        }
        void Update()
        {
            Tooltipable tooltipable = UIManager.GetFirstTooltipableObjectOnMouseHover();
            Shortcutable shortcutable = tooltipable?.GetComponent<Shortcutable>();
            if (shortcutable != null)
            {
                string key = shortcutable.Data.Key.ToString();
                if (!tooltipable.Data.Description.Contains("(Shortcut:"))
                {
                    tooltipable.Data.Description = $"(Shortcut: {key}) {tooltipable.Data.Description}";
                }
            }
            if (tooltipable == null)
            {
                this.tooltipObject.Hide();
                return;
            }
            this.tooltipObject.Show(mainCanvas, tooltipable.Data, mouseScreenPosition);
        }
    }
}
