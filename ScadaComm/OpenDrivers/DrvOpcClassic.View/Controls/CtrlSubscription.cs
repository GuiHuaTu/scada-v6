// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Drivers.DrvOpcClassic.Config;
using Scada.Forms;
using System.ComponentModel;

namespace Scada.Comm.Drivers.DrvOpcClassic.View.Controls
{
    /// <summary>
    /// Represetns a control for editing a subscription.
    /// <para>Представляет элемент управления для редактирования подписки.</para>
    /// </summary>
    public partial class CtrlSubscription : UserControl
    {
        private SubscriptionConfig subscriptionConfig;


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CtrlSubscription()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Gets or sets the edited subscription.
        /// </summary>
        internal SubscriptionConfig SubscriptionConfig
        {
            get
            {
                return subscriptionConfig;
            }
            set
            {
                subscriptionConfig = null;
                ShowSubscriptionProps(value);
                subscriptionConfig = value;
            }
        }


        /// <summary>
        /// Shows the subscription properties.
        /// </summary>
        private void ShowSubscriptionProps(SubscriptionConfig subscriptionConfig)
        {
            if (subscriptionConfig != null)
            {
                chkActive.Checked = subscriptionConfig.Active;
                txtDisplayName.Text = subscriptionConfig.DisplayName;
                numUpdateRate.SetValue(subscriptionConfig.UpdateRate);
                numKeepAlive.SetValue(subscriptionConfig.KeepAlive);
                numDeadband.SetValue(Convert.ToDecimal(subscriptionConfig.Deadband));
                chkReadSync.Checked = subscriptionConfig.ReadSync;
            }
        }

        /// <summary>
        /// Raises an ObjectChanged event.
        /// </summary>
        private void OnObjectChanged(object changeArgument)
        {
            ObjectChanged?.Invoke(this, new ObjectChangedEventArgs(subscriptionConfig, changeArgument));
        }

        /// <summary>
        /// Sets the input focus.
        /// </summary>
        public void SetFocus()
        {
            txtDisplayName.Select();
        }


        /// <summary>
        /// Occurs when the edited object changes.
        /// </summary>
        [Category("Property Changed")]
        public event EventHandler<ObjectChangedEventArgs> ObjectChanged;


        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (subscriptionConfig != null)
            {
                subscriptionConfig.Active = chkActive.Checked;
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void txtDisplayName_TextChanged(object sender, EventArgs e)
        {
            if (subscriptionConfig != null)
            {
                subscriptionConfig.DisplayName = txtDisplayName.Text;
                OnObjectChanged(TreeUpdateTypes.CurrentNode);
            }
        }

        private void numUpdateRate_ValueChanged(object sender, EventArgs e)
        {
            if (subscriptionConfig != null)
            {
                subscriptionConfig.UpdateRate = Convert.ToInt32(numUpdateRate.Value);
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void numKeepAlive_ValueChanged(object sender, EventArgs e)
        {
            if (subscriptionConfig != null)
            {
                subscriptionConfig.KeepAlive = Convert.ToInt32(numKeepAlive.Value);
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void numDeadband_ValueChanged(object sender, EventArgs e)
        {
            if (subscriptionConfig != null)
            {
                subscriptionConfig.Deadband = Convert.ToDouble(numDeadband.Value);
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void chkReadSync_CheckedChanged(object sender, EventArgs e)
        {
            if (subscriptionConfig != null)
            {
                subscriptionConfig.ReadSync = chkReadSync.Checked;
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }
    }
}
