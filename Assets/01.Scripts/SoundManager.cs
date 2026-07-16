using UnityEngine;

public enum SFXType
{
    Jump,
    DoubleJump,
    Shoot
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioClip[] sfxClip;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        SetBGMVolume(PlayerPrefs.GetFloat(ConstString.BGMVolume, 0.5f));
        SetSFXVolume(PlayerPrefs.GetFloat(ConstString.SFXVolume, 0.5f));
        MuteBGM(PlayerPrefs.GetInt(ConstString.BGMMute, 0));
        MuteSFX(PlayerPrefs.GetInt(ConstString.SFXMute, 0));
    }

    public void SetBGMVolume(float vol)
    {
        bgmSource.volume = vol;
        PlayerPrefs.SetFloat(ConstString.BGMVolume, vol);
    }

    public void SetSFXVolume(float vol)
    {
        sfxSource.volume = vol;
        PlayerPrefs.SetFloat(ConstString.SFXVolume, vol);
    }

    public void MuteBGM(int mute)
    {
        if (mute == 1)
        {
            SetMuteBGM(true);
        }
        else
        {
            SetMuteBGM(false);
        }
    }

    public void MuteSFX(int mute)
    {
        if (mute == 1)
        {
            SetMuteSFX(true);
        }
        else
        {
            SetMuteSFX(false);
        }
    }

    // 음소거 설정 저장
    public void SetMuteBGM(bool isMute)
    {
        bgmSource.mute = isMute;
        PlayerPrefs.SetInt(ConstString.BGMMute, isMute ? 1 : 0);
    }

    public void SetMuteSFX(bool isMute)
    {
        sfxSource.mute = isMute;
        PlayerPrefs.SetInt(ConstString.SFXMute, isMute ? 1 : 0);
    }

    // 볼륨 정보 외부 제공용
    public float GetBGMVolume()
    {
        return bgmSource.volume;
    }

    public float GetSFXVolume()
    {
        return sfxSource.volume;
    }

    public void PlaySFX(SFXType type)
    {
        if ((int)type > sfxClip.Length)
            return;

        sfxSource.PlayOneShot(sfxClip[(int)type]);
    }
}
