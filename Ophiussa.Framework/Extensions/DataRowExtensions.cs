using System;
using System.Data;

namespace OphiussaFramework.Extensions { 
    public static class DataRowExtensions {
        #region "By column Name"

        /// <summary>
        ///     Gets the string.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>String Type</returns>
        public static string GetString(this DataRow dr, string column) {
            return IsDBNull(dr, column) ? string.Empty : dr[column].ToString();
        }

        /// <summary>
        ///     Gets the date time.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>DateTime Type</returns>
        public static DateTime GetDateTime(this DataRow dr, string column) {
            DateTime value;
            DateTime.TryParse(dr.GetString(column), out value);
            return value;
        }

        /// <summary>
        ///     Gets the decimal.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>Decimal Type</returns>
        public static decimal GetDecimal(this DataRow dr, string column) {
            decimal value;
            decimal.TryParse(dr.GetString(column), out value);
            return IsDBNull(dr, column) ? 0 : value;
        }

        /// <summary>
        ///     Gets the boolean.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>Boolean Type</returns>
        public static bool GetBoolean(this DataRow dr, string column) {
            bool value;
            bool.TryParse(dr.GetString(column), out value);
            return IsDBNull(dr, column) ? false : value;
        }

        /// <summary>
        ///     Gets the byte.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>Byte Value</returns>
        public static byte GetByte(this DataRow dr, string column) {
            byte value;
            byte.TryParse(dr.GetString(column), out value);
            return value;
        }

        /// <summary>
        ///     Gets the char.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>Char Value</returns>
        public static char GetChar(this DataRow dr, string column) {
            char value;
            char.TryParse(dr.GetString(column), out value);
            return value;
        }

        /// <summary>
        ///     Gets the double.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>Double Value</returns>
        public static double GetDouble(this DataRow dr, string column) {
            double value;
            double.TryParse(dr.GetString(column), out value);
            return IsDBNull(dr, column) ? 0 : value;
        }

        /// <summary>
        ///     Gets the float.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>Double Value</returns>
        public static float GetFloat(this DataRow dr, string column) {
            float value;
            float.TryParse(dr.GetString(column), out value);
            return IsDBNull(dr, column) ? 0 : value;
        }

        /// <summary>
        ///     Gets the int16.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>short Value</returns>
        public static short GetInt16(this DataRow dr, string column) {
            short value;
            short.TryParse(dr.GetString(column), out value);
            return IsDBNull(dr, column) ? Convert.ToInt16(0) : value;
        }

        /// <summary>
        ///     Gets the int32.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>int Value</returns>
        public static int GetInt32(this DataRow dr, string column) {
            int value;
            int.TryParse(dr.GetString(column), out value);
            return IsDBNull(dr, column) ? 0 : value;
        }

        /// <summary>
        ///     Gets the int64.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>long Value</returns>
        public static long GetInt64(this DataRow dr, string column) {
            long value;
            long.TryParse(dr.GetString(column), out value);
            return IsDBNull(dr, column) ? 0 : value;
        }

        /// <summary>
        ///     Gets the S byte.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>sbyte Value</returns>
        public static sbyte GetSByte(this DataRow dr, string column) {
            sbyte value;
            sbyte.TryParse(dr.GetString(column), out value);
            return value;
        }

        /// <summary>
        ///     Gets the single.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>float Value</returns>
        public static float GetSingle(this DataRow dr, string column) {
            return IsDBNull(dr, column) ? 0 : Convert.ToSingle(dr[column]);
        }

        /// <summary>
        ///     Gets the U int16.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>ushort Value</returns>
        public static ushort GetUInt16(this DataRow dr, string column) {
            ushort value;
            ushort.TryParse(dr.GetString(column), out value);
            return IsDBNull(dr, column) ? Convert.ToUInt16(0) : value;
        }

        /// <summary>
        ///     Gets the U int32.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>uint Value</returns>
        public static uint GetUInt32(this DataRow dr, string column) {
            uint value;
            uint.TryParse(dr.GetString(column), out value);
            return IsDBNull(dr, column) ? 0 : value;
        }

        /// <summary>
        ///     Gets the U int64.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>ulong Value</returns>
        public static ulong GetUInt64(this DataRow dr, string column) {
            ulong value;
            ulong.TryParse(dr.GetString(column), out value);
            return IsDBNull(dr, column) ? 0 : value;
        }

        /// <summary>
        ///     Determines whether [is DB null] [the specified dr].
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="column">The column.</param>
        /// <returns>
        ///     <c>true</c> if [is DB null] [the specified dr]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDBNull(this DataRow dr, string column) {
            return Convert.IsDBNull(dr[column]);
        }

        #endregion "By column Name"

        #region "By index"

        /// <summary>
        ///     Gets the string.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>string Value</returns>
        public static string GetString(this DataRow dr, int columnIndex) {
            return IsDBNull(dr, columnIndex) ? string.Empty : dr[columnIndex].ToString();
        }

        /// <summary>
        ///     Gets the date time.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>datetime Value</returns>
        public static DateTime GetDateTime(this DataRow dr, int columnIndex) {
            DateTime value;
            DateTime.TryParse(dr.GetString(columnIndex), out value);
            return value;
        }

        /// <summary>
        ///     Gets the decimal.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>decimal Value</returns>
        public static decimal GetDecimal(this DataRow dr, int columnIndex) {
            decimal value;
            decimal.TryParse(dr.GetString(columnIndex), out value);
            return IsDBNull(dr, columnIndex) ? 0 : value;
        }

        /// <summary>
        ///     Gets the boolean.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>bool Value</returns>
        public static bool GetBoolean(this DataRow dr, int columnIndex) {
            bool value;
            bool.TryParse(dr.GetString(columnIndex), out value);
            return value;
        }

        /// <summary>
        ///     Gets the byte.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>byte Value</returns>
        public static byte GetByte(this DataRow dr, int columnIndex) {
            byte value;
            byte.TryParse(dr.GetString(columnIndex), out value);
            return value;
        }

        /// <summary>
        ///     Gets the char.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>char Value</returns>
        public static char GetChar(this DataRow dr, int columnIndex) {
            char value;
            char.TryParse(dr.GetString(columnIndex), out value);
            return value;
        }

        /// <summary>
        ///     Gets the double.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>double Value</returns>
        public static double GetDouble(this DataRow dr, int columnIndex) {
            double value;
            double.TryParse(dr.GetString(columnIndex), out value);
            return IsDBNull(dr, columnIndex) ? 0 : value;
        }

        /// <summary>
        ///     Gets the int16.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>short Value</returns>
        public static short GetInt16(this DataRow dr, int columnIndex) {
            short value;
            short.TryParse(dr.GetString(columnIndex), out value);
            return IsDBNull(dr, columnIndex) ? Convert.ToInt16(0) : value;
        }

        /// <summary>
        ///     Gets the int32.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>int Value</returns>
        public static int GetInt32(this DataRow dr, int columnIndex) {
            int value;
            int.TryParse(dr.GetString(columnIndex), out value);
            return IsDBNull(dr, columnIndex) ? 0 : value;
        }

        /// <summary>
        ///     Gets the int64.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>long Value</returns>
        public static long GetInt64(this DataRow dr, int columnIndex) {
            long value;
            long.TryParse(dr.GetString(columnIndex), out value);
            return IsDBNull(dr, columnIndex) ? 0 : value;
        }

        /// <summary>
        ///     Gets the S byte.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>sbyte Value</returns>
        public static sbyte GetSByte(this DataRow dr, int columnIndex) {
            sbyte value;
            sbyte.TryParse(dr.GetString(columnIndex), out value);
            return value;
        }

        /// <summary>
        ///     Gets the single.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>float Value</returns>
        public static float GetSingle(this DataRow dr, int columnIndex) {
            float value;
            float.TryParse(dr.GetString(columnIndex), out value);
            return IsDBNull(dr, columnIndex) ? 0 : value;
        }

        /// <summary>
        ///     Gets the U int16.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>ushort Value</returns>
        public static ushort GetUInt16(this DataRow dr, int columnIndex) {
            ushort value;
            ushort.TryParse(dr.GetString(columnIndex), out value);
            return IsDBNull(dr, columnIndex) ? Convert.ToUInt16(0) : value;
        }

        /// <summary>
        ///     Gets the U int32.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>uint Value</returns>
        public static uint GetUInt32(this DataRow dr, int columnIndex) {
            uint value;
            uint.TryParse(dr.GetString(columnIndex), out value);
            return IsDBNull(dr, columnIndex) ? 0 : value;
        }

        /// <summary>
        ///     Gets the U int64.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>ulong Value</returns>
        public static ulong GetUInt64(this DataRow dr, int columnIndex) {
            ulong value;
            ulong.TryParse(dr.GetString(columnIndex), out value);
            return IsDBNull(dr, columnIndex) ? 0 : value;
        }

        /// <summary>
        ///     Determines whether [is DB null] [the specified dr].
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>
        ///     <c>true</c> if [is DB null] [the specified dr]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDBNull(this DataRow dr, int columnIndex) {
            return Convert.IsDBNull(dr[columnIndex]);
        }

        #endregion "By index"
    }
}