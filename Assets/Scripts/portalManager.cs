using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public int portalIndex;

   

    private SceneTransition sceneTransition;

    void Start()
    {
        sceneTransition = FindObjectOfType<SceneTransition>();
        Destroy(this.gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerPrefs.SetInt("Health", GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>().health);
            PlayerPrefs.SetInt("CurrentScore", FindObjectOfType<GameManager>().score);
            sceneTransition.InitiateTransition(portalIndex);
        }
    }
   
   
}
