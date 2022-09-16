using UnityEngine;

public class SpawnTester : MonoBehaviour
{
    public string command;
    public SpawnManager Manager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Spawn Status: " + Manager.TrySpawnCandiate(command));
        }
    }
}
