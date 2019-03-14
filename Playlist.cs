using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Playlist : MonoBehaviour {
    public GameObject but;
    public GameObject parent;
    public GameObject PlaylistName;
    public GameObject SetName;
    public GameObject script1;
    public ScrollRect scroll;
    GameObject[] buttonList;
    public void setColor()

    {
        GameObject[] buttons;

     

            for (int i =0;i< this.GetComponent<player>().clips.Length;i++)
            {
                    ColorBlock cb = buttonList[i].GetComponent<Button>().colors;
                    cb.normalColor = new Color(0.3882353f, 0.09019608f, 0.09019608f, 0);
            buttonList[i].GetComponent<Button>().colors = cb;
               
            }
        
       
            for (int i = 0; i < this.GetComponent<player>().clips.Length; i++)
            {
                if (this.GetComponent<player>().CurrentSongName == GetSongName(i))
                {


                    ColorBlock cb = buttonList[i].GetComponent<Button>().colors;
                    cb.normalColor = new Color(0.3882353f, 0.09019608f, 0.09019608f, 1);
                buttonList[i].GetComponent<Button>().colors = cb;
                }
            }
        

    }
    string GetSongName(int index)
    {
        return this.GetComponent<player>().clips[index].name;
    }

    public void createButtons()
    { Vector3 position ;
        int number = this.GetComponent<player>().clips.Length;
        buttonList = new GameObject[number];
        but.GetComponentInChildren<Text>().text = GetSongName(0);
        but.GetComponent<Button>().onClick.AddListener(() => {
            script1.GetComponent<player>().CurrentSongName = GetSongName(0); script1.GetComponent<player>().PlaySpecific(0); script1.GetComponent<UICOntroller>().nextIconChange();
            setColor(); 
        });

        foreach (Transform child in parent.transform)
        {   if (child.name!= "Button (1)")
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 2; i <= number; i++)
        {
            position = new Vector3(but.GetComponent<RectTransform>().localPosition.x, but.GetComponent<RectTransform>().localPosition.y, but.GetComponent<RectTransform>().localPosition.z);


            GameObject clone= Instantiate(but, but.transform.position, but.transform.rotation);
            

            clone.transform.parent = parent.transform;
            clone.GetComponent<RectTransform>().localPosition = position;
            clone.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            clone.GetComponentInChildren<Text>().text = GetSongName(i-1);
            int index = i - 1;
            clone.GetComponent<Button>().onClick.AddListener(() => {
                script1.GetComponent<player>().CurrentSongName=GetSongName(index); script1.GetComponent<player>().PlaySpecific(index); script1.GetComponent<UICOntroller>().nextIconChange();
                setColor(); 
            });
            buttonList[i - 1] = clone;
        }
        scroll.verticalNormalizedPosition = 1;
        buttonList[0] = but;

        setColor();

    }

    // Use this for initialization
    void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update () {
        PlaylistName.GetComponent<Text>().text = SetName.GetComponent<Text>().text;

    }
}
