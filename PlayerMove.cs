using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  


//first room
// movement (close)
    //finish movement to create graple hook
// platform touch ground for rejump
//rerendering after loading a new scene
//death scene w/ checkpoints 



public class playerObjects : MonoBehaviour{
    //player components
    public Transform cube;
    public Transform camera;


    //puzzel & door
    float dist_puzzel;
    public float timeOpen = 5;
    Vector3 other = new Vector3(237f,275f, 101f); 
    Vector3 posAfterPuzzel = new Vector3(259.0f, 313.1f, 145.0f);

    public bool offerPuzzel = false;
    public int doorOpen = 0;
    public Transform door;


    private void start(){
        //GameObject text = new GameObject();
        
    }

    void Update(){
        Puzzel();
        door = this.gameObject.transform.GetChild(1);
    }

    void Puzzel(){
        dist_puzzel = Vector3.Distance(other, transform.position);
        //print(dist_puzzel);
        if (dist_puzzel < 80){
            offerPuzzel = true;
        }
        if (offerPuzzel == true){
            givePromt();
        }
        if (doorOpen == 1){
            if (timeOpen > 0){
                door.transform.Translate(0f,1f,0f);
                timeOpen--;
            } else{
                doorOpen = 0;
            }
            //do something

        }
    }

    void givePromt(){
        //press "e" to play the puzzel and open the door\
        //print(transform.position);
        if (Input.GetKey("e")){
            SceneManager.LoadScene("puzzelOne");
            doorOpen =1;
        } 
    }

    void OnCollisionEnter(Collision col){


        //the way to die in the first room
        //not done yet
        if (col.gameObject.name == "bottom"){
            SceneManager.LoadScene("deathOne");
        }
    }
        
    void OnDisable(){
        PlayerPrefs.SetFloat("posX", posAfterPuzzel.x);
        PlayerPrefs.SetFloat("posY", posAfterPuzzel.y);
        PlayerPrefs.SetFloat("posZ", posAfterPuzzel.z);
        PlayerPrefs.SetInt("doorOpen", doorOpen);
    }

    void OnEnable(){
        float x;
        float y;
        float z;

        x = PlayerPrefs.GetFloat("posX");
        y = PlayerPrefs.GetFloat("posY");
        z = PlayerPrefs.GetFloat("posZ");
        doorOpen = PlayerPrefs.GetInt("doorOpen");

        posAfterPuzzel= new Vector3(x,y,z);
        transform.Translate(posAfterPuzzel);
    }

    void OnDrawGizmosSelected(){
        // can be used in the future

    }
}
