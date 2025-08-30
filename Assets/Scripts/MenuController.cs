using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Refs")]
    public GameObject settingsPanel;
    public GameObject menuContainer;
    public Slider volumeSlider;
    public AudioMixer masterMixer;

    const string VOL_KEY = "master_volume_linear";

    void Awake()
    {
        // Get saved value or default to 1
        float savedValue = PlayerPrefs.GetFloat(VOL_KEY, 1f);

        // Set slider UI
        if (volumeSlider != null)
            volumeSlider.SetValueWithoutNotify(savedValue);

        // Apply to mixer
        SetVolumeFromSlider(savedValue);
    }

    public void OnStartClicked() => SceneManager.LoadScene("Game");

    public void OnSettingsClicked()
    {
        settingsPanel.SetActive(true);
        menuContainer.SetActive(false);
    }

    public void OnBackFromSettings()
    {
        settingsPanel.SetActive(false);
        menuContainer.SetActive(true);
    }

    public void OnQuitClicked()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Called by slider
    public void OnVolumeChanged(float value)
    {
        SetVolumeFromSlider(value);
    }

    void SetVolumeFromSlider(float sliderValue)
    {
        if (masterMixer == null) return;

        // Avoid log(0) crash by using tiny epsilon
        float linear = Mathf.Clamp(sliderValue, 0.0001f, 1f);

        // Convert linear 0–1 to dB
        float db = Mathf.Log10(linear) * 20f;
        masterMixer.SetFloat("MasterVolume", db);

        // Save PlayerPrefs
        PlayerPrefs.SetFloat(VOL_KEY, sliderValue);
    }
}
