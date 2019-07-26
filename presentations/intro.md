# Game server programming

---

## This course

---

### The focus

Building Web APIs for games.

Broaden your knowledge on different database systems to ``NoSQL`` territory

Note:

After this course you should undertand what Web APIs enable, limit and what needs to be considered in the implementation.

---

### What do we actually do?

Create Web APIs for games using **ASP.NET** Core (C#)

Learn and use **MongoDb** as a database for the Web API

Implement some common patterns that make up a solid game server

Note:

- We focus on APIs they are more general purpose than real-time implementations

---

### Why these things?

Web APIs play an important role in most online games

Relational databases are _not_ commonly used in online games

Online games that aim to be played by significant amount of players can _not_ survive without solid architecture behind them

Note:

- It's important to understand the possibilities of **NoSQL** databases especially in terms of performance, availability and scalability
- There are myriad of things that could be taught in a game server programming course. These are things that I feel generally apply to a great variety of projects - even outside games.

---

## Game server development

Note:

- Go through the Readme.md before moving forward

---

### What can we do with a game server?

Player to player interaction

Persist the game data

Share collective game state between clients

Authorize player actions (prevent cheating)

Gather and analyze data analytics

Do simulation that is too expensive for the client

Note:

- Player to player interaction: can be anything from real-time multiplayer to chat and facilitating in-game economies
- Persisting the data can be: saving profiles, maintaining a game world etc.
- Authorizing the player actions is key to competitive games, the assumption is that the client can always be hacked and thus all critical client actions should be verified by the server
- Data analytics is a growing field in gaming (many games that are provided as a service usually have a person or even a team dedicated to data analytics)

---

### The cost

Programming and maintaining the server(s) as long as the game is alive

Server-side usually multiplies the complexity of the client

Servers and network traffic are not free

Popular multiplayer games will be targeted by hackers and cheaters

Online games often require “extra” features

There is always latency (and the amount varies)

Note:

- Online games usually require extra features on top of the core gameplay such as chat, matchmaking and leaderboards.
- Network debugging is a lot more difficult than debugging a single player game
- Maintenance issue is starting to fade away with the advent of serverless, good 3rd party service providers etc
- Making real-time online games well is all about hiding the latency
- Multiplayer games can’t be done in just fire and forget –style
- Some estimate that it can take 3-4 times as much effort to build a fully fledged multiplayer game vs a similar single player game
- Real time games need to have a lot of users online at all times since they are the content of the game
- Why debugging is hard: a lot of edge cases, latency and jitter can vary considerably, needs more than one client, usually needs special tools
- Bandwidth is often more limited than the amount of information that we would like to send (in real-time games)

---

### I'm a client programmer/artist/designer/insert-role-here. Why should I care about server programming?

To understand the

- limitations
- possibilities
- cost
- requirements

---

## How does a server solution look like?

---

### A simple game server architecture

![Simple game server architecture](/resources/simple-server-architecture.png)

---

### FPS game server architecture example

![FPS game server architecture](/resources/fps-server-architecture.png)

---

## So what about using off-the-shelf solutions?

---

### 3rd party services

Third party services provide readymade solutions for common problems:

- Account management
- Realtime multiplayer
- Commerce
- Analytics
- Social features (chat, leaderboard, friends etc.)
- And many other things...
  
These can reduce the game server development time significantly

There are many viable providers out there such as **GameSparks** and **PlayFab**

---

### Considerations for 3rd party services

You need to constantly keep track of their service status and upcoming updates (which can change the API)

You are completely relying on the service provider to keep updating the service

Be sure the development team is active and trustworthy

---

### Considerations for 3rd party services

You might be giving access to your valuable data

Analytics are the most obvious example

Some or all of the code may not be accessible and/or open for modifications

Extending the service might be limited

Pricing model of the service might change

Note:

- Maybe a story about Omniata analytics

---

## Few important concepts

---

## Network topologies

---

### Peer-to-Peer

All nodes participating in the same game instance are interconnected with each other

- Every node can communicate with any other node

All nodes are equal (no hierarchies)

Limited scaling potential since every node is responsible for delivering its status to all other nodes

Has been popular choice in the early days of multiplayer gaming

---

### Client-Server

One of the nodes is promoted to a server (can be a dedicated server or one of the clients)

All communications are handled through the server which will distribute the data to the clients

Server is an extra step on the communication but there are various ways to optimize the traffic

Server can add security to the system

Note:

- Server can filter the data
- Server can aggregate the packets and smooth out the packet flow

---

## The differences between asynchronous and synchronous multiplayer games

---

### Asynchronous

Players can play with each other without playing it at the same time

Game is usually not simulated in real-time on server side

Cheaper to run

Asynchronous games are usually implemented with TCP protocol

No need for latency compensation on the client side

---

### Synchronous

Players have to be playing the game at the same time

The game is simulated in real-time on server-side

More costly to run

Synchronous games are often implemented with UDP

Often needs latency compensation techniques on the client side

Note:

- Most of the game types can be implemented only as synchronous or only as asynchronous
- Synchronous games might need as many as 10 updates per second
- Implementing a game with UDP is more work intensive: handling dropped packets, handling the connection, and flow control
- Async: The Game doesn’t need to be simulated on server side, usually player actions can be authorized by validating the game state of the player in the database/server
- Sync: Often have servers that run the game simulation along with the clients as authoritative server

---

## Cloud Services would require a course of their own...

---

### Cloud services

Nowadays most games are deployed to cloud

Cloud can scale up and down very quickly

Most commonly used cloud providers (for games) are Amazon AWS and Microsoft Azure

Viable even for smaller game studios

Out the box solutions

Ease of deploying to datacenters around the world

There are four different cloud computing service models: IaaS, PaaS, SaaS and FaaS

Note:

- Cloud can answer to most scaling needs if the application is done properly
- Size of an operations team is a lot smaller than with On Premise solutions
- Cloud services are paid by the usage

---

### Commonly used cloud services

Virtual machines running on any common operating system

Various hosted databases

Load balancers

Automatic scaling

Various security features

And many more…

Note:

- Azure provides hosted databases such as SQL database, DocumentDB, Table storage, and Redis
- Also: Message queues, Monitoring and alerts for the servers and services, Hosting for web sites and web APIs, Logs/Analytics and big data services, Push notifications, Content delivery network, Active Directory for authenticating users

---

## So you decide to build an online game

---

### Things you need to know

How to create good Web APIs

Databases – both traditional SQL and NoSQL databases

How to build scalable, stable, and fault tolerant systems

How to build secure and cheat resistant game servers

Viable game server service providers out there

Cloud services and architectures

Techniques for creating robust realtime experiences

Note:

In this course we'll focus mostly on the first 2 and bit on 3 and 4.
