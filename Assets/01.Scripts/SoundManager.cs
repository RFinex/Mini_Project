using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        SetBgmVolume(PlayerPrefs.GetFloat(ConstString.BGMVolume, 1));
        SetSFXVolume(PlayerPrefs.GetFloat(ConstString.SFXVolume, 1));
        SetBGMMute(PlayerPrefs.GetInt(ConstString.BGMMute, 0));
        SetSFXMute(PlayerPrefs.GetInt(ConstString.SFXMute, 0));
    }

    public void SetBgmVolume(float vol)
    {
        bgmSource.volume = vol;
        PlayerPrefs.SetFloat(ConstString.BGMVolume, vol);
    }

    public void SetSFXVolume(float vol)
    {
        sfxSource.volume = vol;
        PlayerPrefs.SetFloat(ConstString.SFXVolume, vol);
    }

    public void SetBGMMute(int mute)
    {
        if (mute == 1)
        {
            MuteBGM(true);
        }
    }

    public void SetSFXMute(int mute)
    {
        if (mute == 1)
        {
            MuteSFX(true);
        }
    }

    public void MuteBGM(bool isMute)
    {
        bgmSource.mute = isMute;
        PlayerPrefs.SetInt(ConstString.BGMMute, isMute ? 1 : 0);
    }

    public void MuteSFX(bool isMute)
    {
        sfxSource.mute = isMute;
        PlayerPrefs.SetInt(ConstString.SFXMute, isMute ? 1 : 0);
    }

    public float GetBGMVolume()
    {
        return bgmSource.volume;
    }

    public float GetSFXVolume()
    {
        return sfxSource.volume;
    }
}
