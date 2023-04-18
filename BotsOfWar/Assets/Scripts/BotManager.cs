using UnityEngine;

public class BotManager : MonoBehaviour
{
    private GameObject[] _bots;
    private IBehaviour _sampleBotBehaviour;
    private void Start()
    {
        _bots = GameObject.FindGameObjectsWithTag("Bot");
        _sampleBotBehaviour = new SampleBotBehaviour(new Vector3(1,1));
    }

    // this is expensive, will fix later
    private void Update()
    {
        foreach (var bot in _bots)
        {
            var nextPosition = _sampleBotBehaviour.GetNextPosition(bot.transform.position);
            bot.transform.position = nextPosition;
        }
        
    }
}
