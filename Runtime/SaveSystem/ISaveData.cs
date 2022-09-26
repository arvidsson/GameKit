using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameKit.SaveSystem
{
    public interface ISaveData
    {
        void Write(BinaryWriter bw);
        void Read(BinaryReader br);
    }
}