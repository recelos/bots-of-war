using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private float _density;

    private void Start()
    {
        ToggleObstacles(); // Active random Objects
    }

    private void ToggleObstacles()
    {
        foreach (Transform child in transform)
        {
            if (child != null)
            {
                foreach(Transform grandchild in child)
                {
                    if(grandchild != null)
                    {
                        if (grandchild.gameObject.GetComponent<SpriteRenderer>() != null)
                        {
                            if (Random.value < _density) // _density specifies how probable the objects will spawn
                            {
                                grandchild.gameObject.SetActive(true);
                            }
                            else
                            {
                                grandchild.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }
}
