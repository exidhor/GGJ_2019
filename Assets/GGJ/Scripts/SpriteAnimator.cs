using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] float _spriteTime = 0.1f;
    [SerializeField] List<Sprite> _sprites = new List<Sprite>();

    int _index;
    float _currentTime;
    SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _currentTime = UnityEngine.Random.Range(0f, _spriteTime * _sprites.Count);
    }

    void Update()
    {
        _currentTime += Time.deltaTime;

        while (_currentTime > _spriteTime)
        {
            _index++;

            if(_index >= _sprites.Count)
            {
                _index = 0;
            }

            _currentTime -= _spriteTime;
        }

        _spriteRenderer.sprite = _sprites[_index];
    }
}
