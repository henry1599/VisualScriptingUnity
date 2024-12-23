using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] Transform _spriteContainer;
        [SerializeField] AnimationDatabase _animationDatabase;
        [SerializeField] CharacterDatabase _characterDatabase;
        [SerializeField] MapDatabase _mapDatabase;
        [SerializeField] eCharacterAnimation _currentAnimation;
        private int _frameIndex = 0;
        private float _counter = 0;
        private float _animationInterval = 0.15f;
        private bool _isSetup = false;

        [SerializeField] private SerializedDictionary<eCharacterPart, string> _characterSelection = new SerializedDictionary<eCharacterPart, string>();




        private EventSubscription<ChangePartArg> _changePartSubscription;
        private EventSubscription<ChangeAnimationArg> _changeAnimationSubscription;
        private EventSubscription<ExportArg> _exportSubscription;



        private Dictionary<eCharacterPart, SpriteRenderer> _spriteRenderers = new Dictionary<eCharacterPart, SpriteRenderer>();
        private Dictionary<eCharacterAnimation, Dictionary<eCharacterPart, List<Texture2D>>> _currentAnimationTexturesMap = new Dictionary<eCharacterAnimation, Dictionary<eCharacterPart, List<Texture2D>>>();
        private Dictionary<eCharacterAnimation, Dictionary<eCharacterPart, List<Texture2D>>> _currentAnimationTextures = new Dictionary<eCharacterAnimation, Dictionary<eCharacterPart, List<Texture2D>>>();
        public void SetAnimation(eCharacterAnimation newAnimation)
        {
            int childCount = _spriteContainer.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                Destroy(_spriteContainer.GetChild(i).gameObject);
            }
            _spriteRenderers = new Dictionary<eCharacterPart, SpriteRenderer>();
            _currentAnimationTexturesMap = new Dictionary<eCharacterAnimation, Dictionary<eCharacterPart, List<Texture2D>>>();


            _currentAnimation = newAnimation;
            _frameIndex = 0;

            if (!_animationDatabase.Data.TryGetValue(newAnimation, out AnimationData animationData))
            {
                Debug.LogError("Animation not found in database: " + newAnimation);
                return;
            }
            foreach (var (part, data) in animationData.AnimationsByPart)
            {
                GameObject spriteObject = new GameObject(part.ToString());
                spriteObject.transform.SetParent(_spriteContainer);
                SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = _characterDatabase.SortedData[part];
                _spriteRenderers.TryAdd(part, spriteRenderer);
                _currentAnimationTexturesMap.TryAdd(newAnimation, new Dictionary<eCharacterPart, List<Texture2D>>());
                _currentAnimationTexturesMap[newAnimation].TryAdd(part, data.Textures);
            }
            _currentAnimationTextures = new ();
            _animationInterval = _animationDatabase.AnimationInterval;
            _counter = _animationInterval;

            _isSetup = true;
        }
        public void UpdateTexture(eCharacterPart part, string id)
        {
            if (!_currentAnimationTextures.ContainsKey(_currentAnimation))
            {
                _currentAnimationTextures.TryAdd(_currentAnimation, new Dictionary<eCharacterPart, List<Texture2D>>());
            }
            Texture2D baseTexture = _characterDatabase.Data[part].TextureDict[id];
            var map = CSUtils.LoadMappedColors(_mapDatabase.Data[part], baseTexture);
            var data = _currentAnimationTexturesMap[_currentAnimation];
            if (!data.TryGetValue(part, out List<Texture2D> textures))
            {
                return;
            }
            for (int i = 0; i < textures.Count; i++)
            {
                Texture2D generatedTexture = CSUtils.GenerateTexture(textures[i], baseTexture, map);
                _currentAnimationTextures[_currentAnimation].TryAdd(part, new List<Texture2D>(textures.Count));
                if (i >= 0 && i < _currentAnimationTextures[_currentAnimation][part].Count)
                {
                    _currentAnimationTextures[_currentAnimation][part][i] = generatedTexture;
                }
                else
                {
                    _currentAnimationTextures[_currentAnimation][part].Add(generatedTexture);
                }
            }
        }
        void Start()
        {
            _changePartSubscription = EventBus.Instance.Subscribe<ChangePartArg>(OnChangePart);
            _changeAnimationSubscription = EventBus.Instance.Subscribe<ChangeAnimationArg>(OnChangeAnimation);
            _exportSubscription = EventBus.Instance.Subscribe<ExportArg>(OnExport);

            SetAnimation(_currentAnimation);   
            SelectDefault();
            ApplySelection();
        }

        private void OnExport(ExportArg arg)
        {
            switch (arg.ExportType)
            {
                case eExportType.SpriteSheet:
                    ExportSpriteSheet(arg);
                    break;
                case eExportType.SeparatedSprites:
                    ExportSeparatedSprites(arg);
                    break;
                case eExportType.SpriteLibrary:
                    ExportSpriteLibrary(arg);
                    break;
                case eExportType.All:
                    ExportSeparatedSprites(arg);
                    ExportSpriteLibrary(arg);
                    ExportSpriteSheet(arg);
                    break;
            }
        }

        private void ExportSpriteLibrary(ExportArg arg)
        {
        }

        private void ExportSeparatedSprites(ExportArg arg)
        {
            Dictionary<eCharacterPart, Texture2D> sBaseTexture = new Dictionary<eCharacterPart, Texture2D>();
            List<eCharacterAnimation> allAnimations = _animationDatabase.Data.Keys.ToList();
            Dictionary<eCharacterPart, Dictionary<Color32, Color32>> map = new Dictionary<eCharacterPart, Dictionary<Color32, Color32>>();

            // * Load mapped colors for each parts
            foreach (var (part, data) in _characterDatabase.Data)
            {
                Texture2D baseTexture = data.TextureDict[_characterSelection[part]];
                sBaseTexture.TryAdd(part, baseTexture);
                map.TryAdd(part, CSUtils.LoadMappedColors(_mapDatabase.Data[part], baseTexture));
            }

            // * Generate textures for each animation
            foreach (var animation in allAnimations)
            {
                if (!_animationDatabase.Data.TryGetValue(animation, out AnimationData animationData))
                {
                    Debug.LogError("Animation not found in database: " + animation);
                    return;
                }
                int frameCount = animationData.AnimationsByPart.First().Value.Textures.Count;
                for (int i = 0; i < frameCount; i++)
                {
                    List<(int sortingLayer, Texture2D texture)> sortedPart = new List<(int sortingLayer, Texture2D texture)>();
                    foreach (var (part, data) in animationData.AnimationsByPart)
                    {
                        if (!map.TryGetValue(part, out Dictionary<Color32, Color32> partMap))
                        {
                            Debug.LogError("Map not found for part: " + part);
                            return;
                        }
                        Texture2D generatedTexture = CSUtils.GenerateTexture(data.Textures[i], sBaseTexture[part], partMap);
                        sortedPart.Add((_characterDatabase.SortedData[part], generatedTexture));
                    }
                    sortedPart = sortedPart.OrderBy(x => x.sortingLayer).Reverse().ToList();
                    Texture2D assembledTexture = AssembleTextures(sortedPart.Select(x => x.texture).ToList());
                    string path = arg.FolderPath + "/" + animation.ToString();
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    string fileName = animation.ToString() + "_" + i + ".png";
                    Debug.Log("Exporting: " + path + "/" + fileName);
                    CSUtils.SaveTexture(assembledTexture, path, fileName);
                }
            }
        }

        private void ExportSpriteSheet(ExportArg arg)
        {
        }
        private Texture2D AssembleTextures(List<Texture2D> textures)
        {
            int width = 0;
            int height = 0;
            foreach (var texture in textures)
            {
                width = Mathf.Max(width, texture.width);
                height = Mathf.Max(height, texture.height);
            }
            Texture2D result = new Texture2D(width, height, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp,
            };

            for (int x = 0 ; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color32 color = new Color32(0, 0, 0, 0);
                    foreach (var texture in textures)
                    {
                        if (x < texture.width && y < texture.height)
                        {
                            color = texture.GetPixel(x, y);
                            if (color.a > 0)
                            {
                                break;
                            }
                        }
                    }
                    result.SetPixel(x, y, color);
                }
            }
            result.Apply();
            return result;
        }

        private void OnDestroy()
        {
            EventBus.Instance.Unsubscribe(_changePartSubscription);
            EventBus.Instance.Unsubscribe(_changeAnimationSubscription);
            EventBus.Instance.Unsubscribe(_exportSubscription);
        }
        public void Select(eCharacterPart part, string id)
        {
            if (_characterSelection.ContainsKey(part))
            {
                _characterSelection[part] = id;
            }
            else
            {
                _characterSelection.Add(part, id);
            }
        }

        void ApplySelection()
        {
            foreach (var (part, id) in _characterSelection)
            {
                UpdateTexture(part, id);
            }
        }
        private void OnChangeAnimation(ChangeAnimationArg arg)
        {
            SetAnimation(arg.Animation);
            ApplySelection();
        }

        private void OnChangePart(ChangePartArg arg)
        {
            Select(arg.Part, arg.Id);
            ApplySelection();
        }
        void SelectDefault()
        {
            Select(eCharacterPart.Body, "Body_01");
            Select(eCharacterPart.LHand, "LHand_01");
            Select(eCharacterPart.RHand, "RHand_01");
        }
        void Update()
        {
            if (!_isSetup)
            {
                return;
            }
            if (_currentAnimationTextures.Count == 0)
            {
                return;
            }
            if (_counter > 0)
            {
                _counter -= Time.deltaTime;
                return;
            }
            _counter = _animationInterval;
            foreach (var (part, spriteRenderer) in _spriteRenderers)
            {
                if (_currentAnimationTextures[_currentAnimation].TryGetValue(part, out List<Texture2D> textures))
                {
                    if (textures.Count == 0)
                    {
                        continue;
                    }
                    Texture2D texture = textures[_frameIndex];
                    if (texture == null)
                    {
                        continue;
                    }
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    spriteRenderer.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
                }
            }
            _frameIndex++;
            if (_frameIndex >= _currentAnimationTextures[_currentAnimation][_spriteRenderers.Keys.First()].Count)
            {
                _frameIndex = 0;
            }
        }
    }
}
