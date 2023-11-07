using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private GameManager _gameManager;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Exit")
            _gameManager.GameOver(true);
        if (collision.transform.tag == "Out")
            _gameManager.GameOver(false);

    }
}
