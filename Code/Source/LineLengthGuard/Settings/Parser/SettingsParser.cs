using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LineLengthGuard.Settings.Parser
{
    internal sealed class SettingsParser : ISettingsParser
    {
        public ISettings? Parse(string settingsJSON)
        {
            string normalizedJSON = RemoveBlankSpaces(settingsJSON).Replace(Environment.NewLine, string.Empty);

            if (normalizedJSON[0] != '{' || normalizedJSON[normalizedJSON.Length - 1] != '}')
            {
                return null;
            }

            normalizedJSON = normalizedJSON.Substring(1, normalizedJSON.Length - "{}".Length);

            string[] parts = normalizedJSON.Split(',');

            string[] correctlySplittedParts = GetCorrectlySplittedPartsTakingCollectionsIntoAccount(parts);

            return GetFileSettingsFromParts(correctlySplittedParts);
        }

        private static string RemoveBlankSpaces(string json)
        {
            bool openedQuotes = false;

            StringBuilder stringBuilder = new StringBuilder();

            char previousCharacter = ' ';

            foreach (char character in json)
            {
                if (character == '"' && previousCharacter != '\\')
                {
                    openedQuotes = !openedQuotes;

                    stringBuilder.Append(character);

                    continue;
                }

                if (character == ' ' && !openedQuotes)
                {
                    continue;
                }

                stringBuilder.Append(character);

                previousCharacter = character;
            }

            return stringBuilder.ToString();
        }

        private static string[] GetCorrectlySplittedPartsTakingCollectionsIntoAccount(string[] parts)
        {
            Stack<string> correctlySplittedParts = [];

            bool openedSquareBracket = false;

            foreach (string currentPart in parts)
            {
                if (openedSquareBracket)
                {
                    if (currentPart.Contains("]"))
                    {
                        openedSquareBracket = false;
                    }

                    string removedPart = correctlySplittedParts.Pop();

                    correctlySplittedParts.Push($"{removedPart},{currentPart}");

                    continue;
                }

                if (currentPart.Contains("[") && !currentPart.Contains("]"))
                {
                    openedSquareBracket = true;
                }

                correctlySplittedParts.Push(currentPart);
            }

            return [.. correctlySplittedParts];
        }

        private static FileSettings? GetFileSettingsFromParts(string[] parts)
        {
            bool? allowLongMethodNamesWithUnderscores = null;
            List<string>? excludedLineStarts = [];
            int? maximumLineLength = null;

            foreach (string part in parts)
            {
                string[] subparts = part.Split(':');

                if (subparts.Length != 2)
                {
                    return null;
                }

                string key = subparts[0];

                if (!key.StartsWith("\"", StringComparison.Ordinal) || !key.EndsWith("\"", StringComparison.Ordinal))
                {
                    return null;
                }

                key = key.Substring(1, key.Length - "\"\"".Length);
                string stringValue = subparts[1];

                switch (key)
                {
                    case "AllowLongMethodNamesWithUnderscores":
                        bool boolParsed = bool.TryParse(stringValue, out bool boolValue);

                        if (!boolParsed)
                        {
                            return null;
                        }

                        allowLongMethodNamesWithUnderscores = boolValue;
                        break;

                    case "ExcludedLineStarts":
                        excludedLineStarts = ParseExcludedLineStarts(stringValue);
                        break;

                    case "MaximumLineLength":
                        bool intParsed = int.TryParse(
                            stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out int intValue);

                        if (!intParsed)
                        {
                            return null;
                        }

                        maximumLineLength = intValue;
                        break;

                    default:
                        return null;
                }
            }

            return GetFileSettingsFromParsedValues(
                allowLongMethodNamesWithUnderscores, excludedLineStarts, maximumLineLength);
        }

        private static List<string>? ParseExcludedLineStarts(string stringValue)
        {
            List<string>? excludedLineStarts = null;

            if (!stringValue.StartsWith("[", StringComparison.Ordinal)
                || !stringValue.EndsWith("]", StringComparison.Ordinal))
            {
                return null;
            }

            stringValue = stringValue.Substring(1, stringValue.Length - "[]".Length);

            string[] values = stringValue.Split(',');

            if (values.Length > 0)
            {
                excludedLineStarts = [];

                foreach (string currentValue in values)
                {
                    if (!currentValue.StartsWith("\"", StringComparison.Ordinal)
                        || !currentValue.EndsWith("\"", StringComparison.Ordinal))
                    {
                        return null;
                    }

                    string currentValueToAdd = currentValue
                        .Substring(1, currentValue.Length - "\"\"".Length)
                        .Replace("\\\"", "\"");

                    excludedLineStarts.Add(currentValueToAdd);
                }
            }

            return excludedLineStarts;
        }

        private static FileSettings GetFileSettingsFromParsedValues(
            bool? allowLongMethodNamesWithUnderscores, List<string>? excludedLineStarts, int? maximumLineLength)
        {
            FileSettings fileSettings = new FileSettings();

            if (allowLongMethodNamesWithUnderscores is not null)
            {
                fileSettings.AllowLongMethodNamesWithUnderscores = allowLongMethodNamesWithUnderscores.Value;
            }

            if (excludedLineStarts is not null && excludedLineStarts.Count > 0)
            {
                fileSettings.ExcludedLineStarts = excludedLineStarts;
            }

            if (maximumLineLength is not null)
            {
                fileSettings.MaximumLineLength = maximumLineLength.Value;
            }

            return fileSettings;
        }
    }
}