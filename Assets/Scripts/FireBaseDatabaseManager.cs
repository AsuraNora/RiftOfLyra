using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using UnityEngine;


public class FireBaseDatabaseManager : MonoBehaviour
{
    private DatabaseReference reference;

    private void Awake(){
        FirebaseApp firebaseApp = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Start()
    {
        TileMapDetail tileMapDetail = new TileMapDetail(1, 1, TileMapState.Ground);
        WriteDatabase("123", tileMapDetail.ToString());
        ReadDatabase("123");
    }

    public void WriteDatabase(string id, string message){
        reference.Child("Users").Child(id).Child("Message").SetValueAsync(message).ContinueWith(task => {
            if (task.IsFaulted){
                Debug.LogError("Error writing to database: " + task.Exception);
            }
            else if (task.IsCompleted){
                Debug.Log("Data written successfully.");
            }
        });
    }

    public void ReadDatabase(string id)
{
    reference.Child("Users").Child(id).GetValueAsync().ContinueWith(task => {
        if (task.IsFaulted)
        {
            Debug.LogError("Error reading from database: " + task.Exception);
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                // Kiểm tra kiểu dữ liệu của snapshot.Value
                object rawData = snapshot.Value;
                if (rawData is Dictionary<string, object> data)
                {
                    foreach (var pair in data)
                    {
                        Debug.Log($"{pair.Key}: {pair.Value}");
                    }
                }
                else
                {
                    Debug.Log("Data is not a dictionary: " + rawData);
                }
            }
            else
            {
                Debug.Log("No data found.");
            }
        }
    });
}

}
