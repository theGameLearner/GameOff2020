/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #CREATIONDATE#
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGL.Singletons;

public class GameSettings : GenericSingletonMonobehaviour<GameSettings>
{
    public float timeScale = 0.4f;
    public Transform playerTransform;
    public Transform bulletPrefab;
    public float bulletSpeed = 10;
    public float bulletLifeTime = 1;
    public int bulletPoolIndex = 0;

    public Grid<GridSquare> levelGrid;

    public GameData gameData;

    public Transform playerSpawnSpotTransform;

}
