using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] AudioClip hitFX;
    [SerializeField] GameObject powCanvas;
    [SerializeField] Sprite damageSprite;
    [SerializeField] Sprite hitEffectImage;
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
        ShowPowEffect();
        AudioSource.PlayClipAtPoint(hitFX,transform.position);
    }

    private void ShowPowEffect()
    {
        var powGo = Instantiate(powCanvas, transform.position, Quaternion.identity,transform);
        powGo.GetComponentInChildren<Image>().sprite = hitEffectImage;
        Destroy(powGo.gameObject, .2f);
    }
    #endregion
}
