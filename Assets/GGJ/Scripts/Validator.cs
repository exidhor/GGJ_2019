using UnityEngine;
using System.Collections;

public class Validator : MonoBehaviour
{
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] Sprite _defaultSprite;
    [SerializeField] Vector2 _centerCollider;
    [SerializeField] Vector2 _sizeCollider = Vector2.one;
    [SerializeField] SpriteAnimator _animator;

    public Rect GetCollider()
    {
        Vector2 center = _centerCollider + (Vector2)transform.position;
        Vector2 size = _sizeCollider;

        return new Rect(center.x - size.x / 2,
                        center.y - size.y / 2,
                        size.x,
                        size.y);
    }

    public void SetAnim(bool state)
    {
        if(!state)
        {
            _renderer.sprite = _defaultSprite;
        }

        _animator.enabled = state;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Rect rect = GetCollider();
        Gizmos.DrawWireCube(rect.center, rect.size);
    }
}
