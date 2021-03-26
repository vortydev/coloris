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
    [Header("Components")]
    private AudioSource _sfxSource;
    private AudioSource _musicSource;
    private TracksManager _tracksManager;
    private Spawner _spawner;
    private NextPiece _nextPiece;

    [Header("UI")]
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] GameObject pauseButton;

    [Header("Countdown")]
    private bool started = false;
    [SerializeField] private int time;
    public TextMeshProUGUI countdown;
    [SerializeField] AudioClip countdownClip;

    private void Awake()
    {   
        _tracksManager = FindObjectOfType<TracksManager>();
        _spawner = FindObjectOfType<Spawner>();
        _nextPiece = FindObjectOfType<NextPiece>();

        countdown = GetComponent<TextMeshProUGUI>();    // gets tmp component
    }

    private void Start()
    {
        _sfxSource = _tracksManager.audioController.sfxSource;
        _musicSource = _tracksManager.audioController.musicSource;
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

        _tracksManager.audioController.FadeInMusic(_tracksManager.audioController.music / 10, _tracksManager.audioController.music, 3f);

        StartCoroutine(Countdown(time));
        started = true;
    }

    private IEnumerator Countdown(int seconds)
    {
        int count = seconds;
        _sfxSource.clip = countdownClip;

        do
        {
            countdown.text = count.ToString();
            _sfxSource.Play();

            yield return new WaitForSecondsRealtime(1);
            count--;
        } while (count > 0);

        StartGameRoutine();
        countdown.enabled = false;
    }

    private void DisableGameComponents()
    {
        _tracksManager.gameStarted = false;
        _tracksManager.NextTrack();
        _tracksManager.TogglePause();

        pauseMenu.enabled = false;                      // cant pause game before the start of the game
        pauseButton.SetActive(false);                   // disables pause button
    }

    // enables components to play the game
    private void StartGameRoutine()
    {
        _tracksManager.gameStarted = true;
        _tracksManager.TogglePause();

        _nextPiece.DisplayNextPiece();   // shows next piece;
        _spawner.SpawnNext();            // spawns first piece

        pauseMenu.enabled = true;       // can pause game
        pauseButton.SetActive(true);    // enables pause button
    }

    public void GameOverRestart()
    {
        _sfxSource.clip = null;
        StartCoroutine(RestartCountdown(time));
        _tracksManager.audioController.FadeInMusic(_tracksManager.audioController.music / 10, _tracksManager.audioController.music, 3f);
    }

    private IEnumerator RestartCountdown(int seconds)
    {
        int count = seconds;
        _sfxSource.clip = countdownClip;

        do
        {
            countdown.text = count.ToString();
            _sfxSource.Play();

            yield return new WaitForSecondsRealtime(1);
            count--;
        } while (count > 0);

        RestartGameRoutine();
        countdown.enabled = false;
    }

    public void RestartGameRoutine()
    {
        _tracksManager.gameStarted = true;

        _nextPiece.DisplayNextPiece();   // shows next piece;
        _spawner.SpawnNext();

        pauseMenu.enabled = true;       // can pause game
        pauseButton.SetActive(true);    // enables pause button
    }
}
