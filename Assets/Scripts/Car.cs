using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float turnSpeed = 200f;
    [SerializeField] private float speedIncreaseValue = 1f;
    [SerializeField] private float speedIncreaseDelay = 1f;
    [SerializeField] private InputReader inputReader;

    private float lastTimeIncrease;
    private float steerValue;
    
    private void Awake()
    {
        lastTimeIncrease = Time.time;
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0);
        }
    }
    private void Update()
    {
        IncreaseSpeedOverTime();
        SteerCar();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void Steer(int value)
    {
        steerValue = value;
    }
    private void SteerCar()
    {
        transform.Rotate(0f, inputReader.SteerValue.x * turnSpeed * Time.deltaTime, 0f);
    }
    private void IncreaseSpeedOverTime()
    {
        if((Time.time - lastTimeIncrease) > speedIncreaseDelay)
        {
            lastTimeIncrease = Time.time;
            speed += speedIncreaseValue;
        }
    }
}
