using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    //static bool created = false;
    private Animator sceneTransitionAnimation;
    private Canvas canvas;
    private Slider progressSlider;

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

    public void InitiateTransition(int portalIndex)
    {
        
        canvas.sortingOrder = 3;
        sceneTransitionAnimation.SetBool("sceneTransition", true);
        StartCoroutine(LoadScene(portalIndex + 1));
        //StartCoroutine(SceneTransitionController());
    }

    private IEnumerator SceneTransitionController()
    {
        
        yield return new WaitForSeconds(1.0f);
        sceneTransitionAnimation.SetBool("sceneTransition", false);
        //canvas.sortingOrder = -1;
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        yield return new WaitForSeconds(0.5f);
        progressSlider = GetComponentInChildren<Slider>();

        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneIndex);
        asyncScene.allowSceneActivation = false;

        while (!asyncScene.isDone)
        {
            progressSlider.value = Mathf.Clamp01(asyncScene.progress / 0.9f);
            Debug.Log(progressSlider.value);
            if (progressSlider.value == 1)
            {
                asyncScene.allowSceneActivation = true;
            }
            yield return null;
        }
    }

}
