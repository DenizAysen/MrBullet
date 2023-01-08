using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioClip deathClip;
    Rigidbody2D rb;
    bool collisoned;
    void Start()
    {
        collisoned = false;
        rb = GetComponent<Rigidbody2D>();
    }
    public void Death()
    {
        gameObject.tag = "Untagged";
        gameManager.CheckEnemyCount();
        SoundManager.instance.PlaySoundFX(deathClip, 0.75f);
        StartCoroutine(BodyParts());
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Bullet")
        {
            if (!collisoned)
            {
                Vector2 direction = transform.position - target.transform.position;
                if (transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale < 1)
                    Death();
                //Eger x 0 dan büyükse x i 1 yapar deðilse -1 yapar
                //Eger y 0 dan büyükse y i 0.3 yapar deðilse - 0.3 yapar
                rb.AddForce(new Vector2((direction.x > 0 ? 1f : -1f) * 10, (direction.y > 0 ? .3f : -.3f)), ForceMode2D.Impulse);
                collisoned = true;
            }        
        }
        if (target.gameObject.CompareTag("Plank") || target.gameObject.CompareTag("BoxPlank"))
        {
            if (target.GetComponent<Rigidbody2D>().velocity.magnitude > 1.5f)
                Death();
        }
        if(target.gameObject.CompareTag("Ground"))
        {
            if(rb.velocity.magnitude >2)
                Death();
        }
    }

    IEnumerator BodyParts()
    {
        foreach (Transform obj in transform)
        {
            obj.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        yield return new WaitForSeconds(5f);
        foreach (Transform obj in transform)
        {
            obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            if (obj.childCount > 0)
            {
                obj.GetChild(0).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
}
