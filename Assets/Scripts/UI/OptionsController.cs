using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Sprite noVolume;
    [SerializeField] Sprite Volume;

    [SerializeField] Slider music;
    [SerializeField] Slider sfx;

    [SerializeField] Button VolumeButton;

    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;

    private GlobalController globalController;

    private void Start()
    {
        globalController = GameObject.FindGameObjectWithTag("Singleton").GetComponent<GlobalController>();
        SetVolumeMusic(globalController.musicVolume);
        SetVolumeSFX(globalController.sfxVolume);
        music.value = globalController.musicVolume;
        sfx.value = globalController.sfxVolume;
    }

    private bool isVolumeOn = true;

    public void ClickVolume()
    {
        isVolumeOn = !isVolumeOn;

        if(isVolumeOn == true)
        {
            VolumeButton.image.sprite = Volume;
            music.value = 0.5f;
            sfx.value = 0.5f;
        }
        else
        {
            VolumeButton.image.sprite = noVolume;
            music.value = 0.00001f;
            sfx.value = 0.00001f;
        }
    }

    public void SetVolumeMusic(float volume)
    {
        MusicMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
        globalController.musicVolume = volume;
        CheckVolumeValue();
    }

    public void SetVolumeSFX(float volume)
    {
        SFXMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20f);
        globalController.sfxVolume = volume;
        CheckVolumeValue();
    }

    private void CheckVolumeValue()
    {
        float sfxVol = 0;
        float musicVol = 0;
        SFXMixer.GetFloat(("SFXVolume"), out sfxVol);
        MusicMixer.GetFloat(("MusicVolume"), out musicVol);

        float musicDb = Mathf.Pow(10.0f, musicVol / 20.0f);
        float sfxDb = Mathf.Pow(10.0f, sfxVol / 20.0f);

        if (musicDb == 0.00001f && sfxDb == 0.00001f)
        {
            isVolumeOn = false;
            VolumeButton.image.sprite = noVolume;
        }
        else
        {
            isVolumeOn = true;
            VolumeButton.image.sprite = Volume;
        }
    }
}
