using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private GameObject _game_object;
    [SerializeField] private float _speed = 0.5f;
    int _current = 0;
    float time;
    private GameObject g;
    public List<Point> waypoints = new List<Point>();
    private void AddPoints()
    {
        foreach (Transform child in transform)
        {
            Point newPoint = child.GetComponent<Point>();
            waypoints.Add(newPoint);
        }
    }
    void Awake()
    {
        AddPoints();
        g = Instantiate(_game_object, waypoints.ElementAt(0).transform.position, Quaternion.identity, transform);
    }
    void Update()
    {
        if(g.transform.position == waypoints.ElementAt(_current).transform.position)
        {
            _current++;
            if (_current >= waypoints.Count)
            {
                _current = 0;
            }
        }
        g.transform.position = Vector3.MoveTowards(g.transform.position, waypoints.ElementAt(_current).transform.position, Time.deltaTime * _speed);
    }
}
