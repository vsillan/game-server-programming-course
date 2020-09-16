# Assignment 5

_This assignment builds on top of the `GameWebApi` project created in assignment 3 and extended in assignment 4 and 5._

With these exercises you will learn more about creating queries and aggregation against MongoDb. You will also learn how to utilize more advanced query parameters with the Web API.

You can select from the following assignments the ones that interest you the most. Do atleast `4 queries` but doing more queries and aggregation exercises will yield you extra points.

There is some help provided for some of the API urls but some of them you have to think on your own.

---

## Queries

### 1. Ranges

Get `Players` who have more than x score

**hints:**

Specify the x in the query like this: `...api/players?minScore=x`

### 2. Selector matching

Get `Player` with a name

**hints:**

Make sure the API can handle routes `...api/players/{name}` _and_ `..api/players/{id}`

You can use attribute constraints or use a query parameter like this: `...api/players?name={name}`

### 3. Set operators

Add `Tags` for the `Player` model ( `Tags` can be a list of enum values) and create a query that returns the `Players` that have a certain tag.

### 4. Sub documents queries

Find `Players` who have `Item` with certain property

### 5. Size

Get `Players` with certain amount of `Items` by using size method

### 6. Update

Update `Player` name without fetching the `Player`

### 7. Increment

Increment `Player` score without fetching the `Player`

### 8. Push

Add `Item` to the item list propery on the `Player`

### 9. Pop and increment as an atomic operation

Remove from `Item` from `Player` and add some score for the `Player`. You can think of this as a `Player` selling an `Item` and getting score as a reward.

**hints:**

The route should be `..api/players/{playerId}/items/` with DELETE Http verb.

### 10. Sorting

Get top 10 `Players` by score in descending order

---

## Aggregation

### 11. Aggregation exercise based on the example in the slides

Find out what is the most common level for a player (example in the slides).

### 12. Intermediate aggregation exercise

Get the item counts for different prices for items.

### 13. Difficult aggregation exercise

Get the average score for players who were created between two dates using aggregation.
