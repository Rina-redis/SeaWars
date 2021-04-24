﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
namespace SeaWars
{
    class Serializer
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Dictionary<string, PlayerProfile>));             
        public void Serialize(Dictionary<string, PlayerProfile> allProfiles)
        {
            FileStream fileStream = new FileStream("D:\\txt.xml", FileMode.OpenOrCreate, FileAccess.Write);
            serializer.Serialize(fileStream, allProfiles);
            fileStream.Close();     
        }

        public Dictionary<string, PlayerProfile> Deserialize()
        {
            FileStream fileStream = new FileStream("D:\\txt.xml", FileMode.OpenOrCreate, FileAccess.Read);
            if(fileStream.Length != 0)
            {
                var currentPlayerProfiles = serializer.Deserialize(fileStream) as Dictionary<string, PlayerProfile>;
                fileStream.Close();
                return currentPlayerProfiles;
            }
            else
            {
                fileStream.Close();
                return new Dictionary<string, PlayerProfile>();
            }
        }
    }
}
