using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    //static bool created = false;
    private Animator sceneTransitionAnimation;
    private Canvas canvas;

    private void Awake()
    {
        /*if(!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }*/

        sceneTransitionAnimation = GetComponent<Animator>();
        canvas = this.GetComponent<Canvas>();
    }

    public void InitiateTransition()
    {
        canvas.sortingOrder = 3;
        StartCoroutine(SceneTransitionController());
    }

    private IEnumerator SceneTransitionController()
    {
        sceneTransitionAnimation.SetBool("sceneTransition", true);
        yield return new WaitForSeconds(1.0f);
        sceneTransitionAnimation.SetBool("sceneTransition", false);
        canvas.sortingOrder = -1;
    }
}
