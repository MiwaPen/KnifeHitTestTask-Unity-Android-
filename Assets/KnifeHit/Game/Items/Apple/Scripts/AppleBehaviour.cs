using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class AppleBehaviour : MonoBehaviour
{
    [SerializeField] AppleInfo _appleInfo;
    [SerializeField] Sprite appleCut;
    [SerializeField] float gravityScale;
    [Inject] private SoundController soundController;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    public int appleValue { get; private set; }

    public Action<int> OnCutApple;

    private void Awake()
    {
        appleValue = _appleInfo.appleValue;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _collider2D = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        KnifeBehaviourScript knife = null;
        if(collision.gameObject.TryGetComponent<KnifeBehaviourScript>(out knife))
        {
            soundController.PlaySound(soundController.AppleCutSound);
            OnCutApple?.Invoke(appleValue);
            _collider2D.enabled = false;
            _spriteRenderer.sprite = appleCut;
            this.transform.parent = null;
            _rigidbody2D.gravityScale = gravityScale;
            Destroy(this.gameObject, 0.5f);
        } 
    }
}
