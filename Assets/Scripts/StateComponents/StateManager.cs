using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class StateManager {

    private static string fileName = Application.persistentDataPath + "/WhaleState.dat";
    private static AccountStateContainer currentState;

    public static AccountStateContainer GetCurrentState() {
        LoadCurrentState();
        return currentState;
    }

    private static void LoadCurrentState() {
        if (currentState != null) return;

        if (!File.Exists(fileName)) {
            CreateEmptyContainer();
            return;
        }

        FileStream stream = new FileStream(fileName, FileMode.Open);
        try {
            BinaryFormatter formatter = new BinaryFormatter();
            currentState = (AccountStateContainer)formatter.Deserialize(stream);
        } catch (Exception e) {
            UnityEngine.Debug.Log(e.Message);
        } finally {
            stream.Close();
        }

        if (currentState == null) {
            CreateEmptyContainer();
        }
    }

    private static void CreateEmptyContainer() {
        AccountStateContainer container = new AccountStateContainer();
        container.InitializeAccount();
        currentState = container;
        SaveState();
    }

    public static void SaveState() {
        FileStream stream = new FileStream(fileName, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();

        try {
            formatter.Serialize(stream, currentState);
        } finally {
            stream.Close();
        }
    }
}
