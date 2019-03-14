using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoad : MonoBehaviour {
    public GameObject slide;
    public GameObject button;
	// Use this for initialization
    public void Load()
    {

        StartCoroutine(LoadAs());
        slide.SetActive(true);
        button.GetComponent<Button>().interactable = false;
        
    }
    IEnumerator LoadAs()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(1);
        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            slide.GetComponent<Slider>().value = progress*4;
            yield return null;
        }

    }
    void Start () {
        slide.SetActive(false);
        Application.targetFrameRate = 60;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
