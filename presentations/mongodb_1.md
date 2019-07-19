# MongoDB Part 1: basics

---

## What is MongoDB

Open source, written in C++, and licenced under GNU-AGPL

- https://github.com/mongodb/mongo

High availability

Automatic scaling

Document-oriented data model

Compiles in Max OS X, Windows, and most flavors of Linux

Note:

The GNU-AGPL is subject to some controversy. What this licensing means in practice is that the source code is freely available and the contributions from the community are encouraged. The primary limitation of the GNU-AGPL is that any modifications made to the source code must be published publicly for the benefit of the community

Who uses MongoDB:

- EA
- Square Enix
- Grand Cru

Can be used as primary data store unlike many key-value stores (usually)
Not as fast as key-value stores

---

## In a nutshell

JSON-like structure to represent data

Strong consistency

Combine the best features of relational databases and distributed key-value stores

Ad hoc queries – no need to define queries that the system will accept in advance

Secondary indexes

Only a few configuration parameters

Note:

- Just like with relational databases
- Adhoc queries
- JSON is an acronym for JavaScript Object Notation. JSON structures are comprised of keys and values, and they can nest arbitrarily deep. They’re analogous to the dictionaries and hash maps of other  programming languages. 
- JSON is simple, intuitive and human friendly
- Which features?
- Key-value  stores,  because  of their  simplicity,  are  extremely  fast  and  relatively  easy to scale. 
- Relational databases are more difficult to scale, at least horizontally, but admit a rich  data  model  and  a  powerful  query  language.
- Key-value stores only support queries with key
- With big data sets indexes are necessary
- Configuration: for example the memory management is offloaded to the OS kernel

---

## History

Created by startup called 10gen

Started as a more ambitious platform project in mid-2007

Similar to Google AppEngine

Was meant to handle scaling and managing of software and hardware infrastructure automatically

Developers didn’t want to give up that much control but wanted the database technology instead

MongoDB v1.0 was released in November 2009

Note:

- software platform-as-a-service, composed of an application server and a database, that  would host web applications and scale them as needed. 

---

## Example of MongoDB document

```json
{ 
	"_id" : "57ade8face4bf5361c4268d6", 
	"Name" : "john", 
	"Score" : 7, 
	"IsBanned" : false, 
	"CreationDate" : ISODate("2016-08-12T15:19:22.492Z"), 
	"Items" : [ 
		{ 
			"_id" : "57ade921ce4bf5361c4268d7", 
			"Price" : 5, 
			"ItemType" : 0 
		} 
	] 
}
```

Note:

- Set of property names and their values
- Documents can contain subdocuments
- What’s important to note here is that a document-oriented data model naturally represents data in an aggregate form, allowing you towork with an object holistically
- Documents don’t need to conform to predefined schema

---

## Document-oriented data model

Documents are grouped into collections without any schema

Naturally represents the data in an aggregate form, allowing to work on it holistically

Application forces the data structure

Can speed up development when the schema is changing frequently, especially true with games!

Note:

- In practice documents in a collection will be relatively uniform
- No need for alter table type of commands
- Makes possible to represent data with truly variable properties

---

## Simple query example

```js
db.players.find(
{
	"_id" : "57ade8face4bf5361c4268d6"
})

db.players.find(
{
	"Score" : 7,
	"IsBanned" : false
})
```

---

## Use cases

Games

Web applications

Analytics

Logging applications

Capturing data of which structure can’t be known in advance

As a medium grade cache

Note:

MongoDB’s relevance to analytics derives from its speed and from two key features: targeted atomic updates and capped collections

Atomic updates let clients efficiently increment counters and push values onto arrays

Capped collections, often useful for logging,  feature  fixed  allocation,  which  lets  them  age  out  automatically

---

## Why to use MongoDB

Intuitive data model

- Represent rich hierarchical data structures without complicated multi-table joins
- Data can be persisted “as is” without the need for complexity of object mappers

Relatively easy to scale horizontally

Sophisticated query system

Schemaless data model

Note:

- In fully normalized relational database the data for single query might be spread across dozens of tables

---

## When not to use MongoDB

When there is a need for proper transactions

- MongoDB can do them only at document level

Joining many different types of data across many different dimensions

In shared environments

- Memory for the database will be allocated automatically as needed

---

## The core server

The core database server runs via an executable called mongod (mongodb.exe on Windows)

All the data files for a mongod process are stored by default in /data/db (c:\data\db)

Simple to configure (a design feature – not a bug)

The mongod server process receives commands over a network socket using a custom binary protocol

Note:

Database tuning, which in most RDBMSs means tinkering with a wide array of parameters controlling memory allocation and the like, has become something of a black art

MongoDB’s design philosophy dictates that memory management is better handled by the operating system than by a DBA or application developer

---

## The JavaScript shell

Runs via executable called mongo (.exe)

Tool for administering the database and manipulating data

Connects to a specified mongod process

Most commands are issued using JavaScript expressions

Shell is also a JavaScript interpreter so code like this can be run:

```js
for(i=0; i<200000; i++) { db.numbers.save({num: i}); }
```

---

## Basic Administration

show dbs lists all databases on the system

show collections displays a list of all the collections defined in the current database

db.stats() and db.collection.stats() display lower-level insight into databases and collections

db.help() prints list of commonly used methods for operation on database objects

db.foo.help() prints list of methods for collections

Note:

- Some of the values provided in these result documents are useful only in complicated debugging situations

---

## Nuts and bolts

---

### Documents

All documents are serialized to BSON before being sent to MongoDB

It’s important to consider the length of the key names since they are stored in to documents

Documents are limited to 16 MB in size

- To prevent developers from doing bad designs
- For performance reasons

---

### Documents

Can store:

- Simple datatypes such as strings, numbers and dates
- Arrays
- Other documents

Note:

In RDBMS, column names are always kept separate from the rows they refer to

On the server side, querying a large document requires that the document be copied into a buffer before being  sent  to  the  client.  This  copying  can  get  expensive,  especially  (as  is  often  the case)  when  the  client  doesn’t  need  the  entire  document. In  addition,  once  sent, there’s the work of transporting the document across the network and then deserializing it on the driver side. This can become especially costly if large batches of multimegabyte documents are being requested at once

---

### Collections

Containers for structurally or conceptually similar documents

Names can contain numbers, letters or ‘.’ characters

A fully qualified collection name can’t be longer than 128 characters

Note:

‘.’ character can be used for creating virtual namespaces but it’s just an organizational principle

MongoDB also uses some collections internally: system.namespaces and system.indexes

Can be renamed

---

### Databases

Logical and physical grouping of collections

Automatically created when you first write to the collection for the first time

Every collection and index are stored into its own namespace, and the metadata for each is stored in a file which size can’t exceed 16MB.

- In practice the file can store approximately 24 000 collections/indexes

Database file start from 64 MB and each new file is twice the size of the previous one until the size is 2 GB

Note:

- You’re not likely to need anywhere close to 24 000 collections
- If really needed, the file can be made larger with –nssize server option
- A common allocation strategy
- There is –noprealloc and –smallfiles server options if the disk space is limited
- db.stats() can be used for seeing stats from disk usage
- A database can be dropped with a drop database –command which can not be undone
- MongoDB uses preallocation to ensure that as much data as possible will be stored contiguously

---

## Creating and Reading documents

Create

- db.collection.insert({“foo” : “bar”})
- Inserts the document as it is
- If _id field is not passed, it will be automatically generated

Read

- db.collection.findOne({“foo” : “bar”})
- FindOne() returns the first occurrence of a object matching the query

---

### Updating documents

```js
db.collection.update({"_id" : ObjectId("4b2b9f67a1f631733d917a7c")},  objectToUpdate)
```

Update() replaces the object

First parameter is the query for finding the object to replace

---

### Deleting documents

```js
db.collection.remove({"_id" : ObjectId("4b2b9f67a1f631733d917a7c")})
```

The parameter is a query. All the matching objects will be removed.
