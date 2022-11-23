using Common;
using Loading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class BaseToggle : MonoBehaviour
{
    private Toggle _toggle;
    
    protected SignalBus _signalBus;
    protected SaveSystem _saveSystem;
    protected SoundManager _soundManager;

    protected bool _isMute;

    [Inject]
    public void Construct(SignalBus signalBus, SaveSystem saveSystem)
    {
        _signalBus = signalBus;
        _saveSystem = saveSystem;
    }

    protected virtual void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _soundManager = FindObjectOfType<SoundManager>();
    }

    protected abstract void Start();


    protected virtual void ChangeToggleValue(bool isMute)
    {
        if (isMute)
            _toggle.isOn = false;
        else
            _toggle.isOn = true;
    }

    public virtual void ChangeToggleValue()
    {
        _isMute = !_toggle.isOn;
    }

}