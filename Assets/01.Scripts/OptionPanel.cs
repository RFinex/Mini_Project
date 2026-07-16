using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button closeBtn;
    [SerializeField] private Toggle bgmMuteToggle;
    [SerializeField] private Toggle sfxMuteToggle;

    private void Awake()
    {
        bgmSlider.onValueChanged.AddListener(BGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(SFXVolumeChanged);
        closeBtn.onClick.AddListener(UIManager.instance.CloseOptionPanel);
        bgmMuteToggle.onValueChanged.AddListener(BGMMute);
        sfxMuteToggle.onValueChanged.AddListener(SFXMute);
    }

    private void OnEnable()
    {
        bgmSlider.value = SoundManager.instance.GetBGMVolume();
        sfxSlider.value = SoundManager.instance.GetSFXVolume();

        if (PlayerPrefs.GetInt(ConstString.BGMMute) == 1)
        {
            bgmMuteToggle.isOn = true;
        }

        if (PlayerPrefs.GetInt(ConstString.SFXMute) == 1)
        {
            sfxMuteToggle.isOn = true;
        }
    }

    private void BGMVolumeChanged(float vol)
    {
        SoundManager.instance.SetBGMVolume(vol);
    }

    private void SFXVolumeChanged(float vol)
    {
        SoundManager.instance.SetSFXVolume(vol);
    }

    private void BGMMute(bool isMute)
    {
        SoundManager.instance.SetMuteBGM(isMute);
    }
    private void SFXMute(bool isMute)
    {
        SoundManager.instance.SetMuteSFX(isMute);
    }
}
