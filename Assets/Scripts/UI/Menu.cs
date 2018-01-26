using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ButtonAttributes
{
    public string buttonText;
    public delegate void action();
    public action clickAction;

    public ButtonAttributes(string buttonText_, action action_) {
        buttonText = buttonText_;
        clickAction = action_;
    }
}

public class Menu : MonoBehaviour {

    public GameObject[] panels;

    public void ShowMenu(string titleText, ButtonAttributes[] buttonAttributes, Transform transform) {
        var prefab = Resources.Load("Prefabs/Menu");
        GameObject menu = (GameObject)Instantiate(prefab, transform);
        menu.GetComponent<Menu>().Init(buttonAttributes);

    }

    // Use this for initialization
    public void Init(ButtonAttributes[] buttonAttributes) {

        foreach (var buttonAttribute in buttonAttributes) {
            var prefab = Resources.Load("Prefabs/ButtonPrefab");
            GameObject button = (GameObject)Instantiate(prefab, this.transform);
            button.GetComponentInChildren<Text>().text = buttonAttribute.buttonText;
            button.GetComponent<Button>().onClick.AddListener(delegate { buttonAttribute.clickAction(); });
        }

    }

    public void LoadScene(string name) {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void QuitScene() {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

    public void ShowPanel(int panelIndex) {
        panels[panelIndex].SetActive(true);
        //this.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }
}
