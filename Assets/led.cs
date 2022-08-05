using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class led : MonoBehaviour {
										//     port				   boud rate
										//  	|					   |
										//      V					   V
	public SerialPort serial = new SerialPort ("COM4", 9600); //here change port - where you have connected arduino to computer
	private bool lightState = false;

	public void onLed() {
		if (serial.IsOpen == false) {
			serial.Open ();
			
		}
		string valueon = serial.ReadLine();
		print(valueon);
		print("ON Button is pressed");

		serial.Write ("A");
		lightState = true;
	}

	public void offLed() {
		if (serial.IsOpen == false) {
			serial.Open ();
			
		}
		string valueoff = serial.ReadLine();
		print(valueoff);
		print("OFF Button is pressed");

		serial.Write ("a");
		lightState = false;
	}

}
