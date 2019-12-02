using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpawner : MonoBehaviour
{
  [SerializeField] int elementCount;
  [SerializeField] List<Tree> elementPrefab;

  [SerializeField] float minDistanceAway;

  Collider spawnBox;

  [SerializeField] GameObject ground;

  [SerializeField] GameObject objectPool;

  // public static List<Tree> trees = new List<Tree>();

  // [SerializeField] List<Tree> TreePrefabs = new List<Tree>();

  [SerializeField] ElementType elementType;

  public enum ElementType {
    TREE, HOUSE
  }


  /// <summary>
  /// Awake is called when the script instance is being loaded.
  /// </summary>
  void Awake()
  {
    spawnBox = GetComponent<Collider>();
  }

  public void SpawnElements(Vector2 totalWind) {

    // use ground if there is no spawn box
    Debug.Log("spawnBox == null: " + (spawnBox == null));

    float minx = (spawnBox != null) ? spawnBox.GetComponent<Collider>().bounds.min.x : ground.GetComponent<Collider>().bounds.min.x;
    float maxx = (spawnBox != null) ? spawnBox.GetComponent<Collider>().bounds.max.x : ground.GetComponent<Collider>().bounds.max.x;
    float minz = (spawnBox != null) ? spawnBox.GetComponent<Collider>().bounds.min.z : ground.GetComponent<Collider>().bounds.min.z;
    float maxz = (spawnBox != null) ? spawnBox.GetComponent<Collider>().bounds.max.z : ground.GetComponent<Collider>().bounds.max.z; 
    
    for (int i = 0; i < elementCount; ++i) {
      bool validSpot;
      float x, z;
      int loopCount = 0;
      bool exitForLoop = false;

      do{
        loopCount++;
        validSpot = true;

        x = Random.Range(minx, maxx);
        z = Random.Range(minz, maxz);

        if (elementType == ElementType.HOUSE) {
          Debug.Log("point (" + x +  " ," + z + " )");
        }
        

        List<Tree> trees = ManagerSpawner.Instance.GetTrees();
        for (int j = 0; j < trees.Count; j++)
        {
          if (Vector2.Distance(new Vector2(x, z), new Vector2(trees[j].transform.position.x,  trees[j].transform.position.z)) <= minDistanceAway)
          {
              //Debug.Log("House spawn at (" + x + ", " + z + ") blocked by existing house at (" + trees[j].transform.position.x + ", " + trees[j].transform.position.z + ")");
              validSpot = false;
          }
        }

        if (loopCount >= 2000) {
          exitForLoop = true;
          validSpot = true;
          Debug.Log("Maximum number of Houses at: " + i);
        }

      } while (!validSpot);

      if (exitForLoop) {
        break;
      }

      int whichElement = Random.Range(0, elementPrefab.Count);
      Tree newElement = Instantiate(elementPrefab[whichElement], new Vector3(x, .25f, z), Quaternion.Euler(new Vector3(0, Random.Range(0, 180), 0)));
      newElement.TotalWind = totalWind;
      if (elementType == ElementType.TREE) {
        randomize(newElement);
      }

      ManagerSpawner.Instance.addTree(newElement, elementType);
            
      if (exitForLoop) break;

    }


  }

  public void randomize(Tree newElement) {
    float randomWidth = Random.Range(0.75f, 1.25f);
    newElement.transform.localScale = new Vector3(randomWidth, Random.Range(0.75f, 1.25f), randomWidth);

    //Randomly assigns a color to the tree
    Color temp = new Color(0, Random.Range(0.2f, 0.6f), 0, 1.0f);
    newElement.GetComponent<MeshRenderer>().materials[1].SetColor("_Color", temp);

  }
  
  
}
