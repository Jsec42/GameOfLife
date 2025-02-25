﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameOfLifeSimulator.Properties;

namespace GameOfLifeSimulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SettingsWindow settings;
        private void Form1_Load(object sender, EventArgs e)
        {
            settings = new SettingsWindow();
            settings.Show();
            settings.RestartRequested += SettingsOnRestartRequested;
            settings.SettingChanged += Settings_SettingChanged;
            settings.RefreshRequested += Settings_RefreshRequested;
            settings.StepRateChanged += Settings_StepRateChanged;
            Reset();
        }

        private void Settings_RefreshRequested(object sender, EventArgs e)
        {
            Simulator.StepAll();
        }

        private void Settings_StepRateChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)(1000/settings.Speed);
        }
        private void Settings_SettingChanged(object sender, EventArgs e)
        {
            Simulator.ChangeSettings(settings.ShowNew,
                settings.ShowOld,
                settings.ChangeChance,
                settings.OldAge,
                settings.Threshold_lower,
                settings.Threshold_upper,
                settings.Threshold_lower_spawn,
                settings.Threshold_upper_spawn,
                settings.TwoLayer,
                settings.DeathAge);
        }

        private void SettingsOnRestartRequested(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {

            timer1.Stop();
            timer1.Interval = (int)(1000/settings.Speed);
            Simulator.Tick = 0;
            Simulator.Randomize(settings.SpawnChance,settings.BoardSize);
            Settings_SettingChanged(this,EventArgs.Empty);
            pictureBox1.Image = Simulator.RenderBoard();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Text = $"Step-{Simulator.Tick} LastUpdate-{Simulator.LastUpdate}";
            Simulator.Step();
            pictureBox1.Image = Simulator.RenderBoard();

        }

      
    }
}
