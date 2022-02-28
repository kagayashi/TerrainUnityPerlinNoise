using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider sliderVolume;
    public Slider sliderAmplitude;
    public Slider sliderOctawy;
    public Slider sliderPersistance;

    public Text ampText;
    public Text octawyText;
    public Text persistanceText;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(("MixerVolume"), volume);

    }
    public void Start()
    {
        sliderAmplitude.value = DataHolder.Amplitude;
        sliderOctawy.value = DataHolder.Oktawy;
        sliderPersistance.value = DataHolder.Persistance*10;
        float v;
        audioMixer.GetFloat("MixerVolume",out v);
        sliderVolume.value = v;

        ampText.text = sliderAmplitude.value.ToString();
        octawyText.text = sliderOctawy.value.ToString();
        persistanceText.text = sliderPersistance.value.ToString();
    }
    public void Update()
    {
        ampText.text = sliderAmplitude.value.ToString();
        octawyText.text = sliderOctawy.value.ToString();
        persistanceText.text = sliderPersistance.value.ToString();
    }
    public void setOktawy(float Oktawy)
    {
        DataHolder.Oktawy = (int)(Oktawy);
    }
    public void setPersistance(float Persistance)
    {
        DataHolder.Persistance = Persistance/10;
    }
    public void setAmplitude(float Amplitude)
    {
        DataHolder.Amplitude = Amplitude;
    }

    
}
