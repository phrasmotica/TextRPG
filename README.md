# TextRPG

## Inventory Behaviour

### Left click on slot while holding nothing

| slot empty | slot full | result     | implemented        |
| ---------- | --------- | ---------- | ------------------ |
| yes        | no        | do nothing | :heavy_check_mark: |
| no         | no        | remove all | :heavy_check_mark: |
| no         | yes       | remove all | :heavy_check_mark: |

### Right click on slot while holding nothing

| slot empty | slot full | result      | implemented        |
| ---------- | --------- | ----------- | ------------------ |
| yes        | no        | do nothing  | :heavy_check_mark: |
| no         | no        | remove half | :heavy_check_mark: |
| no         | yes       | remove half | :heavy_check_mark: |

### Left click on slot while holding items

| slot empty | slot full | item type | result                        | implemented        |
| ---------- | --------- | --------- | ----------------------------- | ------------------ |
| yes        | no        | N/A       | add all                       | :heavy_check_mark: |
| no         | no        | same      | add to limit, return the rest | :heavy_check_mark: |
| no         | yes       | same      | do nothing                    | :heavy_check_mark: |
| no         | no        | different | swap                          | :heavy_check_mark: |
| no         | yes       | different | swap                          | :heavy_check_mark: |

### Right click on slot while holding items

| slot empty | slot full | item type | result                   | implemented        |
| ---------- | --------- | --------- | ------------------------ | ------------------ |
| yes        | no        | N/A       | add one, return the rest | :heavy_check_mark: |
| no         | no        | same      | add one, return the rest | :heavy_check_mark: |
| no         | yes       | same      | do nothing               | :heavy_check_mark: |
| no         | no        | different | do nothing               | :heavy_check_mark: |
| no         | yes       | different | do nothing               | :heavy_check_mark: |
