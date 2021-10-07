using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BGM : MonoBehaviour
{
    static BGM _instance;

    AudioSource audioSource;

    [SerializeField] AudioClip menuBGM;
    [SerializeField] AudioClip[] levelsBGM;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName.Equals("Options"))
        {
            var musicVolSlider = FindObjectOfType<Slider>();
            PlayerPrefs.SetFloat("MusicVolume", (float)musicVolSlider.value / musicVolSlider.maxValue);
            audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        }

        if (currentSceneName.Contains("Level ") && !currentSceneName.Equals("Level Select"))
        {
            int currentLevelNum = int.Parse(currentSceneName.Replace("Level ", ""));
            audioSource.clip = levelsBGM[currentLevelNum - 1];
            if (audioSource.isPlaying) { return; }
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = menuBGM;
            if (audioSource.isPlaying) { return; }
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
