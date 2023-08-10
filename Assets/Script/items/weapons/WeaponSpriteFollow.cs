using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpriteFollow : MonoBehaviour
{
    //public SpriteRenderer self;
    //public Sprite weaponSprite;
    public Transform following;
    void Update()
    {
        transform.position = following.position;
        transform.rotation = following.rotation;
    }
}
