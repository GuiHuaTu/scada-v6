// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Drivers.DrvOpcClassic.Config;

namespace Scada.Comm.Drivers.DrvOpcClassic.View.Controls
{
    /// <summary>
    /// Represents a control for editing a command.
    /// <para>Представляет элемент управления для редактирования команды.</para>
    /// </summary>
    public partial class CtrlEventCategory : UserControl
    {
        private EventCategoryConfig eventCategoryConfig;


        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CtrlEventCategory()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the edited command.
        /// </summary>
        internal EventCategoryConfig EventCategoryConfig
        {
            get
            {
                return eventCategoryConfig;
            }
            set
            {
                eventCategoryConfig = value;

                if (eventCategoryConfig != null)
                {
                    txtName.Text = eventCategoryConfig.Name;
                    txtID.Text = eventCategoryConfig.ID.ToString();
                }
            }
        }
    }
}
