using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Line
{
    
    [SerializeField] private Vector2 _pos1; 
    public Vector2 Pos1 
    {
        get {return _pos1;}
        set {_pos2 = value;}
    }
    [SerializeField] private Vector2 _pos2; 
    public Vector2 Pos2
    {
        get {return _pos2;}
        set {_pos2 = value;}
    }

    public float GetLineDistance() {
        return Vector2.Distance(Pos1,Pos2);
    }
}

public class Spawner : MonoBehaviour
{
    //For pasting: GenericPropertyJSON:{"name":"lines","type":-1,"arraySize":12,"arrayType":"Line","children":[{"name":"Array","type":-1,"arraySize":12,"arrayType":"Line","children":[{"name":"size","type":12,"val":12},{"name":"data","type":-1,"children":[{"name":"_pos1","type":8,"children":[{"name":"x","type":2,"val":12.5},{"name":"y","type":2,"val":6.5}]},{"name":"_pos2","type":8,"children":[{"name":"x","type":2,"val":-12.5},{"name":"y","type":2,"val":6.5}]}]},{"name":"data","type":-1,"children":[{"name":"_pos1","type":8,"children":[{"name":"x","type":2,"val":-12.5},{"name":"y","type":2,"val":6.5}]},{"name":"_pos2","type":8,"children":[{"name":"x","type":2,"val":-12.5},{"name":"y","type":2,"val":5.5}]}]},{"name":"data","type":-1,"children":[{"name":"_pos1","type":8,"children":[{"name":"x","type":2,"val":-12.5},{"name":"y","type":2,"val":5.5}]},{"name":"_pos2","type":8,"children":[{"name":"x","type":2,"val":-13.5},{"name":"y","type":2,"val":5.5}]}]},{"name":"data","type":-1,"children":[{"name":"_pos1","type":8,"children":[{"name":"x","type":2,"val":-13.5},{"name":"y","type":2,"val":5.5}]},{"name":"_pos2","type":8,"children":[{"name":"x","type":2,"val":-13.5},{"name":"y","type":2,"val":-6.5}]}]},{"name":"data","type":-1,"children":[{"name":"_pos1","type":8,"children":[{"name":"x","type":2,"val":-13.5},{"name":"y","type":2,"val":-6.5}]},{"name":"_pos2","type":8,"children":[{"name":"x","type":2,"val":-12.5},{"name":"y","type":2,"val":-6.5}]}]},{"name":"data","type":-1,"children":[{"name":"_pos1","type":8,"children":[{"name":"x","type":2,"val":-12.5},{"name":"y","type":2,"val":-6.5}]},{"name":"_pos2","type":8,"children":[{"name":"x","type":2,"val":-12.5},{"name":"y","type":2,"val":-7.5}]}]},{"name":"data","type":-1,"children":[{"name":"_pos1","type":8,"children":[{"name":"x","type":2,"val":-12.5},{"name":"y","type":2,"val":-7.5}]},{"name":"_pos2","type":8,"children":[{"name":"x","type":2,"val":12.5},{"name":"y","type":2,"val":-7.5}]}]},{"name":"data","type":-1,"children":[{"name":"_pos1","type":8,"children":[{"name":"x","type":2,"val":12.5},{"name":"y","type":2,"val":-7.5}]},{"name":"_pos2","type":8,"children":[{"name":"x","type":2,"val":12.5},{"name":"y","type":2,"val":-6.5}]}]},{"name":"data","type":-1,"children":[{"name":"_pos1","type":8,"children":[{"name":"x","type":2,"val":12.5},{"name":"y","type":2,"val":-6.5}]},{"name":"_pos2","type":8,"children":[{"name":"x","type":2,"val":13.5},{"name":"y","type":2,"val":-6.5}]}]},{"name":"data","type":-1,"children":[{"name":"_pos1","type":8,"children":[{"name":"x","type":2,"val":13.5},{"name":"y","type":2,"val":-6.5}]},{"name":"_pos2","type":8,"children":[{"name":"x","type":2,"val":13.5},{"name":"y","type":2,"val":5.5}]}]},{"name":"data","type":-1,"children":[{"name":"_pos1","type":8,"children":[{"name":"x","type":2,"val":13.5},{"name":"y","type":2,"val":5.5}]},{"name":"_pos2","type":8,"children":[{"name":"x","type":2,"val":12.5},{"name":"y","type":2,"val":5.5}]}]},{"name":"data","type":-1,"children":[{"name":"_pos1","type":8,"children":[{"name":"x","type":2,"val":12.5},{"name":"y","type":2,"val":5.5}]},{"name":"_pos2","type":8,"children":[{"name":"x","type":2,"val":12.5},{"name":"y","type":2,"val":6.5}]}]}]}]}
    [SerializeField] private List<Line> lines;
    [SerializeField] private GameObject enemy;

    private Vector2 GetRandomPoint() 
    {
        Line randomLine = lines[0];

        float totalDistance = 0;
        foreach (Line line in lines) 
        {
            totalDistance += line.GetLineDistance();
        }

        float randomWeight =  Random.Range(1,totalDistance + 1);
        float reachDistance = 0;
        foreach (Line line in lines) 
        {
            reachDistance += line.GetLineDistance();
            if (randomWeight <= reachDistance)
            {
                randomLine = line;
                break;
            }
        }
        Vector2 randomPoint = randomLine.Pos1 + Random.Range(0.0f, 1.0f) * (randomLine.Pos2 - randomLine.Pos1);
        return randomPoint;
    }

    
    private void OnEnable()
    {
        Note.NoteMissed += OnNoteMissed;
    }

    private void OnDisable()
    {
        Note.NoteMissed -= OnNoteMissed;
    }

    private void OnNoteMissed(int misses) 
    {
        GameObject spawnedEnemy = Instantiate(enemy, GetRandomPoint(), Quaternion.identity);
        spawnedEnemy.transform.parent = gameObject.transform;
    }
}
