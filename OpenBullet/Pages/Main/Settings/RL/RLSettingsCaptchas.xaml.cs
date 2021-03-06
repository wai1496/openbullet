﻿using RuriLib;
using RuriLib.CaptchaServices;
using System;
using System.Windows;
using System.Windows.Controls;

namespace OpenBullet
{
    /// <summary>
    /// Logica di interazione per OBSettingsCaptchas.xaml
    /// </summary>
    public partial class RLSettingsCaptchas : Page
    {
        public RLSettingsCaptchas()
        {
            InitializeComponent();
            DataContext = Globals.rlSettings.Captchas;

            foreach (string i in Enum.GetNames(typeof(BlockCaptcha.CaptchaService)))
                currentServiceCombobox.Items.Add(i);

            currentServiceCombobox.SelectedIndex = (int)Globals.rlSettings.Captchas.CurrentService;
        }

        private void currentServiceCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Globals.rlSettings.Captchas.CurrentService = (BlockCaptcha.CaptchaService)currentServiceCombobox.SelectedIndex;
        }

        private void checkBalanceButton_Click(object sender, RoutedEventArgs e)
        {
            // Save
            IOManager.SaveSettings(Globals.rlSettingsFile, Globals.rlSettings);

            double balance = 0;

            try
            {
                switch (Globals.rlSettings.Captchas.CurrentService)
                {
                    case BlockCaptcha.CaptchaService.AntiCaptcha:
                        balance = new AntiCaptcha(Globals.rlSettings.Captchas.AntiCapToken, Globals.rlSettings.Captchas.Timeout).GetBalance();
                        break;

                    case BlockCaptcha.CaptchaService.DBC:
                        balance = new DeathByCaptcha(Globals.rlSettings.Captchas.DBCUser, Globals.rlSettings.Captchas.DBCPass, Globals.rlSettings.Captchas.Timeout).GetBalance();
                        break;

                    case BlockCaptcha.CaptchaService.DeCaptcher:
                        balance = new DeCaptcher(Globals.rlSettings.Captchas.DCUser, Globals.rlSettings.Captchas.DCPass, Globals.rlSettings.Captchas.Timeout).GetBalance();
                        break;

                    case BlockCaptcha.CaptchaService.ImageTypers:
                        balance = new ImageTyperz(Globals.rlSettings.Captchas.ImageTypToken, Globals.rlSettings.Captchas.Timeout).GetBalance();
                        break;

                    case BlockCaptcha.CaptchaService.TwoCaptcha:
                        balance = new TwoCaptcha(Globals.rlSettings.Captchas.TwoCapToken, Globals.rlSettings.Captchas.Timeout).GetBalance();
                        break;

                    case BlockCaptcha.CaptchaService.RuCaptcha:
                        balance = new RuCaptcha(Globals.rlSettings.Captchas.RuCapToken, Globals.rlSettings.Captchas.Timeout).GetBalance();
                        break;

                    case BlockCaptcha.CaptchaService.AZCaptcha:
                        balance = new AZCaptcha(Globals.rlSettings.Captchas.AZCapToken, Globals.rlSettings.Captchas.Timeout).GetBalance();
                        break;

                    case BlockCaptcha.CaptchaService.SolveRecaptcha:
                        balance = new SolveReCaptcha(Globals.rlSettings.Captchas.SRUserId, Globals.rlSettings.Captchas.SRToken, Globals.rlSettings.Captchas.Timeout).GetBalance();
                        break;

                    case BlockCaptcha.CaptchaService.CaptchasIO:
                        balance = new CaptchasIO(Globals.rlSettings.Captchas.CIOToken, Globals.rlSettings.Captchas.Timeout).GetBalance();
                        break;

                    default:
                        balance = 999;
                        break;
                }
            }
            catch { balanceLabel.Content = "WRONG TOKEN / CREDENTIALS"; balanceLabel.Foreground = Globals.GetBrush("ForegroundBad"); return; }
            
            balanceLabel.Content = balance;
            balanceLabel.Foreground = balance > 0 ? Globals.GetBrush("ForegroundGood") : Globals.GetBrush("ForegroundBad");
        }
    }
}
