using UnityEngine;
using System.Collections.Generic;

public class RhythmManager : MonoBehaviour, IAudioManagerReceiver
{
    public AudioManager AudioManager { set => throw new System.NotImplementedException(); }
}