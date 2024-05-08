// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Drivers.DrvOpcClassic.Config;
using Scada.Forms;
using System.ComponentModel;

namespace Scada.Comm.Drivers.DrvOpcClassic.View.Controls
{
    /// <summary>
    /// Represetns a control for editing an event subscription.
    /// <para>Представляет элемент управления для редактирования подписки на события.</para>
    /// </summary>
    public partial class CtrlEventSubscription : UserControl
    {
        private EventSubscriptionConfig subscriptionConfig;


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CtrlEventSubscription()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Gets or sets the edited subscription.
        /// </summary>
        internal EventSubscriptionConfig SubscriptionConfig
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
        private void ShowSubscriptionProps(EventSubscriptionConfig subscriptionConfig)
        {
            if (subscriptionConfig != null)
            {
                chkActive.Checked = subscriptionConfig.Active;
                txtDisplayName.Text = subscriptionConfig.DisplayName;
                numUpdateRate.SetValue(subscriptionConfig.UpdateRate);
                numKeepAlive.SetValue(subscriptionConfig.KeepAlive);
                numMaxSize.SetValue(subscriptionConfig.MaxSize);
                chkSimpleEvents.Checked = subscriptionConfig.SimpleEvents;
                chkTrackingEvents.Checked = subscriptionConfig.TrackingEvents;
                chkConditionEvents.Checked = subscriptionConfig.ConditionEvents;
                numHighSeverity.SetValue(subscriptionConfig.HighSeverity);
                numLowSeverity.SetValue(subscriptionConfig.LowSeverity);
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

        private void numMaxSize_ValueChanged(object sender, EventArgs e)
        {
            if (subscriptionConfig != null)
            {
                subscriptionConfig.MaxSize = Convert.ToInt32(numMaxSize.Value);
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void chkSimpleEvents_CheckedChanged(object sender, EventArgs e)
        {
            if (subscriptionConfig != null)
            {
                subscriptionConfig.SimpleEvents = chkSimpleEvents.Checked;
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void chkTrackingEvents_CheckedChanged(object sender, EventArgs e)
        {
            if (subscriptionConfig != null)
            {
                subscriptionConfig.TrackingEvents = chkTrackingEvents.Checked;
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void chkConditionEvents_CheckedChanged(object sender, EventArgs e)
        {
            if (subscriptionConfig != null)
            {
                subscriptionConfig.ConditionEvents = chkConditionEvents.Checked;
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void numHighSeverity_ValueChanged(object sender, EventArgs e)
        {
            if (subscriptionConfig != null)
            {
                subscriptionConfig.HighSeverity = Convert.ToInt32(numHighSeverity.Value);
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }

        private void numLowSeverity_ValueChanged(object sender, EventArgs e)
        {
            if (subscriptionConfig != null)
            {
                subscriptionConfig.LowSeverity = Convert.ToInt32(numLowSeverity.Value);
                OnObjectChanged(TreeUpdateTypes.None);
            }
        }
    }
}
