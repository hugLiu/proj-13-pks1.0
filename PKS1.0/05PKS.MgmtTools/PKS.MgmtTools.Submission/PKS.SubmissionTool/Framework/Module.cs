﻿//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add Business Module" recipe.
//
// The Module class derives from the CAB base class ModuleInit and it will be instantiated
// when the module is loaded by the CAB infrastructure.
//
// 
//
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

//using Jurassic.AppCenter.AppServices;
using System.Linq;
using Infragistics.Practices.CompositeUI.WinForms;
using Infragistics.Win.UltraWinToolbars;
using Jurassic.AppCenter.SmartClient.Infrastructure.Interface;
using Jurassic.AppCenter.SmartClient.Infrastructure.Interface.Constants;
using Jurassic.AppCenter.SmartClient.Infrastructure.Shell;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using PKS.SubmissionTool.Index;
using PKS.Utils;
using PKS.WebAPI.Services;

namespace PKS.SubmissionTool
{
    public class Module : ModuleInit
    {
        private WorkItem _rootWorkItem;

        [InjectionConstructor]
        public Module([ServiceDependency] WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
        }


        public override void Load()
        {
            //Test();
            base.Load();
            new ModuleBootstrapper().Initialize();
            ModuleBootstrapper.Kernel.Rebind<IApiServiceConfig>().ToConstant(new ModuleApiServiceConfig());
            _rootWorkItem.WorkItems
                .AddNew<ControlledWorkItem<ModuleController>>()
                .Controller
                .Run();
        }
        private void Test()
        {
            var excelFile = @"C:\Users\Administrator\Documents\Tencent Files\4112630\FileRecv\科研项目统计表.xlsx";
            var options = new Index.ExcelTableOptions();
            options.TitleRow = 1;
            options.BeginColumnRow = 2;
            options.EndColumnRow = 2;
            ExcelBuilder.BuildTable(excelFile, options);
        }
    }
}