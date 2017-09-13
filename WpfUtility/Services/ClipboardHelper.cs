using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace WpfUtility.Services
{
    /// <summary>
    ///     Helper class to get the values from the clipboard
    /// </summary>
    public static class ClipboardHelper
    {
        /// <summary>
        ///     Parses the clipboard data to a list with a string arrays
        ///     Works with CSV (";" separated) and "text" ("\t" separated)
        /// </summary>
        /// <returns>Clipboard data as list with string array</returns>
        public static List<string[]> ParseClipboardData()
        {
            var clipboardData = new List<string[]>();
            ParseFormat parseFormat = null;

            // Get the data and set the parsing method based on the format
            // Currently works with CSV and Text DataFormats
            var dataObj = Clipboard.GetDataObject();
            if (dataObj != null)
            {
                object clipboardRawData;
                if ((clipboardRawData = dataObj.GetData(DataFormats.CommaSeparatedValue)) != null)
                    parseFormat = ParseCsvFormat;
                else if ((clipboardRawData = dataObj.GetData(DataFormats.Text)) != null)
                    parseFormat = ParseTextFormat;

                if (parseFormat != null)
                {
                    var rawDataStr = clipboardRawData as string;

                    // When the data is not a string, try it with a memory stream
                    if (rawDataStr == null && clipboardRawData is MemoryStream)
                    {
                        var ms = clipboardRawData as MemoryStream;
                        var sr = new StreamReader(ms);
                        rawDataStr = sr.ReadToEnd();
                    }

                    var rows = rawDataStr?.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                    if (rows != null && rows.Length > 0)
                    {
                        clipboardData = new List<string[]>();
                        foreach (var row in rows)
                            clipboardData.Add(parseFormat(row));
                    }
                }
            }
            return clipboardData;
        }

        private static string[] ParseCsvFormat(string value)
        {
            return ParseCsvOrTextFormat(value, true);
        }

        private static string[] ParseTextFormat(string value)
        {
            return ParseCsvOrTextFormat(value, false);
        }

        /// <summary>
        ///     Parses the given data to a string array
        /// </summary>
        /// <param name="value">Value which should be parsed</param>
        /// <param name="isCsv">If it is CSV or "text"</param>
        /// <returns>String array</returns>
        private static string[] ParseCsvOrTextFormat(string value, bool isCsv)
        {
            var outputList = new List<string>();

            // CSV just with semicolon and text with a tab stop
            var separator = isCsv ? ';' : '\t';
            var startIndex = 0;
            var endIndex = 0;

            for (var i = 0; i < value.Length; i++)
            {
                var ch = value[i];
                if (ch == separator)
                {
                    outputList.Add(value.Substring(startIndex, endIndex - startIndex));

                    startIndex = endIndex + 1;
                    endIndex = startIndex;
                }
                else if (ch == '\"' && isCsv)
                {
                    // Skip until the ending quotes
                    i++;
                    if (i >= value.Length)
                        throw new FormatException($"Value: \"{value}\" had a format exception!");
                    var tempCh = value[i];
                    while (tempCh != '\"' && i < value.Length)
                        i++;

                    endIndex = i;
                }
                else if (i + 1 == value.Length)
                {
                    // Add the last value
                    outputList.Add(value.Substring(startIndex));
                    break;
                }
                else
                {
                    endIndex++;
                }
            }

            return outputList.ToArray();
        }

        /// <summary>
        ///     Delegate for the format
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>String array</returns>
        private delegate string[] ParseFormat(string value);
    }
}