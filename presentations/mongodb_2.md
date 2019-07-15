# MongoDB Queries

---

## Queries

MongoDB has its own JSON-like query language

You can perform ad hoc queries on the database using the find or findOne functions 

**Query selectors** can be used to match documents

- You can query for ranges, set inclusion, inequalities and more

**Query options** can be used to modify the result of the queries

- sorting, choosing which fields to include etc…

Note:

- Regular expressions can be used for queries and they can take advantage of indexes
- Every MongoDB query is fundamentally a instantiation of a cursor and fetching of that cursor’s result set

---

### Queries: Find method

The find method is used to perform queries in MongoDB

Querying returns a subset of documents in a collection

Takes a query document specifying the query criteria as a parameter

Empty query document matches everything in the collection

Queries can be restricted by adding key-value pairs to the query document

---

### Queries: Find method

Example: db.players.find({name: john})

Example in C#:

```C#
FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(“Name", name);
Player player = await collection.Find(filter).FirstAsync();
```

---

### Query cursor

When you call find the database is not queried immediately

The database returns results from find using a cursor

Cursor allows to control the eventual output of a query

Almost every method on a cursor object returns the cursor itself so that you can chain options in any order

---

### Query cursor

```C#
SortDefinition<Player> sortDef = Builders<Player>.Sort.Ascending("Level");
IFindFluent<Player, Player> cursor = 	collection.Find("").Sort(sortDef).Limit(1).Skip(10);
```

Query is executed when results are requested from the cursor:

```C#
List<Player> players = await cursor.ToListAsync();
```

---

### Query selectors: Comparisons

$lt == lower than ( < )

$lte == lower than or equal ( <= )

$gt == greater than ( > )

$gte == greater than or equal ( >= )

Example in Mongo Shell:

```js
db.players.find({”level" : {"$gte" : 18, "$lte" : 30}})
```

Example in C#:

```C#
FilterDefinition<Player> filter = Builders<Player>.Filter.Gte("Level", 18) & Builders<Player>.Filter.Lte("Level", 30);
List<Player> players = await collection.Find(filter).ToListAsync();
```

Note:

- Range queries will match only if they have same type as the value to be compared against
- Generally you should not store more than one type for key within the same collection
- (e.g. db.players.find({score: {$gte: 100, $lte: 200}})
- When using the set operators, keep in mind that $in and $all can take advantage of indexes, but $nin can’t and thus requires a collection scan. If you use $nin, try to use it in combination with another query term that does use an index. 
- Make example of using set

---

### Query selectors: Set operators

Each takes a list of one or more values as predicate

**$all** – returns a document if all of the given values match the search key

**$in** - returns a document if any of the given values matches the search key

**$nin** – returns a document if none of the given values matches the search key

Example: db.players.find({”Level" : {"$in" : [1, 10, 20]}})

Note:o

- Returns all players who have level of 1 or 10 or 20

---

### Query selectors: Boolean operators

**$ne** – not equal

**$not** – negates the result of another MongoDB operator or regular expression query

**$or** – expresses a logical disjunction of two values for two different keys

**$and** – both selectors match for the document

**$exists** – used for querying documents containing  a particular key, needed because collections don’t enforce schema

Note:

- $ne can not take advantage of indexes
- Do not use $not if there exists a negated form for the operator (e.g. $in and $nin)
- If possible values are scoped to the same key, use $in instead
- $and and $or take array of query selectors, where each selector can be arbitrarily complexand may itself contain other query operators. 
- MongoDB interprets all query selectors containing more than one key by ANDing the conditions, you should use $and only when you can’t express an AND in a simpler way.

---

### Query selectors: Javascript

If you can’t express your query with the tools described thus far, you have an option to write some JavaScript

**$where** operator is used to pass a JavaScript expression

Adds substantial overhead because expressions need to be evaluated within a JavaScript interpreter context

JavaScript expressions can’t use an index

For security, use of "$where" clauses should be highly restricted or eliminated

Note:

- Enables possibility of JavaScript injection attacks – malicious users can’t write or delete this way but they might be able to read sensitive data

---

### Querying Arrays

Querying for matching value in array uses the same syntax as querying a single value

- Db.players.find({‘Tags’ : “active”}) // would find player with Tags : [“active”, “novice”]

Dot notation can be used for querying value in specific position of the array

- Db.players.find({‘Tags.0’ : “active”})

**$size** – allows to query for an array by its size

---

### Querying sub-documents

It’s possible to query sub objects by separating relevant keys with . (dot)

. (dot) means “reach into an embedded document”

Example: db.players.find({'Items._id' : '57ade921ce4bf5361c4268d7‘})

**$elemMatch** can be used to combine multiple conditions for same sub-document


```js
db.players.find({”Items" : {"$elemMatch" : {”Type" : ”Sword",
				          ”Level" : {"$gte" : 5}}}})
```

---

### Query cursor options

Query cursor options can be used to constrain the result set

**Projections**

- Returns only specific fields of the document
- $slice operator can be used for pagination for arrays within documents

**Sorting**

- Can be used for sorting any result in ascending or descending order
- Can be used with one or more fields

---

### Query cursor options

**Skip and limit**

- Skip documents and limit documents returned by the query
- Beware of skipping large number of documents because it gets ineffective

Note:

- Especially in cases where you have large documents, using a projection will minimize the costs of network latency and deserialization.

---

## Updates and deletes

---

### Updates

Documents can be replaced completely 

Specific fields can be updated independently

Targeted updates generally give better performance

- Update documents are smaller
- No need to fetch the existing document

---

### Updates

By default update only updates the first document matched by its query selector

- There is an option to do multidocument updates as well

MongoDB supports upserts

- Upsert means doing an insert if the document doesn’t exist and doing update if it already exists

Note:

- It’s best to keep the documents small if you intend to do a lot of updates
- Updates that modify the structure and size of the document lead into rewriting the whole document (if the document size is several MBs, the updates will start getting costly)

---

### Standard update operators

**$inc** – increment or decrement a numeric value

- Can be used with an upsert

**$set** – set value of any particular key to any valid BSON type

**$unset** – remove a key from the document

- For an array, the value is merely set to null

**$rename** – change the name of a key

- Works with subdocuments as well

---

### Array update operators

**$push** and **$pushAll** – append value/list of values to an array

**$addToSet** – only adds the value if it doesn’t exist in the array

- Can be used in combination with $each to add multiple values

**$pop** – Used for removing an item from an array based on position, does not return removed value

**$pull** and **$pullAll** – remove value/list of values by the value

Note:

- Since arrays are so central to MongoDB’s document model, there exists update operators just for arrays

---

### Atomic operations

findAndModify –command 

Any one document can be updated atomically

Document structure in itself makes it possible to fit within a single document many things that might atomic processing

When updating and removing documents a special option called $atomic can be used to make sure that the whole operation is performed before others read the touched documents

Note:

- Guarantee of consistent reads along a single connection
- Database permits only one writer or multiple readers at once (not both)
- Fetching data from disk yields the execution
- Multidocument operations yield the execution unless explicitly specified not to

---

## Aggregation

Summarizing and reframing the data

Note:

- Queries allow to get data as it’s stored -- Aggregations operations process data records and return computed results
- Aggregation operations group values from multiple documents together, and can perform a variety of operations on the grouped data to return a single result
- Aggregation = crunching

---

### Aggregation

Simplified process of aggregation works like this:

- Group values from multiple documents
- Perform a variety of operations on the values
- Return a single result

Three methods for aggregation:

**Aggregation pipeline**

**Map-reduce **

**Single purpose aggregation methods**

---

### Aggregation example

Find out what is the most common level for a player

1. Project the levels out of each player document
2. Group the levels by the number, counting the number of occurrences
3. Sort the levels by the occurrence count, descending
4. Limit the results to the first three

---

### Aggregation example

```js
db.players.aggregate(
	{"$project" : {"Level" : 1}},
	{"$group" : {"_id" : "$Level", "Count" : {"$sum" : 1 }}},
	{"$sort" : {“Count" :  -1}},
	{"$limit" :  3}
)
```

Note:

{“$project” : {“Level” : 1}}

- This projects the level field in each document

{“$group” : {“_id” : “$Level”, “Count” “ {“$sum” : 1 }}}

- This groups the levels by number and increments “Count"  for each document a level appears in

{"$sort" : {“Count" :  -1}}

- This reorders the result documents by the “Count" field from greatest to least

{"$limit" :  3}

- This limits the result set to the first three result documents

- Project: The syntax is similar to the field selector used in querying: you can select fields to project by specifying " fieldname" : 1 or exclude fields with " fieldname" : 0. After this operation, each document in the results looks like: {"_id" : id, "author" : " authorName"}. These resulting documents only exists in memory and are not written to disk anywhere
- Group: This groups the authors by name and increments "count" for each document an author appears in. First, we specify the field we want to group by, which is "author" . This is indicated by the "_id" : "$author" field. You can picture this as: after the group there will be one result document per author, so "author" becomes the unique identifier("_id" ). The second field means to add 1 to a "count" field for each document in the group. Note that the incoming documents do not have a "count" field; this is a new field created by the "$group"

---

## Aggregation pipeline

Transform and combine documents in a collection

Objects are transformed as they pass through a series of pipeline operators 

- Such as filtering, projecting, grouping, sorting, limiting, and skipping

Pipeline operators need not produce one output document for every input document: operators may 
also generate new documents or filter out documents

---

## Aggregation pipeline

Using projections you can

- Add computed fields
- Create new virtual sub-objects
- Extract sub-fields into the top-level of results

Pipeline operators can be repeated and combined freely

Note:

Returns result set inline

Operations can be repeated, for example, multiple $project or $group steps.

Supports non-sharded and sharded input collections

Can be indexed and further optimized

---

### Aggregation pipeline operators

**$match**

Filters documents so that you can run an aggregation on a subset of documents
Can use all of the usual query operators

**$sort**

Similar as sort with normal querying

---

### Aggregation pipeline operators

**$limit**

Takes a number, n, and returns the first n resulting documents

**$skip**

Takes a number, n, and discards the first n documents from the result set

**$unwind**

Unwinding turns each field of an array into a separate document

---

### Aggregation pipeline: $group

Grouping allows you to group documents based on certain fields and combine their values

When you choose a field or fields to group by, you pass it to the $group function as the group’s "_id" field

{"$group" : {"_id" : "$Level"}}

---

### Aggregation pipeline: $group

There are several operators which can be used with $group

- $sum
- $avg
- $max
- $min
- $first
- $last
- $addToSet
- $push

---

### Aggregation pipeline: $project

Much more powerful in the pipeline than it is in the “normal” query language

Allows to extract fields from subdocuments, rename fields, and perform operations on them

```js
// Selecting fields works as in normal queries
db.players.aggregate({"$project" : {"Name" : 1, "_id" : 0}})
// Renaming example:
db.players.aggregate({"$project" : {"PlayerId" : "$_id", "_id" : 0}})
```

---

### Aggregation pipeline: $project

There are many expressions which can be applied when projecting

- Math expressions - $add, $subtract, $mod…
- Date expressions - $year, $month, $week…
- String expressions - $subsctr, $concat, $toLower…
- Logical expressions - $cmp, $eq, $gt…

---

### Map-reduce

Can solve that is too complex for the aggregation framework

Uses JavaScript as its “query language” so it can express arbitrarily complex logic

Tends to be fairly slow and should not be used for real-time data analysis

Somewhat complex to use

---

### Single purpose aggregation methods

Db.collection.count()

- Returns the count of documents qualifying the defined condition(s)

db.collection.distinct()

- The most simple way of getting distinct values for a key
- Works for array keys and single value keys

db.collection.group()

- Similar to map-reduce but simpler and less flexible
