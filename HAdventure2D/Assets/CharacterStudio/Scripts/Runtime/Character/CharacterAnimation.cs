using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.U2D.Sprites;
using UnityEditor;
#endif
using UnityEngine.U2D.Animation;

namespace CharacterStudio
{
    public class CharacterAnimation : MonoSingleton<CharacterAnimation>
    {
        [SerializeField] Transform _spriteContainer;
        [SerializeField] eCharacterAnimation _currentAnimation;
        [SerializeField] int size = 48;

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
        public bool IsPlaying = true;
        public int LongestTextureCount
        {
            get
            {
                if (_longestTextureCount == 0)
                {
                    _longestTextureCount = GetLongestTextureCount();
                }
                return _longestTextureCount;
            }
        } private int _longestTextureCount = 0;

        [SerializeField] private SerializedDictionary<eCharacterPart, string> _characterSelection = new SerializedDictionary<eCharacterPart, string>();
        public SerializedDictionary<eCharacterPart, string> CharacterSelection => _characterSelection;




        private EventSubscription<ChangePartArg> _changePartSubscription;
        private EventSubscription<ChangeAnimationArg> _changeAnimationSubscription;
        private EventSubscription<SeparatedSpritesExportArg> _exportSeparatedSpritesSubscription;
        private EventSubscription<SpriteLibraryExportArg> _exportSpriteLibrarySubscription;
        private EventSubscription<SpritesheetExportArg> _exportSpriteSheetSubscription;
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

            if (!DataManager.Instance.AnimationDatabase.Data.TryGetValue(newAnimation, out AnimationData animationData))
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
                spriteRenderer.sortingOrder = DataManager.Instance.CharacterDatabase.SortedData[part];
                _spriteRenderers.TryAdd(part, spriteRenderer);
                _currentAnimationTexturesMap.TryAdd(newAnimation, new Dictionary<eCharacterPart, List<Texture2D>>());
                _currentAnimationTexturesMap[newAnimation].TryAdd(part, data.Textures);
            }
            _currentAnimationTextures = new ();
            UpdateInterval();
            _counter = _animationInterval;
            _isSetup = true;
        }
        void ReloadAnimation()
        {
            SetAnimation(_currentAnimation);
        }
        public void UpdateInterval()
        {
            _animationInterval = DataManager.Instance.AnimationDatabase.GetAnimationInterval();
        }
        public void UpdateTexture(eCharacterPart part, string id)
        {
            if (!_currentAnimationTextures.ContainsKey(_currentAnimation))
            {
                _currentAnimationTextures.TryAdd(_currentAnimation, new Dictionary<eCharacterPart, List<Texture2D>>());
            }
            Texture2D baseTexture = DataManager.Instance.CharacterDatabase.Data[part].TextureDict[id].Texture;
            var map = CSUtils.LoadMappedColors(DataManager.Instance.MapDatabase.Data[part], baseTexture);
            var data = _currentAnimationTexturesMap[_currentAnimation];
            if (!data.TryGetValue(part, out List<Texture2D> textures))
            {
                return;
            }
            for (int i = 0; i < textures.Count; i++)
            {
                Texture2D generatedTexture = CSUtils.GenerateTexture(textures[i], map);
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
        public void ClearTexture(eCharacterPart part)
        {
            if (_currentAnimationTextures.ContainsKey(_currentAnimation))
            {
                if (_currentAnimationTextures[_currentAnimation].ContainsKey(part))
                {
                    _currentAnimationTextures[_currentAnimation].Remove(part);
                }
            }
            if (_spriteRenderers.ContainsKey(part))
            {
                _spriteRenderers[part].sprite = null;
            }
            ReloadAnimation();
        }
        public void Setup()
        {
            _changePartSubscription = EventBus.Instance.Subscribe<ChangePartArg>(OnChangePart);
            _changeAnimationSubscription = EventBus.Instance.Subscribe<ChangeAnimationArg>(OnChangeAnimation);
            _exportSeparatedSpritesSubscription = EventBus.Instance.Subscribe<SeparatedSpritesExportArg>(OnExportSeparatedSprites);
            _exportSpriteLibrarySubscription = EventBus.Instance.Subscribe<SpriteLibraryExportArg>(OnExportSpriteLibrary);
            _exportSpriteSheetSubscription = EventBus.Instance.Subscribe<SpritesheetExportArg>(OnExportSpriteSheet);
            _changepartRandomlySubscription = EventBus.Instance.Subscribe<ChangePartRandomlyArg>( OnChangePartRandomly );
            _resetPartSubscription = EventBus.Instance.Subscribe<ResetPartArg>( OnResetPart );

            SetAnimation(_currentAnimation);   
            SelectDefault();
            ApplySelection();
        }

        private void OnResetPart( ResetPartArg arg )
        {
            foreach (var (part, id) in DataManager.Instance.CharacterDatabase.DefaultParts )
            {
                Select( part, id );
            }
            ApplySelection();
        }

        private void OnChangePartRandomly( ChangePartRandomlyArg arg )
        {
            List<(string id, eCharacterPart part)> randomParts = DataManager.Instance.CharacterDatabase.GetRandomAll();
            foreach ( var (id, part) in randomParts )
            {
                Select( part, id );
            }
            ApplySelection();
        }
        private void OnExportSeparatedSprites( SeparatedSpritesExportArg arg )
        {
            _ = ExportSeparatedSprites(arg);
        }
        private void OnExportSpriteLibrary( SpriteLibraryExportArg arg )
        {
            _ = ExportSpriteLibrary(arg);
        }
        private void OnExportSpriteSheet( SpritesheetExportArg arg )
        {
            SpriteSheetResult result = ExportSpriteSheet(arg);
            if (arg.AutoSlice)
                SliceSpriteSheet(result.FrameCount, result.OutputPath, size);
        }

        private SpriteLibrarayResult ExportSpriteLibrary(SpriteLibraryExportArg arg)
        {
            SpriteLibrarayResult result = new SpriteLibrarayResult();
            // * Export sprite sheet

            SpritesheetExportArg spriteSheetExportArg = new SpritesheetExportArg(arg.FolderPath, true, arg.Name);
            SpriteSheetResult spriteSheetResult = ExportSpriteSheet(spriteSheetExportArg);
            SliceSpriteSheet(spriteSheetResult.FrameCount, spriteSheetResult.OutputPath, size);

            // * Then use the sprite sheet to create a sprite library at the same location
            string spriteSheetPath = $"{arg.FolderPath}/{arg.Name}_SpriteSheet.png";
            string assetPath = spriteSheetPath.Substring(spriteSheetPath.IndexOf("Assets"));

#if UNITY_EDITOR
            // Load the texture at the specified path
            Texture2D spriteSheet = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            if (spriteSheet == null)
            {
                Debug.LogError("Failed to load sprite sheet at path: " + assetPath);
                return result;
            }

            // Create a new sprite library asset
            SpriteLibraryAsset spriteLibraryAsset = ScriptableObject.CreateInstance<SpriteLibraryAsset>();

            // Load the texture importer to get the sliced sprites
            TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (textureImporter != null)
            {
                var factory = new SpriteDataProviderFactories();
                factory.Init();
                var dataProvider = factory.GetSpriteEditorDataProviderFromObject(textureImporter);
                dataProvider.InitSpriteEditorDataProvider();
                int rows = spriteSheetResult.FrameCount.Count;
                string spriteLibAssetPath = $"{arg.FolderPath}/{arg.Name}_SpriteLibrary.asset";
                spriteLibAssetPath = spriteLibAssetPath.Substring(spriteLibAssetPath.IndexOf("Assets"));

                var allSprites = AssetDatabase.LoadAllAssetsAtPath(assetPath).OfType<Sprite>().ToList();

                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < spriteSheetResult.FrameCount[y].frameCount; x++)
                    {
                        string category = spriteSheetResult.FrameCount[y].anim.ToString();
                        string spriteName = category + "_" + x;
                        Sprite sprite = allSprites.FirstOrDefault(s => s.name == spriteName);
                        spriteLibraryAsset.AddCategoryLabel(sprite, category, sprite.name);
                    }
                }
                result.OutputPath = spriteLibAssetPath;
                AssetDatabase.CreateAsset(spriteLibraryAsset, spriteLibAssetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();


                SpriteLibraryAsset createdAsset = AssetDatabase.LoadAssetAtPath<SpriteLibraryAsset>(spriteLibAssetPath);
                if (createdAsset != null)
                {
                    EditorGUIUtility.PingObject(createdAsset);
                    Selection.activeObject = createdAsset; 
                }
            }
            else
            {
                Debug.LogError("Failed to load texture importer at path: " + assetPath);
            }
#endif
            return result;
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
        private void SliceSpriteSheet(List<(eCharacterAnimation anim, int animFrameCount)> frameData, string path, int cellSize)
        {
            // path is full path, just get the part from Assets/
            if (string.IsNullOrEmpty(path) || !path.Contains("Assets"))
                return;
            string assetPath = path.Substring(path.IndexOf("Assets"));
#if UNITY_EDITOR
            // Load the texture at the specified path
            TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            Texture2D spriteSheet = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            var factory = new SpriteDataProviderFactories();
            factory.Init();
            var dataProvider = factory.GetSpriteEditorDataProviderFromObject(textureImporter);
            dataProvider.InitSpriteEditorDataProvider();
            var spriteRects = dataProvider.GetSpriteRects();
            int rows = frameData.Count;
            List<SpriteRect> newSpriteRects = new List<SpriteRect>();

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < frameData[y].animFrameCount; x++)
                {
                    SpriteRect spriteRect = new SpriteRect
                    {
                        rect = new Rect(x * cellSize, y * cellSize, cellSize, cellSize),
                        alignment = SpriteAlignment.Custom,
                        pivot = new Vector2(0.5f, 0.25f),
                        name = frameData[y].anim.ToString() + "_" + x
                    };
                    newSpriteRects.Add(spriteRect);
                }
            }

            dataProvider.SetSpriteRects(newSpriteRects.ToArray());
            dataProvider.Apply();
            // Reimport the asset to have the changes applied
            var assetImporter = dataProvider.targetObject as AssetImporter;
            assetImporter.SaveAndReimport();
            textureImporter.SaveAndReimport();
            AssetDatabase.Refresh();
#endif
        }
        private SeparatedSpriteResult ExportSeparatedSprites(SeparatedSpritesExportArg arg)
        {
            SeparatedSpriteResult result = new SeparatedSpriteResult();
            Dictionary<eCharacterPart, Texture2D> sBaseTexture = new Dictionary<eCharacterPart, Texture2D>();
            List<eCharacterAnimation> allAnimations = DataManager.Instance.AnimationDatabase.Data.Keys.ToList();
            Dictionary<eCharacterPart, Dictionary<Color32, Color32>> map = new Dictionary<eCharacterPart, Dictionary<Color32, Color32>>();

            // * Load mapped colors for each parts
            foreach (var (part, data) in DataManager.Instance.CharacterDatabase.Data)
            {
                Texture2D baseTexture = data.TextureDict[_characterSelection[part]].Texture;
                sBaseTexture.TryAdd(part, baseTexture);
                map.TryAdd(part, CSUtils.LoadMappedColors(DataManager.Instance.MapDatabase.Data[part], baseTexture));
            }

            result.Sprites = new Dictionary<string, Texture2D>();
            // * Generate textures for each animation
            foreach (var animation in allAnimations)
            {
                if (!DataManager.Instance.AnimationDatabase.Data.TryGetValue(animation, out AnimationData animationData))
                {
                    Debug.LogError("Animation not found in database: " + animation);
                    return result;
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
                            return result;
                        }
                        Texture2D generatedTexture = CSUtils.GenerateTexture(data.Textures[i], partMap);
                        sortedPart.Add((DataManager.Instance.CharacterDatabase.SortedData[part], generatedTexture));
                    }
                    sortedPart = sortedPart.OrderBy(x => x.sortingLayer).Reverse().ToList();
                    Texture2D assembledTexture = AssembleTextures(sortedPart.Select(x => x.texture).ToList());
                    float percentage = (float)this.size / (float)assembledTexture.width;
                    assembledTexture = CropTexture( assembledTexture, percentage);
                    string path = arg.FolderPath + "/" + animation.ToString();
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    string fileName = animation.ToString() + "_" + i;
                    Debug.Log("Exporting: " + path + "/" + fileName);
                    string fullPath = path + "/" + fileName + ".png";
                    result.Sprites.TryAdd(fullPath, assembledTexture);
                    result.OutputPath = arg.FolderPath;
                    CSUtils.SaveTexture(assembledTexture, path, fileName);
#if UNITY_EDITOR
                    AssetDatabase.Refresh();
                    FormatSprite( fullPath );
#endif
                }
            }
            return result;
        }

        private SpriteSheetResult ExportSpriteSheet( SpritesheetExportArg arg )
        {
            SpriteSheetResult result = new SpriteSheetResult();
            Dictionary<eCharacterPart, Texture2D> sBaseTexture = new Dictionary<eCharacterPart, Texture2D>();
            // For some reason, the order of the animations is reversed
            List<eCharacterAnimation> allAnimations = DataManager.Instance.AnimationDatabase.Data.Keys.Reverse().ToList();
            Dictionary<eCharacterPart, Dictionary<Color32, Color32>> map = new Dictionary<eCharacterPart, Dictionary<Color32, Color32>>();

            // Load mapped colors for each part
            foreach ( var (part, data) in DataManager.Instance.CharacterDatabase.Data )
            {
                Texture2D baseTexture = data.TextureDict[ _characterSelection[ part ] ].Texture;
                sBaseTexture.TryAdd( part, baseTexture );
                map.TryAdd( part, CSUtils.LoadMappedColors( DataManager.Instance.MapDatabase.Data[ part ], baseTexture ) );
            }

            // Calculate the dimensions of the sprite sheet
            int maxFrameCount = allAnimations.Max( animation => DataManager.Instance.AnimationDatabase.Data[ animation ].AnimationsByPart.First().Value.Textures.Count );
            int maxCellWidth = DataManager.Instance.CharacterDatabase.Data.Values.Max( data => data.TextureDict.Values.Max( dataTex => dataTex.Texture.width ) );
            int maxCellHeight = DataManager.Instance.CharacterDatabase.Data.Values.Max( data => data.TextureDict.Values.Max( dataTex => dataTex.Texture.height ) );
            maxCellWidth = maxCellWidth * this.size / maxCellWidth;
            maxCellHeight = maxCellHeight * this.size / maxCellHeight;
            int sheetWidth = maxCellWidth * maxFrameCount;
            int sheetHeight = maxCellHeight * allAnimations.Count;
            List<(eCharacterAnimation anim, int animFrameCount)> frameData = new ();

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
                if ( !DataManager.Instance.AnimationDatabase.Data.TryGetValue( animation, out AnimationData animationData ) )
                {
                    Debug.LogError( "Animation not found in database: " + animation );
                    return result;
                }
                int frameCount = animationData.AnimationsByPart.First().Value.Textures.Count;
                frameData.Add( (animation, frameCount) );
                for ( int frameIndex = 0; frameIndex < frameCount; frameIndex++ )
                {
                    List<(int sortingLayer, Texture2D texture)> sortedPart = new List<(int sortingLayer, Texture2D texture)>();
                    foreach ( var (part, data) in animationData.AnimationsByPart )
                    {
                        if ( !map.TryGetValue( part, out Dictionary<Color32, Color32> partMap ) )
                        {
                            Debug.LogError( "Map not found for part: " + part );
                            return result;
                        }
                        Texture2D generatedTexture = CSUtils.GenerateTexture( data.Textures[ frameIndex ], partMap );
                        sortedPart.Add( (DataManager.Instance.CharacterDatabase.SortedData[ part ], generatedTexture) );
                    }
                    sortedPart = sortedPart.OrderBy( x => x.sortingLayer ).Reverse().ToList();
                    Texture2D assembledTexture = AssembleTextures( sortedPart.Select( x => x.texture ).ToList() );
                    float percentage = (float)this.size / (float)assembledTexture.width;
                    assembledTexture = CropTexture( assembledTexture, percentage );

                    // Copy the assembled texture to the sprite sheet
                    int xOffset = frameIndex * maxCellWidth;
                    int yOffset = animIndex * maxCellHeight;
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
            string fileName = $"{arg.Name}_SpriteSheet";
            Debug.Log( "Exporting: " + path );
            CSUtils.SaveTexture( spriteSheet, path, fileName );
#if UNITY_EDITOR
            AssetDatabase.Refresh();
            string fullPath = path + "/" + fileName + ".png";
            FormatSpritesheet( fullPath );
            result.SpriteSheet = AssetDatabase.LoadAssetAtPath<Texture2D>( fullPath.Substring( fullPath.IndexOf( "Assets" ) ) );
            result.FrameCount = frameData;
            result.OutputPath = fullPath;
            return result;
#else
            result.SpriteSheet = spriteSheet;
            result.FrameCount = frameData;
            result.OutputPath = path;
            return result;
#endif
        }
        private Texture2D CropTexture( Texture2D texture, float percentage )
        {
            if ( texture == null || percentage <= 0 || percentage > 1 )
            {
                throw new ArgumentException( "Invalid texture or percentage" );
            }
            if ( percentage == 1 )
            {
                return texture;
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

        protected override void OnDestroy()
        {
            base.OnDestroy();
            EventBus.Instance.Unsubscribe(_changePartSubscription);
            EventBus.Instance.Unsubscribe(_changeAnimationSubscription);
            EventBus.Instance.Unsubscribe(_exportSeparatedSpritesSubscription);
            EventBus.Instance.Unsubscribe(_exportSpriteLibrarySubscription);
            EventBus.Instance.Unsubscribe(_exportSpriteSheetSubscription);
            EventBus.Instance.Unsubscribe( _changepartRandomlySubscription );
            EventBus.Instance.Unsubscribe( _resetPartSubscription );
        }
        public void Select(eCharacterPart part, string id)
        {
            if (string.IsNullOrEmpty( id ) )
            {
                _characterSelection.Remove( part );
                ClearTexture( part );
                EventBus.Instance.Publish( new PartChangedArg( part, string.Empty ) );
                return;
            }
            if ( !DataManager.Instance.CharacterDatabase.IsValid( part, id ) )
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
            UpdateVisual();
            foreach ( var (part, id) in _characterSelection )
            {
                EventBus.Instance.Publish( new PartChangedArg( part, id ) );
            }
        }
        private void OnChangeAnimation(ChangeAnimationArg arg)
        {
            _longestTextureCount = 0;
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
            foreach (var (part, id) in DataManager.Instance.CharacterDatabase.DefaultParts)
            {
                Select(part, id);
            }
        }
        void Update()
        {
            if (!CharacterStudioMain.Instance.IsSetup)
            {
                return;
            }
            if (!_isSetup)
            {
                return;
            }
            if (!IsPlaying)
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
            UpdateVisual(frameIndex);
            frameIndex++;
            if (frameIndex >= LongestTextureCount)
            {
                frameIndex = 0;
            }
        }
        int GetLongestTextureCount()
        {
            int longest = 0;
            foreach (var (animation, data) in _currentAnimationTexturesMap)
            {
                foreach (var (part, textures) in data)
                {
                    longest = Mathf.Max(longest, textures.Count);
                }
            }
            return longest;
        }
        public void SetFrameIndex(int frameIndex)
        {
            try
            {
                if (_currentAnimationTextures.Count == 0)
                {
                    return;
                }
                if (!_currentAnimationTextures.ContainsKey(_currentAnimation))
                {
                    return;
                }
                if (!_currentAnimationTextures[_currentAnimation].ContainsKey(_spriteRenderers.Keys.First()))
                {
                    return;
                }
                this.frameIndex = Mathf.Clamp(frameIndex, 0, _currentAnimationTextures[_currentAnimation][_spriteRenderers.Keys.First()].Count - 1);
                UpdateVisual(this.frameIndex);
            }
            catch (Exception) { }
        }
        void UpdateVisual(int frameIndex)
        {
            foreach (var (part, spriteRenderer) in _spriteRenderers)
            {
                if (_currentAnimationTextures[_currentAnimation].TryGetValue(part, out List<Texture2D> textures))
                {
                    if (textures.Count == 0)
                    {
                        continue;
                    }
                    if (frameIndex >= textures.Count)
                    {
                        frameIndex = 0;
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
        }
        void UpdateVisual()
        {
            UpdateVisual(this.frameIndex);
        }
    }
}
