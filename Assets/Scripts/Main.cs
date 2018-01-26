using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public Menu menu;

    private ButtonAttributes[] buttons = new ButtonAttributes[] {
            //new ButtonAttributes("MULTIPLAYER", delegate { menu.ShowPanel(0); }),
            //new ButtonAttributes("QUIT", delegate { menu.QuitScene(); }),
        };

    // Use this for initialization
    void Start() {
        menu.ShowMenu("Menu", buttons, GameObject.Find("MenuCanvas").transform);
    }

    // Update is called once per frame
    void Update() {

    }
}
