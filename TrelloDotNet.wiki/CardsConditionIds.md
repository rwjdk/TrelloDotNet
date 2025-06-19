Defines conditions for filtering cards based on their associated child objects such as labels, members, and lists.

## Values
| Value | Description |
| --- | --- |
| `Equal` | Id is equal to the given value. |
| `NotEqual` | Id is not equal to the given value. |
| `AnyOfThese` | Id is any of the given values. |
| `AllOfThese` | Ids exist with all the given values (applicable to labels and members, not lists). |
| `NoneOfThese` | Ids contain none of the given values. |

## Usage
Used in methods like [CardsFilterCondition.ListId](CardsFilterCondition.ListId), [CardsFilterCondition.LabelId](CardsFilterCondition.LabelId), and [CardsFilterCondition.MemberId](CardsFilterCondition.MemberId).
