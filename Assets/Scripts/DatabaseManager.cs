using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private DatabaseReference RealtimeDB;
    // Start is called before the first frame update
    void Start()
    {
        RealtimeDB = FirebaseDatabase.DefaultInstance.RootReference;
        RealtimeDB.ValueChanged += HandleStateChanged;
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
            var sensorEnumerator = dataSnapshot.Child("ThermalSensorStatuses").Children;
            foreach (var sensor in sensorEnumerator)
            {
                MicrocontrollerManager.ToggleHeater?.Invoke(sensor.Key, sensor.Value.ToString());
            }
            
        }
    }

    private void OnDisable()
    {
        RealtimeDB.ValueChanged -= HandleStateChanged;
    }
}
