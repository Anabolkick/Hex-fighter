using CrashKonijn.Goap.Behaviours;
using UnityEngine;

public class GoapSetBinder : MonoBehaviour {
    public void Awake() {
        var runner = FindObjectOfType<GoapRunnerBehaviour>();
        var agent = GetComponent<AgentBehaviour>();
        agent.GoapSet = runner.GetSet("GettingStartedSet");
    }
}