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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Fryz.Apps.SpaceTrader
{
	public class FormMonster : System.Windows.Forms.Form
	{
		#region Control Declarations

		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label lblQuests;
		private System.Windows.Forms.Label lblMercNames;
		private System.Windows.Forms.LinkLabel lblQuestSystems;
		private System.Windows.Forms.Label lblQuestsLabel;
		private System.Windows.Forms.PictureBox picLine1;
		private System.Windows.Forms.PictureBox picLine0;
		private System.Windows.Forms.Label lblMercIds;
		private System.Windows.Forms.LinkLabel lblMercSystems;
		private System.Windows.Forms.Label lblMercSkillsPilot;
		private System.Windows.Forms.Label lblMercSkillsFighter;
		private System.Windows.Forms.Label lblMercSkillsTrader;
		private System.Windows.Forms.Label lblMercSkillsEngineer;
		private System.Windows.Forms.Label lblMercLabel;
		private System.Windows.Forms.Label lblSkillLabelPilot;
		private System.Windows.Forms.Label lblSkillLabelFighter;
		private System.Windows.Forms.Label lblSkillLabelTrader;
		private System.Windows.Forms.Label lblSkillLabelEngineer;
		private System.Windows.Forms.Label lblMercSystemLabel;
		private System.Windows.Forms.Label lblQuestSystemLabel;
		private System.Windows.Forms.Label lblQuestDescLabel;
		private System.Windows.Forms.Label lblMercIDLabel;
		private System.Windows.Forms.Label lblMercNameLabel;
		private System.ComponentModel.Container components = null;

		#endregion

		#region Member Declarations

		private Game	game	= Game.CurrentGame;

		#endregion

		#region Methods

		public FormMonster()
		{
			InitializeComponent();

			UpdateAll();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
				components.Dispose();
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnClose = new System.Windows.Forms.Button();
			this.lblQuestSystems = new System.Windows.Forms.LinkLabel();
			this.lblQuests = new System.Windows.Forms.Label();
			this.lblMercNames = new System.Windows.Forms.Label();
			this.picLine1 = new System.Windows.Forms.PictureBox();
			this.picLine0 = new System.Windows.Forms.PictureBox();
			this.lblQuestsLabel = new System.Windows.Forms.Label();
			this.lblMercIds = new System.Windows.Forms.Label();
			this.lblMercSystems = new System.Windows.Forms.LinkLabel();
			this.lblMercSkillsPilot = new System.Windows.Forms.Label();
			this.lblMercSkillsFighter = new System.Windows.Forms.Label();
			this.lblMercSkillsTrader = new System.Windows.Forms.Label();
			this.lblMercSkillsEngineer = new System.Windows.Forms.Label();
			this.lblMercLabel = new System.Windows.Forms.Label();
			this.lblSkillLabelPilot = new System.Windows.Forms.Label();
			this.lblSkillLabelFighter = new System.Windows.Forms.Label();
			this.lblSkillLabelTrader = new System.Windows.Forms.Label();
			this.lblSkillLabelEngineer = new System.Windows.Forms.Label();
			this.lblMercSystemLabel = new System.Windows.Forms.Label();
			this.lblQuestSystemLabel = new System.Windows.Forms.Label();
			this.lblQuestDescLabel = new System.Windows.Forms.Label();
			this.lblMercIDLabel = new System.Windows.Forms.Label();
			this.lblMercNameLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(-32, -32);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(32, 32);
			this.btnClose.TabIndex = 32;
			this.btnClose.TabStop = false;
			this.btnClose.Text = "X";
			// 
			// lblQuestSystems
			// 
			this.lblQuestSystems.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
			this.lblQuestSystems.Location = new System.Drawing.Point(8, 48);
			this.lblQuestSystems.Name = "lblQuestSystems";
			this.lblQuestSystems.Size = new System.Drawing.Size(68, 376);
			this.lblQuestSystems.TabIndex = 45;
			this.lblQuestSystems.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\n13\r\n14\r\n15\r\nTarchannen";
			this.lblQuestSystems.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblSystems_LinkClicked);
			// 
			// lblQuests
			// 
			this.lblQuests.Location = new System.Drawing.Point(80, 48);
			this.lblQuests.Name = "lblQuests";
			this.lblQuests.Size = new System.Drawing.Size(120, 376);
			this.lblQuests.TabIndex = 46;
			this.lblQuests.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\n13\r\n14\r\n15\r\nInstall Morgan\'s Laser";
			// 
			// lblMercNames
			// 
			this.lblMercNames.Location = new System.Drawing.Point(232, 48);
			this.lblMercNames.Name = "lblMercNames";
			this.lblMercNames.Size = new System.Drawing.Size(56, 376);
			this.lblMercNames.TabIndex = 47;
			this.lblMercNames.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\n13\r\n14\r\nZeethibal1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9" +
				"\r\n10\r\n11\r\n12\r\n13\r\n14\r\nZeethibal";
			// 
			// picLine1
			// 
			this.picLine1.BackColor = System.Drawing.Color.DimGray;
			this.picLine1.Location = new System.Drawing.Point(4, 40);
			this.picLine1.Name = "picLine1";
			this.picLine1.Size = new System.Drawing.Size(512, 1);
			this.picLine1.TabIndex = 133;
			this.picLine1.TabStop = false;
			// 
			// picLine0
			// 
			this.picLine0.BackColor = System.Drawing.Color.DimGray;
			this.picLine0.Location = new System.Drawing.Point(200, 8);
			this.picLine0.Name = "picLine0";
			this.picLine0.Size = new System.Drawing.Size(1, 416);
			this.picLine0.TabIndex = 132;
			this.picLine0.TabStop = false;
			// 
			// lblQuestsLabel
			// 
			this.lblQuestsLabel.AutoSize = true;
			this.lblQuestsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblQuestsLabel.Location = new System.Drawing.Point(71, 4);
			this.lblQuestsLabel.Name = "lblQuestsLabel";
			this.lblQuestsLabel.Size = new System.Drawing.Size(50, 16);
			this.lblQuestsLabel.TabIndex = 134;
			this.lblQuestsLabel.Text = "Quests";
			// 
			// lblMercIds
			// 
			this.lblMercIds.Location = new System.Drawing.Point(204, 48);
			this.lblMercIds.Name = "lblMercIds";
			this.lblMercIds.Size = new System.Drawing.Size(23, 376);
			this.lblMercIds.TabIndex = 135;
			this.lblMercIds.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\n13\r\n14\r\n15\r\n16\r\n17\r\n18\r\n19\r\n20\r\n21\r\n22\r\n23" +
				"\r\n24\r\n25\r\n26\r\n27\r\n28\r\n29\r\n30";
			this.lblMercIds.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblMercSystems
			// 
			this.lblMercSystems.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
			this.lblMercSystems.Location = new System.Drawing.Point(376, 48);
			this.lblMercSystems.Name = "lblMercSystems";
			this.lblMercSystems.Size = new System.Drawing.Size(144, 376);
			this.lblMercSystems.TabIndex = 136;
			this.lblMercSystems.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\n13\r\n14\r\n15\r\nTarchannen (on board ship)";
			this.lblMercSystems.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblSystems_LinkClicked);
			// 
			// lblMercSkillsPilot
			// 
			this.lblMercSkillsPilot.Location = new System.Drawing.Point(284, 48);
			this.lblMercSkillsPilot.Name = "lblMercSkillsPilot";
			this.lblMercSkillsPilot.Size = new System.Drawing.Size(20, 376);
			this.lblMercSkillsPilot.TabIndex = 137;
			this.lblMercSkillsPilot.Text = "10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n1" +
				"0\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10";
			this.lblMercSkillsPilot.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblMercSkillsFighter
			// 
			this.lblMercSkillsFighter.Location = new System.Drawing.Point(304, 48);
			this.lblMercSkillsFighter.Name = "lblMercSkillsFighter";
			this.lblMercSkillsFighter.Size = new System.Drawing.Size(20, 376);
			this.lblMercSkillsFighter.TabIndex = 138;
			this.lblMercSkillsFighter.Text = "10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n1" +
				"0\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10";
			this.lblMercSkillsFighter.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblMercSkillsTrader
			// 
			this.lblMercSkillsTrader.Location = new System.Drawing.Point(324, 48);
			this.lblMercSkillsTrader.Name = "lblMercSkillsTrader";
			this.lblMercSkillsTrader.Size = new System.Drawing.Size(20, 376);
			this.lblMercSkillsTrader.TabIndex = 139;
			this.lblMercSkillsTrader.Text = "10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n1" +
				"0\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10";
			this.lblMercSkillsTrader.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblMercSkillsEngineer
			// 
			this.lblMercSkillsEngineer.Location = new System.Drawing.Point(344, 48);
			this.lblMercSkillsEngineer.Name = "lblMercSkillsEngineer";
			this.lblMercSkillsEngineer.Size = new System.Drawing.Size(20, 376);
			this.lblMercSkillsEngineer.TabIndex = 140;
			this.lblMercSkillsEngineer.Text = "10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n1" +
				"0\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10\r\n10";
			this.lblMercSkillsEngineer.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblMercLabel
			// 
			this.lblMercLabel.AutoSize = true;
			this.lblMercLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblMercLabel.Location = new System.Drawing.Point(316, 4);
			this.lblMercLabel.Name = "lblMercLabel";
			this.lblMercLabel.Size = new System.Drawing.Size(84, 16);
			this.lblMercLabel.TabIndex = 141;
			this.lblMercLabel.Text = "Mercenaries";
			// 
			// lblSkillLabelPilot
			// 
			this.lblSkillLabelPilot.AutoSize = true;
			this.lblSkillLabelPilot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSkillLabelPilot.Location = new System.Drawing.Point(292, 24);
			this.lblSkillLabelPilot.Name = "lblSkillLabelPilot";
			this.lblSkillLabelPilot.Size = new System.Drawing.Size(12, 13);
			this.lblSkillLabelPilot.TabIndex = 142;
			this.lblSkillLabelPilot.Text = "P";
			this.lblSkillLabelPilot.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblSkillLabelFighter
			// 
			this.lblSkillLabelFighter.AutoSize = true;
			this.lblSkillLabelFighter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSkillLabelFighter.Location = new System.Drawing.Point(312, 24);
			this.lblSkillLabelFighter.Name = "lblSkillLabelFighter";
			this.lblSkillLabelFighter.Size = new System.Drawing.Size(11, 13);
			this.lblSkillLabelFighter.TabIndex = 143;
			this.lblSkillLabelFighter.Text = "F";
			this.lblSkillLabelFighter.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblSkillLabelTrader
			// 
			this.lblSkillLabelTrader.AutoSize = true;
			this.lblSkillLabelTrader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSkillLabelTrader.Location = new System.Drawing.Point(332, 24);
			this.lblSkillLabelTrader.Name = "lblSkillLabelTrader";
			this.lblSkillLabelTrader.Size = new System.Drawing.Size(11, 13);
			this.lblSkillLabelTrader.TabIndex = 144;
			this.lblSkillLabelTrader.Text = "T";
			this.lblSkillLabelTrader.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblSkillLabelEngineer
			// 
			this.lblSkillLabelEngineer.AutoSize = true;
			this.lblSkillLabelEngineer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSkillLabelEngineer.Location = new System.Drawing.Point(352, 24);
			this.lblSkillLabelEngineer.Name = "lblSkillLabelEngineer";
			this.lblSkillLabelEngineer.Size = new System.Drawing.Size(12, 13);
			this.lblSkillLabelEngineer.TabIndex = 145;
			this.lblSkillLabelEngineer.Text = "E";
			this.lblSkillLabelEngineer.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblMercSystemLabel
			// 
			this.lblMercSystemLabel.AutoSize = true;
			this.lblMercSystemLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblMercSystemLabel.Location = new System.Drawing.Point(376, 24);
			this.lblMercSystemLabel.Name = "lblMercSystemLabel";
			this.lblMercSystemLabel.Size = new System.Drawing.Size(43, 13);
			this.lblMercSystemLabel.TabIndex = 146;
			this.lblMercSystemLabel.Text = "System";
			// 
			// lblQuestSystemLabel
			// 
			this.lblQuestSystemLabel.AutoSize = true;
			this.lblQuestSystemLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblQuestSystemLabel.Location = new System.Drawing.Point(8, 24);
			this.lblQuestSystemLabel.Name = "lblQuestSystemLabel";
			this.lblQuestSystemLabel.Size = new System.Drawing.Size(43, 13);
			this.lblQuestSystemLabel.TabIndex = 147;
			this.lblQuestSystemLabel.Text = "System";
			// 
			// lblQuestDescLabel
			// 
			this.lblQuestDescLabel.AutoSize = true;
			this.lblQuestDescLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblQuestDescLabel.Location = new System.Drawing.Point(80, 24);
			this.lblQuestDescLabel.Name = "lblQuestDescLabel";
			this.lblQuestDescLabel.Size = new System.Drawing.Size(63, 13);
			this.lblQuestDescLabel.TabIndex = 148;
			this.lblQuestDescLabel.Text = "Description";
			// 
			// lblMercIDLabel
			// 
			this.lblMercIDLabel.AutoSize = true;
			this.lblMercIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblMercIDLabel.Location = new System.Drawing.Point(212, 24);
			this.lblMercIDLabel.Name = "lblMercIDLabel";
			this.lblMercIDLabel.Size = new System.Drawing.Size(16, 13);
			this.lblMercIDLabel.TabIndex = 149;
			this.lblMercIDLabel.Text = "ID";
			// 
			// lblMercNameLabel
			// 
			this.lblMercNameLabel.AutoSize = true;
			this.lblMercNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblMercNameLabel.Location = new System.Drawing.Point(232, 24);
			this.lblMercNameLabel.Name = "lblMercNameLabel";
			this.lblMercNameLabel.Size = new System.Drawing.Size(35, 13);
			this.lblMercNameLabel.TabIndex = 150;
			this.lblMercNameLabel.Text = "Name";
			// 
			// FormMonster
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(522, 427);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																																	this.lblMercNameLabel,
																																	this.lblMercIDLabel,
																																	this.lblQuestDescLabel,
																																	this.lblQuestSystemLabel,
																																	this.lblMercSystemLabel,
																																	this.lblSkillLabelEngineer,
																																	this.lblSkillLabelTrader,
																																	this.lblSkillLabelFighter,
																																	this.lblSkillLabelPilot,
																																	this.lblMercLabel,
																																	this.lblQuestsLabel,
																																	this.lblMercSkillsPilot,
																																	this.lblMercSkillsFighter,
																																	this.lblMercSkillsTrader,
																																	this.lblMercSkillsEngineer,
																																	this.picLine0,
																																	this.lblMercSystems,
																																	this.lblMercIds,
																																	this.picLine1,
																																	this.lblMercNames,
																																	this.lblQuests,
																																	this.lblQuestSystems,
																																	this.btnClose});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMonster";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Monster.com Job Listing";
			this.ResumeLayout(false);
		}
		#endregion

		private void UpdateAll()
		{
			lblQuestSystems.Text	= "";
			lblQuests.Text				= "";
			for (int i = 0; i < game.Universe.Length; i++)
			{
				StarSystem	curSys	= game.Universe[i];

				if (curSys.ShowSpecialButton())
				{
					int	start							 = lblQuestSystems.Text.Length;

					lblQuestSystems.Text	+= curSys.Name + "\r\n";
					lblQuests.Text				+= curSys.SpecialEvent.Title + "\r\n";

					lblQuestSystems.Links.Add(start, curSys.Name.Length, curSys.Name);
				}
			}
			lblQuestSystems.Text	= lblQuestSystems.Text.Trim();
			lblQuests.Text				= lblQuests.Text.Trim();

			lblMercIds.Text							= "";
			lblMercNames.Text						= "";
			lblMercSkillsPilot.Text			= "";
			lblMercSkillsFighter.Text		= "";
			lblMercSkillsTrader.Text		= "";
			lblMercSkillsEngineer.Text	= "";
			lblMercSystems.Text					= "";
			for (int i = 1; i <= (int)CrewMemberId.Zeethibal; i++)
			{
				CrewMember	merc	= game.Mercenaries[i];
				int					start	= lblMercSystems.Text.Length;

				lblMercIds.Text							+= ((int)merc.Id).ToString() + "\r\n";
				lblMercNames.Text						+= merc.Name + "\r\n";
				lblMercSkillsPilot.Text			+= merc.Pilot.ToString() + "\r\n";
				lblMercSkillsFighter.Text		+= merc.Fighter.ToString() + "\r\n";
				lblMercSkillsTrader.Text		+= merc.Trader.ToString() + "\r\n";
				lblMercSkillsEngineer.Text	+= merc.Engineer.ToString() + "\r\n";
				lblMercSystems.Text					+= (merc.CurrentSystem != null ? merc.CurrentSystem.Name : Strings.MercSystemUnknown) +
																			 (game.Commander.Ship.HasCrew(merc.Id) ? Strings.MercOnBoard : "") + "\r\n";

				if (merc.CurrentSystem != null && !game.Commander.Ship.HasCrew(merc.Id))
					lblMercSystems.Links.Add(start, merc.CurrentSystem.Name.Length, merc.CurrentSystem.Name);
			}
			lblMercIds.Text							= lblMercIds.Text.Trim();
			lblMercNames.Text						= lblMercNames.Text.Trim();
			lblMercSkillsPilot.Text			= lblMercSkillsPilot.Text.Trim();
			lblMercSkillsFighter.Text		= lblMercSkillsFighter.Text.Trim();
			lblMercSkillsTrader.Text		= lblMercSkillsTrader.Text.Trim();
			lblMercSkillsEngineer.Text	= lblMercSkillsEngineer.Text.Trim();
			lblMercSystems.Text					= lblMercSystems.Text.Trim();
		}

		#endregion

		#region Event Handlers

		private void lblSystems_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Game.CurrentGame.SelectedSystemName	= e.Link.LinkData.ToString();
			Game.CurrentGame.ParentWindow.UpdateAll();
			Close();
		}

		#endregion
	}
}