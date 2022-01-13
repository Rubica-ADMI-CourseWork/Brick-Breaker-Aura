using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    #region Properties
    [SerializeField] int hitPoints = 2;
    private int HitPoints
    {
        get { return hitPoints; }
        set
        {
            hitPoints = value;
            if (hitPoints == 0)
            {
                DestroyBrick();
            }
        }
    }
    #endregion

    #region Fields
    [SerializeField] Sprite damageSprite;
    SpriteRenderer spriteRenderer;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            DamageBrick();
        }
    }
    #endregion

    #region Gameplay
    private void DestroyBrick()
    {
        Destroy(gameObject);
    }

    private void DamageBrick()
    {
        //decrement hitPoints
        HitPoints--;
        spriteRenderer.sprite = damageSprite;

    } 
    #endregion
}
