using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {
    
    public void Quit()
    {

        StartCoroutine(wait());
    }

    IEnumerator wait() {
        yield return new WaitForSeconds(1);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
