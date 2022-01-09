﻿using CardanoSharp.Wallet;
using CardanoSharp.Wallet.Enums;
using CardanoSharp.Wallet.Extensions.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectTalon.App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTalon.App.ViewModel
{
    public interface IGenerateMnemonicViewModel
    {
        void GenerateMnemonic();
        void HandleValidSubmit();
        void RecordedConfirmed();

        public WalletCreationRequest WalletCreationForm { get; set; }
        bool ConsentToSeeMnumonic { get; set; }
        bool DoneSeeingMnumonic { get; set; }
        string Mnemonic { get; set; }
        EditContext? EditContext { get; set; }

    }
    public class GenerateMnemonicViewModel : IGenerateMnemonicViewModel
    {
        private Services.IWalletService _walletService;

        private NavigationManager _uriHelper;
        private ValidationMessageStore? messageStore;

        public EditContext? EditContext { get; set; }
        public WalletCreationRequest WalletCreationForm { get; set; }
        public bool ConsentToSeeMnumonic { get; set; } = false;
        public bool DoneSeeingMnumonic { get; set; } = false;
        public string Mnemonic { get; set; }

        public GenerateMnemonicViewModel(Services.IWalletService walletService, NavigationManager uriHelper)
        {
            _walletService = walletService;
            _uriHelper = uriHelper;

            WalletCreationForm = new WalletCreationRequest();
            EditContext = new(WalletCreationForm);
            EditContext.OnValidationRequested += HandleValidationRequested;
            messageStore = new(EditContext);
        }

        public void GenerateMnemonic()
        {
            Mnemonic = new MnemonicService().Generate(24).Words;
            ConsentToSeeMnumonic = true;
        }

        private void HandleValidationRequested(object? sender,
        ValidationRequestedEventArgs args)
        {
            messageStore?.Clear();

            if (WalletCreationForm.SpendingPassword != WalletCreationForm.ConfirmSpendingPassword)
            {
                messageStore?.Add(() => WalletCreationForm.ConfirmSpendingPassword, "Spending passwords do not match.");
            }
        }

        public async void HandleValidSubmit()
        {
            await _walletService.AddWallet(WalletCreationForm.Name, WalletCreationForm.RecoveryPhrase, WalletCreationForm.SpendingPassword);
            _uriHelper.NavigateTo("wallet/dashboard");
        }

        public void RecordedConfirmed()
        {
            DoneSeeingMnumonic = true;
        }
    }
}
