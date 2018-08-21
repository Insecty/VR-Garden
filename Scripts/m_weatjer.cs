using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeatherType
{
    Foggy, Rain, Sunny, FallLeaves,Snow,Bugs,Unknown
}
public class m_weatjer : MonoBehaviour {
    private UniStormWeatherSystem_C weatherSys;
    public bool timeStopped = false;
    public bool staticWeather = false;
    public WeatherType initialWeather;
    public float startTime;
    public float dayTime;
    public float nightTime;
    private float dayinitime;
    private float nightinitime;
    public WeatherType currentWeather;
    public ParticleSystem fog;
    public ParticleSystem rain;
    public ParticleSystem snow;
    public ParticleSystem leaves;
    public ParticleSystem bugs;
    // Use this for initialization
    void Awake()
    {

        weatherSys = GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>();
        weatherSys.monthCounter = 10;
        weatherSys.timeStopped = timeStopped;
        weatherSys.staticWeather = staticWeather;
        weatherSys.startTimeHour = startTime;
        weatherSys.numberOfDaysInMonth = 3;
        if (!timeStopped)
        {
            weatherSys.dayLength = dayTime;
            weatherSys.nightLength = nightTime;
        }
        ChangeWeather(initialWeather);
    }
    void Start() {
        dayinitime = dayTime;
        nightinitime = nightTime;
    }
    private void LateUpdate()
    {
        if (!timeStopped)
        {
            if (weatherSys.dayLength != dayTime)
            {
                weatherSys.dayLength = dayTime;
            }
            if (weatherSys.nightLength != nightTime)
            {
                weatherSys.nightLength = nightTime;
            }
        }
    }
    // Update is called once per frame
    void Update() {
        //Debug.Log(weatherSys.Hour);
        currentWeather = CurrentWeather();
        ChangeWeather(currentWeather);
        // Debug.Log(weatherSys.monthCounter);
        //Debug.Log("Spring " + weatherSys.isSpring);
        //Debug.Log("Summer " + weatherSys.isSummer);
        //Debug.Log("Fall " + weatherSys.isFall);
        //Debug.Log("Winter " + weatherSys.isWinter);

        if (weatherSys.dayCounter >= 3)
        {
            weatherSys.dayCounter = 0;
            weatherSys.monthCounter++;
            if (weatherSys.monthCounter > 12)
            {
                weatherSys.monthCounter = 1;
            }
        }

        if (weatherSys.weatherForecaster == 12)
        {
            // rain.gameObject.SetActive(true);

        }
    }
    public void SetTimeSpeed(float day, float night)
    {
        dayTime = day;
        nightTime = night;
    }
    public void TimeSpeedBack()
    {
        dayTime = dayinitime;
        nightTime = nightinitime;
    }
    public WeatherType CurrentWeather()
    {
        int currWeatherType = weatherSys.weatherForecaster;
      //  Debug.Log(currWeatherType);
        switch (currWeatherType)
        {
            case 1:
                {
                    return WeatherType.Foggy;
                }
            case 8:
                {
                    return WeatherType.Sunny;
                }
            case 12:
                {
                    if (weatherSys.isWinter)
                    {
                        return WeatherType.Snow;
                    }
                    return WeatherType.Rain;
                }
            case 2:
                {
                    if (weatherSys.isWinter)
                    {
                        return WeatherType.Snow;
                    }
                    return WeatherType.Rain;
                }
            case 3:
                {
                    if (weatherSys.isWinter)
                    {
                        return WeatherType.Snow;
                    }
                    return WeatherType.Rain;
                }
            case 13:
                {
                    return WeatherType.FallLeaves;

                    
                }
            
            case 10:
                {
                    return WeatherType.Bugs;
                }
            default:
                {
                    return WeatherType.Unknown;
                }
        }
    }
    void ClearState(WeatherType weather)
    {
        
        if (fog.isPlaying&&weather!=WeatherType.Foggy)
        { fog.Stop(); }
        if (snow.isPlaying && weather != WeatherType.Snow)
        {
            snow.Stop();
        }
        if (leaves.isPlaying && weather != WeatherType.FallLeaves)
        {
            leaves.Stop();
        }
        if (rain.isPlaying && weather != WeatherType.Rain)
        {
            rain.Stop();
            NotificationCenter.DefaultCenter().PostNotification(this, "STOPRAIN");
        }
        if (bugs.isPlaying && weather != WeatherType.Bugs)
        {
            fog.Stop();
        }
    }
    public void ChangeWeather(WeatherType weather)
    {
        switch (weather)
        {

            case WeatherType.Foggy:
                {
                    weatherSys.ChangeWeather(1);
                    ClearState(WeatherType.Foggy);
                    if (!fog.isPlaying)
                    {
                        fog.Play();
                    }
                    break;
                }
            case WeatherType.Rain:
                {
                    weatherSys.ChangeWeather(12);
                    //rain.gameObject.SetActive(true);
                    ClearState(WeatherType.Rain);
                    if (!rain.isPlaying)
                    {
                        rain.Play();
                        NotificationCenter.DefaultCenter().PostNotification(this, "STARTRAIN");
                    }
                    break;
                }
            case WeatherType.Sunny:
                {
                    weatherSys.ChangeWeather(8);
                    ClearState(WeatherType.Sunny);
                    break;
                }
            case WeatherType.FallLeaves:
                {
                    weatherSys.ChangeWeather(13);
                    ClearState(WeatherType.FallLeaves);
                    if (!leaves.isPlaying)
                    {
                        leaves.Play();
                    }

                    break;
                }
            case WeatherType.Snow:
                {
                    weatherSys.ChangeWeather(2);
                    ClearState(WeatherType.Snow);
                    if (!snow.isPlaying)
                    {
                        snow.Play();
                    }

                    break;
                }
            case WeatherType.Bugs:
                {
                    weatherSys.ChangeWeather(10);
                    ClearState(WeatherType.Bugs);
                    if (bugs.isPaused)
                    {
                        bugs.Play();
                    }

                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
