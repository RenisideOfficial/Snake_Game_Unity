using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour
{
    public Animator crossFade;
    public Slider musicSlider, sfxSlider;
    public GameObject Panel;
    public float timer = .5f;
    public Button startGame, exitGame, backToMain;

    private void Start()
    {
        startGame.onClick.AddListener(delegate { gameScene(); });
        exitGame.onClick.AddListener(delegate { quitGame(); });
        backToMain.onClick.AddListener(delegate { backToMenu(); });
    }

    public void ToggleMusic()
    {
        SoundManage.instance.ToggleMusic();
    }
    public void ToggleSfx()
    {
        SoundManage.instance.ToggleSfx();
    }

    public void MusicVolume()
    {
        SoundManage.instance.MusicVolume(musicSlider.value);
    }
    public void SfxVolume()
    {
        SoundManage.instance.SfxVolume(sfxSlider.value);
    }

    public void ClosePause()
    {
        SoundManage.instance.PlaySfx("Button");
        Time.timeScale = 1;
        Panel.SetActive(false);
    }
    public void OpenPause()
    {
        SoundManage.instance.PlaySfx("Button");
        Time.timeScale = 0;
        Panel.SetActive(true);
    }

    public void gameScene()
    {
        StartCoroutine(wait(SceneManager.GetActiveScene().buildIndex + 1));        
    }

    public void quitGame()
    {
        SoundManage.instance.PlaySfx("Button");
        StartCoroutine("wait");
        Application.Quit();
    }

    public void backToMenu()
    {
        SoundManage.instance.PlaySfx("Button");
        StartCoroutine("wait");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public IEnumerator wait(int index)
    {
        SoundManage.instance.PlaySfx("Button");
        crossFade.SetTrigger("Start");
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(index);
    }
}

