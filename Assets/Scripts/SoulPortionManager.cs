using UnityEngine;
using UnityEngine.UI;

public class SoulPortionManager : MonoBehaviour
{
    public int soulPortionCount = 0;
    public int totalDreamcatcherCount = 15;
    public Text soulPortionCountText;

    void Start()
    {

    }

    void Update()
    {
        soulPortionCountText.text = "Count: " + soulPortionCount.ToString() + "/" + totalDreamcatcherCount.ToString();
    }
}
