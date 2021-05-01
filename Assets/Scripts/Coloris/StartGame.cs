/*
 * File:        StartGame.cs
 * Author:      Étienne Ménard
 * Description: Countdown before the start of the game.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartGame : MonoBehaviour
{
    [Header("Components")]
    private RectTransform _rectTransform;
    private AudioSource _sfxSource;
    private AudioSource _musicSource;
    private TracksManager _tracksManager;
    private Spawner _spawner;
    private NextPiece _nextPiece;

    [Header("UI")]
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] GameObject pauseButton;

    [Header("Countdown")]
    private bool _started = false;
    [SerializeField] private int time;
    [SerializeField] private float juiceTime;
    public TextMeshProUGUI countdownText;
    private IEnumerator _countdownRoutine;
    [SerializeField] AudioClip countdownClip;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();     // rect transform for UI scaling
        countdownText = GetComponent<TextMeshProUGUI>();    // string being displayed on screen

        _tracksManager = FindObjectOfType<TracksManager>(); // script that controls the music played in this scene
        _spawner = FindObjectOfType<Spawner>();             // script that handles the spawning of pieces
        _nextPiece = FindObjectOfType<NextPiece>();         // script handling the Next Piece UI
    }

    private void Start()
    {
        _sfxSource = _tracksManager.audioController.sfxSource;
        _musicSource = _tracksManager.audioController.musicSource;
    }

    private void FixedUpdate()
    {
        if (!_started)   // should take care of the issue where the game doesn't start
        {
            InitialStart();
        }
    }

    private void InitialStart()
    {
        DisableGameComponents();

        _tracksManager.audioController.FadeInMusic(_tracksManager.audioController.music / 10, _tracksManager.audioController.music, 3f);

        _countdownRoutine = CountdownRoutine(time);
        StartCoroutine(_countdownRoutine);

        _started = true;
    }

    private IEnumerator CountdownRoutine(int seconds)
    {
        int count = seconds;
        _sfxSource.clip = countdownClip;

        do
        {
            countdownText.text = count.ToString();

            _sfxSource.Play();

            yield return new WaitForSecondsRealtime(1);
            count--;
        } while (count > 0);

        StartGameElements();
        countdownText.enabled = false;
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
    private void StartGameElements()
    {
        _tracksManager.gameStarted = true;
        _tracksManager.TogglePause();

        _nextPiece.DisplayNextPiece();   // shows next piece;
        _spawner.SpawnNext();            // spawns first piece

        pauseMenu.enabled = true;       // can pause game
        pauseButton.SetActive(true);    // enables pause button
    }

    public void GameOverRestartMusicFade()
    {
        _sfxSource.clip = null;

        _countdownRoutine = RestartCountdownRoutine(time);
        StartCoroutine(_countdownRoutine);

        _tracksManager.audioController.FadeInMusic(_tracksManager.audioController.music / 10, _tracksManager.audioController.music, 3f);
    }

    private IEnumerator RestartCountdownRoutine(int seconds)
    {
        int count = seconds;
        _sfxSource.clip = countdownClip;

        do
        {
            countdownText.text = count.ToString();
            _sfxSource.Play();

            yield return new WaitForSecondsRealtime(1);
            count--;
        } while (count > 0);

        RestartGameRoutine();
        countdownText.enabled = false;
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
