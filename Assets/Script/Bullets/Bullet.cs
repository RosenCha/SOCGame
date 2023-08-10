using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;
    public int Pierce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DmgObj>() != null)
        {
            collision.GetComponent<DmgObj>().DamageHp(Damage);
            if (Pierce > 1)
            {
                Pierce--;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
