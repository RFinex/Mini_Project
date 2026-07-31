using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : PopupBase
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
        closeBtn.onClick.AddListener(ClosePopup);
        bgmMuteToggle.onValueChanged.AddListener(BGMMute);
        sfxMuteToggle.onValueChanged.AddListener(SFXMute);
    }    

    private void UpdateSoundInfo()
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

    public void Open()
    {
        OpenPopup();
        UpdateSoundInfo();
    }

    public void OpenPopup()
    {
        seq?.Kill();

        seq = DOTween.Sequence();
        seq.Append(transform.DOScale(data.popupSize, data.popupDelay))
            .Append(transform.DOScale(data.openSize, data.popupDelay))
            .SetLink(gameObject, LinkBehaviour.KillOnDisable);
    }

    public void ClosePopup()
    {
        seq?.Kill();

        seq = DOTween.Sequence();
        seq.Append(transform.DOScale(data.popupSize, data.popupDelay))
            .Append(transform.DOScale(data.closeSize, data.popupDelay))
            .SetLink(gameObject, LinkBehaviour.KillOnDisable)
            .OnComplete(() => UIManager.instance.CloseOptionPanel());
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
