using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicPlayer;
    public List<AudioClip> songs;
    public List<string> songTitles;
    public List<string> songArtists;
    public List<string> songAlbums;
    public int songIndex;
    public TextMeshProUGUI songTitle;
    public GameObject songTitlePanel;
    public bool isSwitching;
    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();
        StartCoroutine(DisplaySongInfo());
    }

    // Update is called once per frame
    void Update()
    {
        if ((!musicPlayer.isPlaying && !isSwitching) || (Input.GetKeyDown(KeyCode.P) && !isSwitching))
        {
            musicPlayer.Stop();
            if (!(songIndex >= songs.Count - 1))
            {
                songIndex++;
                isSwitching = true;
            } else
            {
                songIndex = 0;
                isSwitching = true;
            }
            StartCoroutine(DisplaySongInfo());
        }
    }

    public IEnumerator DisplaySongInfo()
    {
        yield return new WaitForSecondsRealtime(.5f);
        musicPlayer.clip = songs[songIndex];
        musicPlayer.Play();
        isSwitching = false;
        songTitlePanel.SetActive(true);
        songTitle.text = $"{songArtists[songIndex]}\n{songTitles[songIndex]}\n{songAlbums[songIndex]}";
        yield return new WaitForSecondsRealtime(3);
        songTitlePanel.SetActive(false);
    }
}
