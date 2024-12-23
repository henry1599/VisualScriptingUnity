using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

            SetAnimation(_currentAnimation);   
            SelectDefault();
            ApplySelection();
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
