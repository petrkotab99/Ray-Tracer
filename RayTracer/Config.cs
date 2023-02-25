﻿using System;
using System.IO;
using System.Xml.Serialization;


namespace RayTracer;

public class Config
{
    public int ImageWidth { get; set; } = 800;
    public int ImageHeight { get; set; } = 600;
    public string OutputFile { get; set; } = "demo.pfm";
    public float SinCoeficient { get; set; } = 1.0f;

    public static Config FromFile(string file)
    {
        if (!File.Exists(file)) 
        {
            Logger.WriteLine($"Config file \"{file}\" does not exist!", LogType.Error);
            return null;
        }

        var serializer = new XmlSerializer(typeof(Config));
        try
        {
            using var reader = new StreamReader(file);
            var config = (Config)serializer.Deserialize(reader);
            Logger.WriteLine("Config file loaded");
            return config;
        }
        catch (Exception ex)
        when(ex is IOException || ex is InvalidOperationException)
        {
            Logger.WriteLine($"Error while loading config file: {ex.Message}.", LogType.Error);
            return null;
        }
    }
}
