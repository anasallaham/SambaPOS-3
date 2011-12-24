﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Samba.Domain.Models.Menus;
using Samba.Domain.Models.Settings;
using Samba.Infrastructure.Data;
using Samba.Persistance.Data;
using Samba.Presentation.Common.Services;
using Samba.Services;

namespace Samba.Modules.SettingsModule.ServiceImplementations
{
    [Export(typeof(ISettingService))]
    class SettingService : AbstractService, ISettingService
    {
        private static IWorkspace _workspace;
        public static IWorkspace Workspace
        {
            get { return _workspace ?? (_workspace = WorkspaceFactory.Create()); }
            set { _workspace = value; }
        }

        private static IEnumerable<Terminal> _terminals;
        public static IEnumerable<Terminal> Terminals { get { return _terminals ?? (_terminals = Workspace.All<Terminal>()); } }

        private IEnumerable<TaxTemplate> _taxTemplates;
        public IEnumerable<TaxTemplate> TaxTemplates
        {
            get { return _taxTemplates ?? (_taxTemplates = Dao.Query<TaxTemplate>()); }
        }

        private IEnumerable<ServiceTemplate> _serviceTemplates;
        public IEnumerable<ServiceTemplate> ServiceTemplates
        {
            get { return _serviceTemplates ?? (_serviceTemplates = Dao.Query<ServiceTemplate>()); }
        }

        public ServiceTemplate GetServiceTemplateById(int id)
        {
            return ServiceTemplates.FirstOrDefault(x => x.Id == id);
        }

        public ServiceTemplate GetServiceTemplateByName(string name)
        {
            return ServiceTemplates.FirstOrDefault(y => y.Name == name);
        }

        public TaxTemplate GetTaxTemplateById(int id)
        {
            return TaxTemplates.FirstOrDefault(x => x.Id == id);
        }

        public TaxTemplate GetTaxTemplateByName(string name)
        {
            return TaxTemplates.FirstOrDefault(y => y.Name == name);
        }

        public Terminal GetTerminalByName(string name)
        {
            return Terminals.SingleOrDefault(x => x.Name == name);
        }

        public Terminal GetDefaultTerminal()
        {
            return Terminals.SingleOrDefault(x => x.IsDefault);
        }

        public IEnumerable<string> GetTerminalNames()
        {
            return Terminals.Select(x => x.Name);
        }

        public override void Reset()
        {
            _taxTemplates = null;
            _serviceTemplates = null;
            _terminals = null;
            Workspace = WorkspaceFactory.Create();
        }
    }
}
