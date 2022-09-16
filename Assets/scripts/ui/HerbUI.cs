using TMPro;
using UnityEngine;

public class HerbUI : MonoBehaviour
{
    public HerbManager Manager;
    public TextMeshProUGUI GreenHerb;
    public TextMeshProUGUI RedHerb;

    public Animator RedHerbAnimator;
    public Animator GreenHerbAnimator;


    void Start()
    {
        Manager.OnPickedUp += PickedUpHerb;
        Manager.OnConsumed += Consumed;
    }

    private void OnDestroy()
    {
        Manager.OnPickedUp -= PickedUpHerb;
    }

    private void Consumed()
    {
        RedHerbAnimator.SetTrigger("consumed");
        GreenHerbAnimator.SetTrigger("consumed");
    }

    private void PickedUpHerb(int green, int red, HerbType type)
    {
        GreenHerb.text = green.ToString();
        RedHerb.text = red.ToString();

        switch (type)
        {
            case HerbType.Red:
                RedHerbAnimator.SetTrigger("pickedup");
                break;
            case HerbType.Green:
                GreenHerbAnimator.SetTrigger("pickedup");
                break;
        }

    }
}
