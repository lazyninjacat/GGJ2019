using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Volume capture was based off code provided to the public by aldonaletto
/// at https://answers.unity.com/questions/157940/getoutputdata-and-getspectrumdata-they-represent-t.html
/// </summary>
public class VolumeCapture : MonoBehaviour
{
    public float RmsValue;
    public float DbValue;

    private const float DELAY = 0.15f;
    private const int QSamples = 1024;
    private const float RefValue = 0.1f;
    private const float Threshold = 0.02f;

    public Text volumeText;
    //public AudioSource sound;

    float[] _samples;
    private float[] _spectrum;
    private float _fSample;
    private float _currentDbs { get; set; }
    private AudioClip _clipRecord;

    //public VolumeCapture(){ }

    //public void Init()
    void Start()
    {
        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;
        _clipRecord = Microphone.Start(Microphone.devices[0], true, 999, 44100);
    }

    public void Init() { }

    /// <summary>
    /// Starts the decibel analysis coroutine
    /// </summary>
    public void StartMic()
    {
        DataStore.barOn = true;
        StartCoroutine("DelayedAnalysis");
    }

    /// <summary>
    /// Stops the decibel analysis coroutine
    /// </summary>
    public void StopMic()
    {
        DataStore.barOn = false;
        StopCoroutine("DelayedAnalysis");
    }

    /// <summary>
    /// Coroutine waits for the set DELAY time and then calls AnalyzeSound.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayedAnalysis()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            AnalyzeSound();
        }
    }
    
    void AnalyzeSound()
    {
        int micPosition = Microphone.GetPosition(null) - (QSamples + 1);

        _clipRecord.GetData(_samples, micPosition);
        int i;
        float sum = 0;
        for (i = 0; i < QSamples; i++)
        {
            sum += _samples[i] * _samples[i]; // sum squared samples
        }

        RmsValue = Mathf.Sqrt(sum / QSamples); // rms = square root of average
        DbValue = 20 * Mathf.Log10(RmsValue / RefValue); // calculate dB
        if (DbValue < -160) DbValue = -160; // clamp it to -160dB min
                                            // get sound spectrum
        DataStore.savedDbValue = DbValue;

        if (DbValue > _currentDbs)
        {
            _currentDbs = DbValue;
        }
        volumeText.text = "Volume: " + DbValue.ToString();
        /*
            if (DbValue < -30f)
            {
                Debug.Log("TOO QUIET PUMP IT UP! \n ");
            }
            else if (DbValue > -30f && DbValue < -10f)
            {
                Debug.Log("sHOt FiRED! \n ");
            }
            else if (DbValue > -10f && DbValue < 0f)
            {
                Debug.Log("DOU DOU DOU DOUBLE SHOT!!! \n ");
            }
            else if (DbValue > 0f && DbValue < 10f)
            {
                Debug.Log("trip THAT SHOT UP YO! \n ");
            }
            else if (DbValue > 10f)
            {
                Debug.Log("IMMMMMMMAAAAAAA FIRE MA LASER! \n ");
            }
        */
    }
}