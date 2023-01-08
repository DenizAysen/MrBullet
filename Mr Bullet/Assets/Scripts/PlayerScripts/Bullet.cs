using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] AudioClip boxHit,plankHit,explodeHit,groundHit;
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Box"))
        {
            SoundManager.instance.PlaySoundFX(boxHit, 1f);
            Destroy(target.gameObject);
        }
        if (target.gameObject.CompareTag("Ground"))
        {
            SoundManager.instance.PlaySoundFX(groundHit, 1f);
        }
        if (target.gameObject.CompareTag("Plank"))
        {
            SoundManager.instance.PlaySoundFX(plankHit, 1f);
        }
        if (target.gameObject.CompareTag("Tnt"))
        {
            SoundManager.instance.PlaySoundFX(explodeHit, 1f);
        }
    }
}
