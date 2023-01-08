using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private int startBB;
    [Header("WinScreen")]
    [SerializeField] private TextMeshProUGUI goodJobText;
    [SerializeField] private Image star1, star2, star3;
    [SerializeField] private Sprite shineStar, darkStar;
    [Header("Panels")]
    [SerializeField] private GameObject[] Panels;
    void Start()
    {
        startBB = gameManager.blackbullets;
    }
    public void GameOverScreen()
    {
        Panels[0].SetActive(false);
        Panels[1].SetActive(true);
    }
    public void WinScreen()
    {
        Panels[0].SetActive(false);
        Panels[2].SetActive(true);
        if(gameManager.blackbullets >= startBB)
        {
            goodJobText.text = "FANTASTIC!";
            StartCoroutine(Stars(3));
        }
        else if (gameManager.blackbullets >= startBB- (gameManager.blackbullets/2))
        {
            goodJobText.text = "AWESOME!";
            StartCoroutine(Stars(2));
        }
        else if (gameManager.blackbullets > startBB)
        {
            goodJobText.text = "WELL DONE!";
            StartCoroutine(Stars(1));
        }
        else
        {
            goodJobText.text = "GOOD";
            StartCoroutine(Stars(0));
        }
    }
    private IEnumerator Stars(int shineNumber)
    {
        yield return new WaitForSeconds(0.5f);
        switch (shineNumber)
        {
            case 3:
                yield return new WaitForSeconds(.15f);
                star1.sprite = shineStar;
                yield return new WaitForSeconds(.15f);
                star2.sprite = shineStar;
                yield return new WaitForSeconds(.15f);
                star3.sprite = shineStar;
                break;
            case 2:
                yield return new WaitForSeconds(.15f);
                star1.sprite = shineStar;
                yield return new WaitForSeconds(.15f);
                star2.sprite = shineStar;
                yield return new WaitForSeconds(.15f);
                star3.sprite = darkStar;
                break;
            case 1:
                yield return new WaitForSeconds(.15f);
                star1.sprite = shineStar;
                yield return new WaitForSeconds(.15f);
                star2.sprite = darkStar;
                yield return new WaitForSeconds(.15f);
                star3.sprite = darkStar;
                break;
            case 0:
                star1.sprite = darkStar;
                star2.sprite = darkStar;
                star3.sprite = darkStar;
                break;
        }
    }
}
