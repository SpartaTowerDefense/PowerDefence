using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums
{
    #region StringKey
    public const string MusicClip = nameof(MusicClip);
    public const string FireClip = nameof(FireClip);
    public const string EnemyDie = nameof(EnemyDie);

    #endregion

    public enum TurretType
    {
        Black,
        Blue,
        Red,
        Green,
        White,

        Count
    }
}
