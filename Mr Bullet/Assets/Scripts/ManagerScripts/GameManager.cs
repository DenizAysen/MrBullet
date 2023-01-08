using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameUI gameUI;
    [SerializeField] private int enemycount = 1;
    [HideInInspector] public bool gameOver;
    [SerializeField] private PlayerController playerController;    
    [SerializeField] private GameObject blackbullet, goldenbullet;
    public int blackbullets = 1;
    public int goldenbullets = 3;
    int activeSceneIndex;
    private int levelNumber;
    void Awake()
    {
        levelNumber = PlayerPrefs.GetInt("Level", 1);
        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerController.ammo = blackbullets + goldenbullets;
        for (int i = 0; i < blackbullets; i++)
        {
            GameObject bbTemp = Instantiate(blackbullet);
            bbTemp.transform.SetParent(GameObject.Find("Bullets").transform);
        }
        for (int i = 0; i < goldenbullets; i++)
        {
            GameObject gbTemp = Instantiate(goldenbullet);
            gbTemp.transform.SetParent(GameObject.Find("Bullets").transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
     if(!gameOver && playerController.ammo<=0&&enemycount>0 && GameObject.FindGameObjectsWithTag("Bullet").Length<=0)
        {
            gameOver = true;
            gameUI.GameOverScreen();
        }
    }
    public void CheckBullets()
    {//Her ateþ edildiðinde çaðýrýlýr.
        if (goldenbullets > 0)
        {
            goldenbullets--;
            GameObject.FindGameObjectWithTag("GoldenBullet").SetActive(false);
        }
        else if(blackbullets > 0)
        {
            blackbullets--;
            GameObject.FindGameObjectWithTag("BlackBullet").SetActive(false);
        }
    }
    public void CheckEnemyCount()
    {//Düþman öldüðü zaman çaðýrýlýr
        enemycount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemycount <= 0)
        {
            gameUI.WinScreen();
            if(levelNumber>= activeSceneIndex)
            {
                PlayerPrefs.SetInt("Level", levelNumber + 1);
            }
        }
    }
    //Staj14.günburada bitti
    public void Restart()
    {
        SceneManager.LoadScene(activeSceneIndex);
    }
    public void NextLevel()
    {
        if(activeSceneIndex+1 >= 4)
        {
            SceneManager.LoadScene(activeSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(activeSceneIndex + 1);
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
