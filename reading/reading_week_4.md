# Reading list week 4

Read more on how to use the MongoDb C# driver from here: http://mongodb.github.io/mongo-csharp-driver/2.7/getting_started/quick_tour/

Here are some examples (in C#) for Inserting/querying/updating/deleting documents on MongoDb.

- https://docs.mongodb.com/manual/tutorial/insert-documents/
- https://docs.mongodb.com/manual/tutorial/query-documents/
- https://docs.mongodb.com/manual/tutorial/update-documents/
- https://docs.mongodb.com/manual/tutorial/remove-documents/

Please note that the examples use plain `BsonDocuments`. `BsonDocument` could be substituted with POCO (plain old c# object) which would make the API easier to use.

In practice this means converting a line like this:
`var filter = Builders<BsonDocument>.Filter.Eq("name", "John");`
into this:
`var filter = Builders<Player>.Filter.Eq("name", "John");`
which would make a filter that can be used for queries which return `Player` objects instead of `BsonDocument` objects.

- Aggregation pipeline: https://docs.mongodb.com/manual/core/aggregation-pipeline/
