Represents various conditions that can be applied to cards in Trello for filtering or automation purposes.

## Values
| Value | Description |
| --- | --- |
| `Equal` | Things are equal. |
| `NotEqual` | Things are not equal. |
| `GreaterThan` | Things are greater than. |
| `LessThan` | Things are less than. |
| `GreaterThanOrEqual` | Things are greater than or equal. |
| `LessThanOrEqual` | Things are less than or equal. |
| `HasAnyValue` | Things have any value (not blank). |
| `DoNotHaveAnyValue` | Things do not have any value (blank). |
| `Contains` | Things contain the value. |
| `DoNotContains` | Things do not contain the value. |
| `AnyOfThese` | Things are any of these values. |
| `AllOfThese` | Things have all of these values (applies to Labels and Members). |
| `NoneOfThese` | Things have none of these values. |
| `RegEx` | Things match the value using a regular expression. |
| `StartsWith` | Things start with the value. |
| `EndsWith` | Things end with the value. |
| `DoNotStartWith` | Things do not start with the value. |
| `DoNotEndWith` | Things do not end with the value. |
| `Between` | Things are between two values (applies to Date and Integer-based values). |
| `NotBetween` | Things are not between two values (applies to Date and Integer-based values). |

## Usage
This enum is utilized in methods such as [CardsFilterCondition](CardsFilterCondition) for defining conditions on cards.
