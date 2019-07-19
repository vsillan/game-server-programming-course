# NoSQL

---

## History

![History](/resources/nosql-history.png)

---

## Application integration

![Integration](/resources/nosql-app-integration.png)

Note:

- Explain integration database
- Explain why not a problem anymore

---

## Definition

Not any specific technology or theory  – concepts that have evolved over many years

Ideas were adopted by independent groups of people who used them to solve their own data problems

By now, the NoSQL movement includes hundreds of database products – most of them solve a very specific set of problems

---

## History 2

What changed: rise of the internet and sites with lots and lots of traffic

Google’s and Amazon’s technologies inspired a new movement: NoSQL

Original spark was the need for clustering

Note:

- Examples: Amazon, Google and Ebay
- How do you scale: buy more expensive hardware
- Companies like google use different approach: lots of little boxes
- RBDMS was designed to run on big boxes
- Clustering RMBDMS is very hard to do; unnatural
- Google (Bigtable) and Amazon (Dynamo) did their proprietary solutions
- Beefing up hardware can become prohibitively costly when scaling vertically
- Horizontally scaled data set can use commodity hardware
- Distributing data across multiple machines mitigates risk of failing nodes

---

## Hardware

![Hardware](/resources/nosql-hardware.png)

---

## Definition 2

Can not be defined

Common characteristics:

- Schema agnostic
- Cluster friendly
- Commodity hardware
- Nonrelational
- Open-source
- 21st century web

Note:

- Open source is at the moment a common characteristic
- Some databases are designed to operate best (or only) with specialized storage and processing hardware. With a NoSQL database, cheap off‐the‐shelf servers can be used. 
- Schema-less is not the whole truth. Application always defines an implicit schema (schema on read). 
- Explain: Why no schema makes the development faster

---

## Data models

![Data models](/resources/nosql-data-models.png)

Note:

- Line between document and key-value stores is fuzzy
- Aggregated oriented put data into bigger chunks
- Graph oriented make the data into even smaller units
- Graph example: neo4j
- Key-value: Just a hashmap but persisted on a disc
- Column: Cassandra, which  implements  many  of  Dynamo’s  scaling  properties  while  providing  a  column-oriented data model inspired by Google’s BigTable. Cassandra is an open source version of a data store built by Facebook for its inbox search feature. The system scaled horizontally to index more than 50 TB of inbox data, allowing for searches on inbox keywords and recipients. Data was indexed by user ID, where each record consisted of an array of search terms for keyword searches and an array of recipient IDs for recipient searches.

---

## Schema agnostic

A database schema is the description of all possible data and data structures in a relational database

With a NoSQL database a schema isn’t required

Development can be started without doing up‐front schema design

Note:

- The application needs to take care of the changes to the schema when reading the data
- Shortens develop time especially in an iterative project where the design evolves throughout development (exactly what games tend to be)

---

## Schema agnostic (2)

Changing the structure of data stored doesn’t need actions on database level

Application still needs to know how the data is stored when reading the data

There are exceptions to NoSQL databases being schema agnostic

- Notable mentions are columnar databases such as HBase

---

## Cluster friendly

Many NoSQL databases are designed to be distributed on multiples computers

Storing the data in natural aggregates is the way to make the clustering feasible

Clustering makes adding _high availability_ easier

Graph databases don’t save the data in aggregates and are generally not distributable

Note:

- Draw on board 
- Natural aggregate -> Storing the combination of data that is commonly accessed together
- High availability means keeping the data accessible even when a limited subset of the database servers are not available

---

## How is the data distributed:

![Data distribution](/resources/nosql-data-distribution.png)

Note:

- Two ways of distributing data: sharding and replication
- You get same logical inconsistencies with sharding as with a single machine setup (exacerbated to some degree)
- Draw the hotel booking example

---

## Nonrelational

NoSQL databases don’t have the concept of relationships built in

NoSQL compensates this partly by having broader variety of data structures available

- Objects within objects, arrays etc…

Note:

- Relational views and NoSQL denormalization are different approaches to the problem of data spread across records
- Explain why you need to be careful with denormalization (volational data)

---

## Nonrelational

In relational databases the goal is to normalize the data (remove duplicate data)

- Easy to update (updates only to one place)
- Queries might get complex to implement and slow to execute

In NoSQL data is often deliberately denormalized (stored multiple times) 

- Enables fast query speed and ease of query implementation
- Updates have to be applied to all the places where a piece of data is stored

---

## Consistency

Consistency is a property which defines how well the read data matches the written data

There are two types of consistency: replication and logical consistency

There are two implementation models of consistency for NoSQL databases

---

## Consistency types

**ACID** Consistency (ACID stands for Atomicity, Consistency, Isolation, Durability)

- Means that once data is written, reads are fully consistent

**BASE** (Eventual Consistency)

- Means that once data is written, it will eventually appear for reading

Note:

- You don’t want to run into a situation where someone else uses the data while you are in the middle of writing it
- Draw on the board why you can’t do just ACID in certain big scale systems anyways

---

## Availability

Availability is a property which defines how much of the data can be accessed at any given time

- Regardless of other parties accessing the data
- Regardless of connections getting lost between the servers
- The data can be stale (not consistent)

Generally NoSQL databases are built for high availability

- High availability databases are built to eliminate single points of failure
- Optimized to ensure that the end user does not experience an interruption in service

Note:

Many NoSQL databases differentiate between read and write availability
Some allow only writes when the connection between cluster nodes is interrupted to prevent serving stale data – some allow only reads to prevent conflicts between writes

---

## CAP-theorem

![CAP](/resources/nosql-cap.png)

---

## CAP-theorem

CAP stands for Consistency, Availability, and Partitioning

States that you cannot have all three completely at the same time

Many NoSQL databases allow tuning between levels of consistency and availability 

In clustered databases the partitioning is always present so the trade off needs to be made between consistency and availability

- It’s a tunable sliding scale – not a binary decision

---

## When to use

For easier and faster development (generally)

- Data can be stored as it is
- Some typical NoSQL data structures can fit some applications inherently better than relational tables such as arrays, sorted sets and graphs

For large scale data

- When there is a need to scale the system horizontally

---

## When to use

When your business requirements are likely to change

When NoSQL can reduce the technical depth of the system

- Relational databases often contain more features
- NoSQL databases usually have less and are more specific to certain use cases 
- Writing SQL queries can get relatively complex when the simultaneously queried data needs to be split in many tables

---

## When not to use

When you have data that is actually highly relational

When you need ACID transactions

When your team has strong relational database skills and it can get the job done

---

## Comparison to traditional relational databases

| Relational                          | NoSQL                                     |
| ----------------------------------- | ----------------------------------------- |
| Scaling vertically                  | Scaling horizontally                      |
| Different products for same problem | Different products for different problems |
| Proven track record of 30+ years    | Products are less mature                  |
| Query language: SQL                 | No standardized query language            |
| Data normalized                     | Data is often denormalized                |
| Has schema                          | Schema agnostic (on read schema)          |
