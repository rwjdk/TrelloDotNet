Defines conditions for filtering cards based on string properties.

## Values
| Value | Description |
| --- | --- |
| `Equal` | String is equal to the given value. |
| `NotEqual` | String is not equal to the given value. |
| `Contains` | String contains the given value. |
| `DoNotContains` | String does not contain the given value. |
| `AnyOfThese` | String matches any of the given values. |
| `AllOfThese` | String matches all of the given values (applicable to labels and members). |
| `NoneOfThese` | String matches none of the given values. |
| `RegEx` | String matches the given regular expression pattern. |
| `StartsWith` | String starts with the given value. |
| `EndsWith` | String ends with the given value. |
| `DoNotStartWith` | String does not start with the given value. |
| `DoNotEndWith` | String does not end with the given value. |

## Usage
Used in methods such as [AdvancedStringCondition](CardsFilterCondition#AdvancedStringCondition), [Name](CardsFilterCondition#Name), [Description](CardsFilterCondition#Description), [LabelName](CardsFilterCondition#LabelName), [MemberName](CardsFilterCondition#MemberName), and [ListName](CardsFilterCondition#ListName).
