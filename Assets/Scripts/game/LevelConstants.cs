﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class LevelConstants {

	private static LevelBehaviour[] allLevels = new LevelBehaviour [] {
		new StartLevel_1 (),
		new StartLevel_2 ()
	};

	/// <summary>
	/// Total number of levels.
	/// </summary>
	/// <returns>The of levels.</returns>
	public static int NumberOfLevels () {
		return allLevels.Length;
	}

	/// <summary>
	/// Gets the level at the given index.
	/// </summary>
	/// <returns>The level.</returns>
	/// <param name="index">Index.</param>
	public static LevelBehaviour GetLevel (int index) {
		return allLevels [index];
	}
}