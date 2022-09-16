using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioSettings : MonoBehaviour
{
    public string mixerTarget;
    public string prefKey;
    public Slider Slider;
    public AudioMixerGroup Group;
    // Start is called before the first frame update
    void Start()
    {
        Slider.minValue = -80.0f;
        Slider.maxValue = 20.0f;

        Slider.value = PlayerPrefs.GetFloat(prefKey);
        Refresh();
    }

    public void OnChange()
    {
        PlayerPrefs.SetFloat(prefKey, Slider.value);
        Refresh();
    }

    public void Refresh()
    {
        Group.audioMixer.SetFloat(mixerTarget, Slider.value);
    }
}
