﻿using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace NeXt.Vdf {
    /// <summary>
    ///     Class that handles serialization of VdfValue objects into Vdf formatted strings
    /// </summary>
    public class VdfSerializer {
        private const string Newline          = "\n";
        private const string ValueDelimiter   = "\"";
        private const string CommentDelimiter = "//";
        private const string TableOpen        = "{";
        private const string TableClose       = "}";
        private const string KvSeperator      = "\t\t";


        private readonly VdfValue _root;
        private          int      _indentLevel;

        /// <summary>
        ///     Creates a VdfSerializer object
        /// </summary>
        /// <param name="value">the VdfValue to serialize</param>
        public VdfSerializer(VdfValue value) {
            _root = value;
        }

        private string IndentString => "\t".Repeat(_indentLevel);

        private string EscapeString(string v) {
            return v.Replace("\\", @"\\").Replace("\t", @"\t").Replace("\n", @"\n").Replace("\r", @"\r").Replace("\"", @"\""");
        }

        private void WriteString(Action<string> step, string text, bool escape) {
            step(ValueDelimiter);
            if (escape)
                step(EscapeString(text));
            else
                step(text);
            step(ValueDelimiter);
        }

        private void RunSerialization(Action<string> onStep, VdfValue current) {
            if (current.Comments.Count > 0)
                foreach (string s in current.Comments) {
                    onStep(IndentString);
                    onStep(CommentDelimiter);
                    onStep(EscapeString(s));
                    onStep(Newline);
                }

            onStep(IndentString);
            WriteString(onStep, current.Name, true);
            switch (current.Type) {
                case VdfValueType.String: {
                    onStep(KvSeperator);
                    WriteString(onStep, (current as VdfString).Content, true);

                    onStep(Newline);
                    break;
                }
                case VdfValueType.Long: {
                    onStep(KvSeperator);
                    WriteString(onStep, (current as VdfLong).Content.ToString(CultureInfo.InvariantCulture), false);

                    onStep(Newline);
                    break;
                }
                case VdfValueType.Decimal: {
                    onStep(KvSeperator);
                    WriteString(onStep, (current as VdfDecimal).Content.ToString(CultureInfo.InvariantCulture), false);
                    onStep(Newline);
                    break;
                }
                case VdfValueType.Table: {
                    onStep(Newline);
                    onStep(IndentString);
                    onStep(TableOpen);
                    onStep(Newline);
                    _indentLevel++;
                    foreach (var v in current as VdfTable) RunSerialization(onStep, v);
                    _indentLevel--;
                    onStep(IndentString);
                    onStep(TableClose);
                    onStep(Newline);
                    break;
                }
            }
        }

        /// <summary>
        ///     Serialize the object to string
        /// </summary>
        /// <returns>a string representing the VdfValue</returns>
        public string Serialize() {
            var sb = new StringBuilder();
            RunSerialization(s => sb.Append(s), _root);

            return sb.ToString();
        }

        /// <summary>
        ///     Serialize the object to a file
        /// </summary>
        /// <param name="filePath">full path to the file to serialize into</param>
        public void Serialize(string filePath) {
            Serialize(filePath, Encoding.Unicode);
        }

        /// <summary>
        ///     Serialize the object to a file using the given encoding
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        public void Serialize(string filePath, Encoding encoding) {
            using (var writer = new StreamWriter(filePath, false, encoding)) {
                RunSerialization(writer.Write, _root);
                writer.Flush();
            }
        }
    }
}