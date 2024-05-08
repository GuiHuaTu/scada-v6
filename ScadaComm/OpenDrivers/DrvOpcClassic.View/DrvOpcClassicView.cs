// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Forms;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvOpcClassic.View
{
    /// <summary>
    /// Implements the driver user interface.
    /// <para>Реализует пользовательский интерфейс драйвера.</para>
    /// </summary>
    public class DrvOpcClassicView : DriverView
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DrvOpcClassicView()
            : base()
        {
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
                    "Драйвер OPC Classic" :
                    "OPC Classic Driver";
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
                    "Взаимодействует с контроллерами по спецификациям OPC DA и OPC AE.\n" +
                    "Команды ТУ определяются конфигурацией устройства." :

                    "Interacts with controllers according to the OPC DA and OPC AE specifications.\n" +
                    "Commands are defined by device configuration.";
            }
        }


        /// <summary>
        /// Loads language dictionaries.
        /// </summary>
        public override void LoadDictionaries()
        {
            if (!Locale.LoadDictionaries(AppDirs.LangDir, DriverUtils.DriverCode, out string errMsg))
                ScadaUiUtils.ShowError(errMsg);

            DriverPhrases.Init();
        }

        /// <summary>
        /// Creates a new device user interface.
        /// </summary>
        public override DeviceView CreateDeviceView(LineConfig lineConfig, DeviceConfig deviceConfig)
        {
            return new DevOpcClassicView(this, lineConfig, deviceConfig);
        }
    }
}
