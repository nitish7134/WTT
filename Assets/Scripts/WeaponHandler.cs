using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public float weaponReloadTime = 1f;
    bool canAttack = true;

    IEnumerator Attack(){

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 500)){
            if(hit.collider.tag == "Enemy"){
            hit.collider.GetComponent<EnemyMovement>().health--;
            hit.collider.GetComponent<Animator>().SetTrigger("Damaged");
            }
        }

        this.GetComponent<AudioSource>().Play();    
        this.GetComponent<Animator>().SetBool("shoot",true);
        yield return new WaitForSeconds(weaponReloadTime);
        this.GetComponent<Animator>().SetBool("shoot",false);
        canAttack = true;
    }

    public void CreateAttack(){
        if(canAttack == true){
            canAttack = false;
            StopCoroutine("Attack");
            StartCoroutine("Attack");
        }
    }

}
