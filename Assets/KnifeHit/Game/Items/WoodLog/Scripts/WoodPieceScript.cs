using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class WoodPieceScript : MonoBehaviour
{
    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;

    private void Awake()
    {
        _collider2D = this.gameObject.GetComponent<Collider2D>();
        _rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        _transform = this.gameObject.GetComponent<Transform>();
    }

    public void SplitPiece()
    {
        Unchild();
        _transform.parent = null;
        _collider2D.enabled = false;

        Vector2 force = Vector2.zero -
                new Vector2(_transform.position.x,
                _transform.position.y);
        _rigidbody2D.AddForceAtPosition(force.normalized*20,
            _transform.position,ForceMode2D.Impulse);
        _rigidbody2D.gravityScale = 4f;
    }

    private void Unchild()
    {
        int childCount = _transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = _transform.GetChild(i);
            Vector2 force = Vector2.zero -
                new Vector2(child.position.x,
                child.position.y);
            Rigidbody2D childBody = child.GetComponent<Rigidbody2D>();
            childBody.AddForceAtPosition(force.normalized * 10,
                _transform.position, ForceMode2D.Impulse);
            childBody.gravityScale = 4f;
            childBody = null;
            child = null;

        }
        _transform.DetachChildren();
    }

    private void OnDestroy()
    {
        _transform.DOKill(true);
    }
}
