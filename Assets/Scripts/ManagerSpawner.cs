using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawner : MonoBehaviour {
    private static ManagerSpawner _instance;

    public static ManagerSpawner Instance { get { return _instance; } }


    [SerializeField] public List<Tree> trees;

    // used to keep track of "houses" count
    public int housesCounterIndex = 0;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public List<Tree> GetTrees() {
      return trees;
    }

    public void addTree(Tree tree, ElementSpawner.ElementType type) {
      if (type == ElementSpawner.ElementType.HOUSE) {
        housesCounterIndex++;
      }

      trees.Add(tree);
    }

  public void StartFire() {
    trees[housesCounterIndex + 1].FireState = 1;
  }



    
}