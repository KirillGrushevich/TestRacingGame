using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [Range(1, 30)]
    [SerializeField]
    private int ObjectsMaxCount = 20;

    [SerializeField]
    private float MinDistanceBetweenObjects = 5;

    [SerializeField]
    public GameObject BaseObject;

    private List<GameObject> AllObjects = new List<GameObject>();

    void Start()
    {
        BuildScene(ObjectsMaxCount);
    }


    void BuildScene(int count)
    {
        int counter = count;
        while (counter > 0)
        {
            if (BuildObject())
            {
                counter--;
            }
        }
    }

    bool BuildObject()
    {
        float pX = Random.Range(-Mathf.Sqrt(ObjectsMaxCount), Mathf.Sqrt(ObjectsMaxCount)) + Random.Range(-0.35f, 0.35f);
        float pZ = Random.Range(-Mathf.Sqrt(ObjectsMaxCount), Mathf.Sqrt(ObjectsMaxCount)) + Random.Range(-0.35f, 0.35f);

        Vector3 pos = new Vector3(pX * MinDistanceBetweenObjects, 0, pZ * MinDistanceBetweenObjects);

        for (int i = 0; i < AllObjects.Count; i++)
        {
            if (AllObjects[i] && Vector3.Distance(AllObjects[i].transform.position, pos) < MinDistanceBetweenObjects)
            {
                return false;
            }
        }

        GameObject obj = Instantiate(BaseObject, pos, Quaternion.identity) as GameObject;
        AllObjects.Add(obj);
        return true;

    }

}
