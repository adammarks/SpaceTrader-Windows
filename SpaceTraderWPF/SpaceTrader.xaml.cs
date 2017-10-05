/*******************************************************************************
 *
 * Space Trader for Windows 2.01
 *
 * Copyright (C) 2008 Jay French, All Rights Reserved
 *
 * Additional coding by David Pierron
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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fryz.Apps.SpaceTrader;
using Microsoft.Win32;


namespace SpaceTraderWPF
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class SpaceTrader : Window
   {

      //private System.ComponentModel.IContainer components;

      private Label[]            lblSellPrice;
      private Label[]            lblBuyPrice;
      private Label[]            lblTargetPrice;
      private Label[]            lblTargetDiff;
      private Label[]            lblTargetPct;
      private Button[]        btnSellQty;
      private Button[]        btnSellAll;
      private Button[]        btnBuyQty;
      private Button[]        btnBuyMax;

      private const string SAVE_ARRIVAL      = "autosave_arrival.sav";
      private const string SAVE_DEPARTURE = "autosave_departure.sav";

      private const  int         OFF_X                = 3;
      private const  int         OFF_Y                = 3;
      private const  int         OFF_X_WORM        = OFF_X + 1;
      private const  int         IMG_G_N              = 0;
      private const  int         IMG_G_V              = 1;
      private const  int         IMG_G_W              = 2;
      private const  int         IMG_S_N              = 3;
      private const  int         IMG_S_NS          = 4;
      private const  int         IMG_S_V              = 5;
      private const  int         IMG_S_VS          = 6;
      private const  int         IMG_S_W              = 7;

      private Game               game;

      private System.Drawing.Pen                DEFAULT_PEN    = new System.Drawing.Pen(System.Drawing.Color.Black);
      private System.Drawing.Brush             DEFAULT_BRUSH  = new System.Drawing.SolidBrush(System.Drawing.Color.White);

      private string          SaveGameFile   = null;
      private int                SaveGameDays   = -1;

      private System.Windows.Forms.ImageList ilChartImages;
      private System.Windows.Forms.ImageList ilDirectionImages;
      private System.Windows.Forms.ImageList ilEquipmentImages;
      private System.Windows.Forms.ImageList ilShipImages;

      System.Windows.Forms.IWin32Window myWin32Window = new System.Windows.Forms.NativeWindow();

      public SpaceTrader()
      {
         string loadFileName = null;

         InitializeComponent();
         (myWin32Window as System.Windows.Forms.NativeWindow).AssignHandle( new System.Windows.Interop.WindowInteropHelper( this ).Handle );
         CreateImageLists();

         InitFileStructure();

         #region Arrays of Cargo controls
         lblSellPrice = new Label[]
         {
            lblSellPrice0,
            lblSellPrice1,
            lblSellPrice2,
            lblSellPrice3,
            lblSellPrice4,
            lblSellPrice5,
            lblSellPrice6,
            lblSellPrice7,
            lblSellPrice8,
            lblSellPrice9
         };

         lblBuyPrice = new Label[]
         {
            lblBuyPrice0,
            lblBuyPrice1,
            lblBuyPrice2,
            lblBuyPrice3,
            lblBuyPrice4,
            lblBuyPrice5,
            lblBuyPrice6,
            lblBuyPrice7,
            lblBuyPrice8,
            lblBuyPrice9
         };

         lblTargetPrice = new Label[]
         {
            lblTargetPrice0,
            lblTargetPrice1,
            lblTargetPrice2,
            lblTargetPrice3,
            lblTargetPrice4,
            lblTargetPrice5,
            lblTargetPrice6,
            lblTargetPrice7,
            lblTargetPrice8,
            lblTargetPrice9
         };

         lblTargetDiff = new Label[]
         {
            lblTargetDiff0,
            lblTargetDiff1,
            lblTargetDiff2,
            lblTargetDiff3,
            lblTargetDiff4,
            lblTargetDiff5,
            lblTargetDiff6,
            lblTargetDiff7,
            lblTargetDiff8,
            lblTargetDiff9
         };

         lblTargetPct = new Label[]
         {
            lblTargetPct0,
            lblTargetPct1,
            lblTargetPct2,
            lblTargetPct3,
            lblTargetPct4,
            lblTargetPct5,
            lblTargetPct6,
            lblTargetPct7,
            lblTargetPct8,
            lblTargetPct9
         };

         btnSellQty = new Button[]
         {
            btnSellQty0,
            btnSellQty1,
            btnSellQty2,
            btnSellQty3,
            btnSellQty4,
            btnSellQty5,
            btnSellQty6,
            btnSellQty7,
            btnSellQty8,
            btnSellQty9
         };

         btnSellAll = new Button[]
         {
            btnSellAll0,
            btnSellAll1,
            btnSellAll2,
            btnSellAll3,
            btnSellAll4,
            btnSellAll5,
            btnSellAll6,
            btnSellAll7,
            btnSellAll8,
            btnSellAll9
         };

         btnBuyQty = new Button[]
         {
            btnBuyQty0,
            btnBuyQty1,
            btnBuyQty2,
            btnBuyQty3,
            btnBuyQty4,
            btnBuyQty5,
            btnBuyQty6,
            btnBuyQty7,
            btnBuyQty8,
            btnBuyQty9
         };

         btnBuyMax = new Button[]
         {
            btnBuyMax0,
            btnBuyMax1,
            btnBuyMax2,
            btnBuyMax3,
            btnBuyMax4,
            btnBuyMax5,
            btnBuyMax6,
            btnBuyMax7,
            btnBuyMax8,
            btnBuyMax9
         };
         #endregion

         if ( loadFileName != null )
            LoadGame( loadFileName );

         UpdateAll();
      }

      private System.ComponentModel.IContainer components;

      BitmapImage imgIMG_G_N;
      BitmapImage imgIMG_G_V;
      BitmapImage imgIMG_G_W;
      BitmapImage imgIMG_S_N;
      BitmapImage imgIMG_S_NS;
      BitmapImage imgIMG_S_V;
      BitmapImage imgIMG_S_VS;
      BitmapImage imgIMG_S_W;

      private void CreateImageLists()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpaceTrader));

         this.components = new System.ComponentModel.Container();

         this.ilChartImages = new System.Windows.Forms.ImageList( this.components );
         this.ilShipImages = new System.Windows.Forms.ImageList( this.components );
         this.ilDirectionImages = new System.Windows.Forms.ImageList( this.components );
         this.ilEquipmentImages = new System.Windows.Forms.ImageList( this.components );

         string r = @"pack://application:,,,/images/";
        
         imgIMG_G_N = new BitmapImage( new Uri( r+"chartlnv.bmp", UriKind.Absolute ) );
         imgIMG_G_V = new BitmapImage( new Uri( r+"chartlv.bmp", UriKind.Absolute ) );
         imgIMG_G_W = new BitmapImage( new Uri( r+"chartlw.bmp", UriKind.Absolute ) );
         imgIMG_S_N = new BitmapImage( new Uri( r+"chartsnv.bmp", UriKind.Absolute ) );
         imgIMG_S_NS = new BitmapImage( new Uri( r+"chartsnvs.bmp", UriKind.Absolute ) );
         imgIMG_S_V = new BitmapImage( new Uri( r+"chartsv.bmp", UriKind.Absolute ) );
         imgIMG_S_VS = new BitmapImage( new Uri( r+"chartsvs.bmp", UriKind.Absolute ) );
         imgIMG_S_W = new BitmapImage( new Uri( r+"chartsw.bmp", UriKind.Absolute ) );

         return;

         //var image.Source = new BitmapImage( new Uri( "pack://application:,,,/YourAssemblyName;component/Resources/someimage.png", UriKind.Absolute ) );

         //// 
         //// ilChartImages
         //// 
         //this.ilChartImages.ImageStream = ( (System.Windows.Forms.ImageListStreamer)( resources.GetObject( "ilChartImages.ImageStream" ) ) );
         //this.ilChartImages.TransparentColor = System.Drawing.Color.White;
         //this.ilChartImages.Images.SetKeyName( 0, "" );
         //this.ilChartImages.Images.SetKeyName( 1, "" );
         //this.ilChartImages.Images.SetKeyName( 2, "" );
         //this.ilChartImages.Images.SetKeyName( 3, "" );
         //this.ilChartImages.Images.SetKeyName( 4, "" );
         //this.ilChartImages.Images.SetKeyName( 5, "" );
         //this.ilChartImages.Images.SetKeyName( 6, "" );
         //this.ilChartImages.Images.SetKeyName( 7, "" );
         //// 
         //// ilShipImages
         //// 
         //this.ilShipImages.ImageStream = ( (System.Windows.Forms.ImageListStreamer)( resources.GetObject( "ilShipImages.ImageStream" ) ) );
         //this.ilShipImages.TransparentColor = System.Drawing.Color.White;
         //this.ilShipImages.Images.SetKeyName( 0, "" );
         //this.ilShipImages.Images.SetKeyName( 1, "" );
         //this.ilShipImages.Images.SetKeyName( 2, "" );
         //this.ilShipImages.Images.SetKeyName( 3, "" );
         //this.ilShipImages.Images.SetKeyName( 4, "" );
         //this.ilShipImages.Images.SetKeyName( 5, "" );
         //this.ilShipImages.Images.SetKeyName( 6, "" );
         //this.ilShipImages.Images.SetKeyName( 7, "" );
         //this.ilShipImages.Images.SetKeyName( 8, "" );
         //this.ilShipImages.Images.SetKeyName( 9, "" );
         //this.ilShipImages.Images.SetKeyName( 10, "" );
         //this.ilShipImages.Images.SetKeyName( 11, "" );
         //this.ilShipImages.Images.SetKeyName( 12, "" );
         //this.ilShipImages.Images.SetKeyName( 13, "" );
         //this.ilShipImages.Images.SetKeyName( 14, "" );
         //this.ilShipImages.Images.SetKeyName( 15, "" );
         //this.ilShipImages.Images.SetKeyName( 16, "" );
         //this.ilShipImages.Images.SetKeyName( 17, "" );
         //this.ilShipImages.Images.SetKeyName( 18, "" );
         //this.ilShipImages.Images.SetKeyName( 19, "" );
         //this.ilShipImages.Images.SetKeyName( 20, "" );
         //this.ilShipImages.Images.SetKeyName( 21, "" );
         //this.ilShipImages.Images.SetKeyName( 22, "" );
         //this.ilShipImages.Images.SetKeyName( 23, "" );
         //this.ilShipImages.Images.SetKeyName( 24, "" );
         //this.ilShipImages.Images.SetKeyName( 25, "" );
         //this.ilShipImages.Images.SetKeyName( 26, "" );
         //this.ilShipImages.Images.SetKeyName( 27, "" );
         //this.ilShipImages.Images.SetKeyName( 28, "" );
         //this.ilShipImages.Images.SetKeyName( 29, "" );
         //this.ilShipImages.Images.SetKeyName( 30, "" );
         //this.ilShipImages.Images.SetKeyName( 31, "" );
         //this.ilShipImages.Images.SetKeyName( 32, "" );
         //this.ilShipImages.Images.SetKeyName( 33, "" );
         //this.ilShipImages.Images.SetKeyName( 34, "" );
         //this.ilShipImages.Images.SetKeyName( 35, "" );
         //this.ilShipImages.Images.SetKeyName( 36, "" );
         //this.ilShipImages.Images.SetKeyName( 37, "" );
         //this.ilShipImages.Images.SetKeyName( 38, "" );
         //this.ilShipImages.Images.SetKeyName( 39, "" );
         //this.ilShipImages.Images.SetKeyName( 40, "" );
         //this.ilShipImages.Images.SetKeyName( 41, "" );
         //this.ilShipImages.Images.SetKeyName( 42, "" );
         //this.ilShipImages.Images.SetKeyName( 43, "" );
         //this.ilShipImages.Images.SetKeyName( 44, "" );
         //this.ilShipImages.Images.SetKeyName( 45, "" );
         //this.ilShipImages.Images.SetKeyName( 46, "" );
         //this.ilShipImages.Images.SetKeyName( 47, "" );
         //this.ilShipImages.Images.SetKeyName( 48, "" );
         //this.ilShipImages.Images.SetKeyName( 49, "" );
         //this.ilShipImages.Images.SetKeyName( 50, "" );
         //this.ilShipImages.Images.SetKeyName( 51, "" );
         //this.ilShipImages.Images.SetKeyName( 52, "" );
         //this.ilShipImages.Images.SetKeyName( 53, "" );
         //this.ilShipImages.Images.SetKeyName( 54, "" );
         //this.ilShipImages.Images.SetKeyName( 55, "" );
         //this.ilShipImages.Images.SetKeyName( 56, "" );
         //this.ilShipImages.Images.SetKeyName( 57, "" );
         //this.ilShipImages.Images.SetKeyName( 58, "" );
         //this.ilShipImages.Images.SetKeyName( 59, "" );
         //this.ilShipImages.Images.SetKeyName( 60, "" );
         //this.ilShipImages.Images.SetKeyName( 61, "" );
         //this.ilShipImages.Images.SetKeyName( 62, "" );
         //this.ilShipImages.Images.SetKeyName( 63, "" );
         //this.ilShipImages.Images.SetKeyName( 64, "" );
         //this.ilShipImages.Images.SetKeyName( 65, "" );
         //this.ilShipImages.Images.SetKeyName( 66, "" );
         //this.ilShipImages.Images.SetKeyName( 67, "" );
         //// 
         //// ilDirectionImages
         //// 
         //this.ilDirectionImages.ImageStream = ( (System.Windows.Forms.ImageListStreamer)( resources.GetObject( "ilDirectionImages.ImageStream" ) ) );
         //this.ilDirectionImages.TransparentColor = System.Drawing.Color.White;
         //this.ilDirectionImages.Images.SetKeyName( 0, "" );
         //this.ilDirectionImages.Images.SetKeyName( 1, "" );
         //this.ilDirectionImages.Images.SetKeyName( 2, "" );
         //this.ilDirectionImages.Images.SetKeyName( 3, "" );
         //// 
         //// ilEquipmentImages
         //// 
         //this.ilEquipmentImages.ImageStream = ( (System.Windows.Forms.ImageListStreamer)( resources.GetObject( "ilEquipmentImages.ImageStream" ) ) );
         //this.ilEquipmentImages.TransparentColor = System.Drawing.Color.Transparent;
         //this.ilEquipmentImages.Images.SetKeyName( 0, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 1, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 2, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 3, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 4, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 5, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 6, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 7, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 8, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 9, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 10, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 11, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 12, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 13, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 14, "" );
         //this.ilEquipmentImages.Images.SetKeyName( 15, "" );
      }

      //public SpaceTrader( string loadFileName )
      //{
      //   InitializeComponent();
      //   ( myWin32Window as System.Windows.Forms.NativeWindow ).AssignHandle( new System.Windows.Interop.WindowInteropHelper( this ).Handle );

      //   CreateImageLists();

      //   InitFileStructure();

      //   #region Arrays of Cargo controls
      //   lblSellPrice = new Label[]
      //   {
      //      lblSellPrice0,
      //      lblSellPrice1,
      //      lblSellPrice2,
      //      lblSellPrice3,
      //      lblSellPrice4,
      //      lblSellPrice5,
      //      lblSellPrice6,
      //      lblSellPrice7,
      //      lblSellPrice8,
      //      lblSellPrice9
      //   };

      //   lblBuyPrice = new Label[]
      //   {
      //      lblBuyPrice0,
      //      lblBuyPrice1,
      //      lblBuyPrice2,
      //      lblBuyPrice3,
      //      lblBuyPrice4,
      //      lblBuyPrice5,
      //      lblBuyPrice6,
      //      lblBuyPrice7,
      //      lblBuyPrice8,
      //      lblBuyPrice9
      //   };

      //   lblTargetPrice = new Label[]
      //   {
      //      lblTargetPrice0,
      //      lblTargetPrice1,
      //      lblTargetPrice2,
      //      lblTargetPrice3,
      //      lblTargetPrice4,
      //      lblTargetPrice5,
      //      lblTargetPrice6,
      //      lblTargetPrice7,
      //      lblTargetPrice8,
      //      lblTargetPrice9
      //   };

      //   lblTargetDiff = new Label[]
      //   {
      //      lblTargetDiff0,
      //      lblTargetDiff1,
      //      lblTargetDiff2,
      //      lblTargetDiff3,
      //      lblTargetDiff4,
      //      lblTargetDiff5,
      //      lblTargetDiff6,
      //      lblTargetDiff7,
      //      lblTargetDiff8,
      //      lblTargetDiff9
      //   };

      //   lblTargetPct = new Label[]
      //   {
      //      lblTargetPct0,
      //      lblTargetPct1,
      //      lblTargetPct2,
      //      lblTargetPct3,
      //      lblTargetPct4,
      //      lblTargetPct5,
      //      lblTargetPct6,
      //      lblTargetPct7,
      //      lblTargetPct8,
      //      lblTargetPct9
      //   };

      //   btnSellQty = new Button[]
      //   {
      //      btnSellQty0,
      //      btnSellQty1,
      //      btnSellQty2,
      //      btnSellQty3,
      //      btnSellQty4,
      //      btnSellQty5,
      //      btnSellQty6,
      //      btnSellQty7,
      //      btnSellQty8,
      //      btnSellQty9
      //   };

      //   btnSellAll = new Button[]
      //   {
      //      btnSellAll0,
      //      btnSellAll1,
      //      btnSellAll2,
      //      btnSellAll3,
      //      btnSellAll4,
      //      btnSellAll5,
      //      btnSellAll6,
      //      btnSellAll7,
      //      btnSellAll8,
      //      btnSellAll9
      //   };

      //   btnBuyQty = new Button[]
      //   {
      //      btnBuyQty0,
      //      btnBuyQty1,
      //      btnBuyQty2,
      //      btnBuyQty3,
      //      btnBuyQty4,
      //      btnBuyQty5,
      //      btnBuyQty6,
      //      btnBuyQty7,
      //      btnBuyQty8,
      //      btnBuyQty9
      //   };

      //   btnBuyMax = new Button[]
      //   {
      //      btnBuyMax0,
      //      btnBuyMax1,
      //      btnBuyMax2,
      //      btnBuyMax3,
      //      btnBuyMax4,
      //      btnBuyMax5,
      //      btnBuyMax6,
      //      btnBuyMax7,
      //      btnBuyMax8,
      //      btnBuyMax9
      //   };
      //   #endregion

      //   if ( loadFileName != null )
      //      LoadGame( loadFileName );

      //   UpdateAll();
      //}

      //[STAThread]
      //static void Main( string[] args )
      //{
      //   Application.Run( new SpaceTrader( args.Length > 0 ? args[0] : null ) );
      //}

      //protected override void Dispose( bool disposing )
      //{
      //   if ( disposing && components != null )
      //      components.Dispose();
      //   base.Dispose( disposing );
      //}

      private void AddHighScore( HighScoreRecord highScore )
      {
         HighScoreRecord[] highScores  = Functions.GetHighScores(myWin32Window);
         highScores[0] = highScore;
         Array.Sort( highScores );

         Functions.SaveFile( Consts.HighScoreFile, STSerializableObject.ArrayToArrayList( highScores ), myWin32Window );
      }

      private void CargoBuy( int tradeItem, bool max )
      {
         game.CargoBuySystem( tradeItem, max, myWin32Window );
         UpdateAll();
      }

      private void CargoSell( int tradeItem, bool all )
      {
         if ( game.PriceCargoSell[tradeItem] > 0 )
            game.CargoSellSystem( tradeItem, all, myWin32Window );
         else
            game.CargoDump( tradeItem, myWin32Window );
         UpdateAll();
      }

      private void ClearHighScores()
      {
         HighScoreRecord[] highScores  = new HighScoreRecord[3];
         Functions.SaveFile( Consts.HighScoreFile, STSerializableObject.ArrayToArrayList( highScores ), myWin32Window );
      }

      private void GameEnd()
      {
         SetInGameControlsEnabled( false );

         AlertType   alertType   = AlertType.Alert;
         switch ( game.EndStatus )
         {
            case GameEndType.Killed:
               alertType = AlertType.GameEndKilled;
               break;
            case GameEndType.Retired:
               alertType = AlertType.GameEndRetired;
               break;
            case GameEndType.BoughtMoon:
               alertType = AlertType.GameEndBoughtMoon;
               break;
         }

         FormAlert.Alert( alertType, myWin32Window );

         FormAlert.Alert( AlertType.GameEndScore, myWin32Window, Functions.FormatNumber( game.Score / 10 ),
            Functions.FormatNumber( game.Score % 10 ) );

         HighScoreRecord   candidate   = new HighScoreRecord(game.Commander.Name, game.Score, game.EndStatus,
            game.Commander.Days, game.Commander.Worth, game.Difficulty);
         if ( candidate > Functions.GetHighScores( myWin32Window )[0] )
         {
            if ( game.CheatEnabled )
               FormAlert.Alert( AlertType.GameEndHighScoreCheat, myWin32Window );
            else
            {
               AddHighScore( candidate );
               FormAlert.Alert( AlertType.GameEndHighScoreAchieved, myWin32Window );
            }
         }
         else
            FormAlert.Alert( AlertType.GameEndHighScoreMissed, myWin32Window );

         Game.CurrentGame = null;
         game = null;
      }

      private string GetRegistrySetting( string settingName, string defaultValue )
      {
         string   settingValue   = defaultValue;

         try
         {
            RegistryKey key   = Functions.GetRegistryKey();
            object   objectValue = key.GetValue(settingName);
            if ( objectValue != null )
               settingValue = objectValue.ToString();
            key.Close();
         }
         catch ( NullReferenceException ex )
         {
            FormAlert.Alert( AlertType.RegistryError, myWin32Window, ex.Message );
         }

         return settingValue;
      }

      Microsoft.Win32.OpenFileDialog dlgOpen = new Microsoft.Win32.OpenFileDialog();
      Microsoft.Win32.SaveFileDialog dlgSave = new Microsoft.Win32.SaveFileDialog();
    
   // Make sure all directories exists.
   private void InitFileStructure()
      {
         string[] paths = new string[]
                                    {
                                       Consts.CustomDirectory,
                                       Consts.CustomImagesDirectory,
                                       Consts.CustomTemplatesDirectory,
                                       Consts.DataDirectory,
                                       Consts.SaveDirectory
                                    };

         foreach ( string path in paths )
         {
            if ( !Directory.Exists( path ) )
               Directory.CreateDirectory( path );
         }

      this.dlgOpen.Filter = "Saved-Game Files (*.sav)|*.sav|All Files (*.*)|*.*";
      this.dlgSave.FileName = "SpaceTrader.sav";
      this.dlgSave.Filter = "Saved-Game Files (*.sav)|*.sav|All Files (*.*)|*.*";

      dlgOpen.InitialDirectory = Consts.SaveDirectory;
         dlgSave.InitialDirectory = Consts.SaveDirectory;
      }

      private void LoadGame( string fileName )
      {
         try
         {
            object      obj      = Functions.LoadFile(fileName, false, myWin32Window);
            if ( obj != null )
            {
               game = new Game( (Hashtable)obj, this );
               SaveGameFile = fileName;
               SaveGameDays = game.Commander.Days;

               SetInGameControlsEnabled( true );
               UpdateAll();
            }
         }
         catch ( FutureVersionException )
         {
            FormAlert.Alert( AlertType.FileErrorOpen, myWin32Window, fileName, Strings.FileFutureVersion );
         }
      }

      private void SaveGame( string fileName, bool saveFileName )
      {
         if ( Functions.SaveFile( fileName, game.Serialize(), myWin32Window ) && saveFileName )
            SaveGameFile = fileName;

         SaveGameDays = game.Commander.Days;
      }

      private void SetInGameControlsEnabled( bool enabled )
      {
         // TODO: Menu
         //mnuGameSave.Enabled = enabled;
         //mnuGameSaveAs.Enabled = enabled;
         //mnuRetire.Enabled = enabled;
         //mnuViewCommander.Enabled = enabled;
         //mnuViewShip.Enabled = enabled;
         //mnuViewPersonnel.Enabled = enabled;
         //mnuViewQuests.Enabled = enabled;
         //mnuViewBank.Enabled = enabled;
      }

      private void SetRegistrySetting( string settingName, string settingValue )
      {
         try
         {
            RegistryKey key   = Functions.GetRegistryKey();
            key.SetValue( settingName, settingValue );
            key.Close();
         }
         catch ( NullReferenceException ex )
         {
            FormAlert.Alert( AlertType.RegistryError, myWin32Window, ex.Message );
         }
      }

      public void UpdateAll()
      {
         UpdateCargo();
         UpdateDock();
         UpdateShipyard();
         UpdateStatusBar();
         UpdateSystemInfo();
         UpdateTargetSystemInfo();
         UpdateCharts();
      }

      private void UpdateCargo()
      {
         int   i;

         if ( game == null || game.Commander.CurrentSystem == null )
         {
            for ( i = 0; i < lblSellPrice.Length; i++ )
            {
               lblSellPrice[i].Content = "";
               lblBuyPrice[i].Content = "";
               lblTargetPrice[i].Content = "";
               lblTargetDiff[i].Content = "";
               lblTargetPct[i].Content = "";
               btnSellQty[i].Visibility = Visibility.Collapsed;
               btnSellAll[i].Visibility = Visibility.Collapsed;
               btnBuyQty[i].Visibility = Visibility.Collapsed;
               btnBuyMax[i].Visibility = Visibility.Collapsed;
            }
         }
         else
         {
            int[]          buy         = game.PriceCargoBuy;
            int[]          sell     = game.PriceCargoSell;
            Commander      cmdr     = game.Commander;
            StarSystem  warpSys  = game.WarpSystem;

            for ( i = 0; i < lblSellPrice.Length; i++ )
            {
               int price = warpSys == null ? 0 : Consts.TradeItems[i].StandardPrice(warpSys);

               lblSellPrice[i].Content = sell[i] > 0 ? Functions.FormatMoney( sell[i] ) : Strings.CargoSellNA;
               btnSellQty[i].Content = cmdr.Ship.Cargo[i].ToString();
               btnSellQty[i].Visibility = Visibility.Visible;
               btnSellAll[i].Content = sell[i] > 0 ? "All" : "Dump";
               btnSellAll[i].Visibility = Visibility.Visible;
               lblBuyPrice[i].Content = buy[i] > 0 ? Functions.FormatMoney( buy[i] ) : Strings.CargoBuyNA;
               btnBuyQty[i].Content = cmdr.CurrentSystem.TradeItems[i].ToString();
               btnBuyQty[i].Visibility = BoolToVisibility( buy[i] > 0 );
               btnBuyMax[i].Visibility = BoolToVisibility( buy[i] > 0 );

               //if ( sell[i] * cmdr.Ship.Cargo[i] > cmdr.PriceCargo[i] )
               //   lblSellPrice[i].Font = lblSystemNameLabel.Font;
               //else
               //   lblSellPrice[i].Font = lblSell.Font;

               if ( warpSys != null && warpSys.DestOk && price > 0 )
                  lblTargetPrice[i].Content = Functions.FormatMoney( price );
               else
                  lblTargetPrice[i].Content = "-----------";

               if ( warpSys != null && warpSys.DestOk && price > 0 && buy[i] > 0 )
               {
                  int   diff                       = price - buy[i];
                  lblTargetDiff[i].Content = ( diff > 0 ? "+" : "" ) + Functions.FormatMoney( diff );
                  lblTargetPct[i].Content = ( diff > 0 ? "+" : "" ) + Functions.FormatNumber( 100 * diff / buy[i] ) + "%";
                  //lblBuyPrice[i].Font = ( diff > 0 && cmdr.CurrentSystem.TradeItems[i] > 0 ) ?
                  //                                       lblSystemNameLabel.Font : lblBuy.Font;
               }
               else
               {
                  lblTargetDiff[i].Content = "------------";
                  lblTargetPct[i].Content = "--------";
                  //lblBuyPrice[i].Font = lblBuy.Font;
               }

               //lblTargetPrice[i].Font = lblBuyPrice[i].Font;
               //lblTargetDiff[i].Font = lblBuyPrice[i].Font;
               //lblTargetPct[i].Font = lblBuyPrice[i].Font;
            }
         }
      }

      private void UpdateCharts()
      {
         picGalacticChartRefresh();
         picShortRangeChartRefresh();

         if ( game == null )
         {
            lblWormholeLabel.Visibility = Visibility.Collapsed;
            lblWormhole.Visibility = Visibility.Collapsed;
            btnJump.Visibility = Visibility.Collapsed;
            btnFind.Visibility = Visibility.Collapsed;
         }
         else
         {
            if ( game.TargetWormhole )
            {
               lblWormholeLabel.Visibility = Visibility.Visible;
               lblWormhole.Visibility = Visibility.Visible;
               lblWormhole.Content = game.WarpSystem.Name;
            }
            else
            {
               lblWormholeLabel.Visibility = Visibility.Collapsed;
               lblWormhole.Visibility = Visibility.Collapsed;
            }
            btnJump.Visibility = BoolToVisibility( game.CanSuperWarp );
            btnFind.Visibility = Visibility.Visible;
         }
      }

      private System.Windows.Visibility BoolToVisibility( bool visible )
      {
         if ( visible )
         {
            return Visibility.Visible;
         }

         return Visibility.Collapsed;
      }

      private void UpdateDock()
      {
         if ( game == null )
         {
            lblFuelStatus.Content = "";
            lblFuelCost.Content = "";
            btnFuel.Visibility = Visibility.Collapsed;
            lblHullStatus.Content = "";
            lblRepairCost.Content = "";
            btnRepair.Visibility = Visibility.Collapsed;
         }
         else
         {
            Ship  ship              = game.Commander.Ship;

            lblFuelStatus.Content = Functions.StringVars( Strings.DockFuelStatus, Functions.Multiples( ship.Fuel, "parsec" ) );
            int   tanksEmpty        = ship.FuelTanks - ship.Fuel;
            lblFuelCost.Content = tanksEmpty > 0 ? Functions.StringVars( Strings.DockFuelCost,
               Functions.FormatMoney( tanksEmpty * ship.FuelCost ) ) : Strings.DockFuelFull;
            btnFuel.Visibility = BoolToVisibility( tanksEmpty > 0 );

            lblHullStatus.Content = Functions.StringVars( Strings.DockHullStatus,
               Functions.FormatNumber( (int)Math.Floor( (double)100 * ship.Hull / ship.HullStrength ) ) );
            int   hullLoss          = ship.HullStrength - ship.Hull;
            lblRepairCost.Content = hullLoss > 0 ? Functions.StringVars( Strings.DockHullCost,
               Functions.FormatMoney( hullLoss * ship.RepairCost ) ) : Strings.DockHullFull;
            btnRepair.Visibility = BoolToVisibility( hullLoss > 0 );
         }
      }

      private void UpdateShipyard()
      {
         if ( game == null )
         {
            lblShipsForSale.Content = "";
            lblEquipForSale.Content = "";
            lblEscapePod.Content = "";
            btnPod.Visibility = Visibility.Collapsed;
            btnBuyShip.Visibility = Visibility.Collapsed;
            btnDesign.Visibility = Visibility.Collapsed;
            btnEquip.Visibility = Visibility.Collapsed;
         }
         else
         {
            bool  noTech               = ((int)game.Commander.CurrentSystem.TechLevel <
               (int)Consts.ShipSpecs[(int)ShipType.Flea].MinimumTechLevel);

            lblShipsForSale.Content = noTech ? Strings.ShipyardShipNoSale : Strings.ShipyardShipForSale;
            btnBuyShip.Visibility = Visibility.Visible;
            btnDesign.Visibility = BoolToVisibility( Game.CurrentGame.Commander.CurrentSystem.Shipyard != null );

            lblEquipForSale.Content = noTech ? Strings.ShipyardEquipNoSale : Strings.ShipyardEquipForSale;
            btnEquip.Visibility = Visibility.Visible;

            btnPod.Visibility = Visibility.Collapsed;
            if ( game.Commander.Ship.EscapePod )
               lblEscapePod.Content = Strings.ShipyardPodInstalled;
            else if ( noTech )
               lblEscapePod.Content = Strings.ShipyardPodNoSale;
            else if ( game.Commander.Cash < 2000 )
               lblEscapePod.Content = Strings.ShipyardPodIF;
            else
            {
               lblEscapePod.Content = Strings.ShipyardPodCost;
               btnPod.Visibility = Visibility.Visible;
            }
         }
      }

      public void UpdateStatusBar()
      {
         if ( game == null )
         {
            statusBarPanelCash.Content = "";
            statusBarPanelBays.Content = "";
            statusBarPanelCosts.Content = "";
            statusBarPanelExtra.Content = "No Game Loaded.";
         }
         else
         {
            statusBarPanelCash.Content = "Cash: " + Functions.FormatMoney( game.Commander.Cash );
            statusBarPanelBays.Content = "Bays: " + game.Commander.Ship.FilledCargoBays.ToString() + "/" +
               game.Commander.Ship.CargoBays.ToString();
            statusBarPanelCosts.Content = "Current Costs: " + Functions.FormatMoney( game.CurrentCosts );
            statusBarPanelExtra.Content = "";
         }
      }

      private void UpdateSystemInfo()
      {
         if ( game == null || game.Commander.CurrentSystem == null )
         {
            lblSystemName.Content = "";
            lblSystemSize.Content = "";
            lblSystemTech.Content = "";
            lblSystemPolSys.Content = "";
            lblSystemResource.Content = "";
            lblSystemPolice.Content = "";
            lblSystemPirates.Content = "";
            lblSystemPressure.Content = "";
            lblSystemPressurePre.Visibility = Visibility.Collapsed;
            btnNews.Visibility = Visibility.Collapsed;
            btnMerc.Visibility = Visibility.Collapsed;
            btnSpecial.Visibility = Visibility.Collapsed;
         }
         else
         {
            StarSystem     system               = game.Commander.CurrentSystem;
            CrewMember[]   mercs                = system.MercenariesForHire;

            lblSystemName.Content = system.Name;
            lblSystemSize.Content = Strings.Sizes[(int)system.Size];
            lblSystemTech.Content = Strings.TechLevelNames[(int)system.TechLevel];
            lblSystemPolSys.Content = system.PoliticalSystem.Name;
            lblSystemResource.Content = Strings.SpecialResources[(int)system.SpecialResource];
            lblSystemPolice.Content = Strings.ActivityLevels[(int)system.PoliticalSystem.ActivityPolice];
            lblSystemPirates.Content = Strings.ActivityLevels[(int)system.PoliticalSystem.ActivityPirates];
            lblSystemPressure.Content = Strings.SystemPressures[(int)system.SystemPressure];
            lblSystemPressurePre.Visibility = Visibility.Visible;
            btnNews.Visibility = Visibility.Visible;
            btnMerc.Visibility = BoolToVisibility( mercs.Length > 0 );
            //TODO: Tooltips
            //if ( btnMerc.Visibility == Visibility.Visible )
            //{
            //   tipMerc.SetToolTip( btnMerc, Functions.StringVars( Strings.MercenariesForHire, mercs.Length == 1 ? mercs[0].Name :
            //      mercs.Length.ToString() + Strings.Mercenaries ) );
            //}
            //btnSpecial.Visibility = BoolToVisibility( system.ShowSpecialButton() );
            //if ( btnSpecial.Visibility == Visibility.Visible )
            //   tipSpecial.SetToolTip( btnSpecial, system.SpecialEvent.Title );
         }
      }

      private void UpdateTargetSystemInfo()
      {
         btnNextSystem.Visibility = BoolToVisibility( game != null );
         btnPrevSystem.Visibility = BoolToVisibility( game != null );

         if ( game == null || game.WarpSystem == null )
         {
            lblTargetName.Content = "";
            lblTargetSize.Content = "";
            lblTargetTech.Content = "";
            lblTargetPolSys.Content = "";
            lblTargetResource.Content = "";
            lblTargetPolice.Content = "";
            lblTargetPirates.Content = "";
            lblTargetDistance.Content = "";
            lblTargetOutOfRange.Visibility = Visibility.Collapsed;
            btnWarp.Visibility = Visibility.Collapsed;
            btnTrack.Visibility = Visibility.Collapsed;
         }
         else
         {
            StarSystem  system                  = game.WarpSystem;
            int               distance             = Functions.Distance(game.Commander.CurrentSystem, system);

            lblTargetName.Content = system.Name;
            lblTargetSize.Content = Strings.Sizes[(int)system.Size];
            lblTargetTech.Content = Strings.TechLevelNames[(int)system.TechLevel];
            lblTargetPolSys.Content = system.PoliticalSystem.Name;
            lblTargetResource.Content = system.Visited ? Strings.SpecialResources[(int)system.SpecialResource] :
                                                            Strings.Unknown;
            lblTargetPolice.Content = Strings.ActivityLevels[(int)system.PoliticalSystem.ActivityPolice];
            lblTargetPirates.Content = Strings.ActivityLevels[(int)system.PoliticalSystem.ActivityPirates];
            lblTargetDistance.Content = distance.ToString();
            lblTargetOutOfRange.Visibility = BoolToVisibility( !system.DestOk && system != game.Commander.CurrentSystem );
            btnWarp.Visibility = BoolToVisibility( system.DestOk );
            btnTrack.Visibility = BoolToVisibility( (lblTargetOutOfRange.Visibility == Visibility.Visible) && system != game.TrackedSystem );
         }
      }

      #region Event Handlers

      private void SpaceTrader_Closing( object sender, System.ComponentModel.CancelEventArgs e )
      {
         if ( game == null || game.Commander.Days == SaveGameDays ||
            FormAlert.Alert( AlertType.GameAbandonConfirm, myWin32Window ) == System.Windows.Forms.DialogResult.Yes )
         {
            // TODO:
            //if ( this.WindowState == FormWindowState.Normal )
            //{
            //   SetRegistrySetting( "X", this.Left.ToString() );
            //   SetRegistrySetting( "Y", this.Top.ToString() );
            //}
         }
         else
            e.Cancel = true;
      }

      private void SpaceTrader_Load( object sender, System.EventArgs e )
      {
         this.Left = int.Parse( GetRegistrySetting( "X", "0" ) );
         this.Top = int.Parse( GetRegistrySetting( "Y", "0" ) );

         FormAlert.Alert( AlertType.AppStart, myWin32Window );
      }

      private void btnBuySell_Click( object sender, System.EventArgs e )
      {
         string   name  = ((Button)sender).Name;
         bool     all      = name.IndexOf("Qty") < 0;
         int         index = int.Parse(name.Substring(name.Length - 1));

         if ( name.IndexOf( "Buy" ) < 0 )
            CargoSell( index, all );
         else
            CargoBuy( index, all );
      }

      private void btnBuyShip_Click( object sender, System.EventArgs e )
      {
         ( new FormShipList() ).ShowDialog( myWin32Window );
         UpdateAll();
      }

      private void btnDesign_Click( object sender, System.EventArgs e )
      {
         ( new Form_Shipyard() ).ShowDialog( myWin32Window );
         UpdateAll();
      }

      private void btnEquip_Click( object sender, System.EventArgs e )
      {
         ( new FormEquipment() ).ShowDialog( myWin32Window );
         UpdateAll();
      }

      private void btnFind_Click( object sender, System.EventArgs e )
      {
         FormFind form  = new FormFind();
         if ( form.ShowDialog( myWin32Window ) == System.Windows.Forms.DialogResult.OK )
         {
            Ship        ship     = game.Commander.Ship;

            string[] words    = form.SystemName.Split(' ');

            string      first    = words.Length > 0 ? words[0] : "";
            string      second   = words.Length > 1 ? words[1] : "";
            string      third    = words.Length > 2 ? words[2] : "";
            int            num1     = Functions.IsInt(second) ? int.Parse(second) : 0;
            int            num2     = Functions.IsInt(third) ? int.Parse(third) : 0;

            bool        find     = false;

            if ( game.CheatEnabled )
            {
               switch ( first )
               {
                  case "Bazaar":
                     game.ChanceOfTradeInOrbit = Math.Max( 0, Math.Min( 1000, num1 ) );
                     break;
                  case "Cover":
                     if ( num1 >= 0 && num1 < ship.Shields.Length && num2 >= 0 && num2 < Consts.Shields.Length )
                        ship.Shields[num1] = (Shield)Consts.Shields[num2].Clone();
                     break;
                  case "DeLorean":
                     game.Commander.Days = Math.Max( 0, num1 );
                     break;
                  case "Diamond":
                     ship.HullUpgraded = !ship.HullUpgraded;
                     break;
                  case "Energize":
                     game.CanSuperWarp = !game.CanSuperWarp;
                     break;
                  case "Events":
                     if ( second == "Reset" )
                        game.ResetVeryRareEncounters();
                     else
                     {
                        string   text  = "";
                        for ( IEnumerator list = game.VeryRareEncounters.GetEnumerator(); list.MoveNext(); )
                           text += Strings.VeryRareEncounters[(int)list.Current] + Environment.NewLine;
                        text = text.Trim();

                        FormAlert.Alert( AlertType.Alert, myWin32Window, "Remaining Very Rare Encounters", text );
                     }
                     break;
                  case "Fame":
                     game.Commander.ReputationScore = Math.Max( 0, num1 );
                     break;
                  case "Go":
                     game.SelectedSystemName = second;
                     if ( game.SelectedSystem.Name.ToLower() == second.ToLower() )
                     {
                        if ( game.AutoSave )
                           SaveGame( SAVE_DEPARTURE, false );

                        game.WarpDirect();

                        if ( game.AutoSave )
                           SaveGame( SAVE_ARRIVAL, false );
                     }
                     break;
                  case "Ice":
                     {
                        switch ( second )
                        {
                           case "Pirate":
                              game.Commander.KillsPirate = Math.Max( 0, num2 );
                              break;
                           case "Police":
                              game.Commander.KillsPolice = Math.Max( 0, num2 );
                              break;
                           case "Trader":
                              game.Commander.KillsTrader = Math.Max( 0, num2 );
                              break;
                        }
                     }
                     break;
                  case "Indemnity":
                     game.Commander.NoClaim = Math.Max( 0, num1 );
                     break;
                  case "IOU":
                     game.Commander.Debt = Math.Max( 0, num1 );
                     break;
                  case "Iron":
                     if ( num1 >= 0 && num1 < ship.Weapons.Length && num2 >= 0 && num2 < Consts.Weapons.Length )
                        ship.Weapons[num1] = (Weapon)Consts.Weapons[num2].Clone();
                     break;
                  case "Juice":
                     ship.Fuel = Math.Max( 0, Math.Min( ship.FuelTanks, num1 ) );
                     break;
                  case "Knack":
                     if ( num1 >= 0 && num1 < game.Mercenaries.Length )
                     {
                        string[] skills = third.Split(',');
                        for ( int i = 0; i < game.Mercenaries[num1].Skills.Length && i < skills.Length; i++ )
                        {
                           if ( Functions.IsInt( skills[i] ) )
                              game.Mercenaries[num1].Skills[i] = Math.Max( 1, Math.Min( Consts.MaxSkill, int.Parse( skills[i] ) ) );
                        }
                     }
                     break;
                  case "L'Engle":
                     game.FabricRipProbability = Math.Max( 0, Math.Min( Consts.FabricRipInitialProbability, num1 ) );
                     break;
                  case "LifeBoat":
                     ship.EscapePod = !ship.EscapePod;
                     break;
                  case "Monster.com":
                     ( new FormMonster() ).ShowDialog( myWin32Window );
                     break;
                  case "PlanB":
                     game.AutoSave = true;
                     break;
                  case "Posse":
                     if ( num1 > 0 && num1 < ship.Crew.Length && num2 > 0 && num2 < game.Mercenaries.Length &&
                        !Consts.SpecialCrewMemberIds.Contains( (CrewMemberId)num2 ) )
                     {
                        int   skill = ship.Trader;
                        ship.Crew[num1] = game.Mercenaries[num2];
                        if ( ship.Trader != skill )
                           game.RecalculateBuyPrices( game.Commander.CurrentSystem );
                     }
                     break;
                  case "RapSheet":
                     game.Commander.PoliceRecordScore = num1;
                     break;
                  case "Rarity":
                     game.ChanceOfVeryRareEncounter = Math.Max( 0, Math.Min( 1000, num1 ) );
                     break;
                  case "Scratch":
                     game.Commander.Cash = Math.Max( 0, num1 );
                     break;
                  case "Skin":
                     ship.Hull = Math.Max( 0, Math.Min( ship.HullStrength, num1 ) );
                     break;
                  case "Status":
                     {
                        switch ( second )
                        {
                           case "Artifact":
                              game.QuestStatusArtifact = Math.Max( 0, num2 );
                              break;
                           case "Dragonfly":
                              game.QuestStatusDragonfly = Math.Max( 0, num2 );
                              break;
                           case "Experiment":
                              game.QuestStatusExperiment = Math.Max( 0, num2 );
                              break;
                           case "Gemulon":
                              game.QuestStatusGemulon = Math.Max( 0, num2 );
                              break;
                           case "Japori":
                              game.QuestStatusJapori = Math.Max( 0, num2 );
                              break;
                           case "Jarek":
                              game.QuestStatusJarek = Math.Max( 0, num2 );
                              break;
                           case "Moon":
                              game.QuestStatusMoon = Math.Max( 0, num2 );
                              break;
                           case "Reactor":
                              game.QuestStatusReactor = Math.Max( 0, num2 );
                              break;
                           case "Princess":
                              game.QuestStatusPrincess = Math.Max( 0, num2 );
                              break;
                           case "Scarab":
                              game.QuestStatusScarab = Math.Max( 0, num2 );
                              break;
                           case "Sculpture":
                              game.QuestStatusSculpture = Math.Max( 0, num2 );
                              break;
                           case "SpaceMonster":
                              game.QuestStatusSpaceMonster = Math.Max( 0, num2 );
                              break;
                           case "Wild":
                              game.QuestStatusWild = Math.Max( 0, num2 );
                              break;
                           default:
                              string   text  = "Artifact: " + game.QuestStatusArtifact.ToString() + Environment.NewLine +
                                                   "Dragonfly: " + game.QuestStatusDragonfly.ToString() + Environment.NewLine +
                                                   "Experiment: " + game.QuestStatusExperiment.ToString() + Environment.NewLine +
                                                   "Gemulon: " + game.QuestStatusGemulon.ToString() + Environment.NewLine +
                                                   "Japori: " + game.QuestStatusJapori.ToString() + Environment.NewLine +
                                                   "Jarek: " + game.QuestStatusJarek.ToString() + Environment.NewLine +
                                                   "Moon: " + game.QuestStatusMoon.ToString() + Environment.NewLine +
                                                   "Princess: " + game.QuestStatusPrincess.ToString() + Environment.NewLine +
                                                   "Reactor: " + game.QuestStatusReactor.ToString() + Environment.NewLine +
                                                   "Scarab: " + game.QuestStatusScarab.ToString() + Environment.NewLine +
                                                   "Sculpture: " + game.QuestStatusSculpture.ToString() + Environment.NewLine +
                                                   "SpaceMonster: " + game.QuestStatusSpaceMonster.ToString() + Environment.NewLine +
                                                   "Wild: " + game.QuestStatusWild.ToString();

                              FormAlert.Alert( AlertType.Alert, myWin32Window, "Status of Quests", text );
                              break;
                        }
                     }
                     break;
                  case "Swag":
                     if ( num1 >= 0 && num1 < ship.Cargo.Length )
                        ship.Cargo[num1] = Math.Max( 0, Math.Min( ship.FreeCargoBays + ship.Cargo[num1], num2 ) );
                     break;
                  case "Test":
                     ( new FormTest() ).ShowDialog( myWin32Window );
                     break;
                  case "Tool":
                     if ( num1 >= 0 && num1 < ship.Gadgets.Length && num2 >= 0 && num2 < Consts.Gadgets.Length )
                        ship.Gadgets[num1] = (Gadget)Consts.Gadgets[num2].Clone();
                     break;
                  case "Varmints":
                     ship.Tribbles = Math.Max( 0, num1 );
                     break;
                  case "Yellow":
                     game.EasyEncounters = true;
                     break;
                  default:
                     find = true;
                     break;
               }
            }
            else
            {
               switch ( first )
               {
                  case "Cheetah":
                     FormAlert.Alert( AlertType.Cheater, myWin32Window );
                     game.CheatEnabled = true;
                     break;
                  default:
                     find = true;
                     break;
               }
            }

            if ( find )
            {
               game.SelectedSystemName = form.SystemName;
               if ( form.TrackSystem && game.SelectedSystem.Name.ToLower() == form.SystemName.ToLower() )
                  game.TrackedSystemId = game.SelectedSystemId;
            }

            UpdateAll();
         }
      }

      private void btnFuel_Click( object sender, System.EventArgs e )
      {
         FormBuyFuel form  = new FormBuyFuel();
         if ( form.ShowDialog( myWin32Window ) == System.Windows.Forms.DialogResult.OK )
         {
            int   toAdd = form.Amount / game.Commander.Ship.FuelCost;
            game.Commander.Ship.Fuel += toAdd;
            game.Commander.Cash -= toAdd * game.Commander.Ship.FuelCost;
            UpdateAll();
         }
      }

      private void btnJump_Click( object sender, System.EventArgs e )
      {
         if ( game.WarpSystem == null )
            FormAlert.Alert( AlertType.ChartJumpNoSystemSelected, myWin32Window );
         else if ( game.WarpSystem == game.Commander.CurrentSystem )
            FormAlert.Alert( AlertType.ChartJumpCurrent, myWin32Window );
         else if ( FormAlert.Alert( AlertType.ChartJump, myWin32Window, game.WarpSystem.Name ) == System.Windows.Forms.DialogResult.Yes )
         {
            game.CanSuperWarp = false;
            try
            {
               if ( game.AutoSave )
                  SaveGame( SAVE_DEPARTURE, false );

               game.Warp( true );

               if ( game.AutoSave )
                  SaveGame( SAVE_ARRIVAL, false );
            }
            catch ( GameEndException )
            {
               GameEnd();
            }
            UpdateAll();
         }
      }

      private void btnMerc_Click( object sender, System.EventArgs e )
      {
         ( new FormViewPersonnel() ).ShowDialog( myWin32Window );
         UpdateAll();
      }

      private void btnNews_Click( object sender, System.EventArgs e )
      {
         game.ShowNewspaper();
      }

      private void btnNextSystem_Click( object sender, System.EventArgs e )
      {
         game.SelectNextSystemWithinRange( true );
         UpdateAll();
      }

      private void btnPod_Click( object sender, System.EventArgs e )
      {
         if ( FormAlert.Alert( AlertType.EquipmentEscapePod, myWin32Window ) == System.Windows.Forms.DialogResult.Yes )
         {
            game.Commander.Cash -= 2000;
            game.Commander.Ship.EscapePod = true;
            UpdateAll();
         }
      }

      private void btnPrevSystem_Click( object sender, System.EventArgs e )
      {
         game.SelectNextSystemWithinRange( false );
         UpdateAll();
      }

      private void btnRepair_Click( object sender, System.EventArgs e )
      {
         FormBuyRepairs form  = new FormBuyRepairs();
         if ( form.ShowDialog( myWin32Window ) == System.Windows.Forms.DialogResult.OK )
         {
            int   toAdd = form.Amount / game.Commander.Ship.RepairCost;
            game.Commander.Ship.Hull += toAdd;
            game.Commander.Cash -= toAdd * game.Commander.Ship.RepairCost;
            UpdateAll();
         }
      }

      private void btnSpecial_Click( object sender, System.EventArgs e )
      {
         SpecialEvent   specEvent   = game.Commander.CurrentSystem.SpecialEvent;
         string            btn1, btn2;
         System.Windows.Forms.DialogResult   res1, res2;

         if ( specEvent.MessageOnly )
         {
            btn1 = "Ok";
            btn2 = null;
            res1 = System.Windows.Forms.DialogResult.OK;
            res2 = System.Windows.Forms.DialogResult.None;
         }
         else
         {
            btn1 = "Yes";
            btn2 = "No";
            res1 = System.Windows.Forms.DialogResult.Yes;
            res2 = System.Windows.Forms.DialogResult.No;
         }

         FormAlert   alert = new FormAlert(specEvent.Title, specEvent.String, btn1, res1, btn2, res2, null);
         if ( alert.ShowDialog() != System.Windows.Forms.DialogResult.No )
         {
            if ( game.Commander.CashToSpend < specEvent.Price )
               FormAlert.Alert( AlertType.SpecialIF, myWin32Window );
            else
            {
               try
               {
                  game.HandleSpecialEvent();
               }
               catch ( GameEndException )
               {
                  GameEnd();
               }
            }
         }

         UpdateAll();
      }

      private void btnTrack_Click( object sender, System.EventArgs e )
      {
         game.TrackedSystemId = game.SelectedSystemId;
         UpdateAll();
      }

      private void btnWarp_Click( object sender, System.EventArgs e )
      {
         try
         {
            if ( game.AutoSave )
               SaveGame( SAVE_DEPARTURE, false );

            game.Warp( false );

            if ( game.AutoSave )
               SaveGame( SAVE_ARRIVAL, false );
         }
         catch ( GameEndException )
         {
            GameEnd();
         }
         UpdateAll();
      }

      private void mnuGameExit_Click( object sender, System.EventArgs e )
      {
         Close();
      }

      private void mnuGameNew_Click( object sender, System.EventArgs e )
      {
         FormNewCommander  form  = new FormNewCommander();
         if ( ( game == null || game.Commander.Days == SaveGameDays ||
            FormAlert.Alert( AlertType.GameAbandonConfirm, myWin32Window ) == System.Windows.Forms.DialogResult.Yes ) &&
            form.ShowDialog( myWin32Window ) == System.Windows.Forms.DialogResult.OK )
         {
            game = new Game( form.CommanderName, form.Difficulty, form.Pilot, form.Fighter, form.Trader,
                                    form.Engineer, this );
            SaveGameFile = null;
            SaveGameDays = 0;

            SetInGameControlsEnabled( true );
            UpdateAll();

            if ( game.Options.NewsAutoShow )
               game.ShowNewspaper();
         }
      }

      private void mnuGameLoad_Click( object sender, System.EventArgs e )
      {
         if ( ( game == null || game.Commander.Days == SaveGameDays ||
            FormAlert.Alert( AlertType.GameAbandonConfirm, myWin32Window ) == System.Windows.Forms.DialogResult.Yes ) &&
            dlgOpen.ShowDialog( this ) == true )
         {
            LoadGame( dlgOpen.FileName );
         }
      }

      private void mnuGameSave_Click( object sender, System.EventArgs e )
      {
         if ( Game.CurrentGame != null )
         {
            if ( SaveGameFile != null )
               SaveGame( SaveGameFile, false );
            else
               mnuGameSaveAs_Click( sender, e );
         }
      }

      private void mnuGameSaveAs_Click( object sender, System.EventArgs e )
      {
         if ( Game.CurrentGame != null && dlgSave.ShowDialog( this ) == true )
         {
            SaveGame( dlgSave.FileName, true );
         }
      }

      private void mnuHelpAbout_Click( object sender, System.EventArgs e )
      {
         ( new FormAbout() ).ShowDialog( myWin32Window );
      }

      private void mnuHighScores_Click( object sender, System.EventArgs e )
      {
         ( new FormViewHighScores() ).ShowDialog( myWin32Window );
      }

      private void mnuOptions_Click( object sender, System.EventArgs e )
      {
         FormOptions form  = new FormOptions();
         if ( form.ShowDialog( myWin32Window ) == System.Windows.Forms.DialogResult.OK )
         {
            game.Options.CopyValues( form.Options );
            UpdateAll();
         }
      }

      private void mnuRetire_Click( object sender, System.EventArgs e )
      {
         if ( FormAlert.Alert( AlertType.GameRetire, myWin32Window ) == System.Windows.Forms.DialogResult.Yes )
         {
            game.EndStatus = GameEndType.Retired;
            GameEnd();
            UpdateAll();
         }
      }

      private void mnuViewBank_Click( object sender, System.EventArgs e )
      {
         ( new FormViewBank() ).ShowDialog( myWin32Window );
      }

      private void mnuViewCommander_Click( object sender, System.EventArgs e )
      {
         ( new FormViewCommander() ).ShowDialog( myWin32Window );
      }

      private void mnuViewPersonnel_Click( object sender, System.EventArgs e )
      {
         ( new FormViewPersonnel() ).ShowDialog( myWin32Window );
      }

      private void mnuViewQuests_Click( object sender, System.EventArgs e )
      {
         ( new FormViewQuests() ).ShowDialog( myWin32Window );
      }

      private void mnuViewShip_Click( object sender, System.EventArgs e )
      {
         ( new FormViewShip() ).ShowDialog( myWin32Window );
      }

      private void picGalacticChart_MouseDown_1( object sender, MouseButtonEventArgs e )
      {
         if ( e.LeftButton == MouseButtonState.Pressed && game != null )
         {
            StarSystem[]   universe       = game.Universe;

            bool              clickedSystem  = false;

            for ( int i = 0; i < universe.Length && !clickedSystem; i++ )
            {
               int   x  = universe[i].X + OFF_X;
               int   y  = universe[i].Y + OFF_Y;

               int mouseX = (int)e.GetPosition(picGalacticChart).X;
               int mouseY = (int)e.GetPosition(picGalacticChart).Y;

               if ( mouseX >= x - 2 &&
                  mouseX <= x + 2 &&
                  mouseY >= y - 2 &&
                  mouseY <= y + 2 )
               {
                  clickedSystem = true;
                  game.SelectedSystemId = (StarSystemId)i;
               }
               else if ( Functions.WormholeExists( i, -1 ) )
               {
                  int   xW = x + OFF_X_WORM;

                  if ( mouseX >= xW - 2 &&
                     mouseX <= xW + 2 &&
                     mouseY >= y - 2 &&
                     mouseY <= y + 2 )
                  {
                     clickedSystem = true;
                     game.SelectedSystemId = (StarSystemId)i;
                     game.TargetWormhole = true;
                  }
               }
            }

            if ( clickedSystem )
            {
               UpdateAll();
            }

         }
      }

      private void picGalacticChartRefresh()
      {
         //object sender, 
         //System.Windows.Forms.PaintEventArgs e = null;

         picGalacticChart.Children.Clear();
         Canvas c = picGalacticChart;

         if ( game != null )
         {
            StarSystem[]   universe = game.Universe;
            int[]             wormholes   = game.Wormholes;
            StarSystem     targetSys   = game.SelectedSystem;
            StarSystem     curSys      = game.Commander.CurrentSystem;
            int                  fuel        = game.Commander.Ship.Fuel;

            if ( fuel > 0 )
            {
               //e.Graphics.DrawEllipse( DEFAULT_PEN, curSys.X + OFF_X - fuel, curSys.Y + OFF_Y - fuel, fuel * 2, fuel * 2 );
               DrawEllipse( c, curSys.X + OFF_X - fuel, curSys.Y + OFF_Y - fuel, fuel * 2, fuel * 2 );
            }

            int index   = (int)game.SelectedSystemId;
            if ( game.TargetWormhole )
            {
               int               dest     = wormholes[(Array.IndexOf(wormholes, index) + 1) % wormholes.Length];
               StarSystem  destSys  = universe[dest];
               //e.Graphics.DrawLine( DEFAULT_PEN, targetSys.X + OFF_X_WORM + OFF_X, targetSys.Y + OFF_Y, destSys.X + OFF_X, destSys.Y + OFF_Y );
               DrawLine( c, targetSys.X + OFF_X_WORM + OFF_X, targetSys.Y + OFF_Y, destSys.X + OFF_X, destSys.Y + OFF_Y );
            }

            for ( int i = 0; i < universe.Length; i++ )
            {
               BitmapImage      imageIndex  = universe[i].Visited ? imgIMG_S_V : imgIMG_S_N;
               if ( universe[i] == game.WarpSystem )
               {
                  imageIndex = universe[i].Visited ? imgIMG_S_VS : imgIMG_S_NS;
               }

               if ( universe[i] == game.TrackedSystem )
               {
                  //e.Graphics.DrawLine( DEFAULT_PEN, (float)universe[i].X, (float)universe[i].Y, (float)( universe[i].X + image.Width - 1 ), (float)( universe[i].Y + image.Height - 1 ) );
                  //e.Graphics.DrawLine( DEFAULT_PEN, (float)universe[i].X, (float)( universe[i].Y + image.Height - 1 ), (float)( universe[i].X + image.Width - 1 ), (float)universe[i].Y );
                  DrawLine( c, (int)universe[i].X, (int)universe[i].Y, (int)( universe[i].X + imageIndex.Width - 1 ), (int)( universe[i].Y + imageIndex.Height - 1 ) );
                  DrawLine( c, (int)universe[i].X, (int)( universe[i].Y + imageIndex.Height - 1 ), (int)( universe[i].X + imageIndex.Width - 1 ), (int)universe[i].Y );
               }

               // TODO                ilChartImages.Draw( e.Graphics, universe[i].X, universe[i].Y, imageIndex );
               Draw( c, universe[i].X, universe[i].Y, imageIndex);

               if ( Functions.WormholeExists( i, -1 ) )
               {
                  // TODO ilChartImages.Draw( e.Graphics, universe[i].X + OFF_X_WORM, universe[i].Y, IMG_S_W );
                  Draw( c, universe[i].X + OFF_X_WORM, universe[i].Y, imgIMG_S_W );
               }
            }
         }
         else
         {
            // TODO e.Graphics.FillRectangle( DEFAULT_BRUSH, 0, 0, (float)picGalacticChart.Width, (float)picGalacticChart.Height );
         }
      }


      //private void picShortRangeChart_MouseDown( object sender, System.Windows.Forms.MouseEventArgs e )
      private void picShortRangeChart_MouseDown_1( object sender, MouseButtonEventArgs e )
      {
         if ( e.LeftButton == MouseButtonState.Pressed && game != null )
         {
            StarSystem[]   universe       = game.Universe;
            StarSystem     curSys            = game.Commander.CurrentSystem;

            bool              clickedSystem  = false;
            int                  centerX           = (int)(picShortRangeChart.Width / 2);
            int                  centerY           = (int)(picShortRangeChart.Height / 2);
            int                  delta             = (int)(picShortRangeChart.Height / (Consts.MaxRange * 2));

            for ( int i = 0; i < universe.Length && !clickedSystem; i++ )
            {
               if ( ( Math.Abs( universe[i].X - curSys.X ) * delta <= picShortRangeChart.Width / 2 - 10 ) &&
                  ( Math.Abs( universe[i].Y - curSys.Y ) * delta <= picShortRangeChart.Height / 2 - 10 ) )
               {
                  int   x  = centerX + (universe[i].X - curSys.X) * delta;
                  int   y  = centerY + (universe[i].Y - curSys.Y) * delta;

                  int mouseX = (int)e.GetPosition(picShortRangeChart).X;
                  int mouseY = (int)e.GetPosition(picShortRangeChart).Y;

                  if ( mouseX >= x - OFF_X &&
                     mouseX <= x + OFF_X &&
                     mouseY >= y - OFF_Y &&
                     mouseY <= y + OFF_Y )
                  {
                     clickedSystem = true;
                     game.SelectedSystemId = (StarSystemId)i;
                  }
                  else if ( Functions.WormholeExists( i, -1 ) )
                  {
                     int   xW = x + 9;

                     if ( mouseX >= xW - OFF_X &&
                        mouseX <= xW + OFF_X &&
                        mouseY >= y - OFF_Y &&
                        mouseY <= y + OFF_Y )
                     {
                        clickedSystem = true;
                        game.SelectedSystemId = (StarSystemId)i;
                        game.TargetWormhole = true;
                     }
                  }
               }
            }

            if ( clickedSystem )
            {
               UpdateAll();
            }
         }
      }

      private void DrawLine( Canvas c, int x1, int y1, int x2, int y2 )
      {
         Line l = new Line();
         l.Stroke = new SolidColorBrush( System.Windows.Media.Color.FromArgb( 255, 255, 255, 255 ) );
         l.StrokeThickness = 10;
         l.X1 = x1;
         l.Y1 = y1;
         l.X2 = x2;
         l.Y2 = y2;
         c.Children.Add( l );
      }

      private void DrawEllipse( Canvas c, int x, int y, int width, int height )
      {
         Ellipse e = new Ellipse();
         e.Stroke = new SolidColorBrush( System.Windows.Media.Color.FromArgb( 255, 255, 255, 255 ) );
         e.StrokeThickness = 10;
         e.Width = width;
         e.Height = height;
         c.Children.Add( e );
         Canvas.SetTop( e as UIElement, y );
         Canvas.SetLeft( e as UIElement, x );
      }

      private void DrawPolygon ( Canvas c, PointCollection points )
      {
         Polygon p = new Polygon();
         p.Stroke = new SolidColorBrush( System.Windows.Media.Color.FromArgb( 255, 255, 255, 255 ) );
         p.StrokeThickness = 10;
         p.Fill = Brushes.Crimson;
         p.Points = points;
         c.Children.Add( p );
      }

      private void DrawString( Canvas c, string text, int x, int y)
      {
         TextBlock textBlock = new TextBlock();
         textBlock.Text = text;
         textBlock.Foreground = new SolidColorBrush( Colors.Black );
         Canvas.SetLeft( textBlock, x );
         Canvas.SetTop( textBlock, y );
         c.Children.Add( textBlock );
         //var font = new System.Drawing.Font(this.FontFamily.Source, (float)this.FontSize );
      }

      private void Draw( Canvas c, int x, int y, BitmapImage image )
      {
         var i = new Image();
         i.Source = image;
         c.Children.Add( i );
         Canvas.SetTop( i as UIElement, y );
         Canvas.SetLeft( i as UIElement, x );
      }


      private void picShortRangeChartRefresh()
      {
         // object sender, 
         //System.Windows.Forms.PaintEventArgs e = null;
         picShortRangeChart.Children.Clear();
         Canvas c = picShortRangeChart;
         if ( game != null )
         {
            StarSystem[]   universe = game.Universe;
            int[]             wormholes   = game.Wormholes;
            StarSystem     trackSys = game.TrackedSystem;
            StarSystem     curSys      = game.Commander.CurrentSystem;
            int                  fuel        = game.Commander.Ship.Fuel;

            int                  centerX     = (int)(picShortRangeChart.Width / 2);
            int                  centerY     = (int)(picShortRangeChart.Height / 2);
            int                  delta       = (int)(picShortRangeChart.Height / (Consts.MaxRange * 2));


            DrawLine( c, centerX - 1, centerY - 1, centerX + 1, centerY + 1 );
            DrawLine( c, centerX - 1, centerY + 1, centerX + 1, centerY - 1 );

            //e.Graphics.DrawLine( DEFAULT_PEN, centerX - 1, centerY - 1, centerX + 1, centerY + 1 );
            //e.Graphics.DrawLine( DEFAULT_PEN, centerX - 1, centerY + 1, centerX + 1, centerY - 1 );
            if ( fuel > 0 )
            {
               //e.Graphics.DrawEllipse( DEFAULT_PEN, centerX - fuel * delta, centerY - fuel * delta, fuel * delta * 2, fuel * delta * 2 );
               DrawEllipse( c, centerX - fuel * delta, centerY - fuel * delta, fuel * delta * 2, fuel * delta * 2 );
            }

            if ( trackSys != null )
            {
               int   dist  = Functions.Distance(curSys, trackSys);
               if ( dist > 0 )
               {
                  int   dX = (int)Math.Round(25 * (trackSys.X - curSys.X) / (double)dist);
                  int   dY = (int)Math.Round(25 * (trackSys.Y - curSys.Y) / (double)dist);

                  int   dX2   = (int)Math.Round(4 * (trackSys.Y - curSys.Y) / (double)dist);
                  int   dY2   = (int)Math.Round(4 * (curSys.X - trackSys.X) / (double)dist);

                  //e.Graphics.FillPolygon( new System.Drawing.SolidBrush( System.Drawing.Color.Crimson ),
                  //   new System.Drawing.Point[] {
                  //      new System.Drawing.Point(centerX + dX, centerY + dY),
                  //      new System.Drawing.Point(centerX - dX2, centerY - dY2),
                  //      new System.Drawing.Point(centerX + dX2, centerY + dY2) } );
                  DrawPolygon( c, new PointCollection {
                     new Point(centerX + dX, centerY + dY),
                     new Point(centerX - dX2, centerY - dY2),
                     new Point(centerX + dX2, centerY + dY2) } );
               }

               if ( game.Options.ShowTrackedRange )
               {
                  //var font = new System.Drawing.Font(this.FontFamily.Source, (float)this.FontSize );
                  //e.Graphics.DrawString( Functions.StringVars( Strings.ChartDistance, Functions.Multiples( dist,
                  //   Strings.DistanceUnit ), trackSys.Name ), font, new System.Drawing.SolidBrush( System.Drawing.Color.Black ), 0,
                  //   (float)( picShortRangeChart.Height - 13 ) );
                  DrawString( c, Functions.StringVars( Strings.ChartDistance, Functions.Multiples( dist,
                     Strings.DistanceUnit ), trackSys.Name ), 0, (int)( picShortRangeChart.Height - 13 ) );
               }
            }

            // Two loops: first draw the names and then the systems. The names may
            // overlap and the systems may be drawn on the names, but at least every
            // system is visible.
            for ( int j = 0; j < 2; j++ )
            {
               for ( int i = 0; i < universe.Length; i++ )
               {
                  if ( ( Math.Abs( universe[i].X - curSys.X ) * delta <= picShortRangeChart.Width / 2 - 10 ) &&
                     ( Math.Abs( universe[i].Y - curSys.Y ) * delta <= picShortRangeChart.Height / 2 - 10 ) )
                  {
                     int   x  = centerX + (universe[i].X - curSys.X) * delta;
                     int   y  = centerY + (universe[i].Y - curSys.Y) * delta;

                     if ( j == 1 )
                     {
                        if ( universe[i] == game.WarpSystem )
                        {
                           //e.Graphics.DrawLine( DEFAULT_PEN, x - 6, y, x + 6, y );
                           //e.Graphics.DrawLine( DEFAULT_PEN, x, y - 6, x, y + 6 );
                           DrawLine( c, x - 6, y, x + 6, y );
                           DrawLine( c, x, y - 6, x, y + 6 );
                        }

                        if ( universe[i] == game.TrackedSystem )
                        {
                           //e.Graphics.DrawLine( DEFAULT_PEN, x - 5, y - 5, x + 5, y + 5 );
                           //e.Graphics.DrawLine( DEFAULT_PEN, x - 5, y + 5, x + 5, y - 5 );
                           DrawLine( c, x - 5, y - 5, x + 5, y + 5 );
                           DrawLine( c, x - 5, y + 5, x + 5, y - 5 );
                        }

                        // TODO ilChartImages.Draw( e.Graphics, x - OFF_X, y - OFF_Y, universe[i].Visited ? IMG_G_V : IMG_G_N );
                        Draw( c, x - OFF_X, y - OFF_Y, universe[i].Visited ? imgIMG_G_V : imgIMG_G_N );

                        if ( Functions.WormholeExists( i, -1 ) )
                        {
                           int   xW = x + 9;
                           if ( game.TargetWormhole && universe[i] == game.SelectedSystem )
                           {
                              //e.Graphics.DrawLine( DEFAULT_PEN, xW - 6, y, xW + 6, y );
                              //e.Graphics.DrawLine( DEFAULT_PEN, xW, y - 6, xW, y + 6 );
                              DrawLine( c, xW - 6, y, xW + 6, y );
                              DrawLine( c, xW, y - 6, xW, y + 6 );
                           }
                           // TODO ilChartImages.Draw( e.Graphics, xW - OFF_X, y - OFF_Y, IMG_G_W );
                           Draw( c, xW - OFF_X, y - OFF_Y, imgIMG_G_W );
                        }
                     }
                     else
                     {
                        //// TODO : Need to get font
                        //var fontFromWindow = new System.Drawing.Font( this.FontFamily.Source, (float)this.FontSize );
                        //System.Drawing.Font  font  = new System.Drawing.Font(this.FontFamily.Source, 7);
                        //System.Drawing.SizeF size  = e.Graphics.MeasureString(universe[i].Name, fontFromWindow);
                        //e.Graphics.DrawString( universe[i].Name, font, new System.Drawing.SolidBrush( System.Drawing.Color.Black ),
                        //   x - size.Width / 2 + OFF_X, y - size.Height );
                        DrawString( c, universe[i].Name, x - 20 + OFF_X, y - 24 );
                     }
                  }
               }
            }
         }
         else
         {
            // TODO e.Graphics.FillRectangle( DEFAULT_BRUSH, 0, 0, (float)picShortRangeChart.Width, (float)picShortRangeChart.Height );
         }
      }

      private void statusBar_PanelClick( object sender, System.Windows.Forms.StatusBarPanelClickEventArgs e )
      {
         if ( game != null )
         {
            // TODO: Click on Statusbar
            //if ( e.StatusBarPanel == this.statusBarPanelCash )
            //   mnuViewBank_Click( sender, e );
            //else if ( e.StatusBarPanel == this.statusBarPanelCosts )
            //   ( new FormCosts() ).ShowDialog( this );
         }
      }

      #endregion

      #region Properties

      public System.Drawing.Image[] CustomShipImages
      {
         get
         {
            System.Drawing.Image[]  images      = new System.Drawing.Image[Consts.ImagesPerShip];
            int         baseIndex   = (int)ShipType.Custom * Consts.ImagesPerShip;
            for ( int index = 0; index < Consts.ImagesPerShip; index++ )
               images[index] = ilShipImages.Images[baseIndex + index];

            return images;
         }
         set
         {
            System.Drawing.Image[]  images      = value;
            int         baseIndex   = (int)ShipType.Custom * Consts.ImagesPerShip;
            for ( int index = 0; index < Consts.ImagesPerShip; index++ )
               ilShipImages.Images[baseIndex + index] = images[index];
         }
      }

      public System.Windows.Forms.ImageList DirectionImages
      {
         get
         {
            return ilDirectionImages;
         }
      }

      public System.Windows.Forms.ImageList EquipmentImages
      {
         get
         {
            return ilEquipmentImages;
         }
      }

      public System.Windows.Forms.ImageList ShipImages
      {
         get
         {
            return ilShipImages;
         }
      }

      #endregion

   }
}
