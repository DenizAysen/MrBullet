using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionPrefab;
    [SerializeField] private float radius = 1f;
    [SerializeField] private float power = 10f;
    Rigidbody2D rb;
    void Explode()
    {
        Vector2 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);

        foreach ( Collider2D hit in colliders)
        {
            if(hit.GetComponent<Rigidbody2D>() != null)
            {
                rb = hit.GetComponent<Rigidbody2D>();
                //Objenin kendi pozisyonundan, kutunun pozisyonu çýkartýlýp kuvvet yönü bulunur.
                var explodeDir = rb.position - explosionPos;

                rb.gravityScale = 1;

                rb.AddForce(power * explodeDir, ForceMode2D.Impulse);
            }
            if (hit.gameObject.CompareTag("Enemy"))
                hit.gameObject.GetComponent<Enemy>().Death();
        }
    }
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Bullet"))
        {
            ExplosionPrefab.transform.position = transform.position;
            ExplosionPrefab.SetActive(true);
            Explode();
            gameObject.SetActive(false);
            Invoke("StopExlosion", 0.8f);
        }
    }

    private void StopExlosion()
    {     
        ExplosionPrefab.SetActive(false);
    }
}
