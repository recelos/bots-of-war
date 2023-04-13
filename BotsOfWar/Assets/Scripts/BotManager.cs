using UnityEngine;

public class BotManager : MonoBehaviour
{
    private GameObject _bot;
    private SampleBotBehaviour _sampleBotBehaviour;
    private void Start()
    {
        _bot = GameObject.FindGameObjectWithTag("Bot");
        _sampleBotBehaviour = new SampleBotBehaviour(new Vector3(100,100));
    }

    private void Update()
    {
        var nextPosition = _sampleBotBehaviour.GetNextPosition(_bot.transform.position);
        _bot.transform.position = nextPosition;
    }

}