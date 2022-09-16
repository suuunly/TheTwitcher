using UnityEngine;

public class PartyHandler : MonoBehaviour
{
    public GameEventPure PartyActivation;
    public GameEventPure PartyTermination;
    public GameObject PartyLights;
    public Light GlobalLight;
    public AudioSource Song;

    public float Duration;

    // Start is called before the first frame update
    void Start()
    {
        PartyLights.SetActive(false);
        GlobalLight.enabled = true;
        PartyActivation.OnRaised += this.StartParty;
    }

    private void OnDestroy() {
        PartyActivation.OnRaised -= this.StartParty;
    }

    void StartParty(object data)
    {
        PartyLights.SetActive(true);
        GlobalLight.enabled = false;

        StopAllCoroutines();
        StartCoroutine(SDE.Timing.GenericDelay(Duration, this.StopParty));

        Song.Play();
    }

    void StopParty()
    {
        PartyLights.SetActive(false);
        GlobalLight.enabled = true;

        PartyTermination?.Raise();

        Song.Stop();
    }
}
