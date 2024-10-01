using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine SceneManagment;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public PlayerController playerData;
    public Image healthBar;
    public TextMeshProUGI clipCounter;
    public TextMeshProUGI ammoCounter;


    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.Find("player").GetComponent<PlayerController>;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)playerData.health / (float)playerData.maxHealth,0,1);
        if (playerData.weaponID < 0)
        {
            clipCounter.gameObject.SetActive(false);
            ammoCounter.gameObject.SetActive(false);
        }
        else
        {
            clipCounter.gameObject.SetActive(true);
            ammoCounter.gameObject.SetActive(true);

            clipCounter.text = "clip;" + playerData.currentClip + "/" + playerData.clipSize;
            ammoCounter.text = "ammo" + playerData.currentAmmo;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                pauseMenue.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.Visible = (true);
                Time.timeScale = 0;
                isPaused = true;

            }
            else
                resume();
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
            sceneManager.LoadScene(SceneID);
        }
        public void RestartLevel()
        {
            LoadLevel(scenemanager.GetActiveScene()buildIndex);
        }
    }
