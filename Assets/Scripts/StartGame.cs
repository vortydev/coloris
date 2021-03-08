/*
 * File:        StartGame.cs
 * Author:      Étienne Ménard
 * Description: Countdown before the start of the game.
 */

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
    private bool started = false;
    [SerializeField] private int time;
    public TextMeshProUGUI countdown;

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
        InitialStart();
    }

    private void Update()
    {
        if (!started)   // should take care of the issue where the game doesn't start
        {
            InitialStart();
        }
    }

    private void InitialStart()
    {
        DisableGameComponents();

        audioSource.volume = audioController.sfx / 10;

        StartCoroutine(Countdown(time));
        started = true;
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

    public void GameOverRestart()
    {
        StartCoroutine(RestartCountdown(time));
    }

    private IEnumerator RestartCountdown(int seconds)
    {
        int count = seconds;

        do
        {
            countdown.text = count.ToString();
            audioSource.Play();

            yield return new WaitForSecondsRealtime(1);
            count--;
        } while (count > 0);

        RestartGameRoutine();
        countdown.enabled = false;
    }

    public void RestartGameRoutine()
    {
        tracksManager.gameStarted = true;

        nextPiece.DisplayNextPiece();   // shows next piece;
        spawner.SpawnNext();

        pauseMenu.enabled = true;       // can pause game
        pauseButton.SetActive(true);    // enables pause button
    }
}
