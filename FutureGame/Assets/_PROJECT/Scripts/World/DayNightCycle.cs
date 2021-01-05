using UnityEngine;
using System;

public class DayNightCycle : MonoBehaviour
{

    //SunAngle.x 90 = high noon 12.00 pm
    //SunAngle.x 180 = sun setting
    //SunAngle.x 270 = midnight 12.00 am
    //SunAngle.x 0 = sun rise

    [Header("Colors")]
    [SerializeField] private Gradient skyColorGradient;
    [SerializeField] private Gradient equatorColorGradient;
    [SerializeField] private Gradient groundColorGradient;

    [Header("Sun Intensity")]
    [SerializeField] private float dayIntensity = 0.97f;
    [SerializeField] private float nightIntensity = 0.01f;
    [SerializeField] private float _intensityLerpTime;
    [SerializeField] private bool _isDay;

    private bool _intensityIsLerping;
    private float _intensity;
    [SerializeField] private float startIntensity = 0;
    [SerializeField] private float endIntensity = 0;

    [Header("Sun")]
    [SerializeField] private Transform sunTransform;
    [SerializeField] private float rotationAmount;
    [SerializeField] private Vector3 sunAngle;

    private Light _sunLight;

    [Header("Fog")]
    [SerializeField] private float fogDensity;
    [SerializeField] private float fogEnterTime;
    [SerializeField] private bool fogIsEnabled;

    private bool _morningDusk;
    private bool _eveningDusk;
    private float _currentDensity;

    private bool _newDay;
    private WorldManager _worldManager;

    //Lerping
    private LerpValueFloat _lerpValueDusk = new LerpValueFloat();
    private LerpValueFloat _lerpValueIntensity = new LerpValueFloat();

    private void Awake()
    {
        _sunLight = sunTransform.GetComponent<Light>();
        _worldManager = GetComponent<WorldManager>();
    }

    private void Update()
    {
        Cycle();
    }

    private void Cycle()
    {
        TimeOfDay();
        RotateSun();
        Intensity();
        Dusk();
    }

    #region Sun

    private void RotateSun()
    {
        sunAngle.x += rotationAmount * Time.deltaTime;
        sunTransform.eulerAngles = sunAngle;
        if (sunAngle.x >= 270 && !_newDay)
        {
            _worldManager.WorldClock.AddDay(1);
            _newDay = true;
        }
        if (sunAngle.x >= 360)
        {
            sunAngle.x = 0;
            _newDay = false;
        }
    }

    private void Intensity()
    {
        if (sunAngle.x >= 354 && !_isDay)
        {
            if (startIntensity == 0 && endIntensity == 0)
            {
                startIntensity = nightIntensity;
                endIntensity = dayIntensity;
            }
            _intensityIsLerping = true;
        }
        if (sunAngle.x >= 175 && _isDay)
        {
            if (startIntensity == 0 && endIntensity == 0)
            {
                startIntensity = dayIntensity;
                endIntensity = nightIntensity;
            }
            _intensityIsLerping = true;
        }
        if (_intensityIsLerping)
        {
            _intensity = LerpValue(startIntensity, endIntensity, _intensityLerpTime, _lerpValueIntensity);
            _sunLight.intensity = _intensity;
            if (_intensity == endIntensity)
            {
                _intensityIsLerping = false;
                endIntensity = 0;
                startIntensity = 0;
                _lerpValueIntensity.LerpValue = 0;
            }
        }
    }

    #endregion

    #region Dusk

    //evening dusk at 175
    //morning dusk at 354

    private void MorningDusk()
    {
        _morningDusk = true;
    }

    private void EveningDusk()
    {
        _eveningDusk = true;
    }

    private void Dusk()
    {
        if (sunAngle.x >= 354 && fogIsEnabled)
        {
            MorningDusk();
        }
        if (sunAngle.x >= 175 && !fogIsEnabled)
        {
            EveningDusk();
        }
        if (_morningDusk)
        {
            _currentDensity = LerpValue(fogDensity, 0, fogEnterTime, _lerpValueDusk);
            RenderSettings.fogDensity = _currentDensity;
            if (_currentDensity == 0)
            {
                _morningDusk = false;
                fogIsEnabled = false;
                _lerpValueDusk.LerpValue = 0;
            }
        }
        if (_eveningDusk)
        {
            _currentDensity = LerpValue(0, fogDensity, fogEnterTime, _lerpValueDusk);
            RenderSettings.fogDensity = _currentDensity;
            if (_currentDensity == fogDensity)
            {
                _eveningDusk = false;
                fogIsEnabled = true;
                _lerpValueDusk.LerpValue = 0;
            }
        }
    }

    #endregion

    #region TimeOfTheDay

    private void TimeOfDay()
    {
        if ((sunAngle.x >= 200 && sunAngle.x < 347) && _isDay) 
        {
            _isDay = false;
            MessageSender.SendMessageToClients("EveningFallsEvent");
        }
        else if (sunAngle.x >= 347 && !_isDay)
        {
            _isDay = true;
            MessageSender.SendMessageToClients("MorningRisesEvent");
        }
    }

    #endregion

    private float LerpValue(float startValue, float endValue, float lerpTime, LerpValueFloat lerpValue)
    {
        lerpValue.LerpValue += Time.deltaTime / lerpTime;
        return Mathf.Lerp(startValue, endValue, lerpValue.LerpValue);
    }

}

[Serializable]
public class LerpValueFloat
{
    public float LerpValue;
}


