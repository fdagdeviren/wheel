using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyWheel : MonoBehaviour
{
    [SerializeField] private GameObject _wheel;
    private Vector2 _startWheelPosition;
    private Quaternion _startWheelRotation;
    [SerializeField] private List<GameObject> _gifts = new List<GameObject>();
    [SerializeField] private List<Vector2> _giftsTransforms=new List<Vector2>();
    public WheelSystem wheelSystem;
    //LuckyWheel i kapattigimizda baslangic konumuna ayarladim
    private void OnDisable()
    {
        wheelSystem.Reset();
        _wheel.transform.position = _startWheelPosition;
        _wheel.transform.rotation = _startWheelRotation;
        for (int i = 0; i < _gifts.Count; i++)
        {
            _gifts[i].transform.position = _giftsTransforms[i];
            _gifts[i].transform.localScale = new Vector3(1,1,1);
        }
    }

    private void OnEnable()
    {
            _startWheelPosition = _wheel.transform.position;
            _startWheelRotation = _wheel.transform.rotation;
            for (int i = 0; i < _gifts.Count; i++)
            {
                _giftsTransforms[i] = _gifts[i].transform.position;
            }
    }
}
