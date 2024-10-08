using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    public Transform player;
    private Image batteryBar;
    public bool isOn = false;
    public float battery = 10f;
    public float drainRate = 1f;
    private bool outBat = false;
    public bool canFlash;

    void Start()
    {
        canFlash = true;
        batteryBar = GameObject.Find("FlashFront").GetComponent<Image>();
    }

    void Update()
    {
        if(battery > 10f)
        {
            battery = 10f;
        }
        batteryBar.fillAmount = battery/10f;

        if(battery < 0f)
        {
            batteryBar.fillAmount = 0;
            battery = 0;
            outBat = true;
        }

        if(battery > 0)
        {
            outBat = false;
        }
        
        if(isOn && !outBat && canFlash){
            battery -= drainRate * Time.deltaTime;
            GetComponent<Light2D>().intensity = 2.5f;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (mousePosition - player.position).normalized;
            transform.position = new Vector3(player.position.x, player.position.y, 0);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        else{
            GetComponent<Light2D>().intensity = 0;
        }

        if(Input.GetKeyDown(KeyCode.F) && !outBat && canFlash)
        {
            isOn = !isOn;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            outBat = false;
            battery = 10f;
        }
    }
}
