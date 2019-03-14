using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class player : MonoBehaviour {
    public GameObject audioSRC;
    public Object[] clips;
    int times = 0;
    public AudioClip sound;
    int[] randomList;
    int leng;
    public float FadeSpeed;
    public float MaxVolume;
    public bool isPaused=false;
    public string CurrentSongName;
    public GameObject back;

    // Use this for initialization
    public void setBackButton()
    {
        back.GetComponent<Button>().onClick.AddListener( () => { this.GetComponent<UICOntroller>().MenuChanger(this.GetComponent<UICOntroller>().Cmenu);   });
    }
    IEnumerator FadeOut(string state)
    {
        float volume = audioSRC.GetComponent<AudioSource>().volume;
        while (volume > 0)
        {
            volume -= FadeSpeed;
            if (volume < 0)
            {
                volume = 0;
            }
            audioSRC.GetComponent<AudioSource>().volume = volume;
            yield return new WaitForFixedUpdate();
        }
        if (state=="stop")
        {
            audioSRC.GetComponent<AudioSource>().Stop();
            audioSRC.GetComponent<AudioSource>().clip = null;
            isPaused = true;

        }
        if (state.Contains("ps"))
        {
            int index = int.Parse( state.Substring(2, state.Length - 2));
            
            sound = clips[index] as AudioClip;
            audioSRC.GetComponent<AudioSource>().clip = sound;

            audioSRC.GetComponent<AudioSource>().Play();
            isPaused = false;

            StartCoroutine(FadeIn());


        }
        if (state == "pause"){
            audioSRC.GetComponent<AudioSource>().Pause();

        }
    }
    IEnumerator FadeIn() { 

            float volume = 0;
            while (volume < MaxVolume)
            {
                volume += FadeSpeed;
                if (volume > MaxVolume)
                {
                    volume = MaxVolume;
                }
                audioSRC.GetComponent<AudioSource>().volume = volume;
                yield return new WaitForFixedUpdate();
            }
        

    }

    void randomInit(int len)
    {
        randomList = new int[len];

    }
    public void InitName(string playlist)
    {
        clips= Resources.LoadAll(playlist);
        times = 0;
        isPaused = false;

    }
    public void InitLenght()
    {
        randomInit(clips.Length);
        leng = clips.Length;
    }

    void Start () {
                audioSRC.GetComponent<AudioSource>().volume =MaxVolume;

    }
    bool randomFullTest()
    {
        for (int i = 0; i < randomList.Length; i++)
        {
            if (randomList[i] !=1 )
                return false;
        }
        return true;
    }
    public void Play()

    {
        if (times > 0)
        {
            if (audioSRC.GetComponent<AudioSource>().isPlaying)
            {
                StopAllCoroutines();
                StartCoroutine(FadeOut("pause"));
                isPaused = true;
            }
            else
            {
                audioSRC.GetComponent<AudioSource>().UnPause();
                StopAllCoroutines();
                StartCoroutine( FadeIn());
                isPaused = false;
            }
        }

        if (times == 0)
        {
            int randomIndex = Random.Range(0, clips.Length);
            bool x = true;
            while (x)
            {
                if (randomList[randomIndex] != 1)
                {
                    x = false;
                    randomList[randomIndex] = 1;
                }
                else
                { randomIndex = Random.Range(0, clips.Length); }
            }
            sound = clips[randomIndex] as AudioClip;
            audioSRC.GetComponent<AudioSource>().clip = sound;
            CurrentSongName = this.GetComponent<player>().clips[randomIndex].name;

            audioSRC.GetComponent<AudioSource>().Play();
            StopAllCoroutines();
            StartCoroutine(FadeIn());

            isPaused = false;

            times++;
        }
    }
    public void PlaySpecific(int index)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut("ps" + index.ToString()));
    }
    public void next()
    {
        if (randomFullTest() == true)
        {
            randomInit(leng);
        }
        int randomIndex = Random.Range(0, clips.Length);
        bool x = true;
        while (x)
        {
            if (randomList[randomIndex] != 1)
            {
                x = false;
                randomList[randomIndex] = 1;
            }
            else
                randomIndex = Random.Range(0, clips.Length);
        }

        CurrentSongName =this.GetComponent<player>().clips[randomIndex].name;

        StopAllCoroutines();
        if (times == 0)
        {
            sound = clips[randomIndex] as AudioClip;
            audioSRC.GetComponent<AudioSource>().clip = sound;

            audioSRC.GetComponent<AudioSource>().Play();

            StartCoroutine(FadeIn());
        }
        else

        StartCoroutine(FadeOut("ps" + randomIndex.ToString()));
        times++;


    }
    public void stop()
    {
        StopAllCoroutines();

        StartCoroutine( FadeOut("stop"));
    }
    // Update is called once per frame
    void Update () {
        if (audioSRC.GetComponent<AudioSource>().isPlaying == false)
        {
            if (isPaused == false)
            {
                next();
            }
        }
        string val="";
        foreach (int el in randomList)
        {
            val += el + ",";
            
        }
         Debug.Log(val);
        //Debug.Log(audioSRC.GetComponent<AudioSource>().volume);
       // Debug.Log(CurrentSongName);


}
}
