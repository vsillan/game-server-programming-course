# Game server programming

---

## The focus

Building **Web APIs** for games

Get familiar with **NoSQL** databases

---

### What do we actually do?

Create Web APIs for games using **ASP.NET Core** (C#)

Use **MongoDb** as a database for a Web API

Learn some best practices for server development

Note:

- APIs more general purpose than real-time implementations

---

### Why these things?

Web APIs play an important role in most online games

NoSQL databases are more suited for **global scale products**

A solid backend is a must for a succesful online game

Note:

- Import to understand NoSQL databases especially in terms of performance, availability and scalability
- There are myriad of things that could be taught in a game server programming course
- These are things that I feel generally apply to a great variety of projects - even outside games

---

### I'm a client programmer / artist / designer / insert-a-role-here - **Why should I care** about server programming?

To understand <!-- .element: class="fragment" -->

- the limitations <!-- .element: class="fragment" -->
- the possibilities <!-- .element: class="fragment" -->
- the cost <!-- .element: class="fragment" -->
- the requirements <!-- .element: class="fragment" -->

---

## Game server development

Note:

- Go through the Readme.md before moving forward

---

### What can we do with a server?

Player to player interaction <!-- .element: class="fragment" -->

Share collective game state between players <!-- .element: class="fragment" -->

Persist the game data <!-- .element: class="fragment" -->

Authorize player actions (prevent cheating) <!-- .element: class="fragment" -->

Gather and analyze data analytics <!-- .element: class="fragment" -->

Do simulation that is too expensive for the client <!-- .element: class="fragment" -->

Note:

- Player to player interaction: can be anything from real-time multiplayer to chat and facilitating in-game economies
- Persisting the data can be: saving profiles, maintaining a game world etc.
- Authorizing the player actions is a mustor  fcompetitive games, the assumption is that the client can always be hacked and thus all critical client actions should be verified by the server
- Data analytics is a growing field in gaming (many games that are provided as a service usually have a person or even a team dedicated to data analytics)

---

### The cost

Programming and maintaining the server(s) as long as the game is alive <!-- .element: class="fragment" -->

Server-side usually multiplies the complexity of the client <!-- .element: class="fragment" -->

Servers and network traffic create operating costs <!-- .element: class="fragment" -->

Popular multiplayer games will be targeted by hackers and cheaters <!-- .element: class="fragment" -->

Online games often require “extra” features <!-- .element: class="fragment" -->

Note:

- Maintenance: getting easier with SaaS and PaaS
- Complexity: There is always latency (and the amount varies)
- Complexity: Network debugging is a lot more difficult than debugging a local game
- Complexity: Bandwidth is often more limited than the amount of information that we would like to send (in real-time games)
- Complexity: Why debugging is hard: a lot of edge cases, latency and jitter can vary considerably, needs more than one client, usually needs special tools
- Complexity: Some estimate that it can take 3-4 times as much effort to build online vs local game
- Real time games need to have a lot of users online at all times since they are the content of the game

---

## Alright...

---

## So, how does a server solution look like?

---

### Example: a simple game server architecture

![Simple game server architecture](/resources/simple-server-architecture.png)

---

### Example: FPS game server architecture

![FPS game server architecture](/resources/fps-server-architecture.png)

---

## Few important concepts

---

## Network topologies

---

### Peer-to-Peer (client-to-client)

All clients participating in the same game instance are interconnected with each other  <!-- .element: class="fragment" -->

All clients are equal (no hierarchies)  <!-- .element: class="fragment" -->

Limited scaling potential <!-- .element: class="fragment" -->

- Amount of connections grows exponentially with each client <!-- .element: class="fragment" -->

Has been popular choice in the early days of multiplayer gaming  <!-- .element: class="fragment" -->

There is no persistence or data collection, cheat prevention is hard, and all simulation is on client side <!-- .element: class="fragment" -->

Note:

- Still viable in games like fighting games where there are limited amount of players in a single match

---

### Client-Server

There is a separate server application <!-- .element: class="fragment" -->

All communications go through the server <!-- .element: class="fragment" -->

Server is an extra step on the communication <!-- .element: class="fragment" -->

- But there are various ways to optimize the traffic <!-- .element: class="fragment" -->

Server enables all the good things we have discussed before... <!-- .element: class="fragment" -->

Note:

- In some cases we might have one client promoted to a server
- Server can filter the data
- Server can aggregate the packets and smooth out the packet flow

---

## Asynchronous vs synchronous multiplayer games

---

### Asynchronous

Messaging is not time critical <!-- .element: class="fragment" -->

Players can even play with each other without playing it at the same time <!-- .element: class="fragment" -->

Actions are usually validated against the game state in database <!-- .element: class="fragment" -->

No need for latency compensation on the client side <!-- .element: class="fragment" -->

Cheaper to run than... <!-- .element: class="fragment" -->

Note:

- Asynchronous games are usually implemented with TCP protocol
- Usually player actions can be authorized by validating the game state of the player in the database/server

---

### Synchronous

Messaging is time critical <!-- .element: class="fragment" -->

Players are playing the game at the same time <!-- .element: class="fragment" -->

Actions are usually validated against real-time simulation on server-side <!-- .element: class="fragment" -->

Often needs latency compensation techniques on the client side <!-- .element: class="fragment" -->

More costly to run <!-- .element: class="fragment" -->

Note:

- Most of the game types can be implemented only as synchronous or only as asynchronous
- Synchronous games might need as many as 10 updates per second
- Synchronous games are often implemented with UDP
- Implementing a game with UDP is more work intensive: handling dropped packets, handling the connection, and flow control

---

## Cloud Services

These would require a course of their own...

---

### Cloud services

Nowadays most games are deployed to cloud

Viable even for smaller game studios

Out the box solutions

Cloud can scale up and down very quickly

Ease of deploying to datacenters around the world

Most commonly used public clouds are Amazon AWS and Microsoft Azure

There are four different cloud computing service models: IaaS, PaaS, SaaS and FaaS

Note:

- Cloud can answer to most scaling needs if the application is done properly
- Size of an operations team is a lot smaller than with On Premise solutions
- Cloud services are paid by the usage

---

### Commonly used cloud services

PaaS solutions for the code run-time environment <!-- .element: class="fragment" -->

SaaS databases <!-- .element: class="fragment" -->

Load balancers & Automatic scaling <!-- .element: class="fragment" -->

Various security features <!-- .element: class="fragment" -->

And many more… <!-- .element: class="fragment" -->

Note:

- Also things like: Message queues, Monitoring and alerts, Logs, Analytics and big data services, Push notifications, Content delivery network, Account management

---

## So you decide to build an online game

---

### Things you need to know

How to create good Web APIs <!-- .element: class="fragment" -->

Databases – both traditional SQL and NoSQL databases <!-- .element: class="fragment" -->

How to build scalable, stable, and fault tolerant systems <!-- .element: class="fragment" -->

How to build security and cheat resistancy <!-- .element: class="fragment" -->

How to use cloud services <!-- .element: class="fragment" -->

Creating robust realtime experiences <!-- .element: class="fragment" -->

Note:

In this course we'll focus mostly on the first 2 and bit on 3 and 4.

---

## But wait, why to make your own solution when there are companies that provide backend as a service?

---

### 3rd party services

Provide readymade solutions for the common problems:

- Account management <!-- .element: class="fragment" -->
- Realtime multiplayer <!-- .element: class="fragment" -->
- Commerce <!-- .element: class="fragment" -->
- Analytics <!-- .element: class="fragment" -->
- Social features (chat, leaderboard, friends etc.) <!-- .element: class="fragment" -->
- And many other things... <!-- .element: class="fragment" -->
  
These can reduce the game server development time significantly <!-- .element: class="fragment" -->

There are many viable providers out there such as **GameSparks** and **PlayFab** <!-- .element: class="fragment" -->

---

### Considerations for 3rd party services

Limits to your designs <!-- .element: class="fragment" -->

Dependecy to the service provider <!-- .element: class="fragment" -->

Trustworthiness of the service provider <!-- .element: class="fragment" -->

---

### More considerations for 3rd party services

Giving access to your valuable data <!-- .element: class="fragment" -->

Black box implementations: you might not be able see or touch the code <!-- .element: class="fragment" -->

Surpises: updates with breaking changes, company going bankcrupt, downtimes, pricing model changes... <!-- .element: class="fragment" -->

Note:

- Data can be one of the more valuable assets of a game company
- Maybe a story about the analytics firm X
