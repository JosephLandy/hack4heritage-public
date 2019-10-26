using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//code riped honestly from http://blog.danskingdom.com/saving-and-loading-a-c-objects-data-to-an-xml-json-or-binary-file/


/// <summary>
/// Functions for performing common binary Serialization operations.
/// <para>All properties and variables will be serialized.</para>
/// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
/// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
/// </summary>
public static class BinarySerialization
{
    const string folderName = "BinaryData";
    const string fileExtension = ".dat";
    static string dataPath = "";
    /// <summary>
    /// Writes the given object instance to a binary file.
    /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
    /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
    /// </summary>
    /// <typeparam name="T">The type of object being written to the XML file.</typeparam>
    /// <param name="filePath">The file path to write the object instance to.</param>
    /// <param name="objectToWrite">The object instance to write to the XML file.</param>
    /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
    public static void WriteToBinaryFile<T>(string filePath, string fileName, T objectToWrite, bool append = false)
    {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName +"/"+ filePath);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        dataPath = Path.Combine(folderPath, fileName + fileExtension);
        using (Stream stream = File.Open(dataPath, append ? FileMode.Append : FileMode.Create))
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, objectToWrite);
        }
        Debug.Log("saved at " + dataPath);
    }

    /// <summary>
    /// Reads an object instance from a binary file.
    /// </summary>
    /// <typeparam name="T">The type of object to read from the XML.</typeparam>
    /// <param name="filePath">The file path to read the object instance from.</param>
    /// <returns>Returns a new instance of the object read from the binary file.</returns>
    public static T ReadFromBinaryFile<T>(string filePath)
    {
        string realfilePath = Path.Combine(Application.persistentDataPath, folderName + "/" + filePath + fileExtension);
        //Debug.Log("")
        if (System.IO.File.Exists(realfilePath)){
            using (Stream stream = File.Open(realfilePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
        else
        {
            Debug.Log("no file found at " + realfilePath);
            return default(T);
        }
    }
}