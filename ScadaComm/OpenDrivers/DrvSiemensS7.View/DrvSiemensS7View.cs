﻿// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvSiemensS7.View.Forms;
using Scada.Forms;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvSiemensS7.View
{
    /// <summary>
    /// Implements the driver user interface.
    /// <para>Реализует пользовательский интерфейс драйвера.</para>
    /// </summary>
    public class DrvSiemensS7View : DriverView
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DrvSiemensS7View()
             : base()
        {
            CanShowProperties = true;
            CanCreateDevice = true;
        }


        /// <summary>
        /// Gets the driver name.
        /// </summary>
        public override string Name
        {
            get
            {
                return Locale.IsRussian ? 
                    "Драйвер SiemensS7" : 
                    "SiemensS7 Driver";
            }
        }

        /// <summary>
        /// Gets the driver description.
        /// </summary>
        public override string Descr
        {
            get
            {
                return Locale.IsRussian ?
                    "Взаимодействует с контроллерами по протоколу SiemensS7.\n\n" +
                    "Пользовательский параметр линии связи:\n" +
                    "CpuType - режим передачи данных (RTU, ASCII, TCP).\n\n" +
                    "Параметр командной строки:\n" +
                    "имя файла шаблона устройства.\n\n" +
                    "Команды ТУ:\n" +
                    "определяются шаблоном устройства." :

                    "Interacts with controllers via SiemensS7 protocol.\n\n" +
                    "Custom communication line parameter:\n" +
                    "CpuType - data transmission mode (RTU, ASCII, TCP).\n\n" +
                    "Command line parameter:\n" +
                    "device template file name.\n\n" +
                    "Commands:\n" +
                    "defined by device template.";
            }
        }


        /// <summary>
        /// Gets a UI customization object.
        /// </summary>
        protected virtual CustomUi GetCustomUi()
        {
            return new CustomUi();
        }

        /// <summary>
        /// Loads language dictionaries.
        /// </summary>
        public override void LoadDictionaries()
        {
            if (!Locale.LoadDictionaries(AppDirs.LangDir, "DrvSiemensS7", out string errMsg))
                ScadaUiUtils.ShowError(errMsg);

            DriverPhrases.Init();
        }

        /// <summary>
        /// Shows a modal dialog box for editing driver properties.
        /// </summary>
        public override bool ShowProperties()
        {
            new FrmDeviceTemplate(AppDirs, GetCustomUi()).ShowDialog();
            return false;
        }

        /// <summary>
        /// Creates a new device user interface.
        /// </summary>
        public override DeviceView CreateDeviceView(LineConfig lineConfig, DeviceConfig deviceConfig)
        {
            return new DevSiemensS7View(this, lineConfig, deviceConfig, GetCustomUi());
        }
    }
}
