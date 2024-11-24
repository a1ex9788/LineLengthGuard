using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LineLengthGuard.Settings.Parser
{
    // This class does not use any library for parsing JSON because it is so difficult to pack libraries in .NET
    // analysers. It was not possible to make it work when building with MSBuild a project that consumes the analyser.
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
            bool? allowLongStringDefinitions = null;
            List<string>? excludedLineStarts = [];
            int? maximumLineLength = null;

            foreach (string part in parts)
            {
                Configuration? configuration = ParseConfiguration(part);

                if (configuration is null)
                {
                    return null;
                }

                switch (configuration.Key)
                {
                    case "AllowLongMethodNamesWithUnderscores":
                        bool boolParsed = bool
                            .TryParse(configuration.StringValue, out bool boolValue);

                        if (!boolParsed)
                        {
                            return null;
                        }

                        allowLongMethodNamesWithUnderscores = boolValue;
                        break;

                    case "AllowLongStringDefinitions":
                        boolParsed = bool.TryParse(configuration.StringValue, out boolValue);

                        if (!boolParsed)
                        {
                            return null;
                        }

                        allowLongStringDefinitions = boolValue;
                        break;

                    case "ExcludedLineStarts":
                        excludedLineStarts = ParseExcludedLineStarts(configuration.StringValue);
                        break;

                    case "MaximumLineLength":
                        bool intParsed = int.TryParse(
                            configuration.StringValue,
                            NumberStyles.Integer,
                            CultureInfo.InvariantCulture,
                            out int intValue);

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
                allowLongMethodNamesWithUnderscores,
                allowLongStringDefinitions,
                excludedLineStarts,
                maximumLineLength);
        }

        private static Configuration? ParseConfiguration(string part)
        {
            int colonIndex = part.IndexOf(':');

            if (colonIndex == -1 || colonIndex == part.Length - 1)
            {
                return null;
            }

            string key = part.Substring(0, colonIndex);
            string stringValue = part.Substring(colonIndex + 1);

            if (!key.StartsWith("\"", StringComparison.Ordinal) || !key.EndsWith("\"", StringComparison.Ordinal))
            {
                return null;
            }

            key = key.Substring(1, key.Length - "\"\"".Length);

            return new Configuration(key, stringValue);
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
            bool? allowLongMethodNamesWithUnderscores,
            bool? allowLongStringDefinitions,
            List<string>? excludedLineStarts,
            int? maximumLineLength)
        {
            FileSettings fileSettings = new FileSettings();

            if (allowLongMethodNamesWithUnderscores is not null)
            {
                fileSettings.AllowLongMethodNamesWithUnderscores = allowLongMethodNamesWithUnderscores.Value;
            }

            if (allowLongStringDefinitions is not null)
            {
                fileSettings.AllowLongStringDefinitions = allowLongStringDefinitions.Value;
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

        private sealed class Configuration
        {
            public Configuration(string key, string stringValue)
            {
                this.Key = key;
                this.StringValue = stringValue;
            }

            public string Key { get; }

            public string StringValue { get; }
        }
    }
}