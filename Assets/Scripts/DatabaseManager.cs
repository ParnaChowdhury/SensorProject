using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private DatabaseReference RealtimeDB;
    [SerializeField] private MicrocontrollerManager microcontrollerManager;
    
    // Start is called before the first frame update
    void Start()
    {
        RealtimeDB = FirebaseDatabase.DefaultInstance.RootReference;
        RealtimeDB.ValueChanged += HandleStateChanged;
        microcontrollerManager = transform.GetComponent<MicrocontrollerManager>();
    }

    public void ToggleSensor(int microControllerID)
    {
        bool newBool = !microcontrollerManager.temperatureSensors[microControllerID].isHeaterOn;
        RealtimeDB.Child("ThermalSensorStatuses").Child(microControllerID.ToString()).SetValueAsync(newBool);
    }
   

    void HandleStateChanged(object sender, ValueChangedEventArgs valueChangedEventArgs)
    {
        if (valueChangedEventArgs.DatabaseError != null) return;
        Debug.Log("UpdatingValues");
        StartCoroutine(UpdateState());
    }
    
    private IEnumerator UpdateState()
    {
        Task<DataSnapshot> currentValues = RealtimeDB.GetValueAsync();
        yield return new WaitUntil(predicate: () => currentValues.IsCompleted);
        Debug.Log("ReceivedValues");
        DataSnapshot dataSnapshot = currentValues.Result;
        if (dataSnapshot.Exists)
        {
            var sensorStatusEnumerator = dataSnapshot.Child("ThermalSensorStatuses").Children;
            foreach (var sensor in sensorStatusEnumerator)
            {
                MicrocontrollerManager.ToggleHeater?.Invoke(sensor.Key, sensor.Value.ToString());
            }
            
            var sensorEnumerator = dataSnapshot.Child("IsHeated").Children;
            foreach (var sensor in sensorEnumerator)
            {
                SensorStatus.ReceiveHeatStatus?.Invoke();
            }
        }
    }

    private void OnDisable()
    {
        RealtimeDB.ValueChanged -= HandleStateChanged;
    }
}
