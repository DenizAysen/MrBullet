using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float bulletSpeed;
    public int ammo;
    private Transform handPos;
    private Transform firePos1, firePos2;
    private LineRenderer Laser;
    [SerializeField] private GameObject bullet;
    [SerializeField] GameManager gameManager;
    [SerializeField] private GameObject Crosshair;
    [SerializeField] private AudioClip gunShot;
    void Awake()
    {
        handPos = GameObject.Find("RightArm").transform;
        firePos1 = GameObject.Find("FirePos1").transform;
        firePos2 = GameObject.Find("FirePos2").transform;
        Laser = GameObject.Find("Gun").GetComponent<LineRenderer>();
        Laser.enabled = false;
    }
    void Update()
    {
        if (!isMouseOverUI() /*&& !start*/)
        {
            if (Input.GetMouseButton(0))
            {
                Aim();
                //start = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (ammo > 0)
                    Shoot();
                else
                {
                    Laser.enabled = false;
                    Crosshair.SetActive(false);
                }
                //start = false;
            }
        }
        
    }
    void Aim()
    {
        //Farenin sahnedeki yeri ile karakteri sað kolu arasýndaki farký döndürür
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - handPos.position;
        //Atan2 tan(direction.y/direction.x) radyan þeklinde açý döndürür
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        //z açýsýný deðiþtirir
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //Açýlar için Lerp metodu
        handPos.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);
        Laser.enabled = true;
        Laser.SetPosition(0, firePos1.position);
        Laser.SetPosition(1, firePos2.position);
        Crosshair.SetActive(true);
        Crosshair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10f);
    }
    void Shoot()
    {
        Crosshair.SetActive(false);
        Laser.enabled = false;
        GameObject b = Instantiate(bullet,firePos1.position,Quaternion.identity);
        if (transform.localScale.x > 0)
        {
            b.GetComponent<Rigidbody2D>().AddForce(firePos1.right * bulletSpeed, ForceMode2D.Impulse);
        }
        else
        {
            b.GetComponent<Rigidbody2D>().AddForce(-firePos1.right * bulletSpeed, ForceMode2D.Impulse);
        }
        ammo--;
        gameManager.CheckBullets();
        SoundManager.instance.PlaySoundFX(gunShot, 0.3f);
        Destroy(b, 2);
    }
    bool isMouseOverUI()
    {//Ýmleç objenin üstündeyse true deðilse false döndürür
        return EventSystem.current.IsPointerOverGameObject();
    }
}
