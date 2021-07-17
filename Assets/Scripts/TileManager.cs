using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour
{
    public GameObject currentTile;
    public GameObject[] tilePrefabs;
    private static TileManager instance;


    private Stack<GameObject> leftTiles = new Stack<GameObject>();
    private Stack<GameObject> forwardTiles = new Stack<GameObject>();


    public Stack<GameObject> LeftTiles { get => leftTiles; set => leftTiles = value; }
    public Stack<GameObject> ForwardTiles { get => forwardTiles; set => forwardTiles = value; }


    public static TileManager Instance { get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<TileManager>();
            }
            return instance;
        }
    }

    //when the game starts create some titles and after that call he spawn tile function to spawn new tiles
    void Start()
    {
        CreateTiles(20);
        for(int i = 0; i < 10; i++)
        {
            SpawnTiles();
        }
    }

    //first start with creating tiles if there is no left or forward tile
    public void SpawnTiles()
    {

        if(LeftTiles.Count == 0 || ForwardTiles.Count == 0)
        {
            CreateTiles(10);
        }


      //take a random number 0 or 1 for tiles
      int randomNum = Random.Range(0, 2);

        //if our random number is 0 use left tiles to spawn  and set the new tile as temp and make them current tile
        if (randomNum == 0)
        {
            GameObject temp = LeftTiles.Pop();
            temp.SetActive(true);
            temp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(randomNum).position;
            currentTile = temp;
        }

        //if our random number is 1 use forward tiles to spawn  and set the new tile as temp and make them current tile
        else if (randomNum == 1)
        {
            GameObject temp = ForwardTiles.Pop();
            temp.SetActive(true);
            temp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(randomNum).position;
            currentTile = temp;
        }

        //to spawn poinht objects randomly if we decrease the second number the probability to spawn point objects are going to increase.
        int spawnPoint = Random.Range(0, 10);
        if(spawnPoint == 0)
        {
            currentTile.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    //to create the left and forward tiles.
    public void CreateTiles(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            LeftTiles.Push(Instantiate(tilePrefabs[0]));
            ForwardTiles.Push(Instantiate(tilePrefabs[1]));
            ForwardTiles.Peek().name = "ForwardTile";
            ForwardTiles.Peek().SetActive(false);
            LeftTiles.Peek().name = "LeftTile";
            LeftTiles.Peek().SetActive(false);
        }
    }

    //when press play again button load the same scene from start to play again
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
