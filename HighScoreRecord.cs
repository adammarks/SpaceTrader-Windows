/*******************************************************************************
 *
 * Space Trader for Windows 1.3.0
 *
 * Copyright (C) 2004 Jay French, All Rights Reserved
 *
 * Original coding by Pieter Spronck, Sam Anderson, Samuel Goldstein, Matt Lee
 *
 * This program is free software; you can redistribute it and/or modify it
 * under the terms of the GNU General Public License as published by the Free
 * Software Foundation; either version 2 of the License, or (at your option) any
 * later version.
 *
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 * FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
 *
 * If you'd like a copy of the GNU General Public License, go to
 * http://www.gnu.org/copyleft/gpl.html.
 * 
 * You can contact the author at spacetrader@frenchfryz.com
 *
 ******************************************************************************/
using System;

namespace Fryz.Apps.SpaceTrader
{
	[Serializable()]      
	public class HighScoreRecord: IComparable
	{
		#region Member Declarations

		private string			_name;
		private int					_score;
		private GameEndType	_type;
		private int					_days;
		private int					_worth;
		private Difficulty	_difficulty;

		#endregion

		#region Methods

		public HighScoreRecord(string name, int score, GameEndType type, int days, int worth, Difficulty difficulty)
		{
			_name				= name;
			_score			= score;
			_type				= type;
			_days				= days;
			_worth			= worth;
			_difficulty	= difficulty;
		}

		public int CompareTo(object value)
		{
			int							compared;
			HighScoreRecord	highScore	= (HighScoreRecord)value;

			if (value == null)
				compared	= 1;
			else if (highScore.Score < Score)
				compared	= 1;
			else if (highScore.Score > Score)
				compared	= -1;
			else if (highScore.Worth < Worth)
				compared	= 1;
			else if (highScore.Worth > Worth)
				compared	= -1;
			else if (highScore.Days < Days)
				compared	= 1;
			else if (highScore.Days > Days)
				compared	= -1;
			else
				compared	= 0;

			return compared;
		}

		#endregion

		#region Operators

		public static bool operator > (HighScoreRecord a, HighScoreRecord b)
		{
			return a.CompareTo(b) > 0;
		}

		public static bool operator < (HighScoreRecord a, HighScoreRecord b)
		{
			return a.CompareTo(b) < 0;
		}

		#endregion

		#region Properties

		public string Name
		{
			get
			{
				return _name;
			}
		}

		public int Score
		{
			get
			{
				return _score;
			}
		}

		public GameEndType Type
		{
			get
			{
				return _type;
			}
		}

		public int Days
		{
			get
			{
				return _days;
			}
		}

		public int Worth
		{
			get
			{
				return _worth;
			}
		}

		public Difficulty Difficulty
		{
			get
			{
				return _difficulty;
			}
		}

		#endregion
	}
}