using CertificateVault.Models.ADOModels;
using CertificateVault.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CertificateVault.App_Start
{
    public class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Certificate, CertificateVM>();
                config.CreateMap<CertificateVM, Certificate>();
                config.CreateMap<Vault, VaultVM>();
                config.CreateMap<VaultVM, Vault>();
                config.CreateMap<Client,ClientVM>();
                config.CreateMap<ClientVM,Client>();
                config.CreateMap<usp_GetReport_Result, usp_GetSymbols_ResultVM>();
                config.CreateMap<usp_GetSymbols_ResultVM, usp_GetReport_Result>();
            });
        }
    }
}