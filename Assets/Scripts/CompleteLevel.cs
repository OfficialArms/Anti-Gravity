using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{

    public int levelNumberToLoad;
    public string levelNameToLoad;

    public bool loadByLevelNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("a trigger was registered");

        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.gameObject.name == "Player")
        {
            print("Collided with Player");
            LoadScene();
        }
    }

    void LoadScene()
    {
        if (loadByLevelNumber)
        {
            print("Now loading level #"+levelNumberToLoad);
            SceneManager.LoadScene(levelNumberToLoad);
        } else
        {
            print("Now loading level " + levelNameToLoad);
            SceneManager.LoadScene(levelNameToLoad);
        }
    }
}
