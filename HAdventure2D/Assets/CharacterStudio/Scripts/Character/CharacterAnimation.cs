using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CharacterStudio
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] Transform _spriteContainer;
        [SerializeField] AnimationDatabase _animationDatabase;
        [SerializeField] CharacterDatabase _characterDatabase;
        [SerializeField] MapDatabase _mapDatabase;
        [SerializeField] eCharacterAnimation _currentAnimation;

        private int frameIndex
        {
            get => _frameIndex;
            set
            {
                _frameIndex = value;
                EventBus.Instance.Publish(new FrameIndexUpdateArg(_frameIndex));
            }
        } private int _frameIndex = 0;
        private float _counter = 0;
        private float _animationInterval = 0.15f;
        private bool _isSetup = false;

        [SerializeField] private SerializedDictionary<eCharacterPart, string> _characterSelection = new SerializedDictionary<eCharacterPart, string>();




        private EventSubscription<ChangePartArg> _changePartSubscription;
        private EventSubscription<ChangeAnimationArg> _changeAnimationSubscription;
        private EventSubscription<ExportArg> _exportSubscription;
        private EventSubscription<ChangePartRandomlyArg> _changepartRandomlySubscription;
        private EventSubscription<ResetPartArg> _resetPartSubscription;



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
            frameIndex = 0;

            if (!_animationDatabase.Data.TryGetValue(newAnimation, out AnimationData animationData))
            {
                Debug.LogError("Animation not found in database: " + newAnimation);
                return;
            }
            EventBus.Instance.Publish(new AnimationUpdateArg(newAnimation));
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
            _changepartRandomlySubscription = EventBus.Instance.Subscribe<ChangePartRandomlyArg>( OnChangePartRandomly );
            _resetPartSubscription = EventBus.Instance.Subscribe<ResetPartArg>( OnResetPart );

            SetAnimation(_currentAnimation);   
            SelectDefault();
            ApplySelection();
        }

        private void OnResetPart( ResetPartArg arg )
        {
            foreach (var (part, id) in _characterDatabase.DefaultParts )
            {
                Select( part, id );
            }
            ApplySelection();
        }

        private void OnChangePartRandomly( ChangePartRandomlyArg arg )
        {
            List<(string id, eCharacterPart part)> randomParts = _characterDatabase.GetRandomAll();
            foreach ( var (id, part) in randomParts )
            {
                Select( part, id );
            }
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
        private void FormatSprite( string path )
        {
            // path is full path, just get the part from Assets/
            if ( string.IsNullOrEmpty( path ) || !path.Contains( "Assets" ) )
                return;
            string assetPath = path.Substring( path.IndexOf( "Assets" ) );

#if UNITY_EDITOR
            // Load the texture at the specified path
            TextureImporter textureImporter = AssetImporter.GetAtPath( assetPath ) as TextureImporter;
            if ( textureImporter != null )
            {
                // Apply texture import settings
                textureImporter.spritePixelsPerUnit = 32;
                textureImporter.textureType = TextureImporterType.Sprite;
                textureImporter.spriteImportMode = SpriteImportMode.Single;
                textureImporter.filterMode = FilterMode.Point;
                textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
                textureImporter.isReadable = true;

                // Save the changes
                textureImporter.SaveAndReimport();
            }
            AssetDatabase.Refresh();
#endif
        }
        private void FormatSpritesheet(string path)
        {
            // path is full path, just get the part from Assets/
            if ( string.IsNullOrEmpty( path ) || !path.Contains( "Assets" ) )
                return;
            string assetPath = path.Substring( path.IndexOf( "Assets" ) );

#if UNITY_EDITOR
            // Load the texture at the specified path
            TextureImporter textureImporter = AssetImporter.GetAtPath( assetPath ) as TextureImporter;
            if ( textureImporter != null )
            {
                // Apply texture import settings
                textureImporter.spritePixelsPerUnit = 32;
                textureImporter.textureType = TextureImporterType.Sprite;
                textureImporter.spriteImportMode = SpriteImportMode.Single;
                textureImporter.filterMode = FilterMode.Point;
                textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
                textureImporter.isReadable = true;
                textureImporter.spriteImportMode = SpriteImportMode.Multiple;


                // Save the changes
                textureImporter.SaveAndReimport();
            }
            AssetDatabase.Refresh();
#endif
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
                    assembledTexture = CropTexture( assembledTexture, 32f / 48f );
                    string path = arg.FolderPath + "/" + animation.ToString();
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    string fileName = animation.ToString() + "_" + i;
                    Debug.Log("Exporting: " + path + "/" + fileName);
                    CSUtils.SaveTexture(assembledTexture, path, fileName);
                    AssetDatabase.Refresh();
#if UNITY_EDITOR
                    FormatSprite( path + "/" + fileName + ".png");
#endif
                }
            }
        }

        private void ExportSpriteSheet( ExportArg arg )
        {
            Dictionary<eCharacterPart, Texture2D> sBaseTexture = new Dictionary<eCharacterPart, Texture2D>();
            List<eCharacterAnimation> allAnimations = _animationDatabase.Data.Keys.ToList();
            Dictionary<eCharacterPart, Dictionary<Color32, Color32>> map = new Dictionary<eCharacterPart, Dictionary<Color32, Color32>>();

            // Load mapped colors for each part
            foreach ( var (part, data) in _characterDatabase.Data )
            {
                Texture2D baseTexture = data.TextureDict[ _characterSelection[ part ] ];
                sBaseTexture.TryAdd( part, baseTexture );
                map.TryAdd( part, CSUtils.LoadMappedColors( _mapDatabase.Data[ part ], baseTexture ) );
            }

            // Calculate the dimensions of the sprite sheet
            int maxFrameCount = allAnimations.Max( animation => _animationDatabase.Data[ animation ].AnimationsByPart.First().Value.Textures.Count );
            int maxWidth = _characterDatabase.Data.Values.Max( data => data.TextureDict.Values.Max( texture => texture.width ) );
            int maxHeight = _characterDatabase.Data.Values.Max( data => data.TextureDict.Values.Max( texture => texture.height ) );
            int sheetWidth = maxWidth * maxFrameCount;
            int sheetHeight = maxHeight * allAnimations.Count;

            Texture2D spriteSheet = new Texture2D( sheetWidth, sheetHeight, TextureFormat.RGBA32, false ) { filterMode = FilterMode.Point };
            var colors = spriteSheet.GetPixels32();
            for ( int i = 0; i < colors.Length; i++ )
            {
                colors[ i ] = new Color32( 0, 0, 0, 0 );
            }
            spriteSheet.SetPixels32( colors );

            // Generate textures for each animation and assemble them into the sprite sheet
            for ( int animIndex = 0; animIndex < allAnimations.Count; animIndex++ )
            {
                var animation = allAnimations[ animIndex ];
                if ( !_animationDatabase.Data.TryGetValue( animation, out AnimationData animationData ) )
                {
                    Debug.LogError( "Animation not found in database: " + animation );
                    return;
                }
                int frameCount = animationData.AnimationsByPart.First().Value.Textures.Count;
                for ( int frameIndex = 0; frameIndex < frameCount; frameIndex++ )
                {
                    List<(int sortingLayer, Texture2D texture)> sortedPart = new List<(int sortingLayer, Texture2D texture)>();
                    foreach ( var (part, data) in animationData.AnimationsByPart )
                    {
                        if ( !map.TryGetValue( part, out Dictionary<Color32, Color32> partMap ) )
                        {
                            Debug.LogError( "Map not found for part: " + part );
                            return;
                        }
                        Texture2D generatedTexture = CSUtils.GenerateTexture( data.Textures[ frameIndex ], sBaseTexture[ part ], partMap );
                        sortedPart.Add( (_characterDatabase.SortedData[ part ], generatedTexture) );
                    }
                    sortedPart = sortedPart.OrderBy( x => x.sortingLayer ).Reverse().ToList();
                    Texture2D assembledTexture = AssembleTextures( sortedPart.Select( x => x.texture ).ToList() );

                    // Copy the assembled texture to the sprite sheet
                    int xOffset = frameIndex * maxWidth;
                    int yOffset = animIndex * maxHeight;
                    for ( int x = 0; x < assembledTexture.width; x++ )
                    {
                        for ( int y = 0; y < assembledTexture.height; y++ )
                        {
                            spriteSheet.SetPixel( x + xOffset, y + yOffset, assembledTexture.GetPixel( x, y ) );
                        }
                    }
                }
            }
            spriteSheet.Apply();

            // Save the sprite sheet to a file
            string path = arg.FolderPath;
            string fileName = "SpriteSheet";
            Debug.Log( "Exporting: " + path );
            CSUtils.SaveTexture( spriteSheet, path, fileName );
            AssetDatabase.Refresh();
#if UNITY_EDITOR
            FormatSpritesheet( path + "/" + fileName + ".png");
#endif
        }
        private Texture2D CropTexture( Texture2D texture, float percentage )
        {
            if ( texture == null || percentage <= 0 || percentage > 1 )
            {
                throw new ArgumentException( "Invalid texture or percentage" );
            }

            int newWidth = Mathf.RoundToInt( texture.width * percentage );
            int newHeight = Mathf.RoundToInt( texture.height * percentage );

            Texture2D croppedTexture = new Texture2D( newWidth, newHeight, texture.format, false )
            {
                filterMode = texture.filterMode,
                wrapMode = texture.wrapMode
            };

            Color[] pixels = texture.GetPixels( (texture.width - newWidth) / 2, (texture.height - newHeight) / 2, newWidth, newHeight );
            croppedTexture.SetPixels( pixels );
            croppedTexture.Apply();

            return croppedTexture;
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
            Texture2D result = new Texture2D( width, height, TextureFormat.RGBA32, false );

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
            EventBus.Instance.Unsubscribe( _changepartRandomlySubscription );
            EventBus.Instance.Unsubscribe( _resetPartSubscription );
        }
        public void Select(eCharacterPart part, string id)
        {
            if (string.IsNullOrEmpty( id ) )
            {
                return;
            }
            if ( !_characterDatabase.IsValid( part, id ) )
            {
                return;
            }
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
                    Texture2D texture = textures[frameIndex];
                    if (texture == null)
                    {
                        continue;
                    }
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    spriteRenderer.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
                }
            }
            frameIndex++;
            if (frameIndex >= _currentAnimationTextures[_currentAnimation][_spriteRenderers.Keys.First()].Count)
            {
                frameIndex = 0;
            }
        }
    }
}
