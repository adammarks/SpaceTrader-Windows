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
	public class StarSystem
	{
		#region Member Declarations

		private StarSystemId		_id;
		private int							_x;
		private int							_y;
		private Size						_size;
		private TechLevel				_techLevel;
		private PoliticalSystem	_politicalSystem;
		private SystemPressure	_pressure;
		private SpecialResource	_specialResource;
		private SpecialEvent		_specialEvent			= null;
		private int[]						_tradeItems				= new int[10];
		private int							_countDown				= 0;
		private bool						_visited					= false;

		#endregion

		#region Methods

		public StarSystem(StarSystemId id, int x, int y, Size size, TechLevel techLevel, PoliticalSystem politicalSystem,
			SystemPressure pressure, SpecialResource specialResource)
		{
			_id								= id;
			_x								= x;
			_y								= y;
			_size							= size;
			_techLevel				= techLevel;
			_politicalSystem	= politicalSystem;
			_pressure					= pressure;
			_specialResource	= specialResource;

			InitializeTradeItems();
		}

		public void InitializeTradeItems()
		{
			for (int i = 0; i < Consts.TradeItems.Length; i++)
			{
				if (!ItemTraded(Consts.TradeItems[i]))
				{
					_tradeItems[i]	= 0;
				}
				else
				{
					_tradeItems[i]	= ((int)this.Size + 1) * (Functions.GetRandom(9, 14) -
						Math.Abs(Consts.TradeItems[i].TechTopProduction - this.TechLevel));

					// Because of the enormous profits possible, there shouldn't be too many robots or narcotics available.
					if (i >= (int)TradeItemType.Narcotics)
						_tradeItems[i]	= ((_tradeItems[i] * (5 - (int)Game.CurrentGame.Difficulty)) / (6 - (int)Game.CurrentGame.Difficulty)) + 1;

					if (this.SpecialResource == Consts.TradeItems[i].ResourceLowPrice)
						_tradeItems[i]	= _tradeItems[i] * 4 / 3;

					if (this.SpecialResource == Consts.TradeItems[i].ResourceHighPrice)
						_tradeItems[i]	= _tradeItems[i] * 3 / 4;

					if (this.Pressure == Consts.TradeItems[i].PressurePriceHike)
						_tradeItems[i]	= _tradeItems[i] / 5;

					_tradeItems[i]	= _tradeItems[i] - Functions.GetRandom(10) + Functions.GetRandom(10);

					if (_tradeItems[i] < 0)
						_tradeItems[i] = 0;
				}
			}
		}

		public bool ItemTraded(TradeItem item)
		{
			return ((item.Type != TradeItemType.Narcotics || PoliticalSystem.DrugsOk) &&
				(item.Type != TradeItemType.Firearms || PoliticalSystem.FirearmsOk) &&
				TechLevel >= item.TechProduction);
		}

		public bool ItemUsed(TradeItem item)
		{
			return ((item.Type != TradeItemType.Narcotics || PoliticalSystem.DrugsOk) &&
				(item.Type != TradeItemType.Firearms || PoliticalSystem.FirearmsOk) &&
				TechLevel >= item.TechUsage);
		}

		public bool ShowSpecialButton()
		{
			Game	game	= Game.CurrentGame;
			bool	show	= false;

			if (SpecialEvent != null)
			{
				switch (SpecialEvent.Type)
				{
					case SpecialEventType.Artifact:
					case SpecialEventType.Dragonfly:
					case SpecialEventType.Experiment:
					case SpecialEventType.Jarek:
						show	= game.Commander.PoliceRecordScore >=
							Consts.PoliceRecordScoreDubious;
						break;
					case SpecialEventType.ArtifactDelivery:
						show	= game.Commander.Ship.ArtifactOnBoard;
						break;
					case SpecialEventType.CargoForSale:
						show	= game.Commander.Ship.FreeCargoBays >= 3;
						break;
					case SpecialEventType.DragonflyBaratas:
						show	= game.QuestStatusDragonfly > SpecialEvent.StatusDragonflyNotStarted &&
							game.QuestStatusDragonfly < SpecialEvent.StatusDragonflyDestroyed;
						break;
					case SpecialEventType.DragonflyDestroyed:
						show	= game.QuestStatusDragonfly == SpecialEvent.StatusDragonflyDestroyed;
						break;
					case SpecialEventType.DragonflyMelina:
						show	= game.QuestStatusDragonfly > SpecialEvent.StatusDragonflyFlyBaratas &&
							game.QuestStatusDragonfly < SpecialEvent.StatusDragonflyDestroyed;
						break;
					case SpecialEventType.DragonflyRegulas:
						show	= game.QuestStatusDragonfly > SpecialEvent.StatusDragonflyFlyMelina &&
							game.QuestStatusDragonfly < SpecialEvent.StatusDragonflyDestroyed;
						break;
					case SpecialEventType.DragonflyShield:
					case SpecialEventType.ExperimentFailed:
					case SpecialEventType.Gemulon:
					case SpecialEventType.GemulonFuel:
					case SpecialEventType.GemulonInvaded:
					case SpecialEventType.Lottery:
					case SpecialEventType.ReactorLaser:
					case SpecialEventType.Skill:
					case SpecialEventType.SpaceMonster:
					case SpecialEventType.Tribble:
						show	= true;
						break;
					case SpecialEventType.EraseRecord:
					case SpecialEventType.Wild:
						show	= game.Commander.PoliceRecordScore < Consts.PoliceRecordScoreDubious;
						break;
					case SpecialEventType.ExperimentStopped:
						show	= game.QuestStatusExperiment > SpecialEvent.StatusExperimentNotStarted &&
							game.QuestStatusExperiment < SpecialEvent.StatusExperimentPerformed;
						break;
					case SpecialEventType.GemulonRescued:
						show	= game.QuestStatusGemulon > SpecialEvent.StatusGemulonNotStarted &&
							game.QuestStatusGemulon < SpecialEvent.StatusGemulonTooLate;
						break;
					case SpecialEventType.Japori:
						show	= game.QuestStatusJapori						== SpecialEvent.StatusJaporiNotStarted &&
							game.Commander.PoliceRecordScore	>= Consts.PoliceRecordScoreDubious;
						break;
					case SpecialEventType.JaporiDelivery:
						show	= game.QuestStatusJapori == SpecialEvent.StatusJaporiInTransit;
						break;
					case SpecialEventType.JarekGetsOut:
						show	= game.Commander.Ship.JarekOnBoard;
						break;
					case SpecialEventType.Moon:
						show	= game.QuestStatusMoon == SpecialEvent.StatusMoonNotStarted &&
							game.Commander.Worth >  SpecialEvent.MoonCost * .8;
						break;
					case SpecialEventType.MoonRetirement:
						show	= game.QuestStatusMoon == SpecialEvent.StatusMoonBought;
						break;
					case SpecialEventType.Reactor:
						show	= game.QuestStatusReactor						== SpecialEvent.StatusReactorNotStarted &&
							game.Commander.PoliceRecordScore	<  Consts.PoliceRecordScoreDubious &&
							game.Commander.ReputationScore		>= Consts.ReputationScoreAverage;
						break;
					case SpecialEventType.ReactorDelivered:
						show	= game.Commander.Ship.ReactorOnBoard;
						break;
					case SpecialEventType.Scarab:
						show	= game.QuestStatusScarab					== SpecialEvent.StatusScarabNotStarted &&
							game.Commander.ReputationScore	>= Consts.ReputationScoreAverage;
						break;
					case SpecialEventType.ScarabDestroyed:
					case SpecialEventType.ScarabUpgradeHull:
						show	= game.QuestStatusScarab == SpecialEvent.StatusScarabDestroyed;
						break;
					case SpecialEventType.SpaceMonsterKilled:
						show	= game.QuestStatusSpaceMonster == SpecialEvent.StatusSpaceMonsterDestroyed;
						break;
					case SpecialEventType.TribbleBuyer:
						show	= game.Commander.Ship.Tribbles > 0;
						break;
					case SpecialEventType.WildGetsOut:
						show	= game.Commander.Ship.WildOnBoard;
						break;
					case SpecialEventType.KesselShipyard:
					case SpecialEventType.LoronarShipyard:
					case SpecialEventType.SienarShipyard:
					case SpecialEventType.RepublicShipyard:
					case SpecialEventType.SorosuubShipyard:
						show = true;
						break;
					default:
						break;
				}
			}

			return show;
		}

		#endregion

		#region Properties

		public string Name
		{
			get
			{
				return Strings.SystemNames[(int)_id];
			}
		}

		public CrewMember MercenaryForHire
		{
			get
			{
				CrewMember[]	mercs	= Game.CurrentGame.Mercenaries;
				CrewMember[]	crew	= Game.CurrentGame.Commander.Ship.Crew;
				CrewMember		merc	= null;
	
				for (int i = 1; i < mercs.Length && merc == null; i++)
				{
					if (mercs[i].CurrentSystem == mercs[0].CurrentSystem &&
						!Game.CurrentGame.Commander.Ship.HasCrew(mercs[i].Id))
						merc	= mercs[i];
				}
	
				return merc;
			}
		}

		public StarSystemId Id
		{
			get
			{
				return _id;
			}
		}

		public int X
		{
			get
			{
				return _x;
			}
			set
			{
				_x	= value;
			}
		}

		public int Y
		{
			get
			{
				return _y;
			}
			set
			{
				_y	= value;
			}
		}

		public Size Size
		{
			get
			{
				return _size;
			}
		}

		public TechLevel TechLevel
		{
			get
			{
				return _techLevel;
			}
			set
			{
				_techLevel	= value;
			}
		}

		public PoliticalSystem PoliticalSystem
		{
			get
			{
				return _politicalSystem;
			}
			set
			{
				_politicalSystem	= value;
			}
		}

		public SystemPressure Pressure
		{
			get
			{
				return _pressure;
			}
			set
			{
				_pressure   = value;
			}
		}

		public SpecialResource SpecialResource
		{
			get
			{
				return _specialResource;
			}
		}

		public SpecialEvent SpecialEvent
		{
			get
			{
				return _specialEvent;
			}
			set
			{
				_specialEvent   = value;
			}
		}

		public int[] TradeItems
		{
			get
			{
				return _tradeItems;
			}
		}

		public int CountDown
		{
			get
			{
				return _countDown;
			}
			set
			{
				_countDown	= value;
			}
		}

		public bool Visited
		{
			get
			{
				return _visited;
			}
			set
			{
				_visited	= value;
			}
		}

		public int Distance
		{
			get
			{
				return Functions.Distance(this, Game.CurrentGame.Commander.CurrentSystem);
			}
		}

		public bool DestOk
		{
			get
			{
				Commander	comm	= Game.CurrentGame.Commander;
				return this != comm.CurrentSystem && (Distance <= comm.Ship.Fuel ||
					Functions.WormholeExists(comm.CurrentSystem, this));
			}
		}

		#endregion
	}
}