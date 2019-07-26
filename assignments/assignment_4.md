# Assignment 4

_This assignment builds on top of the ``GameWebApi`` project created in assignment 3._

Following exercises will make you gain more experience in implementing RESTful APIs with the addition of doing server-side validation for the data that the client is sending.

It is immensily important to get the validation on server-side right (especially) for competetive multiplayer games to make sure that players can't cheat in the game by sending illegal data.
  
---

## 1. CRUD-operations for ``Item``

Implement CRUD-operations and data classes for ``Item`` (see the previous assignment for help if needed).

You need at least the following new classes:

``Item``, ``NewItem``, ``ModifiedItem``, ``ItemsController``

Items should be owned by the players which means that we want to add a list of items (List<Item>) to the player model.

The RESTful routes for the items resource should start with ``.../api/players/{playerId}/items``.

---

## 2. Model validation using attributes

``NewItem`` and ``Item`` models should have the following properties:

- int Level
- ItemType Type (define the ``ItemType`` enum yourself with values SWORD, POTION and SHIELD)
- DateTime CreationDate

Define the following validations for the model using attributes:

- "Level can be only within the range from 1 to 99
- "Type" is one of the types defined in the ``ItemType`` enum
- "CreationDate" is a date from the past (Create custom validation attribute)

---

## 3. Implement a game rule validation in Controller

Implement a game rule validation for the ``[POST]`` (the one that creates a new item) endpoint in the ``ItemsContoller``:

The rule should be: an item of type of ``Sword`` should not be allowed for a ``Player`` below level 3.

If the rule is not followed, throw your own custom exception (create the exception class) and catch the exception in an ``exception filter``. The ``exception filter`` should write a response to the client with a _suitable error code_ and a _descriptive error message_. The ``exception filter`` should be only applied to that specific endpoint.
