Defines conditions for filtering cards based on date-related criteria.

## Values
| Value | Description |
| --- | --- |
| `Equal` | Date is equal to the given value. |
| `NotEqual` | Date is not equal to the given value. |
| `GreaterThan` | Date is greater than the given value. |
| `LessThan` | Date is less than the given value. |
| `GreaterThanOrEqual` | Date is greater than or equal to the given value. |
| `LessThanOrEqual` | Date is less than or equal to the given value. |
| `HasAnyValue` | Date has any value (is not blank). |
| `DoNotHaveAnyValue` | Date does not have any value (is blank). |
| `AnyOfThese` | Date matches any of the specified values. |
| `NoneOfThese` | Date matches none of the specified values. |
| `Between` | Date is between two specified values. |
| `NotBetween` | Date is not between two specified values. |

## Usage
Used in methods such as [AdvancedDateTimeOffsetCondition](CardsFilterCondition#AdvancedDateTimeOffsetCondition), [CustomField](CardsFilterCondition#CustomField), [Due](CardsFilterCondition#Due), [Start](CardsFilterCondition#Start), and [Created](CardsFilterCondition#Created).
