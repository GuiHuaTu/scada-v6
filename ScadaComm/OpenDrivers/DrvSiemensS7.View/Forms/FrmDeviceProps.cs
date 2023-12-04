// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using S7.Net;
using Scada.Comm.Config;
using Scada.Comm.Drivers.DrvSiemensS7.Protocol;
using Scada.Forms;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvSiemensS7.View.Forms
{
    /// <summary>
    /// Represents a form for configuring device and communication line properties.
    /// <para>Представляет форму для настройки свойств устройства и линии связи.</para>
    /// </summary>
    public partial class FrmDeviceProps : Form
    {
        private readonly AppDirs appDirs;           // the application directories
        private readonly LineConfig lineConfig;     // the communication line configuration
        private readonly DeviceConfig deviceConfig; // the device configuration
        private readonly CustomUi customUi;         // the UI customization object


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        private FrmDeviceProps()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmDeviceProps(AppDirs appDirs, LineConfig lineConfig, DeviceConfig deviceConfig, CustomUi customUi)
            : this()
        {
            this.appDirs = appDirs ?? throw new ArgumentNullException(nameof(appDirs));
            this.lineConfig = lineConfig ?? throw new ArgumentNullException(nameof(lineConfig));
            this.deviceConfig = deviceConfig ?? throw new ArgumentNullException(nameof(deviceConfig));
            this.customUi = customUi ?? throw new ArgumentNullException(nameof(customUi));
        }


        /// <summary>
        /// Sets the controls according to the configuration.
        /// </summary>
        private void ConfigToControls()
        {

            string[] dataTypeArr = Enum.GetNames(typeof(CpuType));
            foreach (var item in dataTypeArr)
            {
                cbCpuType.Items.Add(item);
            }

            cbCpuType.SelectedItem = lineConfig.CustomOptions.GetValueAsEnum("CpuType", CpuType.S71200).ToString();
            txt_Plc_IP.Text = lineConfig.CustomOptions.GetValueAsString("PlcIP");
            txt_Plc_Rack.Text = lineConfig.CustomOptions.GetValueAsString("PlcRack");
            txt_Plc_Slot.Text = lineConfig.CustomOptions.GetValueAsString("PlcSlot");
            txtTemplateFileName.Text = deviceConfig.PollingOptions.CmdLine;
        }

        /// <summary>
        /// Sets the configuration according to the controls.
        /// </summary>
        private void ControlsToConfig()
        {
            try
            {
                CpuType cpuType = (CpuType)Enum.Parse(typeof(CpuType), cbCpuType.SelectedItem.ToString());
 
                lineConfig.CustomOptions["CpuType"] = Convert.ToInt32(cpuType).ToString();

                lineConfig.CustomOptions["PlcIP"] = txt_Plc_IP.Text.ToString();
                lineConfig.CustomOptions["PlcRack"] = (Convert.ToInt16(txt_Plc_Rack.Text)).ToString();
                lineConfig.CustomOptions["PlcSlot"] = (Convert.ToInt16(txt_Plc_Slot.Text)).ToString();
                deviceConfig.PollingOptions.CmdLine = txtTemplateFileName.Text;
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message + ex.StackTrace);
            }  
        }

        /// <summary>
        /// Validates the form controls.
        /// </summary>
        private bool ValidateControls()
        {
            if (!File.Exists(GetTemplatePath()))
            {
                ScadaUiUtils.ShowError(DriverPhrases.TemplateNotExists);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates the path of the device template file.
        /// </summary>
        private bool ValidateTemplatePath(string fileName, out string shortFileName)
        {
            if (fileName.StartsWith(appDirs.ConfigDir))
            {
                shortFileName = fileName[appDirs.ConfigDir.Length..];
                return true;
            }
            else
            {
                ScadaUiUtils.ShowError(DriverPhrases.ConfigDirRequired, appDirs.ConfigDir);
                shortFileName = "";
                return false;
            }
        }

        /// <summary>
        /// Gets the file path of the device template.
        /// </summary>
        private string GetTemplatePath()
        {
            return Path.Combine(appDirs.ConfigDir, txtTemplateFileName.Text);
        }

        /// <summary>
        /// Shows a form for editing the device template.
        /// </summary>
        private void EditDeviceTemplate(string fileName = "")
        {
            FrmDeviceTemplate frmDeviceTemplate = new(appDirs, customUi)
            {
                FileName = fileName
            };

            frmDeviceTemplate.ShowDialog();
            fileName = frmDeviceTemplate.FileName;

            if (string.IsNullOrEmpty(fileName))
                txtTemplateFileName.Text = "";
            else if (ValidateTemplatePath(fileName, out string shortFileName))
                txtTemplateFileName.Text = shortFileName;
        }


        private void FrmDevProps_Load(object sender, EventArgs e)
        {
            FormTranslator.Translate(this, GetType().FullName);
            openFileDialog.SetFilter(CommonPhrases.XmlFileFilter);

            Text = string.Format(Text, deviceConfig.DeviceNum);
            ConfigToControls();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // show dialog to select template file
            openFileDialog.InitialDirectory = appDirs.ConfigDir;
            openFileDialog.FileName = "";

            if (openFileDialog.ShowDialog() == DialogResult.OK &&
                ValidateTemplatePath(openFileDialog.FileName, out string shortFileName))
            {
                txtTemplateFileName.Text = shortFileName;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTemplateFileName.Text))
                EditDeviceTemplate();
            else
                EditDeviceTemplate(GetTemplatePath());
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateControls())
            {
                ControlsToConfig();
                DialogResult = DialogResult.OK;
            }
        }
    }
}
