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

Not any specific technology or theory – concepts that have evolved over many years <!-- .element: class="fragment" -->

Ideas were adopted by independent groups of people who used them to solve their own data problems <!-- .element: class="fragment" -->

By now, the NoSQL movement includes hundreds of database products – most of them solve a very specific set of problems <!-- .element: class="fragment" -->

---

## History

What changed: rise of the internet and sites with lots and lots of traffic <!-- .element: class="fragment" -->

Google’s and Amazon’s technologies inspired a new movement: NoSQL <!-- .element: class="fragment" -->

Original spark was the need for clustering <!-- .element: class="fragment" -->

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

Common characteristics: <!-- .element: class="fragment" -->

- Schema agnostic <!-- .element: class="fragment" -->
- Cluster friendly <!-- .element: class="fragment" -->
- Commodity hardware <!-- .element: class="fragment" -->
- Nonrelational <!-- .element: class="fragment" -->
- Open-source <!-- .element: class="fragment" -->
- 21st century web <!-- .element: class="fragment" -->

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
- Columnar: Cassandra, which implements many of Dynamo’s scaling properties while providing a column-oriented data model inspired by Google’s BigTable. Cassandra is an open source version of a data store built by Facebook for its inbox search feature. The system scaled horizontally to index more than 50 TB of inbox data, allowing for searches on inbox keywords and recipients. Data was indexed by user ID, where each record consisted of an array of search terms for keyword searches and an array of recipient IDs for recipient searches.

---

## Schema agnostic

A database schema is the description of all possible data and data structures in a relational database <!-- .element: class="fragment" -->

With a NoSQL database (usually) a schema isn’t required <!-- .element: class="fragment" -->

Changing the structure of the data stored doesn’t need actions on a database level <!-- .element: class="fragment" -->

Application still needs to know how the data is stored when reading the data <!-- .element: class="fragment" -->

Note:

- The application needs to take care of the changes to the schema when reading the data
- Shortens develop time especially in an iterative project where the design evolves throughout development (exactly what games tend to be)

---

## Cluster friendly

Many NoSQL databases are designed to be distributed on multiples computers <!-- .element: class="fragment" -->

Storing the data in natural aggregates is the way to make the clustering feasible <!-- .element: class="fragment" -->

Clustering makes adding _high availability_ easier <!-- .element: class="fragment" -->

Note:

- Natural aggregate -> Storing the combination of data that is commonly accessed together
- High availability means keeping the data accessible even when a limited subset of the database servers are not available
- Graph databases don’t save the data in aggregates and are generally not distributable <!-- .element: class="fragment" -->

---

## How the data is distributed:

![Data distribution](/resources/nosql-data-distribution.png)

Note:

- Two ways of distributing data: sharding and replication
- You get same logical inconsistencies with sharding as with a single machine setup (exacerbated to some degree)
- Draw the hotel booking example

---

## Nonrelational

NoSQL databases don’t have the concept of relationships built in <!-- .element: class="fragment" -->

NoSQL compensates this partly by having broader variety of data structures available <!-- .element: class="fragment" -->

- Objects within objects, arrays etc… <!-- .element: class="fragment" -->

Note:

- Relational views and NoSQL denormalization are different approaches to the problem of data spread across records
- Explain why you need to be careful with denormalization (volational data)

---

## Nonrelational

In relational databases the goal is to normalize the data (remove duplicate data) <!-- .element: class="fragment" -->

- Easy to update (updates only to one place) <!-- .element: class="fragment" -->
- Queries might get complex to implement and slow to execute <!-- .element: class="fragment" -->

In NoSQL data is often deliberately denormalized (stored multiple times) <!-- .element: class="fragment" -->

- Enables fast query speed and ease of query implementation <!-- .element: class="fragment" -->
- Updates have to be applied to all the places where a piece of data is stored <!-- .element: class="fragment" -->

---

## Consistency

Consistency is a property which defines how well the read data matches the written data <!-- .element: class="fragment" -->

There are two implementation models of consistency for NoSQL databases <!-- .element: class="fragment" -->

**ACID** Consistency (ACID stands for Atomicity, Consistency, Isolation, Durability) <!-- .element: class="fragment" -->

- Means that once data is written, reads are fully consistent <!-- .element: class="fragment" -->

**BASE** (Eventual Consistency) <!-- .element: class="fragment" -->

- Means that once data is written, it will eventually appear for reading <!-- .element: class="fragment" -->

Note:

- There are two types of consistency: replication and logical consistency <!-- .element: class="fragment" -->
- You don’t want to run into a situation where someone else uses the data while you are in the middle of writing it
- Draw on the board why you can’t do just ACID in certain big scale systems anyways

---

## Availability

Availability is a property which defines how much of the data can be accessed at any given time <!-- .element: class="fragment" -->

- Regardless of other parties accessing the data <!-- .element: class="fragment" -->
- Regardless of connections getting lost between the servers <!-- .element: class="fragment" -->
- The data can be stale (not consistent) <!-- .element: class="fragment" -->

Generally NoSQL databases are built for high availability <!-- .element: class="fragment" -->

- High availability databases are built to eliminate single points of failure <!-- .element: class="fragment" -->
- Optimized to ensure that the end user does not experience an interruption in service <!-- .element: class="fragment" -->

Note:

- Many NoSQL databases differentiate between read and write availability
- Some allow only writes when the connection between cluster nodes is interrupted to prevent serving stale data – some allow only reads to prevent conflicts between writes

---

## CAP-theorem

![CAP](/resources/nosql-cap.png)

---

## CAP-theorem

CAP stands for Consistency, Availability, and Partitioning <!-- .element: class="fragment" -->

States that you cannot have all three completely at the same time <!-- .element: class="fragment" -->

Many NoSQL databases allow tuning between levels of consistency and availability <!-- .element: class="fragment" -->

In clustered databases the partitioning is always present so the trade off needs to be made between consistency and availability <!-- .element: class="fragment" -->

- It’s a tunable sliding scale – not a binary decision <!-- .element: class="fragment" -->

---

## When to use

When you need one more of these:

- Large scale data <!-- .element: class="fragment" -->
- Scale the system horizontally <!-- .element: class="fragment" -->
- An ability to adapt to changing business requirements quickly <!-- .element: class="fragment" -->
- NoSQL data structures such as arrays, sorted sets and graphs <!-- .element: class="fragment" -->

And when NoSQL can reduce the technical complexity of the system <!-- .element: class="fragment" -->

Note:

- Relational databases often contain more features <!-- .element: class="fragment" -->
- NoSQL databases usually have less and are more specific to certain use cases <!-- .element: class="fragment" -->
- Writing SQL queries can get relatively complex when the simultaneously queried data needs to be split in many tables <!-- .element: class="fragment" -->

---

## When not to use

When you have data that is actually highly relational <!-- .element: class="fragment" -->

When you need ACID transactions <!-- .element: class="fragment" -->

When your team has strong relational database skills and it can get the job done <!-- .element: class="fragment" -->

---

## Comparison

| Relational                          | NoSQL                                     |
| ----------------------------------- | ----------------------------------------- |
| Scaling vertically                  | Scaling horizontally                      |
| Different products for same problem | Different products for different problems |
| Proven track record of 30+ years    | Products are less mature                  |
| Query language: SQL                 | No standardized query language            |
| Data normalized                     | Data is often denormalized                |
| Has schema                          | Schema agnostic (on read schema)          |
