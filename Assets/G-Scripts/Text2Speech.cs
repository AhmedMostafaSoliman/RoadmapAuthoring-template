using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;
using System.Threading.Tasks;

public class Text2Speech : MonoBehaviour
{
    public GameObject showText;

    SpVoice voice = new SpVoice();
    string sentence = "Bitterroot has been used to treat fever, sore throat, cough, and other health issues. Sṕiƛəm teaches us about health and wellness. For more information, click button B.";
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            showText.SetActive(true);
            voice.Speak(sentence, SpeechVoiceSpeakFlags.SVSFlagsAsync | SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
            delayTimeNew();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            sylixFactlink();
        }
    }

    public async void delayTimeNew()
    {
        await Task.Delay((int)(10f * 1000));
        showText.SetActive(false);

    }

    public void sylixFactlink()
    {
        Application.OpenURL("https://www.syilx.org/about-us/syilx-nation/");
    }
}
