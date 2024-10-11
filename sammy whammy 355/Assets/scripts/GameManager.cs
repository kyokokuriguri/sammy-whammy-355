using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public PlayerController playerData;
    public Image healthBar;
    public TextMeshProUGUI clipCounter;
    public TextMeshProUGUI ammoCounter;
    public GameObject pauseMenu;



    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.Find("Player").GetComponent<PlayerController>();
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            healthBar.fillAmount = Mathf.Clamp((float)playerData.health / (float)playerData.maxHealth,0,1);
            if (playerData.weaponID < 0)
            {
                clipCounter.gameObject.SetActive(false);
                ammoCounter.gameObject.SetActive(false);
            }
            else
            {
                clipCounter.gameObject.SetActive(true);
                ammoCounter.gameObject.SetActive(true);

                clipCounter.text = "clip:" + playerData.currentClip + "/" + playerData.clipSize;
                ammoCounter.text = "ammo" + playerData.currentAmmo;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    pauseMenu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = (true);
                    Time.timeScale = 0;
                    isPaused = true;

                }
                else
                    Resume();
            }
        }
    }
            public void Resume()
            {
                pauseMenu.SetActive(false);

                Time.timeScale = 1;

                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;

                isPaused = false;
            }
            public void QuitGame()
            {
                Application.Quit();
            }
            public void LoadLevel(int sceneID)
            {
                SceneManager.LoadScene(sceneID);
            }
            public void RestartLevel()
            {
                LoadLevel(SceneManager.GetActiveScene().buildIndex);
            }
        
    
}
