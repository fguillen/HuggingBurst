using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresentationController : MonoBehaviour
{
    private AudioManager audioManager;

    [SerializeField] GameObject creditsPage;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void QuitGame() {
        Application.Quit();
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    public void StartMusic() {
        audioManager.Play("Theme");
    }

    public void ShowCredits() {
        creditsPage.SetActive(true);
    }
}
