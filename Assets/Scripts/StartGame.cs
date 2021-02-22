using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartGame : MonoBehaviour
{
    [Header("Game")]
    public TracksManager tracksManager;
    public Spawner spawner;

    [Header("UI")]
    public NextPiece nextPiece;
    public PauseMenu pauseMenu;
    public GameObject pauseButton;

    [Header("Countdown")]
    [SerializeField] private int time;
    private TextMeshProUGUI countdown;

    [Header("Audio")]
    public AudioController audioController;
    private AudioSource audioSource;

    private void Awake()
    {
        countdown = GetComponent<TextMeshProUGUI>();    // gets tmp component
        audioSource = GetComponent<AudioSource>();      // gets audiosource
    }

    private void Start()
    {
        DisableGameComponents();

        audioSource.volume = audioController.sfx / 10;

        StartCoroutine(Countdown(time));
    }

    private IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        do
        {
            countdown.text = count.ToString();
            audioSource.Play();

            yield return new WaitForSecondsRealtime(1);
            count--;
        } while (count > 0);

        StartGameRoutine();
        countdown.enabled = false;
    }

    private void DisableGameComponents()
    {
        tracksManager.gameStarted = false;
        tracksManager.NextTrack();
        tracksManager.TogglePause();

        pauseMenu.enabled = false;                      // cant pause game before the start of the game
        pauseButton.SetActive(false);                   // disables pause button
    }

    // enables components to play the game
    private void StartGameRoutine()
    {
        tracksManager.gameStarted = true;
        tracksManager.TogglePause();

        nextPiece.DisplayNextPiece();   // shows next piece;
        spawner.SpawnNext();            // spawns first piece

        pauseMenu.enabled = true;       // can pause game
        pauseButton.SetActive(true);    // enables pause button
    }
}
