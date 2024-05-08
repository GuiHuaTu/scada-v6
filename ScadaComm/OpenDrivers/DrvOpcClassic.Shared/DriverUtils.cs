// Copyright (c) Rapid Software LLC. All rights reserved.

using Scada.Comm.Drivers.DrvOpcClassic.Config;

namespace Scada.Comm.Drivers.DrvOpcClassic
{
    /// <summary>
    /// The class provides helper methods for the driver.
    /// <para>Класс, предоставляющий вспомогательные методы для драйвера.</para>
    /// </summary>
    internal static class DriverUtils
    {
        /// <summary>
        /// The driver code.
        /// </summary>
        public const string DriverCode = "DrvOpcClassic";
        
        /// <summary>
        /// The known type names.
        /// </summary>
        public static readonly string[] TypeNames = new string[]
        {
            "System.Boolean",
            "System.Byte",
            "System.DateTime",
            "System.Decimal",
            "System.Double",
            "System.Int16",
            "System.Int32",
            "System.Int64",
            "System.SByte",
            "System.Single",
            "System.String",
            "System.UInt16",
            "System.UInt32",
            "System.UInt64"
        };


        /// <summary>
        /// Checks if the specified data type name matches the type.
        /// </summary>
        public static bool DataTypeEquals(string dataTypeName, Type type)
        {
            return type != null && string.Equals(dataTypeName, type.FullName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Converts the OPC specification to a string.
        /// </summary>
        public static string ToString(this Spec spec, bool isRussian)
        {
            return spec switch
            {
                Spec.None => isRussian ? "Нет" : "None",
                Spec.DA10 => "Data Access 1.0a",
                Spec.DA20 => "Data Access 2.XX",
                Spec.DA30 => "Data Access 3.00",
                Spec.AE10 => "Alarms and Events 1.XX",
                _ => spec.ToString()
            };
        }

        /// <summary>
        /// Disposes the OPC server, hiding possible exception.
        /// </summary>
        public static void SafeDispose(this Opc.Server server)
        {
            if (server != null)
            {
                try { server.Dispose(); } 
                catch { }
            }
        }

        /// <summary>
        /// Determines whether the element is a variable. 
        /// </summary>
        public static bool IsVariable(this Opc.Da.BrowseElement browseElement)
        {
            return browseElement.IsItem && !browseElement.HasChildren;
        }
    }
}
