# Line Length Guard

Roslyn analyser that checks if the maximum line length is respected.

## Rules

This analyser contains one single rule to achieve its objective:

Rule|Title|Description
-|-|-
LLG001|Line too long|Ensures that lines are not longer than the maximum established value. It can be configured by settings.

## Settings

A `LineLengthGuardSettings.json` file can be used to configure some settings of the analyser.

### Available settings

Available settings are explained below:

Key|Description
-|-
ExcludedLineStarts|A collection of strings that make lines starting with any of them to be excluded from the analysis. Lines with any start are analysed by default.
MaximumLineLength|Maximum number of characters that a line is allowed to have. Its default value is `120`.

### Importation

Settings file must be imported into the project to be analysed in this way:

```XML
<ItemGroup>
  <AdditionalFiles Include="LineLengthGuardSettings.json" />
</ItemGroup>
```

### Example

An example of settings file is the following:

```JSON
{
  "ExcludedLineStarts": [
    "// Filename: ",
    "namespace "
  ],
  "MaximumLineLength": 120
}
```