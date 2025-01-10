using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterStudio
{
    public class ScrollBackground : MonoBehaviour
    {
        [SerializeField] RawImage _scrollBackground;
        [SerializeField] float _speed;
        public void Update()
        {
            Rect rect = _scrollBackground.uvRect;
            Vector2 uv = rect.position;
            uv.x += _speed * Time.deltaTime;
            uv.y += _speed * Time.deltaTime;

            if (uv.x > 1)
                uv.x = 0;
            if (uv.y > 1)
                uv.y = 0;

            _scrollBackground.uvRect = new Rect(uv, rect.size);
        }
    }
}
