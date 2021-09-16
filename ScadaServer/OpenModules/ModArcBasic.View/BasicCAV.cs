﻿// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Server.Archives;
using Scada.Server.Config;
using Scada.Server.Modules.ModArcBasic.View.Forms;
using System.Windows.Forms;

namespace Scada.Server.Modules.ModArcBasic.View
{
    /// <summary>
    /// Implements the current data archive user interface.
    /// <para>Реализует пользовательский интерфейс архива текущих данных.</para>
    /// </summary>
    public class BasicCAV : ArchiveView
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public BasicCAV(ModuleView parentView, ArchiveConfig archiveConfig)
            : base(parentView, archiveConfig)
        {
            CanShowProperties = true;
        }

        /// <summary>
        /// Shows a modal dialog box for editing archive properties.
        /// </summary>
        public override bool ShowProperties()
        {
            return new FrmCAO(ArchiveConfig).ShowDialog() == DialogResult.OK;
        }
    }
}
