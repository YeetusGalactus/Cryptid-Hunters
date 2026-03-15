//using Alteruna;
//using UnityEngine;

//public class PlayerSync : Synchronizable
//{
//    [SynchronizableField]
//    bool isReady = false;

//    private Alteruna.Avatar _avatar;

//    public override void AssembleData(Writer writer, byte index)
//    {
//        writer.Write(isReady);
//    }

//    public override void DisassembleData(Reader reader, byte index)
//    {
//        isReady = reader.ReadBool();
//    }

//    private void Awake()
//    {
//        _avatar = GetComponent<Alteruna.Avatar>();
//    }

//    public void SetReady(bool value)
//    {
//        isReady = value;
//    }

//    [SynchronizableMethod]
//    public void SyncReady(bool value)
//    {
//        isReady = value;
//    }
//}