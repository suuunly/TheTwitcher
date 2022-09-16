using UnityEngine;
using TMPro;

public class BellUI : MonoBehaviour
{
    public PointCollection Collection;
    public TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        Collection.CollectedPoints += UpdatePoints;
    }

    void UpdatePoints()
    {
        Text.text = Collection.CurrentPoints.ToString();
    }
}
