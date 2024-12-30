using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class CSPaintingManager : MonoBehaviour
    {
        [SerializeField] private CSImage _csImage;
        [SerializeField] private CSPaintingRenderer _paintingRenderer;

        EventSubscription<PointerDownArgs> _pointerDownSubscription;
        EventSubscription<PointerMoveArgs> _pointerMoveSubscription;
        EventSubscription<PointerUpArgs> _pointerUpSubscription;

        private void Awake()
        {
            _pointerDownSubscription = EventBus.Instance.Subscribe<PointerDownArgs>( OnPointerDown );
            _pointerUpSubscription = EventBus.Instance.Subscribe<PointerUpArgs>( OnPointerUp );
        }
        private void OnDestroy()
        {
            EventBus.Instance.Unsubscribe( _pointerDownSubscription );
            EventBus.Instance.Unsubscribe( _pointerMoveSubscription );
            EventBus.Instance.Unsubscribe( _pointerUpSubscription );
        }
        private void OnPointerDown( PointerDownArgs args )
        {
            StartDrawing( args.NormalizedPosition );
        }
        private void OnPointerMove( PointerMoveArgs args )
        {
            Draw( args.NormalizedPosition );
        }
        private void OnPointerUp( PointerUpArgs args )
        {
            StopDrawing();
        }

        void StopDrawing()
        {
            EventBus.Instance.Unsubscribe( _pointerMoveSubscription );
        }
        void Draw(Vector2 normalizedPixelPosition)
        {
            _paintingRenderer.DrawOnTexture( normalizedPixelPosition, Color.yellow );
        }
        void StartDrawing( Vector2 normalizedPixelPosition )
        {
            Draw( normalizedPixelPosition );
            _pointerMoveSubscription = EventBus.Instance.Subscribe<PointerMoveArgs>( OnPointerMove );
        }
    }
}
