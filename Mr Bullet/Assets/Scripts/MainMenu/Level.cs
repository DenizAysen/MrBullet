using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private Button LevelButton;

    public int levelReq;
    void Start()
    {
        LevelButton = GetComponent<Button>();
        if (PlayerPrefs.GetInt("Level", 1) >= levelReq)
        {
            LevelButton.onClick.AddListener(() => LoadLevel());
        }
        else
        {
            GetComponent<CanvasGroup>().alpha = .5f;
        }
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(gameObject.name);
    }
}
