using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class SetUpButtons : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private GameObject _buttonPrefab;
    private GameObject[] _bots;
    
    // Start is called before the first frame update
    void Start()
    {
        _bots = GameObject.FindGameObjectsWithTag("Player");

        if(_bots.Length == 0)
            return;

        for (int i = 0; i < _bots.Length; i++)
        {
            GameObject button = Instantiate(_buttonPrefab) as GameObject;
            button.transform.SetParent(this.transform);
            button.GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(360, -i*35,0),Quaternion.identity);
            button.GetComponentInChildren<Text>().text = "Follow bot " + (i+1); 
            int x = i; // if we pass i as parameter of funticon FollowBotOnClick,
            //then its equal same for every button for some reason and its value equals _bots.Length + 1
            //therefore this additional variable x must exist here
            button.GetComponent<Button>().onClick.AddListener(() => FollowBotOnClick(x));
        }
    }

    public void FollowBotOnClick(int index)
    {
        _virtualCamera.Follow = _bots[index].transform;
        _virtualCamera.Priority = 1;
    }
}

