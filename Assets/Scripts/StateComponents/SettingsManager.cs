using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SettingsManager {

    private static string fileName = Application.persistentDataPath + "/WhaleSettings.dat";

    private static SettingsContainer container;

    public static SettingsContainer GetInstance() {
        LoadContainer();
        return container;
    }

    private static void LoadContainer() {
        if (container != null) return;

        if (!File.Exists(fileName)) {
            CreateNewContainer();
            return;
        }

        FileStream stream = new FileStream(fileName, FileMode.Open);
        try {
            BinaryFormatter formatter = new BinaryFormatter();
            container = (SettingsContainer)formatter.Deserialize(stream);
        } catch (Exception e) {
            UnityEngine.Debug.Log(e.Message);
        } finally {
            stream.Close();
        }

        if (container == null) {
            CreateNewContainer();
        }
    }

    private static void CreateNewContainer() {
        container = new SettingsContainer();
        SaveContainer();
    }

    public static void SaveContainer() {
        FileStream stream = new FileStream(fileName, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();

        try {
            formatter.Serialize(stream, container);
        } finally {
            stream.Close();
        }
    }
}

[Serializable]
public class SettingsContainer {

    [SerializeField] public float musicVolume;
    [SerializeField] public float effectVolume;

    public SettingsContainer() {
        musicVolume = 0.5f;
        effectVolume = 0.5f;
    }
}
