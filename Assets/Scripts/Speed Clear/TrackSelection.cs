/* File:        TrackSelection.cs
 * Author:      Étienne Ménard
 * Description: Handles the selection of the track that will be played during the Speed Clear gamemode.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class TrackSelection : MonoBehaviour
{
    private ReferenceTracks _tracks;
    private AudioSource _musicSource;
    [SerializeField] public TrackSO selectedTrack;
    private int _previousTrack = 0;
    [SerializeField] private int[] _highScores;

    public GameObject trackSelectionUI;
    [SerializeField] private GameObject bg;
    [SerializeField] GameObject timerUI;

    [SerializeField] List<GameObject> catalog;
    [SerializeField] GameObject catalogScroll;
    [SerializeField] GameObject catalogTrackPrefab;

    [SerializeField] TextMeshProUGUI selectedTrackName;
    [SerializeField] TextMeshProUGUI trackHighscore;

    [SerializeField] Button playButton;

    private void Awake()
    {
        _tracks = FindObjectOfType<ReferenceTracks>();
        _musicSource = FindObjectOfType<AudioController>().musicSource;
        trackSelectionUI = gameObject;

        _highScores = new int[_tracks.GetArraySize()];
        LoadCatalog();

        FindObjectOfType<UISFX>().LoadUIElements();
    }

    private void Start()
    {
        playButton.interactable = false;
        timerUI.SetActive(false);
    }

    private void LoadCatalog()
    {
        for (int i = 0; i < _tracks.GetArraySize(); i++)
        {
            GameObject newTrack = Instantiate(catalogTrackPrefab, catalogScroll.transform, false);
            newTrack.GetComponentInChildren<TextMeshProUGUI>().text = _tracks.GetTrackName(i);
            newTrack.gameObject.name = _tracks.GetTrackName(i);
            newTrack.transform.position = catalogScroll.transform.position;

            newTrack.GetComponent<TrackSelectionTrack>().trackNb = _tracks.GetTrackNb(i);

            catalog.Add(newTrack);

            _highScores[i] = GetTrackHighscore(i);
        }
    }

    public void SelectTrack(int trackNb)
    {
        selectedTrack = _tracks.refTracks[trackNb - 1];

        _musicSource.clip = selectedTrack.Clip;

        DisplaySelectedTrack(selectedTrack.TrackName);
        DisplayTrackHighScore(selectedTrack.TrackNb);

        _previousTrack = selectedTrack.TrackNb;

        if (selectedTrack != null)
        {
            playButton.interactable = true;
            timerUI.SetActive(true);
        }

        FindObjectOfType<Timer>().DisplayTrackDuration((int)selectedTrack.Clip.length);
    }

    public void RandomlySelectTrack()
    {
        int rng = Random.Range(1, _tracks.GetArraySize() + 1);

        if (_previousTrack != 0)
        {
            while (rng == _previousTrack)
            {
                rng = Random.Range(1, _tracks.GetArraySize() + 1);
            }
        }

        SelectTrack(rng);
    }

    private void DisplaySelectedTrack(string name)
    {
        selectedTrackName.text = "Selected: " + name;
    }

    private void DisplayTrackHighScore(int trackNb)
    {
        trackHighscore.text = "Highscore: " + _highScores[trackNb - 1].ToString();
    }

    public void QuitToMainMenu()
    {
        FindObjectOfType<AudioController>().KillAudio();
        SceneManager.LoadScene(0);
    }

    public void LoadClassicGameScene()
    {
        FindObjectOfType<EventSystem>().enabled = false;
        SceneManager.LoadScene("Coloris", LoadSceneMode.Additive);

        //GameObject.Find("GameAudio").GetComponent<TracksManager>().curTrack = selectedTrack;

        trackSelectionUI.SetActive(false);
        bg.SetActive(false);
    }

    private int GetTrackHighscore(int ind)
    {
        if (_highScores[ind] == 0)
        {
            switch (ind + 1)
            {
                case 1:
                    return PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.SP_HS_1_KEY, 0);
                case 2:
                    return PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.SP_HS_2_KEY, 0);
                case 3:
                    return PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.SP_HS_3_KEY, 0);
                case 4:
                    return PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.SP_HS_4_KEY, 0);
                case 5:
                    return PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.SP_HS_5_KEY, 0);
                case 6:
                    return PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.SP_HS_6_KEY, 0);
                case 7:
                    return PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.SP_HS_7_KEY, 0);
                case 8:
                    return PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.SP_HS_8_KEY, 0);
                case 9:
                    return PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.SP_HS_9_KEY, 0);
                case 10:
                    return PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.SP_HS_10_KEY, 0);
                case 11:
                    return PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.SP_HS_11_KEY, 0);
                case 12:
                    return PlayerPrefsManager.GetIntPlayerPref(PlayerPrefsManager.SP_HS_12_KEY, 0);
            }
        }

        return 0;
    }
}
