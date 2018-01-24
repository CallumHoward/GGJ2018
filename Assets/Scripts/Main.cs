using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    private ButtonAttributes[] buttons = new ButtonAttributes[] {
            //new ButtonAttributes("BUTTON LABEL", delegate { Menu.LoadScene("SceneName"); }),
            new ButtonAttributes("QUIT", delegate { Menu.QuitScene(); }),
        };

    // Use this for initialization
    void Start() {
        Menu.ShowMenu("Menu", buttons, GameObject.Find("MenuCanvas").transform);
    }

    // Update is called once per frame
    void Update() {

    }
}
