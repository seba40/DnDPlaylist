using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICOntroller : MonoBehaviour {
    public GameObject play;
    public Sprite ply;
    public Sprite pause;
    public GameObject nameText;
    public GameObject songText;
    public GameObject coverArt;
    public GameObject Cmenu;
    public void setCoverArt(string name)
    {
        Object img = Resources.Load<Sprite>("Images/" + name);
        coverArt.GetComponent<Image>().sprite = img as Sprite;
    }

    public void MenuChanger(GameObject display)
    {
        GameObject[] menus = GameObject.FindGameObjectsWithTag("UI");
        foreach (GameObject menu in menus)
        {
            menu.GetComponent<Canvas>().enabled = false;
        }

        display.GetComponent<Canvas>().enabled = true;
    }
    public void CHangeIcon()
    {
        if( play.GetComponent<Image>().sprite.name == "play")
        {
            play.GetComponent<Image>().sprite = pause;
        }
        else
        {
            play.GetComponent<Image>().sprite = ply;
        }
    }
    public void nextIconChange()
    {
        play.GetComponent<Image>().sprite = pause;

    }
    public void playName(string name)
    {
        nameText.GetComponent<Text>().text = name;
    }
    public void setSongName()
    {
        songText.GetComponent<Text>().text = this.GetComponent<player>().sound.name;
    }
    public void currentMenu(GameObject menu)
    {
        Cmenu = menu;
        
    }
    // Use this for initialization
    void Start () {

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        MenuChanger(GameObject.Find("Main Menu"));
        GameObject[] scrolls;
        scrolls = GameObject.FindGameObjectsWithTag("Scroll");
        foreach (GameObject scroll in scrolls)
        {
            scroll.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
        }



    }

    // Update is called once per frame
    void Update () {

        setSongName();
        if (this.GetComponent<player>().isPaused == false)
        {
            play.GetComponent<Image>().sprite = pause;
        }
        else
            play.GetComponent<Image>().sprite = ply;
    }
}
